SELECT 'ALTER INDEX [' + ix.name + '] ON [' + s.name + '].[' + t.name + '] ' +
       CASE WHEN ps.avg_fragmentation_in_percent > 40 THEN 'REBUILD' ELSE 'REORGANIZE' END +
       CASE WHEN pc.partition_count > 1 THEN ' PARTITION = ' + cast(ps.partition_number as nvarchar(max)) ELSE '' END
FROM   sys.indexes AS ix INNER JOIN sys.tables t
           ON t.object_id = ix.object_id
       INNER JOIN sys.schemas s
           ON t.schema_id = s.schema_id
       INNER JOIN (SELECT object_id, index_id, avg_fragmentation_in_percent, partition_number
                   FROM sys.dm_db_index_physical_stats (DB_ID(), NULL, NULL, NULL, NULL)) ps
           ON t.object_id = ps.object_id AND ix.index_id = ps.index_id
       INNER JOIN (SELECT object_id, index_id, COUNT(DISTINCT partition_number) AS partition_count
                   FROM sys.partitions
                   GROUP BY object_id, index_id) pc
           ON t.object_id = pc.object_id AND ix.index_id = pc.index_id
WHERE  ps.avg_fragmentation_in_percent > 10 AND
       ix.name IS NOT NULL