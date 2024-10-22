SELECT [id], [date_logged], [timok_date], [start], [local_number], [prefix_in]
  FROM [CdrDb_269_200710].[dbo].[CDR]
where local_number like '%#%' 
and prefix_in =''
and start between '2007-10-19 00:00:00' and '2007-10-19 23:59:59'


SELECT [id], [date_logged], [timok_date], [start], [local_number], [prefix_in]
  FROM [CdrDb_269_200710].[dbo].[CDR]
where start between '2007-10-18 04:01:00' and '2007-10-18 04:15:00'




---- This would be the input parameter of the stored procedure, if you want to do it that way, or a UDF
--declare @string varchar(500)
--set @string = 'ABC,DEF,GHIJK,LMNOPQRS,T,UV,WXY,Z'
--
--
--declare @pos int
--declare @piece varchar(500)
--
---- Need to tack a delimiter onto the end of the input string if one doesn't exist
--if right(rtrim(@string),1) <> ','
-- set @string = @string  + ','
--
--set @pos =  patindex('%,%' , @string)
--while @pos <> 0
--begin
-- set @piece = left(@string, @pos - 1)
-- 
-- -- You have a piece of data, so insert it, print it, do whatever you want to with it.
-- print cast(@piece as varchar(500))
--
-- set @string = stuff(@string, 1, @pos, '')
-- set @pos =  patindex('%,%' , @string)
--end