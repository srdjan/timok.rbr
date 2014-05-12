// <fileinfo name="CDRViewRow.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			You can update this source code manually. If the file
//			already exists it will not be rewritten by the generator.
//		</remarks>
//		<generator rewritefile="False" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using System.Collections.Specialized;
using Timok.NetworkLib;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.CdrDatabase.Base;

namespace Timok.Rbr.DAL.CdrDatabase {
	/// <summary>
	/// Represents a record in the <c>CDRView</c> view.
	/// </summary>
	public class CDRViewRow : CDRViewRow_Base {
		public const string DisconnectCause_PropName = "DisconnectCause";
		public const string DisconnectSource_PropName = "DisconnectSource";
		public const string RbrResult_PropName = "RbrResult";
		public const string MappedDisconnectCause_PropName = "MappedDisconnectCause";

		StringDictionary fieldValues;
		public string this[string pFieldName] {
			get {
				if (fieldValues == null) {
					createFieldIndexer();
				}
				return fieldValues[pFieldName];
			}
		}

		public GkDisconnectCause DisconnectCause {
			get { return (GkDisconnectCause) this.Disconnect_cause; }
		}

		public GkDisconnectSource DisconnectSource {
			get { return (GkDisconnectSource) this.Disconnect_source; }
		}

		public RbrResult RbrResult {
			get { return (RbrResult) this.Rbr_result; }
		}

		public GkDisconnectCause MappedDisconnectCause {
			get { return (GkDisconnectCause) this.Mapped_disconnect_cause; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CDRViewRow"/> class.
		/// </summary>
		public CDRViewRow() {}

		void createFieldIndexer() {
			fieldValues = new StringDictionary();
			fieldValues.Add(date_logged_DbName, Date_logged.ToString());
			fieldValues.Add(timok_date_DbName, Timok_date.ToString());
			fieldValues.Add(start_DbName, Start.ToString());
			fieldValues.Add(duration_DbName, Duration.ToString());
			fieldValues.Add(ccode_DbName, Ccode.ToString());
			fieldValues.Add(local_number_DbName, Local_number);
			fieldValues.Add(carrier_route_id_DbName, Carrier_route_id.ToString());
			fieldValues.Add(price_DbName, Price.ToString());
			fieldValues.Add(cost_DbName, Cost.ToString());
			fieldValues.Add(orig_IP_address_DbName, IPUtil.ToString(Orig_IP_address));
			fieldValues.Add(orig_end_point_id_DbName, Orig_end_point_id.ToString());
			fieldValues.Add(term_end_point_id_DbName, Term_end_point_id.ToString());
			fieldValues.Add(customer_acct_id_DbName, Customer_acct_id.ToString());
			fieldValues.Add(disconnect_cause_DbName, Disconnect_cause.ToString());
			fieldValues.Add(disconnect_source_DbName, Disconnect_source.ToString());
			fieldValues.Add(rbr_result_DbName, Rbr_result.ToString());
			fieldValues.Add(prefix_in_DbName, Prefix_in);
			fieldValues.Add(prefix_out_DbName, Prefix_out);
			fieldValues.Add(DNIS_DbName, DNIS.ToString());
			fieldValues.Add(ANI_DbName, ANI.ToString());
			fieldValues.Add(serial_number_DbName, Serial_number.ToString());
			fieldValues.Add(end_user_price_DbName, End_user_price.ToString());
			fieldValues.Add(used_bonus_minutes_DbName, Used_bonus_minutes.ToString());
			fieldValues.Add(reseller_price_DbName, Reseller_price.ToString());
			fieldValues.Add(node_id_DbName, Node_id.ToString());
			fieldValues.Add(customer_route_id_DbName, Customer_acct_name);
			fieldValues.Add(mapped_disconnect_cause_DbName, Mapped_disconnect_cause.ToString());
			fieldValues.Add(carrier_acct_id_DbName, Carrier_acct_name);
			fieldValues.Add(orig_dot_IP_address_DbName, Orig_dot_IP_address);
			fieldValues.Add(dialed_number_DbName, Dialed_number);
			fieldValues.Add(retail_acct_id_DbName, Retail_acct_id.ToString());
			fieldValues.Add(customer_duration_DbName, Customer_duration.ToString());
			fieldValues.Add(carrier_duration_DbName, Carrier_duration.ToString());
			fieldValues.Add(retail_duration_DbName, Retail_duration.ToString());
			fieldValues.Add(minutes_DbName, IsMinutesNull ? "<NULL>" : Minutes.ToString());
			fieldValues.Add(carrier_minutes_DbName, IsCarrier_minutesNull ? "<NULL>" : Carrier_minutes.ToString());
			fieldValues.Add(retail_minutes_DbName, IsRetail_minutesNull ? "<NULL>" : Retail_minutes.ToString());
			fieldValues.Add(carrier_route_name_DbName, Carrier_route_name);
			fieldValues.Add(customer_route_name_DbName, Customer_route_name);
			fieldValues.Add(orig_alias_DbName, Orig_alias);
			fieldValues.Add(term_alias_DbName, Term_alias);
			fieldValues.Add(term_ip_address_range_DbName, Term_ip_address_range);
			fieldValues.Add(customer_acct_name_DbName, Customer_acct_name);
			fieldValues.Add(orig_partner_id_DbName, IsOrig_partner_idNull ? "<NULL>" : Orig_partner_id.ToString());
			fieldValues.Add(orig_partner_name_DbName, Orig_partner_name);
			fieldValues.Add(carrier_acct_name_DbName, Carrier_acct_name);
			fieldValues.Add(term_partner_id_DbName, IsTerm_partner_idNull ? "<NULL>" : Term_partner_id.ToString());
			fieldValues.Add(term_partner_name_DbName, Term_partner_name);
			fieldValues.Add(node_name_DbName, Node_name);
		}
	} // End of CDRViewRow class
} // End of namespace
