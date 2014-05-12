@ECHO OFF

SET dbVersion=%1
ECHO dbVersion

ECHO !!! MAKE SURE SQL instance "(local)\TRBR" is intalled and running
ECHO ---------------------------

ECHO LOADING INIT DATA...
ECHO ---------------------------
set countryFilePath=%CD%\Country.txt
ECHO %countryFilePath%

set routeFilePath=%CD%\Route.txt
ECHO %routeFilePath%

set dialCodeFilePath=%CD%\DialCode.txt
ECHO %dialCodeFilePath%

sqlcmd /E /i LoadInitData.sql /v IMPORT_FILES_PATH="%CD%" /v DB_VERSION="%dbVersion%" /S (local)\TRBR
GOTO END 

:CANCEL
ECHO ***************************
ECHO BATCH CANCELLED !
ECHO ***************************

:END

cmd

