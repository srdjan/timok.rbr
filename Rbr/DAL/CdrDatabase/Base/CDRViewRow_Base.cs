// <fileinfo name="Base\CDRViewRow_Base.cs">
//		<copyright>
//			Copyright © 2002-2006 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			Do not change this source code manually. Changes to this file may 
//			cause incorrect behavior and will be lost if the code is regenerated.
//		</remarks>
//		<generator rewritefile="True" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using System.Runtime.Serialization;
using Timok.Core;

namespace Timok.Rbr.DAL.CdrDatabase.Base
{
	/// <summary>
	/// The base class for <see cref="CDRViewRow"/> that 
	/// represents a record in the <c>CDRView</c> view.
	/// </summary>
	/// <remarks>
	/// Do not change this source code manually. Update the <see cref="CDRViewRow"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	[Serializable]
	public abstract class CDRViewRow_Base
	{
	#region Timok Custom

		//db field names
		public const string id_DbName = "id";
		public const string date_logged_DbName = "date_logged";
		public const string timok_date_DbName = "timok_date";
		public const string start_DbName = "start";
		public const string duration_DbName = "duration";
		public const string ccode_DbName = "ccode";
		public const string local_number_DbName = "local_number";
		public const string carrier_route_id_DbName = "carrier_route_id";
		public const string price_DbName = "price";
		public const string cost_DbName = "cost";
		public const string orig_IP_address_DbName = "orig_IP_address";
		public const string orig_end_point_id_DbName = "orig_end_point_id";
		public const string term_end_point_id_DbName = "term_end_point_id";
		public const string customer_acct_id_DbName = "customer_acct_id";
		public const string disconnect_cause_DbName = "disconnect_cause";
		public const string disconnect_source_DbName = "disconnect_source";
		public const string rbr_result_DbName = "rbr_result";
		public const string prefix_in_DbName = "prefix_in";
		public const string prefix_out_DbName = "prefix_out";
		public const string DNIS_DbName = "DNIS";
		public const string ANI_DbName = "ANI";
		public const string info_digits_DbName = "info_digits";
		public const string serial_number_DbName = "serial_number";
		public const string end_user_price_DbName = "end_user_price";
		public const string used_bonus_minutes_DbName = "used_bonus_minutes";
		public const string reseller_price_DbName = "reseller_price";
		public const string node_id_DbName = "node_id";
		public const string customer_route_id_DbName = "customer_route_id";
		public const string mapped_disconnect_cause_DbName = "mapped_disconnect_cause";
		public const string carrier_acct_id_DbName = "carrier_acct_id";
		public const string orig_dot_IP_address_DbName = "orig_dot_IP_address";
		public const string dialed_number_DbName = "dialed_number";
		public const string retail_acct_id_DbName = "retail_acct_id";
		public const string customer_duration_DbName = "customer_duration";
		public const string carrier_duration_DbName = "carrier_duration";
		public const string retail_duration_DbName = "retail_duration";
		public const string minutes_DbName = "minutes";
		public const string carrier_minutes_DbName = "carrier_minutes";
		public const string retail_minutes_DbName = "retail_minutes";
		public const string carrier_route_name_DbName = "carrier_route_name";
		public const string customer_route_name_DbName = "customer_route_name";
		public const string orig_alias_DbName = "orig_alias";
		public const string term_alias_DbName = "term_alias";
		public const string term_ip_address_range_DbName = "term_ip_address_range";
		public const string customer_acct_name_DbName = "customer_acct_name";
		public const string orig_partner_id_DbName = "orig_partner_id";
		public const string orig_partner_name_DbName = "orig_partner_name";
		public const string carrier_acct_name_DbName = "carrier_acct_name";
		public const string term_partner_id_DbName = "term_partner_id";
		public const string term_partner_name_DbName = "term_partner_name";
		public const string node_name_DbName = "node_name";

		//prop names
		public const string id_PropName = "Id";
		public const string date_logged_PropName = "Date_logged";
		public const string timok_date_PropName = "Timok_date";
		public const string start_PropName = "Start";
		public const string duration_PropName = "Duration";
		public const string ccode_PropName = "Ccode";
		public const string local_number_PropName = "Local_number";
		public const string carrier_route_id_PropName = "Carrier_route_id";
		public const string price_PropName = "Price";
		public const string cost_PropName = "Cost";
		public const string orig_IP_address_PropName = "Orig_IP_address";
		public const string orig_end_point_id_PropName = "Orig_end_point_id";
		public const string term_end_point_id_PropName = "Term_end_point_id";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string disconnect_cause_PropName = "Disconnect_cause";
		public const string disconnect_source_PropName = "Disconnect_source";
		public const string rbr_result_PropName = "Rbr_result";
		public const string prefix_in_PropName = "Prefix_in";
		public const string prefix_out_PropName = "Prefix_out";
		public const string DNIS_PropName = "DNIS";
		public const string ANI_PropName = "ANI";
		public const string info_digits_PropName = "Info_digits";
		public const string serial_number_PropName = "Serial_number";
		public const string end_user_price_PropName = "End_user_price";
		public const string used_bonus_minutes_PropName = "Used_bonus_minutes";
		public const string reseller_price_PropName = "Reseller_price";
		public const string node_id_PropName = "Node_id";
		public const string customer_route_id_PropName = "Customer_route_id";
		public const string mapped_disconnect_cause_PropName = "Mapped_disconnect_cause";
		public const string carrier_acct_id_PropName = "Carrier_acct_id";
		public const string orig_dot_IP_address_PropName = "Orig_dot_IP_address";
		public const string dialed_number_PropName = "Dialed_number";
		public const string retail_acct_id_PropName = "Retail_acct_id";
		public const string customer_duration_PropName = "Customer_duration";
		public const string carrier_duration_PropName = "Carrier_duration";
		public const string retail_duration_PropName = "Retail_duration";
		public const string minutes_PropName = "Minutes";
		public const string carrier_minutes_PropName = "Carrier_minutes";
		public const string retail_minutes_PropName = "Retail_minutes";
		public const string carrier_route_name_PropName = "Carrier_route_name";
		public const string customer_route_name_PropName = "Customer_route_name";
		public const string orig_alias_PropName = "Orig_alias";
		public const string term_alias_PropName = "Term_alias";
		public const string term_ip_address_range_PropName = "Term_ip_address_range";
		public const string customer_acct_name_PropName = "Customer_acct_name";
		public const string orig_partner_id_PropName = "Orig_partner_id";
		public const string orig_partner_name_PropName = "Orig_partner_name";
		public const string carrier_acct_name_PropName = "Carrier_acct_name";
		public const string term_partner_id_PropName = "Term_partner_id";
		public const string term_partner_name_PropName = "Term_partner_name";
		public const string node_name_PropName = "Node_name";

		//db field display names
		public const string id_DisplayName = "id";
		public const string date_logged_DisplayName = "date logged";
		public const string timok_date_DisplayName = "timok date";
		public const string start_DisplayName = "start";
		public const string duration_DisplayName = "duration";
		public const string ccode_DisplayName = "ccode";
		public const string local_number_DisplayName = "local number";
		public const string carrier_route_id_DisplayName = "carrier route id";
		public const string price_DisplayName = "price";
		public const string cost_DisplayName = "cost";
		public const string orig_IP_address_DisplayName = "orig IP address";
		public const string orig_end_point_id_DisplayName = "orig end point id";
		public const string term_end_point_id_DisplayName = "term end point id";
		public const string customer_acct_id_DisplayName = "customer acct id";
		public const string disconnect_cause_DisplayName = "disconnect cause";
		public const string disconnect_source_DisplayName = "disconnect source";
		public const string rbr_result_DisplayName = "rbr result";
		public const string prefix_in_DisplayName = "prefix in";
		public const string prefix_out_DisplayName = "prefix out";
		public const string DNIS_DisplayName = "DNIS";
		public const string ANI_DisplayName = "ANI";
		public const string info_digits_DisplayName = "info digits";
		public const string serial_number_DisplayName = "serial number";
		public const string end_user_price_DisplayName = "end user price";
		public const string used_bonus_minutes_DisplayName = "used bonus minutes";
		public const string reseller_price_DisplayName = "reseller price";
		public const string node_id_DisplayName = "node id";
		public const string customer_route_id_DisplayName = "customer route id";
		public const string mapped_disconnect_cause_DisplayName = "mapped disconnect cause";
		public const string carrier_acct_id_DisplayName = "carrier acct id";
		public const string orig_dot_IP_address_DisplayName = "orig dot IP address";
		public const string dialed_number_DisplayName = "dialed number";
		public const string retail_acct_id_DisplayName = "retail acct id";
		public const string customer_duration_DisplayName = "customer duration";
		public const string carrier_duration_DisplayName = "carrier duration";
		public const string retail_duration_DisplayName = "retail duration";
		public const string minutes_DisplayName = "minutes";
		public const string carrier_minutes_DisplayName = "carrier minutes";
		public const string retail_minutes_DisplayName = "retail minutes";
		public const string carrier_route_name_DisplayName = "carrier route name";
		public const string customer_route_name_DisplayName = "customer route name";
		public const string orig_alias_DisplayName = "orig alias";
		public const string term_alias_DisplayName = "term alias";
		public const string term_ip_address_range_DisplayName = "term ip address range";
		public const string customer_acct_name_DisplayName = "customer acct name";
		public const string orig_partner_id_DisplayName = "orig partner id";
		public const string orig_partner_name_DisplayName = "orig partner name";
		public const string carrier_acct_name_DisplayName = "carrier acct name";
		public const string term_partner_id_DisplayName = "term partner id";
		public const string term_partner_name_DisplayName = "term partner name";
		public const string node_name_DisplayName = "node name";
	#endregion Timok Custom


		private string _id;
		private System.DateTime _date_logged;
		private int _timok_date;
		private System.DateTime _start;
		private short _duration;
		private int _ccode;
		private string _local_number;
		private int _carrier_route_id;
		private decimal _price;
		private decimal _cost;
		private int _orig_IP_address;
		private short _orig_end_point_id;
		private short _term_end_point_id;
		private short _customer_acct_id;
		private short _disconnect_cause;
		private byte _disconnect_source;
		private short _rbr_result;
		private string _prefix_in;
		private string _prefix_out;
		private long _dnis;
		private long _ani;
		private byte _info_digits;
		private long _serial_number;
		private decimal _end_user_price;
		private short _used_bonus_minutes;
		private decimal _reseller_price;
		private short _node_id;
		private int _customer_route_id;
		private short _mapped_disconnect_cause;
		private short _carrier_acct_id;
		private string _orig_dot_IP_address;
		private string _dialed_number;
		private int _retail_acct_id;
		private short _customer_duration;
		private short _carrier_duration;
		private short _retail_duration;
		private decimal _minutes;
		private bool _minutesNull = true;
		private decimal _carrier_minutes;
		private bool _carrier_minutesNull = true;
		private decimal _retail_minutes;
		private bool _retail_minutesNull = true;
		private string _carrier_route_name;
		private string _customer_route_name;
		private string _orig_alias;
		private string _term_alias;
		private string _term_ip_address_range;
		private string _customer_acct_name;
		private int _orig_partner_id;
		private bool _orig_partner_idNull = true;
		private string _orig_partner_name;
		private string _carrier_acct_name;
		private int _term_partner_id;
		private bool _term_partner_idNull = true;
		private string _term_partner_name;
		private string _node_name;

		/// <summary>
		/// Initializes a new instance of the <see cref="CDRViewRow_Base"/> class.
		/// </summary>
		public CDRViewRow_Base()
		{
			// EMPTY
		}

		/// <summary>
		/// Gets or sets the <c>id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>id</c> column value.</value>
		public string Id
		{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>date_logged</c> column value.
		/// </summary>
		/// <value>The <c>date_logged</c> column value.</value>
		public System.DateTime Date_logged
		{
			get { return _date_logged; }
			set { _date_logged = value; }
		}

		/// <summary>
		/// Gets or sets the <c>timok_date</c> column value.
		/// </summary>
		/// <value>The <c>timok_date</c> column value.</value>
		public int Timok_date
		{
			get { return _timok_date; }
			set { _timok_date = value; }
		}

		/// <summary>
		/// Gets or sets the <c>start</c> column value.
		/// </summary>
		/// <value>The <c>start</c> column value.</value>
		public System.DateTime Start
		{
			get { return _start; }
			set { _start = value; }
		}

		/// <summary>
		/// Gets or sets the <c>duration</c> column value.
		/// </summary>
		/// <value>The <c>duration</c> column value.</value>
		public short Duration
		{
			get { return _duration; }
			set { _duration = value; }
		}

		/// <summary>
		/// Gets or sets the <c>ccode</c> column value.
		/// </summary>
		/// <value>The <c>ccode</c> column value.</value>
		public int Ccode
		{
			get { return _ccode; }
			set { _ccode = value; }
		}

		/// <summary>
		/// Gets or sets the <c>local_number</c> column value.
		/// </summary>
		/// <value>The <c>local_number</c> column value.</value>
		public string Local_number
		{
			get { return _local_number; }
			set { _local_number = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_route_id</c> column value.
		/// </summary>
		/// <value>The <c>carrier_route_id</c> column value.</value>
		public int Carrier_route_id
		{
			get { return _carrier_route_id; }
			set { _carrier_route_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>price</c> column value.
		/// </summary>
		/// <value>The <c>price</c> column value.</value>
		public decimal Price
		{
			get { return _price; }
			set { _price = value; }
		}

		/// <summary>
		/// Gets or sets the <c>cost</c> column value.
		/// </summary>
		/// <value>The <c>cost</c> column value.</value>
		public decimal Cost
		{
			get { return _cost; }
			set { _cost = value; }
		}

		/// <summary>
		/// Gets or sets the <c>orig_IP_address</c> column value.
		/// </summary>
		/// <value>The <c>orig_IP_address</c> column value.</value>
		public int Orig_IP_address
		{
			get { return _orig_IP_address; }
			set { _orig_IP_address = value; }
		}

		/// <summary>
		/// Gets or sets the <c>orig_end_point_id</c> column value.
		/// </summary>
		/// <value>The <c>orig_end_point_id</c> column value.</value>
		public short Orig_end_point_id
		{
			get { return _orig_end_point_id; }
			set { _orig_end_point_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>term_end_point_id</c> column value.
		/// </summary>
		/// <value>The <c>term_end_point_id</c> column value.</value>
		public short Term_end_point_id
		{
			get { return _term_end_point_id; }
			set { _term_end_point_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>customer_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>customer_acct_id</c> column value.</value>
		public short Customer_acct_id
		{
			get { return _customer_acct_id; }
			set { _customer_acct_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>disconnect_cause</c> column value.
		/// </summary>
		/// <value>The <c>disconnect_cause</c> column value.</value>
		public short Disconnect_cause
		{
			get { return _disconnect_cause; }
			set { _disconnect_cause = value; }
		}

		/// <summary>
		/// Gets or sets the <c>disconnect_source</c> column value.
		/// </summary>
		/// <value>The <c>disconnect_source</c> column value.</value>
		public byte Disconnect_source
		{
			get { return _disconnect_source; }
			set { _disconnect_source = value; }
		}

		/// <summary>
		/// Gets or sets the <c>rbr_result</c> column value.
		/// </summary>
		/// <value>The <c>rbr_result</c> column value.</value>
		public short Rbr_result
		{
			get { return _rbr_result; }
			set { _rbr_result = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_in</c> column value.
		/// </summary>
		/// <value>The <c>prefix_in</c> column value.</value>
		public string Prefix_in
		{
			get { return _prefix_in; }
			set { _prefix_in = value; }
		}

		/// <summary>
		/// Gets or sets the <c>prefix_out</c> column value.
		/// </summary>
		/// <value>The <c>prefix_out</c> column value.</value>
		public string Prefix_out
		{
			get { return _prefix_out; }
			set { _prefix_out = value; }
		}

		/// <summary>
		/// Gets or sets the <c>DNIS</c> column value.
		/// </summary>
		/// <value>The <c>DNIS</c> column value.</value>
		public long DNIS
		{
			get { return _dnis; }
			set { _dnis = value; }
		}

		/// <summary>
		/// Gets or sets the <c>ANI</c> column value.
		/// </summary>
		/// <value>The <c>ANI</c> column value.</value>
		public long ANI
		{
			get { return _ani; }
			set { _ani = value; }
		}

		/// <summary>
		/// Gets or sets the <c>info_digits</c> column value.
		/// </summary>
		/// <value>The <c>info_digits</c> column value.</value>
		public byte Info_digits
		{
			get { return _info_digits; }
			set { _info_digits = value; }
		}

		/// <summary>
		/// Gets or sets the <c>serial_number</c> column value.
		/// </summary>
		/// <value>The <c>serial_number</c> column value.</value>
		public long Serial_number
		{
			get { return _serial_number; }
			set { _serial_number = value; }
		}

		/// <summary>
		/// Gets or sets the <c>end_user_price</c> column value.
		/// </summary>
		/// <value>The <c>end_user_price</c> column value.</value>
		public decimal End_user_price
		{
			get { return _end_user_price; }
			set { _end_user_price = value; }
		}

		/// <summary>
		/// Gets or sets the <c>used_bonus_minutes</c> column value.
		/// </summary>
		/// <value>The <c>used_bonus_minutes</c> column value.</value>
		public short Used_bonus_minutes
		{
			get { return _used_bonus_minutes; }
			set { _used_bonus_minutes = value; }
		}

		/// <summary>
		/// Gets or sets the <c>reseller_price</c> column value.
		/// </summary>
		/// <value>The <c>reseller_price</c> column value.</value>
		public decimal Reseller_price
		{
			get { return _reseller_price; }
			set { _reseller_price = value; }
		}

		/// <summary>
		/// Gets or sets the <c>node_id</c> column value.
		/// </summary>
		/// <value>The <c>node_id</c> column value.</value>
		public short Node_id
		{
			get { return _node_id; }
			set { _node_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>customer_route_id</c> column value.
		/// </summary>
		/// <value>The <c>customer_route_id</c> column value.</value>
		public int Customer_route_id
		{
			get { return _customer_route_id; }
			set { _customer_route_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>mapped_disconnect_cause</c> column value.
		/// </summary>
		/// <value>The <c>mapped_disconnect_cause</c> column value.</value>
		public short Mapped_disconnect_cause
		{
			get { return _mapped_disconnect_cause; }
			set { _mapped_disconnect_cause = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>carrier_acct_id</c> column value.</value>
		public short Carrier_acct_id
		{
			get { return _carrier_acct_id; }
			set { _carrier_acct_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>orig_dot_IP_address</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>orig_dot_IP_address</c> column value.</value>
		public string Orig_dot_IP_address
		{
			get { return _orig_dot_IP_address; }
			set { _orig_dot_IP_address = value; }
		}

		/// <summary>
		/// Gets or sets the <c>dialed_number</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>dialed_number</c> column value.</value>
		public string Dialed_number
		{
			get { return _dialed_number; }
			set { _dialed_number = value; }
		}

		/// <summary>
		/// Gets or sets the <c>retail_acct_id</c> column value.
		/// </summary>
		/// <value>The <c>retail_acct_id</c> column value.</value>
		public int Retail_acct_id
		{
			get { return _retail_acct_id; }
			set { _retail_acct_id = value; }
		}

		/// <summary>
		/// Gets or sets the <c>customer_duration</c> column value.
		/// </summary>
		/// <value>The <c>customer_duration</c> column value.</value>
		public short Customer_duration
		{
			get { return _customer_duration; }
			set { _customer_duration = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_duration</c> column value.
		/// </summary>
		/// <value>The <c>carrier_duration</c> column value.</value>
		public short Carrier_duration
		{
			get { return _carrier_duration; }
			set { _carrier_duration = value; }
		}

		/// <summary>
		/// Gets or sets the <c>retail_duration</c> column value.
		/// </summary>
		/// <value>The <c>retail_duration</c> column value.</value>
		public short Retail_duration
		{
			get { return _retail_duration; }
			set { _retail_duration = value; }
		}

		/// <summary>
		/// Gets or sets the <c>minutes</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>minutes</c> column value.</value>
		public decimal Minutes
		{
			get
			{
				//if(IsMinutesNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _minutes;
			}
			set
			{
				_minutesNull = false;
				_minutes = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Minutes"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsMinutesNull
		{
			get { return _minutesNull; }
			set { _minutesNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_minutes</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>carrier_minutes</c> column value.</value>
		public decimal Carrier_minutes
		{
			get
			{
				//if(IsCarrier_minutesNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _carrier_minutes;
			}
			set
			{
				_carrier_minutesNull = false;
				_carrier_minutes = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Carrier_minutes"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsCarrier_minutesNull
		{
			get { return _carrier_minutesNull; }
			set { _carrier_minutesNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>retail_minutes</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>retail_minutes</c> column value.</value>
		public decimal Retail_minutes
		{
			get
			{
				//if(IsRetail_minutesNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _retail_minutes;
			}
			set
			{
				_retail_minutesNull = false;
				_retail_minutes = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Retail_minutes"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsRetail_minutesNull
		{
			get { return _retail_minutesNull; }
			set { _retail_minutesNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_route_name</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>carrier_route_name</c> column value.</value>
		public string Carrier_route_name
		{
			get { return _carrier_route_name; }
			set { _carrier_route_name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>customer_route_name</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>customer_route_name</c> column value.</value>
		public string Customer_route_name
		{
			get { return _customer_route_name; }
			set { _customer_route_name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>orig_alias</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>orig_alias</c> column value.</value>
		public string Orig_alias
		{
			get { return _orig_alias; }
			set { _orig_alias = value; }
		}

		/// <summary>
		/// Gets or sets the <c>term_alias</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>term_alias</c> column value.</value>
		public string Term_alias
		{
			get { return _term_alias; }
			set { _term_alias = value; }
		}

		/// <summary>
		/// Gets or sets the <c>term_ip_address_range</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>term_ip_address_range</c> column value.</value>
		public string Term_ip_address_range
		{
			get { return _term_ip_address_range; }
			set { _term_ip_address_range = value; }
		}

		/// <summary>
		/// Gets or sets the <c>customer_acct_name</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>customer_acct_name</c> column value.</value>
		public string Customer_acct_name
		{
			get { return _customer_acct_name; }
			set { _customer_acct_name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>orig_partner_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>orig_partner_id</c> column value.</value>
		public int Orig_partner_id
		{
			get
			{
				//if(IsOrig_partner_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _orig_partner_id;
			}
			set
			{
				_orig_partner_idNull = false;
				_orig_partner_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Orig_partner_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsOrig_partner_idNull
		{
			get { return _orig_partner_idNull; }
			set { _orig_partner_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>orig_partner_name</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>orig_partner_name</c> column value.</value>
		public string Orig_partner_name
		{
			get { return _orig_partner_name; }
			set { _orig_partner_name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>carrier_acct_name</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>carrier_acct_name</c> column value.</value>
		public string Carrier_acct_name
		{
			get { return _carrier_acct_name; }
			set { _carrier_acct_name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>term_partner_id</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>term_partner_id</c> column value.</value>
		public int Term_partner_id
		{
			get
			{
				//if(IsTerm_partner_idNull)
					//throw new InvalidOperationException("Cannot get value because it is DBNull.");
				return _term_partner_id;
			}
			set
			{
				_term_partner_idNull = false;
				_term_partner_id = value;
			}
		}

		/// <summary>
		/// Indicates whether the <see cref="Term_partner_id"/>
		/// property value is null.
		/// </summary>
		/// <value>true if the property value is null, otherwise false.</value>
		public bool IsTerm_partner_idNull
		{
			get { return _term_partner_idNull; }
			set { _term_partner_idNull = value; }
		}

		/// <summary>
		/// Gets or sets the <c>term_partner_name</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>term_partner_name</c> column value.</value>
		public string Term_partner_name
		{
			get { return _term_partner_name; }
			set { _term_partner_name = value; }
		}

		/// <summary>
		/// Gets or sets the <c>node_name</c> column value.
		/// This column is nullable.
		/// </summary>
		/// <value>The <c>node_name</c> column value.</value>
		public string Node_name
		{
			get { return _node_name; }
			set { _node_name = value; }
		}

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
			dynStr.Append(':');
			dynStr.Append("  Id=");
			dynStr.Append(Id);
			dynStr.Append("  Date_logged=");
			dynStr.Append(Date_logged);
			dynStr.Append("  Timok_date=");
			dynStr.Append(Timok_date);
			dynStr.Append("  Start=");
			dynStr.Append(Start);
			dynStr.Append("  Duration=");
			dynStr.Append(Duration);
			dynStr.Append("  Ccode=");
			dynStr.Append(Ccode);
			dynStr.Append("  Local_number=");
			dynStr.Append(Local_number);
			dynStr.Append("  Carrier_route_id=");
			dynStr.Append(Carrier_route_id);
			dynStr.Append("  Price=");
			dynStr.Append(Price);
			dynStr.Append("  Cost=");
			dynStr.Append(Cost);
			dynStr.Append("  Orig_IP_address=");
			dynStr.Append(Orig_IP_address);
			dynStr.Append("  Orig_end_point_id=");
			dynStr.Append(Orig_end_point_id);
			dynStr.Append("  Term_end_point_id=");
			dynStr.Append(Term_end_point_id);
			dynStr.Append("  Customer_acct_id=");
			dynStr.Append(Customer_acct_id);
			dynStr.Append("  Disconnect_cause=");
			dynStr.Append(Disconnect_cause);
			dynStr.Append("  Disconnect_source=");
			dynStr.Append(Disconnect_source);
			dynStr.Append("  Rbr_result=");
			dynStr.Append(Rbr_result);
			dynStr.Append("  Prefix_in=");
			dynStr.Append(Prefix_in);
			dynStr.Append("  Prefix_out=");
			dynStr.Append(Prefix_out);
			dynStr.Append("  DNIS=");
			dynStr.Append(DNIS);
			dynStr.Append("  ANI=");
			dynStr.Append(ANI);
			dynStr.Append("  Info_digits=");
			dynStr.Append(Info_digits);
			dynStr.Append("  Serial_number=");
			dynStr.Append(Serial_number);
			dynStr.Append("  End_user_price=");
			dynStr.Append(End_user_price);
			dynStr.Append("  Used_bonus_minutes=");
			dynStr.Append(Used_bonus_minutes);
			dynStr.Append("  Reseller_price=");
			dynStr.Append(Reseller_price);
			dynStr.Append("  Node_id=");
			dynStr.Append(Node_id);
			dynStr.Append("  Customer_route_id=");
			dynStr.Append(Customer_route_id);
			dynStr.Append("  Mapped_disconnect_cause=");
			dynStr.Append(Mapped_disconnect_cause);
			dynStr.Append("  Carrier_acct_id=");
			dynStr.Append(Carrier_acct_id);
			dynStr.Append("  Orig_dot_IP_address=");
			dynStr.Append(Orig_dot_IP_address);
			dynStr.Append("  Dialed_number=");
			dynStr.Append(Dialed_number);
			dynStr.Append("  Retail_acct_id=");
			dynStr.Append(Retail_acct_id);
			dynStr.Append("  Customer_duration=");
			dynStr.Append(Customer_duration);
			dynStr.Append("  Carrier_duration=");
			dynStr.Append(Carrier_duration);
			dynStr.Append("  Retail_duration=");
			dynStr.Append(Retail_duration);
			dynStr.Append("  Minutes=");
			dynStr.Append(IsMinutesNull ? (object)"<NULL>" : Minutes);
			dynStr.Append("  Carrier_minutes=");
			dynStr.Append(IsCarrier_minutesNull ? (object)"<NULL>" : Carrier_minutes);
			dynStr.Append("  Retail_minutes=");
			dynStr.Append(IsRetail_minutesNull ? (object)"<NULL>" : Retail_minutes);
			dynStr.Append("  Carrier_route_name=");
			dynStr.Append(Carrier_route_name);
			dynStr.Append("  Customer_route_name=");
			dynStr.Append(Customer_route_name);
			dynStr.Append("  Orig_alias=");
			dynStr.Append(Orig_alias);
			dynStr.Append("  Term_alias=");
			dynStr.Append(Term_alias);
			dynStr.Append("  Term_ip_address_range=");
			dynStr.Append(Term_ip_address_range);
			dynStr.Append("  Customer_acct_name=");
			dynStr.Append(Customer_acct_name);
			dynStr.Append("  Orig_partner_id=");
			dynStr.Append(IsOrig_partner_idNull ? (object)"<NULL>" : Orig_partner_id);
			dynStr.Append("  Orig_partner_name=");
			dynStr.Append(Orig_partner_name);
			dynStr.Append("  Carrier_acct_name=");
			dynStr.Append(Carrier_acct_name);
			dynStr.Append("  Term_partner_id=");
			dynStr.Append(IsTerm_partner_idNull ? (object)"<NULL>" : Term_partner_id);
			dynStr.Append("  Term_partner_name=");
			dynStr.Append(Term_partner_name);
			dynStr.Append("  Node_name=");
			dynStr.Append(Node_name);
			return dynStr.ToString();
		}

	#region Timok Custom

		public override bool Equals(object obj) {
			if(obj == null || obj.GetType() != this.GetType())
				return false;

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return this.ToString().GetHashCode();
		}
	#endregion Timok Custom

	} // End of CDRViewRow_Base class
} // End of namespace
