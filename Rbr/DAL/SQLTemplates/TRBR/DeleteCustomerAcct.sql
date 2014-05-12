DELETE FROM DialPeer
	WHERE DialPeer.customer_acct_id in (
										SELECT [customer_acct_id]
									    FROM [RbrDb_267].[dbo].[CustomerAcct]
										where name like 'z%'
										)
DELETE FROM CustomerAcctPayment
	WHERE customer_acct_id in (
						  SELECT [customer_acct_id]
						  FROM [RbrDb_267].[dbo].[CustomerAcct]
						  where name like 'z%'
						  ) 

DELETE FROM ResellAcct
	WHERE customer_acct_id in (
						  SELECT [customer_acct_id]
						  FROM [RbrDb_267].[dbo].[CustomerAcct]
						  where name like 'z%'
						  ) 

DELETE FROM DialPeer
	WHERE customer_acct_id in (
						  SELECT [customer_acct_id]
						  FROM [RbrDb_267].[dbo].[CustomerAcct]
						  where name like 'z%'
						  ) 

DELETE FROM CustomerAcct
      WHERE customer_acct_id in (
								  SELECT [customer_acct_id]
								  FROM [RbrDb_267].[dbo].[CustomerAcct]
								  where name like 'z%'
								  ) 

--select * FROM WholesaleRateHistory
--      WHERE wholesale_route_id IN (
--			SELECT     WholesaleRateHistory.wholesale_route_id
--			FROM WholesaleRateHistory 
--			INNER JOIN WholesaleRoute ON WholesaleRateHistory.wholesale_route_id = WholesaleRoute.wholesale_route_id
--			WHERE  WholesaleRoute.service_id in (
--								  SELECT [service_id]
--								  FROM [RbrDb_267].[dbo].[Service]
--								  where name like 'z%'
--								  ) 
--)
--
--DELETE FROM WholesaleRoute
--      WHERE wholesale_route_id IN (
--			SELECT wholesale_route_id
--			FROM WholesaleRoute 
--			INNER JOIN Service ON WholesaleRoute.service_id = Service.service_id
--			WHERE     (Service.service_id = $(ID))
--)
--
--DELETE FROM Service
--      WHERE service_id in (
--						  SELECT [service_id]
--						  FROM [RbrDb_267].[dbo].[Service]
--						  where name like 'z%'
--						  ) 
--
