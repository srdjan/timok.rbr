/*
   Monday, April 30, 200710:59:45 AM
   User: 
   Server: OGSTERDT\TRBR
   Database: CdrDb_266_200704
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
ALTER TABLE dbo.CDR ADD
	info_digits tinyint default 0 NOT NULL
GO
COMMIT
