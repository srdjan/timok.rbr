/* MSSQL 2000 */ 
/* IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE object_id = OBJECT_ID(N'[dbo].[CDRView]')) */

/* MSSQL 2005 */ 
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[CDRView]'))
DROP VIEW [dbo].[CDRView]
 