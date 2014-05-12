// <fileinfo name="Base\Rbr_Db_Base.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			Do not change this source code manually. Changes to this file may 
//			cause incorrect behavior and will be lost if the code is regenerated.
//		</remarks>
//		<generator rewritefile="True" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using System.Data;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase.Base
{
	/// <summary>
	/// The base class for the <see cref="Rbr_Db"/> class that 
	/// represents a connection to the <c>Rbr_Db</c> database. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Modify the Rbr_Db class
	/// if you need to add or change some functionality.
	/// </remarks>
	public abstract class Rbr_Db_Base : IDisposable
	{
		private IDbConnection _connection;
		private IDbTransaction _transaction;

		// Table and view fields
		private AccessListViewCollection _accessListView;
		private AccessNumberListCollection _accessNumberList;
		private BalanceAdjustmentReasonCollection _balanceAdjustmentReason;
		private BatchCollection _batch;
		private BoxCollection _box;
		private CallCenterCabinaCollection _callCenterCabina;
		private CallingPlanCollection _callingPlan;
		private CarrierAcctCollection _carrierAcct;
		private CarrierAcctEPMapCollection _carrierAcctEPMap;
		private CarrierRateHistoryCollection _carrierRateHistory;
		private CarrierRouteCollection _carrierRoute;
		private CdrAggregateCollection _cdrAggregate;
		private CdrExportMapCollection _cdrExportMap;
		private CdrExportMapDetailCollection _cdrExportMapDetail;
		private ContactInfoCollection _contactInfo;
		private CountryCollection _country;
		private CustomerAcctCollection _customerAcct;
		private CustomerAcctPaymentCollection _customerAcctPayment;
		private CustomerAcctSupportMapCollection _customerAcctSupportMap;
		private CustomerSupportGroupCollection _customerSupportGroup;
		private CustomerSupportVendorCollection _customerSupportVendor;
		private DialCodeCollection _dialCode;
		private DialPeerCollection _dialPeer;
		private DialPeerViewCollection _dialPeerView;
		private EndPointCollection _endPoint;
		private GenerationRequestCollection _generationRequest;
		private HolidayCalendarCollection _holidayCalendar;
		private InventoryHistoryCollection _inventoryHistory;
		private InventoryLotCollection _inventoryLot;
		private InventoryUsageCollection _inventoryUsage;
		private IPAddressCollection _iPAddress;
		private IPAddressViewCollection _iPAddressView;
		private LCRBlackListCollection _lCRBlackList;
		private LoadBalancingMapCollection _loadBalancingMap;
		private LoadBalancingMapViewCollection _loadBalancingMapView;
		private NodeCollection _node;
		private NodeViewCollection _nodeView;
		private OutboundANICollection _outboundANI;
		private OutDialPeerViewCollection _outDialPeerView;
		private PartnerCollection _partner;
		private PayphoneSurchargeCollection _payphoneSurcharge;
		private PersonCollection _person;
		private PhoneCardCollection _phoneCard;
		private PlatformCollection _platform;
		private PrefixInTypeCollection _prefixInType;
		private RateCollection _rate;
		private RateInfoCollection _rateInfo;
		private ResellAcctCollection _resellAcct;
		private ResellRateHistoryCollection _resellRateHistory;
		private ResidentialPSTNCollection _residentialPSTN;
		private ResidentialVoIPCollection _residentialVoIP;
		private RetailAccountCollection _retailAccount;
		private RetailAccountPaymentCollection _retailAccountPayment;
		private RetailRateHistoryCollection _retailRateHistory;
		private RetailRouteCollection _retailRoute;
		private RetailRouteBonusMinutesCollection _retailRouteBonusMinutes;
		private RouteCollection _route;
		private RoutingPlanCollection _routingPlan;
		private RoutingPlanDetailCollection _routingPlanDetail;
		private ScheduleCollection _schedule;
		private ServiceCollection _service;
		private TableSequenceCollection _tableSequence;
		private TerminationChoiceCollection _terminationChoice;
		private TerminationRouteViewCollection _terminationRouteView;
		private TimeOfDayCollection _timeOfDay;
		private TimeOfDayPeriodCollection _timeOfDayPeriod;
		private TimeOfDayPolicyCollection _timeOfDayPolicy;
		private TypeOfDayCollection _typeOfDay;
		private TypeOfDayChoiceCollection _typeOfDayChoice;
		private VirtualSwitchCollection _virtualSwitch;
		private WholesaleRateHistoryCollection _wholesaleRateHistory;
		private WholesaleRouteCollection _wholesaleRoute;

		/// <summary>
		/// Initializes a new instance of the <see cref="Rbr_Db_Base"/> 
		/// class and opens the database connection.
		/// </summary>
		protected Rbr_Db_Base() {
      InitConnection();
		}

		///// <summary>
		///// Initializes a new instance of the <see cref="Rbr_Db_Base"/> class.
		///// </summary>
		///// <param name="init">Specifies whether the constructor calls the
		///// <see cref="InitConnection"/> method to initialize the database connection.</param>
		//protected Rbr_Db_Base(bool init)
		//{
		//  if(init)
		//    InitConnection();
		//}

		/// <summary>
		/// Initializes the database connection.
		/// </summary>
		protected void InitConnection()
		{
			_connection = CreateConnection();
			_connection.Open();
		}

		/// <summary>
		/// Creates a new connection to the database.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbConnection"/> object.</returns>
		protected abstract IDbConnection CreateConnection();

		/// <summary>
		/// Returns a SQL statement parameter name that is specific for the data provider.
		/// For example it returns ? for OleDb provider, or @paramName for MS SQL provider.
		/// </summary>
		/// <param name="paramName">The data provider neutral SQL parameter name.</param>
		/// <returns>The SQL statement parameter name.</returns>
		protected internal abstract string CreateSqlParameterName(string paramName);

		/// <summary>
		/// Creates <see cref="System.Data.IDataReader"/> for the specified DB command.
		/// </summary>
		/// <param name="command">The <see cref="System.Data.IDbCommand"/> object.</param>
		/// <returns>A reference to the <see cref="System.Data.IDataReader"/> object.</returns>
		protected internal virtual IDataReader ExecuteReader(IDbCommand command)
		{
			return command.ExecuteReader();
		}

		/// <summary>
		/// Adds a new parameter to the specified command. It is not recommended that 
		/// you use this method directly from your custom code. Instead use the 
		/// <c>AddParameter</c> method of the &lt;TableCodeName&gt;Collection_Base classes.
		/// </summary>
		/// <param name="cmd">The <see cref="System.Data.IDbCommand"/> object to add the parameter to.</param>
		/// <param name="paramName">The name of the parameter.</param>
		/// <param name="dbType">One of the <see cref="System.Data.DbType"/> values. </param>
		/// <param name="value">The value of the parameter.</param>
		/// <returns>A reference to the added parameter.</returns>
		internal IDbDataParameter AddParameter(IDbCommand cmd, string paramName,
												DbType dbType, object value)
		{
			IDbDataParameter parameter = cmd.CreateParameter();
			parameter.ParameterName = CreateCollectionParameterName(paramName);
			parameter.DbType = dbType;
			parameter.Value = null == value ? DBNull.Value : value;
			cmd.Parameters.Add(parameter);
			return parameter;
		}
		
		/// <summary>
		/// Creates a .Net data provider specific name that is used by the 
		/// <see cref="AddParameter"/> method.
		/// </summary>
		/// <param name="baseParamName">The base name of the parameter.</param>
		/// <returns>The full data provider specific parameter name.</returns>
		protected abstract string CreateCollectionParameterName(string baseParamName);

		/// <summary>
		/// Gets <see cref="System.Data.IDbConnection"/> associated with this object.
		/// </summary>
		/// <value>A reference to the <see cref="System.Data.IDbConnection"/> object.</value>
		public IDbConnection Connection
		{
			get { return _connection; }
		}

		/// <summary>
		/// Gets an object that represents the <c>AccessListView</c> view.
		/// </summary>
		/// <value>A reference to the <see cref="AccessListViewCollection"/> object.</value>
		public AccessListViewCollection AccessListViewCollection
		{
			get
			{
				if(null == _accessListView)
					_accessListView = new AccessListViewCollection((Rbr_Db)this);
				return _accessListView;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>AccessNumberList</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="AccessNumberListCollection"/> object.</value>
		public AccessNumberListCollection AccessNumberListCollection
		{
			get
			{
				if(null == _accessNumberList)
					_accessNumberList = new AccessNumberListCollection((Rbr_Db)this);
				return _accessNumberList;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>BalanceAdjustmentReason</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="BalanceAdjustmentReasonCollection"/> object.</value>
		public BalanceAdjustmentReasonCollection BalanceAdjustmentReasonCollection
		{
			get
			{
				if(null == _balanceAdjustmentReason)
					_balanceAdjustmentReason = new BalanceAdjustmentReasonCollection((Rbr_Db)this);
				return _balanceAdjustmentReason;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>Batch</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="BatchCollection"/> object.</value>
		public BatchCollection BatchCollection
		{
			get
			{
				if(null == _batch)
					_batch = new BatchCollection((Rbr_Db)this);
				return _batch;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>Box</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="BoxCollection"/> object.</value>
		public BoxCollection BoxCollection
		{
			get
			{
				if(null == _box)
					_box = new BoxCollection((Rbr_Db)this);
				return _box;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CallCenterCabina</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CallCenterCabinaCollection"/> object.</value>
		public CallCenterCabinaCollection CallCenterCabinaCollection
		{
			get
			{
				if(null == _callCenterCabina)
					_callCenterCabina = new CallCenterCabinaCollection((Rbr_Db)this);
				return _callCenterCabina;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CallingPlan</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CallingPlanCollection"/> object.</value>
		public CallingPlanCollection CallingPlanCollection
		{
			get
			{
				if(null == _callingPlan)
					_callingPlan = new CallingPlanCollection((Rbr_Db)this);
				return _callingPlan;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CarrierAcct</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CarrierAcctCollection"/> object.</value>
		public CarrierAcctCollection CarrierAcctCollection
		{
			get
			{
				if(null == _carrierAcct)
					_carrierAcct = new CarrierAcctCollection((Rbr_Db)this);
				return _carrierAcct;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CarrierAcctEPMap</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CarrierAcctEPMapCollection"/> object.</value>
		public CarrierAcctEPMapCollection CarrierAcctEPMapCollection
		{
			get
			{
				if(null == _carrierAcctEPMap)
					_carrierAcctEPMap = new CarrierAcctEPMapCollection((Rbr_Db)this);
				return _carrierAcctEPMap;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CarrierRateHistory</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CarrierRateHistoryCollection"/> object.</value>
		public CarrierRateHistoryCollection CarrierRateHistoryCollection
		{
			get
			{
				if(null == _carrierRateHistory)
					_carrierRateHistory = new CarrierRateHistoryCollection((Rbr_Db)this);
				return _carrierRateHistory;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CarrierRoute</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CarrierRouteCollection"/> object.</value>
		public CarrierRouteCollection CarrierRouteCollection
		{
			get
			{
				if(null == _carrierRoute)
					_carrierRoute = new CarrierRouteCollection((Rbr_Db)this);
				return _carrierRoute;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CdrAggregate</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CdrAggregateCollection"/> object.</value>
		public CdrAggregateCollection CdrAggregateCollection
		{
			get
			{
				if(null == _cdrAggregate)
					_cdrAggregate = new CdrAggregateCollection((Rbr_Db)this);
				return _cdrAggregate;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CdrExportMap</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CdrExportMapCollection"/> object.</value>
		public CdrExportMapCollection CdrExportMapCollection
		{
			get
			{
				if(null == _cdrExportMap)
					_cdrExportMap = new CdrExportMapCollection((Rbr_Db)this);
				return _cdrExportMap;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CdrExportMapDetail</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CdrExportMapDetailCollection"/> object.</value>
		public CdrExportMapDetailCollection CdrExportMapDetailCollection
		{
			get
			{
				if(null == _cdrExportMapDetail)
					_cdrExportMapDetail = new CdrExportMapDetailCollection((Rbr_Db)this);
				return _cdrExportMapDetail;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>ContactInfo</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="ContactInfoCollection"/> object.</value>
		public ContactInfoCollection ContactInfoCollection
		{
			get
			{
				if(null == _contactInfo)
					_contactInfo = new ContactInfoCollection((Rbr_Db)this);
				return _contactInfo;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>Country</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CountryCollection"/> object.</value>
		public CountryCollection CountryCollection
		{
			get
			{
				if(null == _country)
					_country = new CountryCollection((Rbr_Db)this);
				return _country;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CustomerAcct</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CustomerAcctCollection"/> object.</value>
		public CustomerAcctCollection CustomerAcctCollection
		{
			get
			{
				if(null == _customerAcct)
					_customerAcct = new CustomerAcctCollection((Rbr_Db)this);
				return _customerAcct;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CustomerAcctPayment</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CustomerAcctPaymentCollection"/> object.</value>
		public CustomerAcctPaymentCollection CustomerAcctPaymentCollection
		{
			get
			{
				if(null == _customerAcctPayment)
					_customerAcctPayment = new CustomerAcctPaymentCollection((Rbr_Db)this);
				return _customerAcctPayment;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CustomerAcctSupportMap</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CustomerAcctSupportMapCollection"/> object.</value>
		public CustomerAcctSupportMapCollection CustomerAcctSupportMapCollection
		{
			get
			{
				if(null == _customerAcctSupportMap)
					_customerAcctSupportMap = new CustomerAcctSupportMapCollection((Rbr_Db)this);
				return _customerAcctSupportMap;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CustomerSupportGroup</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CustomerSupportGroupCollection"/> object.</value>
		public CustomerSupportGroupCollection CustomerSupportGroupCollection
		{
			get
			{
				if(null == _customerSupportGroup)
					_customerSupportGroup = new CustomerSupportGroupCollection((Rbr_Db)this);
				return _customerSupportGroup;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>CustomerSupportVendor</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="CustomerSupportVendorCollection"/> object.</value>
		public CustomerSupportVendorCollection CustomerSupportVendorCollection
		{
			get
			{
				if(null == _customerSupportVendor)
					_customerSupportVendor = new CustomerSupportVendorCollection((Rbr_Db)this);
				return _customerSupportVendor;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>DialCode</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="DialCodeCollection"/> object.</value>
		public DialCodeCollection DialCodeCollection
		{
			get
			{
				if(null == _dialCode)
					_dialCode = new DialCodeCollection((Rbr_Db)this);
				return _dialCode;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>DialPeer</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="DialPeerCollection"/> object.</value>
		public DialPeerCollection DialPeerCollection
		{
			get
			{
				if(null == _dialPeer)
					_dialPeer = new DialPeerCollection((Rbr_Db)this);
				return _dialPeer;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>DialPeerView</c> view.
		/// </summary>
		/// <value>A reference to the <see cref="DialPeerViewCollection"/> object.</value>
		public DialPeerViewCollection DialPeerViewCollection
		{
			get
			{
				if(null == _dialPeerView)
					_dialPeerView = new DialPeerViewCollection((Rbr_Db)this);
				return _dialPeerView;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>EndPoint</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="EndPointCollection"/> object.</value>
		public EndPointCollection EndPointCollection
		{
			get
			{
				if(null == _endPoint)
					_endPoint = new EndPointCollection((Rbr_Db)this);
				return _endPoint;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>GenerationRequest</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="GenerationRequestCollection"/> object.</value>
		public GenerationRequestCollection GenerationRequestCollection
		{
			get
			{
				if(null == _generationRequest)
					_generationRequest = new GenerationRequestCollection((Rbr_Db)this);
				return _generationRequest;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>HolidayCalendar</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="HolidayCalendarCollection"/> object.</value>
		public HolidayCalendarCollection HolidayCalendarCollection
		{
			get
			{
				if(null == _holidayCalendar)
					_holidayCalendar = new HolidayCalendarCollection((Rbr_Db)this);
				return _holidayCalendar;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>InventoryHistory</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="InventoryHistoryCollection"/> object.</value>
		public InventoryHistoryCollection InventoryHistoryCollection
		{
			get
			{
				if(null == _inventoryHistory)
					_inventoryHistory = new InventoryHistoryCollection((Rbr_Db)this);
				return _inventoryHistory;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>InventoryLot</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="InventoryLotCollection"/> object.</value>
		public InventoryLotCollection InventoryLotCollection
		{
			get
			{
				if(null == _inventoryLot)
					_inventoryLot = new InventoryLotCollection((Rbr_Db)this);
				return _inventoryLot;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>InventoryUsage</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="InventoryUsageCollection"/> object.</value>
		public InventoryUsageCollection InventoryUsageCollection
		{
			get
			{
				if(null == _inventoryUsage)
					_inventoryUsage = new InventoryUsageCollection((Rbr_Db)this);
				return _inventoryUsage;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>IPAddress</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="IPAddressCollection"/> object.</value>
		public IPAddressCollection IPAddressCollection
		{
			get
			{
				if(null == _iPAddress)
					_iPAddress = new IPAddressCollection((Rbr_Db)this);
				return _iPAddress;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>IPAddressView</c> view.
		/// </summary>
		/// <value>A reference to the <see cref="IPAddressViewCollection"/> object.</value>
		public IPAddressViewCollection IPAddressViewCollection
		{
			get
			{
				if(null == _iPAddressView)
					_iPAddressView = new IPAddressViewCollection((Rbr_Db)this);
				return _iPAddressView;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>LCRBlackList</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="LCRBlackListCollection"/> object.</value>
		public LCRBlackListCollection LCRBlackListCollection
		{
			get
			{
				if(null == _lCRBlackList)
					_lCRBlackList = new LCRBlackListCollection((Rbr_Db)this);
				return _lCRBlackList;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>LoadBalancingMap</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="LoadBalancingMapCollection"/> object.</value>
		public LoadBalancingMapCollection LoadBalancingMapCollection
		{
			get
			{
				if(null == _loadBalancingMap)
					_loadBalancingMap = new LoadBalancingMapCollection((Rbr_Db)this);
				return _loadBalancingMap;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>LoadBalancingMapView</c> view.
		/// </summary>
		/// <value>A reference to the <see cref="LoadBalancingMapViewCollection"/> object.</value>
		public LoadBalancingMapViewCollection LoadBalancingMapViewCollection
		{
			get
			{
				if(null == _loadBalancingMapView)
					_loadBalancingMapView = new LoadBalancingMapViewCollection((Rbr_Db)this);
				return _loadBalancingMapView;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>Node</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="NodeCollection"/> object.</value>
		public NodeCollection NodeCollection
		{
			get
			{
				if(null == _node)
					_node = new NodeCollection((Rbr_Db)this);
				return _node;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>NodeView</c> view.
		/// </summary>
		/// <value>A reference to the <see cref="NodeViewCollection"/> object.</value>
		public NodeViewCollection NodeViewCollection
		{
			get
			{
				if(null == _nodeView)
					_nodeView = new NodeViewCollection((Rbr_Db)this);
				return _nodeView;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>OutboundANI</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="OutboundANICollection"/> object.</value>
		public OutboundANICollection OutboundANICollection
		{
			get
			{
				if(null == _outboundANI)
					_outboundANI = new OutboundANICollection((Rbr_Db)this);
				return _outboundANI;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>OutDialPeerView</c> view.
		/// </summary>
		/// <value>A reference to the <see cref="OutDialPeerViewCollection"/> object.</value>
		public OutDialPeerViewCollection OutDialPeerViewCollection
		{
			get
			{
				if(null == _outDialPeerView)
					_outDialPeerView = new OutDialPeerViewCollection((Rbr_Db)this);
				return _outDialPeerView;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>Partner</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="PartnerCollection"/> object.</value>
		public PartnerCollection PartnerCollection
		{
			get
			{
				if(null == _partner)
					_partner = new PartnerCollection((Rbr_Db)this);
				return _partner;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>PayphoneSurcharge</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="PayphoneSurchargeCollection"/> object.</value>
		public PayphoneSurchargeCollection PayphoneSurchargeCollection
		{
			get
			{
				if(null == _payphoneSurcharge)
					_payphoneSurcharge = new PayphoneSurchargeCollection((Rbr_Db)this);
				return _payphoneSurcharge;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>Person</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="PersonCollection"/> object.</value>
		public PersonCollection PersonCollection
		{
			get
			{
				if(null == _person)
					_person = new PersonCollection((Rbr_Db)this);
				return _person;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>PhoneCard</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="PhoneCardCollection"/> object.</value>
		public PhoneCardCollection PhoneCardCollection
		{
			get
			{
				if(null == _phoneCard)
					_phoneCard = new PhoneCardCollection((Rbr_Db)this);
				return _phoneCard;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>Platform</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="PlatformCollection"/> object.</value>
		public PlatformCollection PlatformCollection
		{
			get
			{
				if(null == _platform)
					_platform = new PlatformCollection((Rbr_Db)this);
				return _platform;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>PrefixInType</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="PrefixInTypeCollection"/> object.</value>
		public PrefixInTypeCollection PrefixInTypeCollection
		{
			get
			{
				if(null == _prefixInType)
					_prefixInType = new PrefixInTypeCollection((Rbr_Db)this);
				return _prefixInType;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>Rate</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="RateCollection"/> object.</value>
		public RateCollection RateCollection
		{
			get
			{
				if(null == _rate)
					_rate = new RateCollection((Rbr_Db)this);
				return _rate;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>RateInfo</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="RateInfoCollection"/> object.</value>
		public RateInfoCollection RateInfoCollection
		{
			get
			{
				if(null == _rateInfo)
					_rateInfo = new RateInfoCollection((Rbr_Db)this);
				return _rateInfo;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>ResellAcct</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="ResellAcctCollection"/> object.</value>
		public ResellAcctCollection ResellAcctCollection
		{
			get
			{
				if(null == _resellAcct)
					_resellAcct = new ResellAcctCollection((Rbr_Db)this);
				return _resellAcct;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>ResellRateHistory</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="ResellRateHistoryCollection"/> object.</value>
		public ResellRateHistoryCollection ResellRateHistoryCollection
		{
			get
			{
				if(null == _resellRateHistory)
					_resellRateHistory = new ResellRateHistoryCollection((Rbr_Db)this);
				return _resellRateHistory;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>ResidentialPSTN</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="ResidentialPSTNCollection"/> object.</value>
		public ResidentialPSTNCollection ResidentialPSTNCollection
		{
			get
			{
				if(null == _residentialPSTN)
					_residentialPSTN = new ResidentialPSTNCollection((Rbr_Db)this);
				return _residentialPSTN;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>ResidentialVoIP</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="ResidentialVoIPCollection"/> object.</value>
		public ResidentialVoIPCollection ResidentialVoIPCollection
		{
			get
			{
				if(null == _residentialVoIP)
					_residentialVoIP = new ResidentialVoIPCollection((Rbr_Db)this);
				return _residentialVoIP;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>RetailAccount</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="RetailAccountCollection"/> object.</value>
		public RetailAccountCollection RetailAccountCollection
		{
			get
			{
				if(null == _retailAccount)
					_retailAccount = new RetailAccountCollection((Rbr_Db)this);
				return _retailAccount;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>RetailAccountPayment</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="RetailAccountPaymentCollection"/> object.</value>
		public RetailAccountPaymentCollection RetailAccountPaymentCollection
		{
			get
			{
				if(null == _retailAccountPayment)
					_retailAccountPayment = new RetailAccountPaymentCollection((Rbr_Db)this);
				return _retailAccountPayment;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>RetailRateHistory</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="RetailRateHistoryCollection"/> object.</value>
		public RetailRateHistoryCollection RetailRateHistoryCollection
		{
			get
			{
				if(null == _retailRateHistory)
					_retailRateHistory = new RetailRateHistoryCollection((Rbr_Db)this);
				return _retailRateHistory;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>RetailRoute</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="RetailRouteCollection"/> object.</value>
		public RetailRouteCollection RetailRouteCollection
		{
			get
			{
				if(null == _retailRoute)
					_retailRoute = new RetailRouteCollection((Rbr_Db)this);
				return _retailRoute;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>RetailRouteBonusMinutes</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="RetailRouteBonusMinutesCollection"/> object.</value>
		public RetailRouteBonusMinutesCollection RetailRouteBonusMinutesCollection
		{
			get
			{
				if(null == _retailRouteBonusMinutes)
					_retailRouteBonusMinutes = new RetailRouteBonusMinutesCollection((Rbr_Db)this);
				return _retailRouteBonusMinutes;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>Route</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="RouteCollection"/> object.</value>
		public RouteCollection RouteCollection
		{
			get
			{
				if(null == _route)
					_route = new RouteCollection((Rbr_Db)this);
				return _route;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>RoutingPlan</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="RoutingPlanCollection"/> object.</value>
		public RoutingPlanCollection RoutingPlanCollection
		{
			get
			{
				if(null == _routingPlan)
					_routingPlan = new RoutingPlanCollection((Rbr_Db)this);
				return _routingPlan;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>RoutingPlanDetail</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="RoutingPlanDetailCollection"/> object.</value>
		public RoutingPlanDetailCollection RoutingPlanDetailCollection
		{
			get
			{
				if(null == _routingPlanDetail)
					_routingPlanDetail = new RoutingPlanDetailCollection((Rbr_Db)this);
				return _routingPlanDetail;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>Schedule</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="ScheduleCollection"/> object.</value>
		public ScheduleCollection ScheduleCollection
		{
			get
			{
				if(null == _schedule)
					_schedule = new ScheduleCollection((Rbr_Db)this);
				return _schedule;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>Service</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="ServiceCollection"/> object.</value>
		public ServiceCollection ServiceCollection
		{
			get
			{
				if(null == _service)
					_service = new ServiceCollection((Rbr_Db)this);
				return _service;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>TableSequence</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="TableSequenceCollection"/> object.</value>
		public TableSequenceCollection TableSequenceCollection
		{
			get
			{
				if(null == _tableSequence)
					_tableSequence = new TableSequenceCollection((Rbr_Db)this);
				return _tableSequence;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>TerminationChoice</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="TerminationChoiceCollection"/> object.</value>
		public TerminationChoiceCollection TerminationChoiceCollection
		{
			get
			{
				if(null == _terminationChoice)
					_terminationChoice = new TerminationChoiceCollection((Rbr_Db)this);
				return _terminationChoice;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>TerminationRouteView</c> view.
		/// </summary>
		/// <value>A reference to the <see cref="TerminationRouteViewCollection"/> object.</value>
		public TerminationRouteViewCollection TerminationRouteViewCollection
		{
			get
			{
				if(null == _terminationRouteView)
					_terminationRouteView = new TerminationRouteViewCollection((Rbr_Db)this);
				return _terminationRouteView;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>TimeOfDay</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="TimeOfDayCollection"/> object.</value>
		public TimeOfDayCollection TimeOfDayCollection
		{
			get
			{
				if(null == _timeOfDay)
					_timeOfDay = new TimeOfDayCollection((Rbr_Db)this);
				return _timeOfDay;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>TimeOfDayPeriod</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="TimeOfDayPeriodCollection"/> object.</value>
		public TimeOfDayPeriodCollection TimeOfDayPeriodCollection
		{
			get
			{
				if(null == _timeOfDayPeriod)
					_timeOfDayPeriod = new TimeOfDayPeriodCollection((Rbr_Db)this);
				return _timeOfDayPeriod;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>TimeOfDayPolicy</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="TimeOfDayPolicyCollection"/> object.</value>
		public TimeOfDayPolicyCollection TimeOfDayPolicyCollection
		{
			get
			{
				if(null == _timeOfDayPolicy)
					_timeOfDayPolicy = new TimeOfDayPolicyCollection((Rbr_Db)this);
				return _timeOfDayPolicy;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>TypeOfDay</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="TypeOfDayCollection"/> object.</value>
		public TypeOfDayCollection TypeOfDayCollection
		{
			get
			{
				if(null == _typeOfDay)
					_typeOfDay = new TypeOfDayCollection((Rbr_Db)this);
				return _typeOfDay;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>TypeOfDayChoice</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="TypeOfDayChoiceCollection"/> object.</value>
		public TypeOfDayChoiceCollection TypeOfDayChoiceCollection
		{
			get
			{
				if(null == _typeOfDayChoice)
					_typeOfDayChoice = new TypeOfDayChoiceCollection((Rbr_Db)this);
				return _typeOfDayChoice;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>VirtualSwitch</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="VirtualSwitchCollection"/> object.</value>
		public VirtualSwitchCollection VirtualSwitchCollection
		{
			get
			{
				if(null == _virtualSwitch)
					_virtualSwitch = new VirtualSwitchCollection((Rbr_Db)this);
				return _virtualSwitch;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>WholesaleRateHistory</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="WholesaleRateHistoryCollection"/> object.</value>
		public WholesaleRateHistoryCollection WholesaleRateHistoryCollection
		{
			get
			{
				if(null == _wholesaleRateHistory)
					_wholesaleRateHistory = new WholesaleRateHistoryCollection((Rbr_Db)this);
				return _wholesaleRateHistory;
			}
		}

		/// <summary>
		/// Gets an object that represents the <c>WholesaleRoute</c> table.
		/// </summary>
		/// <value>A reference to the <see cref="WholesaleRouteCollection"/> object.</value>
		public WholesaleRouteCollection WholesaleRouteCollection
		{
			get
			{
				if(null == _wholesaleRoute)
					_wholesaleRoute = new WholesaleRouteCollection((Rbr_Db)this);
				return _wholesaleRoute;
			}
		}

		/// <summary>
		/// Begins a new database transaction.
		/// </summary>
		/// <seealso cref="CommitTransaction"/>
		/// <seealso cref="RollbackTransaction"/>
		/// <returns>An object representing the new transaction.</returns>
		public IDbTransaction BeginTransaction()
		{
			CheckTransactionState(false);
			_transaction = _connection.BeginTransaction();
			return _transaction;
		}

		/// <summary>
		/// Begins a new database transaction with the specified 
		/// transaction isolation level.
		/// <seealso cref="CommitTransaction"/>
		/// <seealso cref="RollbackTransaction"/>
		/// </summary>
		/// <param name="isolationLevel">The transaction isolation level.</param>
		/// <returns>An object representing the new transaction.</returns>
		public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			CheckTransactionState(false);
			_transaction = _connection.BeginTransaction(isolationLevel);
			return _transaction;
		}

		/// <summary>
		/// Commits the current database transaction.
		/// <seealso cref="BeginTransaction"/>
		/// <seealso cref="RollbackTransaction"/>
		/// </summary>
		public virtual void CommitTransaction()
		{
			CheckTransactionState(true);
			_transaction.Commit();
			_transaction = null;
		}

		/// <summary>
		/// Rolls back the current transaction from a pending state.
		/// <seealso cref="BeginTransaction"/>
		/// <seealso cref="CommitTransaction"/>
		/// </summary>
		public virtual void RollbackTransaction()
		{
			CheckTransactionState(true);
			_transaction.Rollback();
			_transaction = null;
		}

		// Checks the state of the current transaction
		private void CheckTransactionState(bool mustBeOpen)
		{
			if(mustBeOpen)
			{
				if(null == _transaction)
					throw new InvalidOperationException("Transaction is not open.");
			}
			else
			{
				if(null != _transaction)
					throw new InvalidOperationException("Transaction is already open.");
			}
		}

		/// <summary>
		/// Creates and returns a new <see cref="System.Data.IDbCommand"/> object.
		/// </summary>
		/// <param name="sqlText">The text of the query.</param>
		/// <returns>An <see cref="System.Data.IDbCommand"/> object.</returns>
		internal IDbCommand CreateCommand(string sqlText)
		{
			return CreateCommand(sqlText, false);
		}

		/// <summary>
		/// Creates and returns a new <see cref="System.Data.IDbCommand"/> object.
		/// </summary>
		/// <param name="sqlText">The text of the query.</param>
		/// <param name="procedure">Specifies whether the sqlText parameter is 
		/// the name of a stored procedure.</param>
		/// <returns>An <see cref="System.Data.IDbCommand"/> object.</returns>
		internal IDbCommand CreateCommand(string sqlText, bool procedure)
		{
			IDbCommand cmd = _connection.CreateCommand();
			cmd.CommandText = sqlText;
			cmd.Transaction = _transaction;
			if(procedure)
				cmd.CommandType = CommandType.StoredProcedure;
			return cmd;
		}

		/// <summary>
		/// Rolls back any pending transactions and closes the DB connection.
		/// An application can call the <c>Close</c> method more than
		/// one time without generating an exception.
		/// </summary>
		public virtual void Close()
		{
			if(null != _connection)
				_connection.Close();
		}

		/// <summary>
		/// Rolls back any pending transactions and closes the DB connection.
		/// </summary>
		public virtual void Dispose()
		{
			Close();
			if(null != _connection)
				_connection.Dispose();
		}
	} // End of Rbr_Db_Base class
} // End of namespace
