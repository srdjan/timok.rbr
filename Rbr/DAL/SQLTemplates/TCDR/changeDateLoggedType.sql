/*
   Sunday, January 20, 200811:59:17 AM
   User: 
   Server: OGSTERDT\TRBR
   Database: CdrDb_269_200801
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CDR
	DROP CONSTRAINT R_1
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CDRIdentity', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CDRIdentity', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CDRIdentity', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CDR
	(
	date_logged datetime NOT NULL,
	timok_date int NOT NULL,
	start datetime NOT NULL,
	duration smallint NOT NULL,
	ccode int NOT NULL,
	local_number varchar(18) NOT NULL,
	carrier_route_id int NOT NULL,
	price decimal(9, 5) NOT NULL,
	cost decimal(9, 5) NOT NULL,
	orig_IP_address int NOT NULL,
	orig_end_point_id smallint NOT NULL,
	term_end_point_id smallint NOT NULL,
	customer_acct_id smallint NOT NULL,
	disconnect_cause smallint NOT NULL,
	disconnect_source tinyint NOT NULL,
	rbr_result smallint NOT NULL,
	prefix_in varchar(10) NOT NULL,
	prefix_out varchar(10) NOT NULL,
	DNIS bigint NOT NULL,
	ANI bigint NOT NULL,
	serial_number bigint NOT NULL,
	end_user_price decimal(6, 2) NOT NULL,
	used_bonus_minutes smallint NOT NULL,
	node_id smallint NOT NULL,
	customer_route_id int NOT NULL,
	mapped_disconnect_cause smallint NOT NULL,
	carrier_acct_id smallint NOT NULL,
	customer_duration smallint NOT NULL,
	retail_acct_id int NOT NULL,
	reseller_price decimal(9, 5) NOT NULL,
	carrier_duration smallint NOT NULL,
	retail_duration smallint NOT NULL,
	info_digits tinyint NOT NULL,
	id char(32) NULL
	)  ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.CDR)
	 EXEC('INSERT INTO dbo.Tmp_CDR (date_logged, timok_date, start, duration, ccode, local_number, carrier_route_id, price, cost, orig_IP_address, orig_end_point_id, term_end_point_id, customer_acct_id, disconnect_cause, disconnect_source, rbr_result, prefix_in, prefix_out, DNIS, ANI, serial_number, end_user_price, used_bonus_minutes, node_id, customer_route_id, mapped_disconnect_cause, carrier_acct_id, customer_duration, retail_acct_id, reseller_price, carrier_duration, retail_duration, info_digits, id)
		SELECT CONVERT(datetime, date_logged), timok_date, start, duration, ccode, local_number, carrier_route_id, price, cost, orig_IP_address, orig_end_point_id, term_end_point_id, customer_acct_id, disconnect_cause, disconnect_source, rbr_result, prefix_in, prefix_out, DNIS, ANI, serial_number, end_user_price, used_bonus_minutes, node_id, customer_route_id, mapped_disconnect_cause, carrier_acct_id, customer_duration, retail_acct_id, reseller_price, carrier_duration, retail_duration, info_digits, id FROM dbo.CDR WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.CDR
GO
EXECUTE sp_rename N'dbo.Tmp_CDR', N'CDR', 'OBJECT' 
GO
CREATE NONCLUSTERED INDEX XIECDR_Identity ON dbo.CDR
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX XIECDR_TimokDate_RetailAcct ON dbo.CDR
	(
	timok_date,
	retail_acct_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX XIECDR_TimokDate_OEP ON dbo.CDR
	(
	timok_date,
	orig_end_point_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX XIECDR_TimokDate_TEP ON dbo.CDR
	(
	timok_date,
	term_end_point_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX XIECDR_TimokDate_customer ON dbo.CDR
	(
	timok_date,
	customer_acct_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX XIECDR_Date_Logged ON dbo.CDR
	(
	date_logged
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX XIECDR_TimokDate_carrier ON dbo.CDR
	(
	timok_date,
	carrier_acct_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.CDR ADD CONSTRAINT
	R_1 FOREIGN KEY
	(
	id
	) REFERENCES dbo.CDRIdentity
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CDR', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CDR', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CDR', 'Object', 'CONTROL') as Contr_Per 