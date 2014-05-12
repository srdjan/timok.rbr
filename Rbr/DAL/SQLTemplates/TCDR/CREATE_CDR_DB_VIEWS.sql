USE CdrDb_$(DB_NAME_SUFFIX)
GO

CREATE VIEW dbo.CDRView
AS
SELECT 
dbo.CDR.id, 
dbo.CDR.date_logged, 
dbo.CDR.timok_date, 
dbo.CDR.start, 
dbo.CDR.duration, 
dbo.CDR.ccode, 
dbo.CDR.local_number, 
dbo.CDR.carrier_route_id, 
dbo.CDR.price, 
dbo.CDR.cost, 
dbo.CDR.orig_IP_address, 
dbo.CDR.orig_end_point_id, 
dbo.CDR.term_end_point_id, 
dbo.CDR.customer_acct_id, 
dbo.CDR.disconnect_cause, 
dbo.CDR.disconnect_source, 
dbo.CDR.rbr_result, 
dbo.CDR.prefix_in, 
dbo.CDR.prefix_out, 
dbo.CDR.DNIS, 
dbo.CDR.ANI, 
dbo.CDR.info_digits, 
dbo.CDR.serial_number, 
dbo.CDR.end_user_price, 
dbo.CDR.used_bonus_minutes, 
dbo.CDR.reseller_price, 
dbo.CDR.node_id, 
dbo.CDR.customer_route_id, 
dbo.CDR.mapped_disconnect_cause, 
dbo.CDR.carrier_acct_id, 
dbo.IntToDottedIPAddress(dbo.CDR.orig_IP_address) AS orig_dot_IP_address, 

CASE WHEN dbo.CDR.ccode = 0 THEN '' 
ELSE CAST(dbo.CDR.ccode AS varchar) END + dbo.CDR.local_number AS dialed_number, 

dbo.CDR.retail_acct_id, 
dbo.CDR.customer_duration, 
dbo.CDR.carrier_duration, 
dbo.CDR.retail_duration, 


CAST(dbo.CDR.customer_duration / 6 + (CASE WHEN dbo.CDR.customer_duration % 6 > 0 THEN 1 ELSE 0 END) 
AS decimal(9, 1)) / 10 AS minutes, /* customer_minutes */

CAST(dbo.CDR.carrier_duration / 6 + (CASE WHEN dbo.CDR.carrier_duration % 6 > 0 THEN 1 ELSE 0 END) 
AS decimal(9, 1)) / 10 AS carrier_minutes, 

CAST(dbo.CDR.retail_duration / 6 + (CASE WHEN dbo.CDR.retail_duration % 6 > 0 THEN 1 ELSE 0 END) 
AS decimal(9, 1)) / 10 AS retail_minutes, 


COALESCE (CRR.name, 'UNKNOWN') AS carrier_route_name, 
COALESCE (CUR.name, 'UNKNOWN') AS customer_route_name, 
COALESCE (OEP.alias, 'UNKNOWN') AS orig_alias, 
COALESCE (TEP.alias, 'UNKNOWN') AS term_alias, 
COALESCE (TEP.ip_address_range, '0') AS term_ip_address_range, 
COALESCE (CU.name, 'UNKNOWN') AS customer_acct_name, 
COALESCE (CU.partner_id, 0) AS orig_partner_id, 
COALESCE (OPT.name, 'UNKNOWN') AS orig_partner_name, 
COALESCE (CA.name, 'UNKNOWN') AS carrier_acct_name, 
COALESCE (CA.partner_id, 0) AS term_partner_id, 
COALESCE (TPT.name, 'UNKNOWN') AS term_partner_name, 
COALESCE (ND.description, 'UNKNOWN') AS node_name

FROM  
RbrDb_$(RBR_DB_VERSION).dbo.Node AS ND 
RIGHT OUTER JOIN dbo.CDR ON ND.node_id = dbo.CDR.node_id 
LEFT OUTER JOIN 
RbrDb_$(RBR_DB_VERSION).dbo.CarrierAcct AS CA ON dbo.CDR.carrier_acct_id = CA.carrier_acct_id 
LEFT OUTER JOIN 
RbrDb_$(RBR_DB_VERSION).dbo.Partner AS TPT ON CA.partner_id = TPT.partner_id 
LEFT OUTER JOIN 
RbrDb_$(RBR_DB_VERSION).dbo.CustomerAcct AS CU ON dbo.CDR.customer_acct_id = CU.customer_acct_id 
LEFT OUTER JOIN 
RbrDb_$(RBR_DB_VERSION).dbo.Partner AS OPT ON CU.partner_id = OPT.partner_id 
LEFT OUTER JOIN 
RbrDb_$(RBR_DB_VERSION).dbo.EndPoint AS TEP ON dbo.CDR.term_end_point_id = TEP.end_point_id 
LEFT OUTER JOIN 
RbrDb_$(RBR_DB_VERSION).dbo.EndPoint AS OEP ON dbo.CDR.orig_end_point_id = OEP.end_point_id 
LEFT OUTER JOIN 
RbrDb_$(RBR_DB_VERSION).dbo.Route AS CRR ON dbo.CDR.carrier_route_id = CRR.route_id 
LEFT OUTER JOIN 
RbrDb_$(RBR_DB_VERSION).dbo.Route AS CUR ON dbo.CDR.customer_route_id = CUR.route_id
go



