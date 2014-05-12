REM MAKE SURE SQL instance "(local)\TRBR" is intalled and running
echo on

SET cdrDbVersion=%1
SET rbrDbVersion=%2
SET dbDir=%3

SET dbNameSuffix=%cdrDbVersion%_Template

net start MSSQL$TRBR
sqlcmd /E /i DETACH_CDR_DB_TEMPLATE.sql /v DB_NAME_SUFFIX="%dbNameSuffix%" /S (local)\TRBR

del %dbDir%\CDRDb_%dbNameSuffix%_Data.MDF
del %dbDir%\CDRDb_%dbNameSuffix%_Log.LDF

sqlcmd /E /i CREATE_CDR_DB.sql /v DB_DIR="%dbDir%" /v DB_NAME_SUFFIX="%dbNameSuffix%" /S (local)\TRBR

sqlcmd /E /i CREATE_CDR_DB_FUNCTIONS.sql /v DB_NAME_SUFFIX="%dbNameSuffix%" /S (local)\TRBR

sqlcmd /E /i CREATE_CDR_DB_TABLES.sql /v DB_NAME_SUFFIX="%dbNameSuffix%" /S (local)\TRBR

sqlcmd /E /i CREATE_CDR_DB_VIEWS.sql /v DB_NAME_SUFFIX="%dbNameSuffix%" /v RBR_DB_VERSION="%rbrDbVersion%" /S (local)\TRBR

cmd

