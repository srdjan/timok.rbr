USE RbrDb_266;
GO
-- Add filegroup
ALTER DATABASE RbrDb_266
ADD FILEGROUP CdrAggFg_266
GO

-- Add file to filegroup
ALTER DATABASE RbrDb_266
ADD FILE
(	
  NAME = N'CdrAggFg_266_Data',
  FILENAME = N'C:\Timok\Rbr\SqlDb\CdrAggFg_266_Data.NDF',
  SIZE = 64MB,
  FILEGROWTH = 64MB
) TO FILEGROUP CdrAggFg_266;
GO

-- Move CdrAggr table to the new filegroup
-- The base table is stored with the clustered index, so moving the clustered index moves the base table
CREATE CLUSTERED INDEX IE_clustered_date_hour ON CdrAggregate
(
  date_hour
)
WITH (DROP_EXISTING = ON)
ON CdrAggFg_266
GO

-- Move existing non-clustered indexes
CREATE INDEX IE_date_hour_node ON CdrAggregate
(
       date_hour,
       node_id
)
WITH (DROP_EXISTING = ON)
ON CdrAggFg_266
go

CREATE INDEX IE_date_hour_node_orig_ip ON CdrAggregate
(
       date_hour,
       node_id,
       orig_end_point_IP
)
WITH (DROP_EXISTING = ON)
ON CdrAggFg_266
go

CREATE INDEX IE_date_hour_node_term_ep_id ON CdrAggregate
(
       date_hour,
       node_id,
       term_end_point_id
)
WITH (DROP_EXISTING = ON)
ON CdrAggFg_266
go

CREATE INDEX IE_date_hour_node_cust_route ON CdrAggregate
(
       date_hour,
       node_id,
       customer_route_id
)
WITH (DROP_EXISTING = ON)
ON CdrAggFg_266
go

CREATE INDEX IE_date_hour_node_cust_acct ON CdrAggregate
(
       date_hour,
       node_id,
       customer_acct_id
)
WITH (DROP_EXISTING = ON)
ON CdrAggFg_266
go

CREATE INDEX IE_date_hour_node_orig_ep_id ON CdrAggregate
(
       date_hour,
       node_id,
       orig_end_point_id
)
WITH (DROP_EXISTING = ON)
ON CdrAggFg_266
go

CREATE INDEX IE_date_hour_node_carr_acct ON CdrAggregate
(
       date_hour,
       node_id,
       carrier_acct_id
)
WITH (DROP_EXISTING = ON)
ON CdrAggFg_266
go

CREATE INDEX IE_date_hour_node_access_number ON CdrAggregate
(
       date_hour,
       node_id,
       access_number
)
WITH (DROP_EXISTING = ON)
ON CdrAggFg_266
go

CREATE INDEX IE_date_hour_node_cust_acct_access_number ON CdrAggregate
(
       date_hour,
       node_id,
       customer_acct_id,
       access_number
)
WITH (DROP_EXISTING = ON)
ON CdrAggFg_266
go
