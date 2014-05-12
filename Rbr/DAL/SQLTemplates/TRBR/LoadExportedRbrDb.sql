--*****************************************************************************
--*****************************************************************************
--INSERT ORDER:
--*****************************************************************************

--BULK INSERT RbrDb_$(DB_VERSION).dbo.CdrAggregate   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CdrAggregate.txt' WITH (BATCHSIZE = 10000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.TableSequence   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\TableSequence.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.PrefixInType   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\PrefixInType.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.Platform   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\Platform.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.Node   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\Node.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.ContactInfo   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\ContactInfo.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.VirtualSwitch   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\VirtualSwitch.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.CallingPlan   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CallingPlan.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.Country   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\Country.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.Route   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\Route.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.DialCode   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\DialCode.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.RoutingPlan   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\RoutingPlan.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.RoutingPlanDetail   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\RoutingPlanDetail.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.Schedule   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\Schedule.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.Partner   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\Partner.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.EndPoint   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\EndPoint.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.IPAddress   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\IPAddress.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.Service   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\Service.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.PayphoneSurcharge   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\PayphoneSurcharge.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.Person   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\Person.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.CustomerAcct   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CustomerAcct.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.AccessNumberList   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\AccessNumberList.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.DialPeer   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\DialPeer.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.LoadBalancingMap   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\LoadBalancingMap.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.ResellAcct   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\ResellAcct.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.CustomerSupportVendor   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CustomerSupportVendor.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.CustomerSupportGroup   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CustomerSupportGroup.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.CarrierAcct   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CarrierAcct.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.CarrierRoute   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CarrierRoute.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.CarrierAcctEPMap   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CarrierAcctEPMap.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.OutboundANI   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\OutboundANI.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.LCRBlackList   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\LCRBlackList.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.BalanceAdjustmentReason   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\BalanceAdjustmentReason.txt'
--BULK INSERT RbrDb_$(DB_VERSION).dbo.RetailAccount   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\RetailAccount.txt' WITH (BATCHSIZE = 10000)
--BULK INSERT RbrDb_$(DB_VERSION).dbo.RetailRouteBonusMinutes   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\RetailRouteBonusMinutes.txt' WITH (BATCHSIZE = 10000)
--BULK INSERT RbrDb_$(DB_VERSION).dbo.RetailAccountPayment   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\RetailAccountPayment.txt' WITH (BATCHSIZE = 1000)
--BULK INSERT RbrDb_$(DB_VERSION).dbo.ResidentialVoIP   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\ResidentialVoIP.txt' WITH (BATCHSIZE = 10000)
--BULK INSERT RbrDb_$(DB_VERSION).dbo.ResidentialPSTN   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\ResidentialPSTN.txt' WITH (BATCHSIZE = 10000)
--BULK INSERT RbrDb_$(DB_VERSION).dbo.PhoneCard   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\PhoneCard.txt' WITH (BATCHSIZE = 10000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.CallCenterCabina   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CallCenterCabina.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.CustomerAcctSupportMap   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CustomerAcctSupportMap.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.CustomerAcctPayment   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CustomerAcctPayment.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.TerminationChoice   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\TerminationChoice.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.InventoryLot   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\InventoryLot.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.GenerationRequest   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\GenerationRequest.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.Box   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\Box.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.Batch   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\Batch.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.InventoryHistory   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\InventoryHistory.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.InventoryUsage   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\InventoryUsage.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.RetailRoute   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\RetailRoute.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.WholesaleRoute   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\WholesaleRoute.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.RateInfo   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\RateInfo.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.RetailRateHistory   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\RetailRateHistory.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.ResellRateHistory   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\ResellRateHistory.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.WholesaleRateHistory   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\WholesaleRateHistory.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.CarrierRateHistory   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CarrierRateHistory.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.TypeOfDayChoice   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\TypeOfDayChoice.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.TimeOfDayPolicy   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\TimeOfDayPolicy.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.TimeOfDay   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\TimeOfDay.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.HolidayCalendar   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\HolidayCalendar.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.TypeOfDay   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\TypeOfDay.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.TimeOfDayPeriod   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\TimeOfDayPeriod.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.Rate   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\Rate.txt' WITH (BATCHSIZE = 1000)
BULK INSERT RbrDb_$(DB_VERSION).dbo.CdrExportMap   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CdrExportMap.txt'
BULK INSERT RbrDb_$(DB_VERSION).dbo.CdrExportMapDetail   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CdrExportMapDetail.txt'



