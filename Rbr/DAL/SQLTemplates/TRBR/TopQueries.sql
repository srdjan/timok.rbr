SELECT TOP 10
       total_logical_reads,
       total_logical_writes,
       execution_count,
       total_logical_reads+total_logical_writes AS [IO_total],
        st.text AS query_text,
       db_name(st.dbid) AS database_name,
       st.objectid AS object_id
FROM sys.dm_exec_query_stats  qs
CROSS APPLY sys.dm_exec_sql_text(sql_handle) st
WHERE total_logical_reads+total_logical_writes > 0
ORDER BY [IO_total] DESC