using System;

using Timok.Rbr.Core;

namespace Timok.Rbr.DTO 
{
	[Serializable]
	public class RawCDRDto {

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
		public const string orig_dot_IP_address_PropName = "Orig_dot_IP_address";
		public const string term_end_point_id_PropName = "Term_end_point_id";
		public const string customer_acct_id_PropName = "Customer_acct_id";
		public const string disconnect_cause_PropName = "Disconnect_cause";
		public const string disconnect_source_PropName = "Disconnect_source";
		public const string rbr_result_PropName = "Rbr_result";
		public const string prefix_in_PropName = "Prefix_in";
		public const string prefix_out_PropName = "Prefix_out";
		public const string DNIS_PropName = "DNIS";
		public const string ANI_PropName = "ANI";
		public const string serial_number_PropName = "Serial_number";
		public const string end_user_price_PropName = "End_user_price";
		public const string used_bonus_minutes_PropName = "Used_bonus_minutes";
		public const string node_id_PropName = "Node_id";
		public const string customer_route_id_PropName = "Customer_route_id";
		public const string mapped_disconnect_cause_PropName = "Mapped_disconnect_cause";
		public const string dialed_number_PropName = "Dialed_number";
		public const string minutes_PropName = "Minutes";
		public const string carrier_route_name_PropName = "Carrier_route_name";
		public const string orig_end_point_id_PropName = "Orig_end_point_id";
		public const string orig_alias_PropName = "Orig_alias";
		public const string customer_acct_name_PropName = "Customer_acct_name";
		public const string orig_partner_id_PropName = "Orig_partner_id";
		public const string orig_partner_name_PropName = "Orig_partner_name";
		public const string customer_route_name_PropName = "Customer_route_name";
		public const string term_IP_address_PropName = "Term_IP_address";
		public const string term_dot_IP_address_PropName = "Term_dot_IP_address";
		public const string term_alias_PropName = "Term_alias";
		public const string carrier_acct_id_PropName = "Carrier_acct_id";
		public const string carrier_acct_name_PropName = "Carrier_acct_name";
		public const string term_partner_id_PropName = "Term_partner_id";
		public const string term_partner_name_PropName = "Term_partner_name";

    public const string customer_duration_PropName = "Customer_duration";
    public const string retail_acct_id_PropName = "Retail_acct_id";
    public const string reseller_price_PropName = "Reseller_price";
    public const string carrier_duration_PropName = "Carrier_duration";
    public const string retail_duration_PropName = "Retail_duration";

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
		private short _term_end_point_id;
		private short _customer_acct_id;
		private int _disconnect_cause;
		private byte _disconnect_source;
		private short _rbr_result;
		private string _prefix_in;
		private string _prefix_out;
		private long _dnis;
		private long _ani;
		private long _serial_number;
		private decimal _end_user_price;
		private short _used_bonus_minutes;
		private short _node_id;
		private int _customer_route_id;
		private int _mapped_disconnect_cause;
		private short _customer_duration;
		private int _retail_acct_id;
		private decimal _reseller_price;
		private short _carrier_duration;
		private short _retail_duration;


		public System.DateTime Date_logged {
			get { return _date_logged; }
			set { _date_logged = value; }
		}

		public int Timok_date {
			get { return _timok_date; }
			set { _timok_date = value; }
		}

		public System.DateTime Start {
			get { return _start; }
			set { _start = value; }
		}

		public short Duration {
			get { return _duration; }
			set { _duration = value; }
		}

		public int Ccode {
			get { return _ccode; }
			set { _ccode = value; }
		}

		public string Local_number {
			get { return _local_number; }
			set { _local_number = value; }
		}

		public int Carrier_route_id {
			get { return _carrier_route_id; }
			set { _carrier_route_id = value; }
		}

		public decimal Price {
			get { return _price; }
			set { _price = value; }
		}

		public decimal Cost {
			get { return _cost; }
			set { _cost = value; }
		}

		public int Orig_IP_address {
			get { return _orig_IP_address; }
			set { _orig_IP_address = value; }
		}

		public short Term_end_point_id {
			get { return _term_end_point_id; }
			set { _term_end_point_id = value; }
		}

		public short Customer_acct_id {
			get { return _customer_acct_id; }
			set { _customer_acct_id = value; }
		}

		public int Disconnect_cause {
			get { return _disconnect_cause; }
			set { _disconnect_cause = value; }
		}

		public byte Disconnect_source {
			get { return _disconnect_source; }
			set { _disconnect_source = value; }
		}

		public short Rbr_result {
			get { return _rbr_result; }
			set { _rbr_result = value; }
		}

		public string Prefix_in {
			get { return _prefix_in; }
			set { _prefix_in = value; }
		}

		public string Prefix_out {
			get { return _prefix_out; }
			set { _prefix_out = value; }
		}

		public long DNIS {
			get { return _dnis; }
			set { _dnis = value; }
		}

		public long ANI {
			get { return _ani; }
			set { _ani = value; }
		}

		public long Serial_number {
			get { return _serial_number; }
			set { _serial_number = value; }
		}

		public decimal End_user_price {
			get { return _end_user_price; }
			set { _end_user_price = value; }
		}

		public short Used_bonus_minutes {
			get { return _used_bonus_minutes; }
			set { _used_bonus_minutes = value; }
		}

		public short Node_id {
			get { return _node_id; }
			set { _node_id = value; }
		}

		public int Customer_route_id {
			get { return _customer_route_id; }
			set { _customer_route_id = value; }
		}

		public int Mapped_disconnect_cause {
			get { return _mapped_disconnect_cause; }
			set { _mapped_disconnect_cause = value; }
		}

		public short Customer_duration {
			get { return _customer_duration; }
			set { _customer_duration = value; }
		}

		public int Retail_acct_id {
			get { return _retail_acct_id; }
			set { _retail_acct_id = value; }
		}

		public decimal Reseller_price {
			get { return _reseller_price; }
			set { _reseller_price = value; }
		}


    public short Carrier_duration {
      get { return _carrier_duration; }
      set { _carrier_duration = value; }
    }

    public short Retail_duration {
      get { return _retail_duration; }
      set { _retail_duration = value; }
    }
    

    public RawCDRDto() { }
	}
}
