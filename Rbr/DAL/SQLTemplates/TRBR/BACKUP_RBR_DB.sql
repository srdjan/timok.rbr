USE master

-- define db name
DECLARE @dbname varchar(500)
SET @dbname = 'RbrDb_$(DB_VERSION)'
DECLARE @datetime varchar(15)

-- START Prepare directories 
DECLARE @dbDir varchar(500)
SET @dbDir = 'C:\Timok\Rbr\SqlDb\'

DECLARE @dbBackupDir varchar(500)
SET @dbBackupDir = 'C:\Timok\Rbr\SqlDb\BACKUPS\'

-- get datetime for backup file name
SET @datetime = 
CAST(YEAR(GETDATE()) AS char(4)) + 
RIGHT('0' + RTRIM(CAST(MONTH(GETDATE()) AS char(2))), 2) + 
RIGHT('0' + RTRIM(CAST(DAY(GETDATE()) AS char(2))), 2) + 
'_' + 
RIGHT('0' + RTRIM(CAST(DATEPART(hh, GETDATE()) AS char(2))), 2) + 
RIGHT('0' + RTRIM(CAST(DATEPART(mi, GETDATE()) AS char(2))), 2) + 
RIGHT('0' + RTRIM(CAST(DATEPART(ss, GETDATE()) AS char(2))), 2) 

-- define backup device and backup file path
DECLARE @bakdevice varchar(1000), @bakfile varchar(254)
SET @bakdevice = @dbname + '_BACKUP_' + @datetime
SET @bakfile = @dbBackupDir + @bakdevice + '.bak'

IF EXISTS(SELECT * FROM master..sysdatabases WHERE [name]=@dbname)
BEGIN
 EXEC sp_addumpdevice 'disk', @bakdevice, @bakfile

 -- Back up the full database.
 BACKUP DATABASE @dbname TO @bakdevice

 EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = @dbname

 ALTER DATABASE [RbrDb_$(DB_VERSION)] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE

 DROP DATABASE [RbrDb_$(DB_VERSION)]
END

GO
