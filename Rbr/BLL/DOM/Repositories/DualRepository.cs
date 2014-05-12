using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using Timok.Logger;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.DOM.Repositories {
	public class DualRepository {
		readonly IConfiguration configuration;
		readonly ILogger T;
		readonly CdrAggregateRepository[] repositoryArray;
		Timer recycleTimer { get; set; }
		readonly int frequency; // in minutes

		int recycleTimerInterval {
			get {
				var _now = DateTime.Now;
				var _minutes = _now.Minute;

				//-- calculate timeout in minutes
				var _timeout = frequency - _minutes % frequency;
				if (_timeout == 0) {
					_timeout = frequency;
				}

				var _seconds = _now.Second;
				if (_seconds > 0) {
					_seconds = 60 - _seconds;
					if (_timeout >= 1) {
						_timeout -= 1;
					}
				}

				//-- convert to seconds and then to miliseconds
				_timeout = _timeout * 60 + _seconds;
				T.LogRbr(LogSeverity.Debug, "DualRepository.recycleTimerInterval", string.Format("Timout: [{0:D2}:{1:D2}]", _timeout / 60, _timeout % 60));
				return _timeout * 1000;
			}
		}

		int index;
		int nextIndex { get { return index == 1 ? 0 : 1; } }
		int previousIndex { get { return index == 1 ? 0 : 1; } }

		public IRepository Current {
			get {
				lock (CdrAggrExporter.Padlock) {
					return repositoryArray[index];
				}
			}
		}

		public IRepository Previous { get { return repositoryArray[previousIndex]; } }

		public DualRepository(IConfiguration pConfiguration, ILogger pLogger) {
			configuration = pConfiguration;
			T = pLogger;
			frequency = pConfiguration.Main.CdrAggrExportFrequency;
			index = 0;

#if DEBUG
			frequency = 1;
#endif

			//-- initialize repository array:
			repositoryArray = new CdrAggregateRepository[2];
			try {
				repositoryArray[0] = new CdrAggregateRepository();
				repositoryArray[1] = new CdrAggregateRepository();
			}
			catch (Exception _ex) {
				T.LogRbr(LogSeverity.Debug, "DualRepository.Ctor", "Exception creating repository Array: " + _ex);
			}

			//-- initialize timer
			recycleTimer = new Timer(swap, null, recycleTimerInterval, Timeout.Infinite);
		}

		void swap(object pState) {
			Thread.Sleep(0); //Fix for SP1 bug, when timer stops fiaring after a while

			try {
				T.LogRbr(LogSeverity.Debug, "DualRepository.swap", string.Format("PrevIndex: {0} time: {1}", index, DateTime.Now));

				//TODO: this lock should be the same as in CdrAggreqagteExporter otherwise we have potential problem
				lock (CdrAggrExporter.Padlock) {
					index = nextIndex;
					repositoryArray[index] = new CdrAggregateRepository();
					T.LogRbr(LogSeverity.Debug, "DualRepository.swap", string.Format("index: {0} count: {1}", index, repositoryArray[index].Count));
				}

				//--SavePrevious
				savePrevious();
				T.LogRbr(LogSeverity.Debug, "DualRepository.swap", string.Format("index: {0} Seriliazed", index));
			}
			catch (Exception _ex) {
				T.LogRbr(LogSeverity.Critical, "DualRepository.swap", string.Format("Exception:\r\n{0}", _ex));
			}
			finally {
				recycleTimer = new Timer(swap, null, recycleTimerInterval, Timeout.Infinite);
			}
		}

		public void Dispose() {
			T.LogRbr(LogSeverity.Debug, "DualRepository.Dispose", "DualRepository, dispose Called!");
			repositoryArray[0].Dispose();
			repositoryArray[1].Dispose();
		}

		//-------------------- Private methods --------------------------------
		void savePrevious() {
			T.LogRbr(LogSeverity.Debug, "DualRepository.savePrevious", "Started");

			var _cdrAggregates = repositoryArray[previousIndex].ToArray();
			if (_cdrAggregates == null) {
				throw new Exception("CdrAggrExporter.export | CdrAggregates == null ? ");
			}
			T.LogRbr(LogSeverity.Debug, "DualRepository.savePrevious", string.Format("CdrAggregates Length: {0}", _cdrAggregates.Length));
			if (_cdrAggregates.Length <= 0) {
				return;
			}

			var _currentNode = new CurrentNode();
			if (_currentNode.BelongsToStandalonePlatform) {
				(new CdrAggregate()).ImportToDb(_cdrAggregates);
				return;
			}

			var _targetNodes = Node.GetNodes(NodeRole.Admin);
			if (_targetNodes == null || _targetNodes.Length != 1) {
				T.LogRbr(LogSeverity.Critical, "DualRepository.veprevious", "CdrAggrExport: Admin server doesn't exist");
				return;
			}

			var _folder = Path.Combine(configuration.Folders.CdrAggrPublishingFolder, _targetNodes[0].UserName);
			if (!Directory.Exists(_folder)) {
				Directory.CreateDirectory(_folder);
			}

			//-- (1) Create a FULL AND 'temp' file name:
			var _fileName = _currentNode.Id.ToString("D2") + "-" + DateTime.Now.ToString(AppConstants.CdrAggrFileNameFormat);
			var _filePath = Path.Combine(_folder, _fileName + AppConstants.CdrAggrFileExtension);
			if (File.Exists(_filePath)) {
				T.LogRbr(LogSeverity.Critical, "DualRepository.savePrevious", string.Format("File already exist: {0}", _filePath));
				return;
			}

			//-- (2) Serialize and save:
			using (var _fs = new FileStream(_filePath, FileMode.Create)) {
				var _serializer = new XmlSerializer(typeof (CdrAggregate[]));
				_serializer.Serialize(_fs, _cdrAggregates);
				_fs.Flush();
				_fs.Close();
			}

			//-- (3) rename (by appending ".pending")
			FileHelper.AddExtension(_filePath, AppConstants.PendingExtension);
		}
	}
}