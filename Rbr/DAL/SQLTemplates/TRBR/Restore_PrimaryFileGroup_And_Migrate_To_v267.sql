USE master;

IF EXISTS(SELECT * FROM sysdatabases WHERE name='RbrDb_266')  
ALTER DATABASE [RbrDb_266] SET RECOVERY FULL;

RESTORE DATABASE [RbrDb_266]
FILEGROUP = N'PRIMARY'
FROM DISK = N'C:\Temp\DbBackup\RbrDb_266.bak'
WITH REPLACE
GO

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT

use RbrDb_266;

BEGIN TRANSACTION
GO
ALTER TABLE dbo.CarrierAcct ADD
	max_call_length smallint default '10800' NOT NULL
GO
COMMIT

BEGIN TRANSACTION
GO
ALTER TABLE dbo.AccessNumberList ADD
	script_type int default '0' NOT NULL
GO
COMMIT
