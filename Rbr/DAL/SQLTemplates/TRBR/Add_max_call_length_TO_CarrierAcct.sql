/*
   Saturday, April 14, 20073:39:55 PM
   User: 
   Server: OGSTERDT\TRBR
   Database: RbrDb_266
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
ALTER TABLE dbo.CarrierAcct ADD
	max_call_length smallint default '10800' NOT NULL
GO
COMMIT
