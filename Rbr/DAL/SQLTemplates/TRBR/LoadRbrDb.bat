@ECHO On
ECHO !!! MAKE SURE SQL instance "(local)\TRBR" is intalled and running
ECHO ***************************
ECHO RELOAD DATA for RBR Database BATCH
ECHO ---------------------------
ECHO !!! WARNING: this will delete ALL data from the current RBR Database !!!
ECHO press:   
ECHO      Y - to proceed 
ECHO      N - to cancel   
ECHO      NOTE: (Y/N case sensitive)
ECHO CHOICE /c YN /cs /d N /t 20 /N 
IF ERRORLEVEL == 2 GOTO CANCEL

SET DB_VERSION=%1
ECHO %DB_VERSION%

ECHO DELETING EXISTING DATA...
ECHO ---------------------------
sqlcmd /E /i DeleteData.sql /v DB_VERSION="%DB_VERSION%" /S (local)\TRBR

ECHO using SSE 2005 ENGINE (SQLCMD) ---------------------------
ECHO START LOADING DATA...

ECHO LOADING EXPORTED DATA...
ECHO ---------------------------
sqlcmd /E /i "LoadExportedRbrDb.sql" -S(local)\TRBR /v DB_VERSION="%DB_VERSION%"

:FINISHED
ECHO ---------------------------
ECHO BATCH FINISHED
ECHO ***************************
GOTO END

:CANCEL
ECHO ---------------------------
ECHO BATCH CANCELLED
ECHO ***************************

:END

cmd
