using System;
using System.Collections;
using System.Diagnostics;

namespace Timok.NetworkLib.Udp {
	public class CommandFactoryRepository {
		readonly SortedList factoryRepository;

		public CommandFactoryRepository() {
			try { // Creates a non-synchronized SortedList and then a synchronized wrapper around it:
				factoryRepository = SortedList.Synchronized(new SortedList());
			}
			catch (Exception _ex) {
				throw(new ApplicationException(string.Format("CommandFactoryRepository.Ctor, Exception:\r\n{0}", _ex)));
			}
		}

		//------------------------------------- Static Methods ----------------------------------------
		public void Add(ICommandFactory pCommandFactory) {
			string _api = string.Empty;

			try {
				_api = pCommandFactory.APIName;
				if (factoryRepository.ContainsKey(_api.ToLower())) {
					throw(new ApplicationException(string.Format("CommandFactoryRepository.Save, API: {0} Exception: already in the list", _api)));
				}
				else {
					factoryRepository.Add(_api.ToLower(), pCommandFactory);
				}
			}
			catch(Exception _ex) {
				throw(new ApplicationException(string.Format("CommandFactoryRepository.Save, API: {0}, Exception:\r\n{1}", _api, _ex)));
			}
		}

		public ICommandFactory Get(string pAPI) {
			ICommandFactory _commandFactory = null;
			try {
				if (factoryRepository.ContainsKey(pAPI.ToLower())) {
					_commandFactory = (ICommandFactory) factoryRepository[pAPI.ToLower()];
				}
			}
			catch(Exception _ex) {
				throw(new ApplicationException(string.Format("CommandFactoryRepository.Save, API: {0}, Exception:\r\n{1}", pAPI, _ex)));
			}

			Debug.Assert(_commandFactory != null);
			return _commandFactory;
		}

		public void Delete(string pAPI) {
			try {
				if (factoryRepository.ContainsKey(pAPI.ToLower())) {
					factoryRepository.Remove(pAPI.ToLower());
				}
				else {
					throw(new ApplicationException(string.Format("CommandFactoryRepository.Delete, Doesn't contain API: {0}", pAPI)));
				}
			}
			catch(Exception _ex) {
				throw(new ApplicationException(string.Format("CommandFactoryRepository.Delete, Exception:\r\n{0}", _ex)));
			}
		}
	}
}