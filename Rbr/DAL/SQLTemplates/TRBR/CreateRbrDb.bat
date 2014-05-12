@ECHO OFF

SET dbVersion=%1
ECHO dbVersion

ECHO !!! MAKE SURE SQL instance "(local)\TRBR" is intalled and running
ECHO ---------------------------
ECHO REBUILD RBR Database BATCH
ECHO ---------------------------
ECHO !!! WARNING: This will delete ALL data from the current RBR Database !!!
ECHO (backup will be created first)
ECHO press:   
ECHO      Y - to proceed 
ECHO      N - to cancel   
ECHO      NOTE: (Y/N case sensitive)
CHOICE /c YN /cs /d N /t 20 /N 
IF ERRORLEVEL == 2 GOTO CANCEL

ECHO using SSE 2005 ENGINE (SQLCMD) ---------------------------
ECHO PREPARE BACKUP DIRECTORIES

SET dbDir=C:\Timok\Rbr\SqlDb\
if not exist %dbDir% mkdir %dbDir%

SET dbBackupDir=C:\Timok\Rbr\SqlDb\BACKUPS\
if not exist %dbBackupDir% mkdir %dbBackupDir%

ECHO START BACKUP...
sqlcmd /E /i BACKUP_RBR_DB.sql /v DB_VERSION="%dbVersion%" /S (local)\TRBR

ECHO START DB CREATION...
sqlcmd /E /i CREATE_RBR_DB.sql /v DB_VERSION="%dbVersion%" /S (local)\TRBR

REM ******* FOR DEV MACHINE ONLY ****************************************************
ECHO CREATING ASPNET DB USER...
SET COMP_NAME=%COMPUTERNAME%
ECHO %COMP_NAME%

sqlcmd /E /i ..\CREATE_ASPNET_DB_USER.sql /v DB_VERSION="%dbVersion%" /v COMP_NAME="%COMP_NAME%" /S (local)\TRBR

REM ******* FOR DEV MACHINE ONLY ****************************************************

ECHO LOADING INIT DATA...
set countryFilePath=%CD%\Country.txt
ECHO %countryFilePath%

set routeFilePath=%CD%\Route.txt
ECHO %routeFilePath%

set dialCodeFilePath=%CD%\DialCode.txt
ECHO %dialCodeFilePath%

sqlcmd /E /i LOAD_INIT_DATA.sql /v IMPORT_FILES_PATH="%CD%" /v DB_VERSION="%dbVersion%" /S (local)\TRBR
GOTO END 

:CANCEL
ECHO ***************************
ECHO BATCH CANCELLED !
ECHO ***************************

:END

cmd

