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

ALTER TABLE dbo.Country ADD calling_plan_id int NULL
GO

ALTER TABLE [dbo].[Country]  WITH NOCHECK ADD  CONSTRAINT [FK_Country_CallingPlan] FOREIGN KEY([calling_plan_id])
REFERENCES [dbo].[CallingPlan] ([calling_plan_id])
GO

COMMIT

