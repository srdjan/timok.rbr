BULK INSERT RbrDb_$(DB_VERSION).dbo.CdrAggregate   FROM 'C:\Temp\DbBackup\$(DB_VERSION)\CdrAggregate.txt' WITH (BATCHSIZE = 10000)
