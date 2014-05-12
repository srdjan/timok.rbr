echo on

mkdir "C:\Temp\DbBackup\%1\"

bcp "SELECT * FROM RbrDb_%1.dbo.Route ORDER BY route_id" 							queryout "C:\Temp\DbBackup\%1\Route.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.ResellAcct ORDER BY resell_acct_id" 						queryout "C:\Temp\DbBackup\%1\ResellAcct.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.InventoryHistory ORDER BY service_id,batch_id,timestamp" 			queryout "C:\Temp\DbBackup\%1\InventoryHistory.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.InventoryUsage ORDER BY service_id,customer_acct_id,timestamp" 		queryout "C:\Temp\DbBackup\%1\InventoryUsage.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CarrierAcctEPMap ORDER BY carrier_acct_EP_map_id" 				queryout "C:\Temp\DbBackup\%1\CarrierAcctEPMap.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.IPAddress ORDER BY IP_address" 						queryout "C:\Temp\DbBackup\%1\IPAddress.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CarrierAcct ORDER BY carrier_acct_id" 						queryout "C:\Temp\DbBackup\%1\CarrierAcct.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.EndPoint ORDER BY end_point_id" 						queryout "C:\Temp\DbBackup\%1\EndPoint.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.DialPeer ORDER BY end_point_id, prefix_in, customer_acct_id" 			queryout "C:\Temp\DbBackup\%1\DialPeer.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.PrefixInType ORDER BY prefix_in_type_id" 					queryout "C:\Temp\DbBackup\%1\PrefixInType.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CustomerAcct ORDER BY customer_acct_id" 					queryout "C:\Temp\DbBackup\%1\CustomerAcct.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.Node ORDER BY node_id" 							queryout "C:\Temp\DbBackup\%1\Node.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.LoadBalancingMap ORDER BY node_id, customer_acct_id" 				queryout "C:\Temp\DbBackup\%1\LoadBalancingMap.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.Partner ORDER BY partner_id" 							queryout "C:\Temp\DbBackup\%1\Partner.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.Platform ORDER BY platform_id" 						queryout "C:\Temp\DbBackup\%1\Platform.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.Service ORDER BY service_id" 							queryout "C:\Temp\DbBackup\%1\Service.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.TypeOfDayChoice ORDER BY type_of_day_choice" 					queryout "C:\Temp\DbBackup\%1\TypeOfDayChoice.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.TimeOfDayPolicy ORDER BY time_of_day_policy" 					queryout "C:\Temp\DbBackup\%1\TimeOfDayPolicy.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.PayphoneSurcharge ORDER BY payphone_surcharge_id" 				queryout "C:\Temp\DbBackup\%1\PayphoneSurcharge.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.Country ORDER BY country_id" 							queryout "C:\Temp\DbBackup\%1\Country.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.RetailRoute ORDER BY retail_route_id" 						queryout "C:\Temp\DbBackup\%1\RetailRoute.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.WholesaleRoute ORDER BY wholesale_route_id" 					queryout "C:\Temp\DbBackup\%1\WholesaleRoute.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.TimeOfDay ORDER BY time_of_day" 						queryout "C:\Temp\DbBackup\%1\TimeOfDay.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.TypeOfDay ORDER BY rate_info_id, type_of_day_choice" 				queryout "C:\Temp\DbBackup\%1\TypeOfDay.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CustomerSupportGroup ORDER BY group_id" 					queryout "C:\Temp\DbBackup\%1\CustomerSupportGroup.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.Schedule ORDER BY schedule_id" 						queryout "C:\Temp\DbBackup\%1\Schedule.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.RoutingPlanDetail ORDER BY routing_plan_id, route_id" 				queryout "C:\Temp\DbBackup\%1\RoutingPlanDetail.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.InventoryLot ORDER BY lot_id" 							queryout "C:\Temp\DbBackup\%1\InventoryLot.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.ContactInfo ORDER BY contact_info_id" 						queryout "C:\Temp\DbBackup\%1\ContactInfo.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CustomerSupportVendor ORDER BY vendor_id" 					queryout "C:\Temp\DbBackup\%1\CustomerSupportVendor.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.BalanceAdjustmentReason ORDER BY balance_adjustment_reason_id" 		queryout "C:\Temp\DbBackup\%1\BalanceAdjustmentReason.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.Person ORDER BY person_id" 							queryout "C:\Temp\DbBackup\%1\Person.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.RoutingPlan ORDER BY routing_plan_id" 						queryout "C:\Temp\DbBackup\%1\RoutingPlan.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CdrExportMap ORDER BY map_id" 							queryout "C:\Temp\DbBackup\%1\CdrExportMap.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.RateInfo ORDER BY rate_info_id" 						queryout "C:\Temp\DbBackup\%1\RateInfo.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CarrierRoute ORDER BY carrier_route_id" 					queryout "C:\Temp\DbBackup\%1\CarrierRoute.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CallingPlan ORDER BY calling_plan_id" 						queryout "C:\Temp\DbBackup\%1\CallingPlan.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.VirtualSwitch ORDER BY virtual_switch_id" 					queryout "C:\Temp\DbBackup\%1\VirtualSwitch.txt" -c -S(local)\TRBR -T

REM ------ BIG TABLE --------------------
bcp "SELECT * FROM RbrDb_%1.dbo.RetailAccount ORDER BY retail_acct_id" 					queryout "C:\Temp\DbBackup\%1\RetailAccount.txt" -c -S(local)\TRBR -T

bcp "SELECT * FROM RbrDb_%1.dbo.Box ORDER BY box_id" 								queryout "C:\Temp\DbBackup\%1\Box.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.GenerationRequest ORDER BY request_id" 					queryout "C:\Temp\DbBackup\%1\GenerationRequest.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.WholesaleRateHistory ORDER BY wholesale_route_id, date_on" 			queryout "C:\Temp\DbBackup\%1\WholesaleRateHistory.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.TimeOfDayPeriod ORDER BY rate_info_id, type_of_day_choice, start_hour" 	queryout "C:\Temp\DbBackup\%1\TimeOfDayPeriod.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.TerminationChoice ORDER BY termination_choice_id" 				queryout "C:\Temp\DbBackup\%1\TerminationChoice.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.TableSequence ORDER BY TableName" 						queryout "C:\Temp\DbBackup\%1\TableSequence.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.RetailRouteBonusMinutes ORDER BY retail_acct_id, retail_route_id" 		queryout "C:\Temp\DbBackup\%1\RetailRouteBonusMinutes.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.RetailRateHistory ORDER BY retail_route_id, date_on" 				queryout "C:\Temp\DbBackup\%1\RetailRateHistory.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.RetailAccountPayment ORDER BY retail_acct_id,date_time" 			queryout "C:\Temp\DbBackup\%1\RetailAccountPayment.txt" -c -S(local)\TRBR -T

REM ------ BIG TABLES --------------------
bcp "SELECT * FROM RbrDb_%1.dbo.ResidentialVoIP ORDER BY user_id" 						queryout "C:\Temp\DbBackup\%1\ResidentialVoIP.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.ResidentialPSTN ORDER BY service_id, ANI" 					queryout "C:\Temp\DbBackup\%1\ResidentialPSTN.txt" -c -S(local)\TRBR -T

bcp "SELECT * FROM RbrDb_%1.dbo.ResellRateHistory ORDER BY resell_acct_id,wholesale_route_id,date_on" 		queryout "C:\Temp\DbBackup\%1\ResellRateHistory.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.Rate ORDER BY rate_info_id, type_of_day_choice, time_of_day" 			queryout "C:\Temp\DbBackup\%1\Rate.txt" -c -S(local)\TRBR -T

REM ------ BIG TABLE --------------------
bcp "SELECT * FROM RbrDb_%1.dbo.PhoneCard ORDER BY service_id, pin" 						queryout "C:\Temp\DbBackup\%1\PhoneCard.txt" -c -S(local)\TRBR -T

bcp "SELECT * FROM RbrDb_%1.dbo.OutboundANI ORDER BY outbound_ani_id" 						queryout "C:\Temp\DbBackup\%1\OutboundANI.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.LCRBlackList ORDER BY routing_plan_id, route_id, carrier_acct_id" 		queryout "C:\Temp\DbBackup\%1\LCRBlackList.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.HolidayCalendar ORDER BY rate_info_id, holiday_day" 				queryout "C:\Temp\DbBackup\%1\HolidayCalendar.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.DialCode ORDER BY calling_plan_id, dial_code" 					queryout "C:\Temp\DbBackup\%1\DialCode.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CustomerAcctSupportMap ORDER BY customer_acct_id, vendor_id" 			queryout "C:\Temp\DbBackup\%1\CustomerAcctSupportMap.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CustomerAcctPayment ORDER BY customer_acct_id, date_time" 			queryout "C:\Temp\DbBackup\%1\CustomerAcctPayment.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CdrExportMapDetail ORDER BY map_detail_id" 					queryout "C:\Temp\DbBackup\%1\CdrExportMapDetail.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CarrierRateHistory ORDER BY carrier_route_id, date_on" 			queryout "C:\Temp\DbBackup\%1\CarrierRateHistory.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.CallCenterCabina ORDER BY service_id, serial_number" 				queryout "C:\Temp\DbBackup\%1\CallCenterCabina.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.Batch ORDER BY batch_id" 							queryout "C:\Temp\DbBackup\%1\Batch.txt" -c -S(local)\TRBR -T
bcp "SELECT * FROM RbrDb_%1.dbo.AccessNumberList ORDER BY access_number" 					queryout "C:\Temp\DbBackup\%1\AccessNumberList.txt" -c -S(local)\TRBR -T

cmd
