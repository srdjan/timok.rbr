echo on

mkdir "C:\Temp\DbBackup\%1\"

bcp "SELECT * FROM RbrDb_%1.dbo.CdrAggregate ORDER BY date_hour" 	queryout "C:\Temp\DbBackup\%1\CdrAggregate.txt" -c -S(local)\TRBR -T

cmd
