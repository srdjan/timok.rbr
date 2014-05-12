@ECHO On
SET DB_VERSION=269

ECHO LOADING EXPORTED DATA...
ECHO ---------------------------
sqlcmd /E /i "LoadCdrAgg.sql" -S(local)\TRBR /v DB_VERSION="%DB_VERSION%"

cmd
