USE [master]
GO
/****** Object:  Database [RbrDb_269]    Script Date: 03/22/2009 02:47:20 ******/
CREATE DATABASE [RbrDb_269] ON  PRIMARY 
( NAME = N'RbrDb_269_Data', FILENAME = N'C:\Timok\Rbr\SqlDb\RbrDb_269_Data.MDF' , SIZE = 393216KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB ), 
 FILEGROUP [CdrAggFg_269] 
( NAME = N'CdrAggFg_269_Data', FILENAME = N'C:\Timok\Rbr\SqlDb\CdrAggFg_269_Data.NDF' , SIZE = 65536KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RbrDb_269_Log', FILENAME = N'C:\Timok\Rbr\SqlDb\RbrDb_269_Log.LDF' , SIZE = 1158528KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'RbrDb_269', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RbrDb_269].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RbrDb_269] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RbrDb_269] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RbrDb_269] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RbrDb_269] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RbrDb_269] SET ARITHABORT OFF 
GO
ALTER DATABASE [RbrDb_269] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RbrDb_269] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [RbrDb_269] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RbrDb_269] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RbrDb_269] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RbrDb_269] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RbrDb_269] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RbrDb_269] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RbrDb_269] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RbrDb_269] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RbrDb_269] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RbrDb_269] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RbrDb_269] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RbrDb_269] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RbrDb_269] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RbrDb_269] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RbrDb_269] SET  READ_WRITE 
GO
ALTER DATABASE [RbrDb_269] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RbrDb_269] SET  MULTI_USER 
GO
ALTER DATABASE [RbrDb_269] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RbrDb_269] SET DB_CHAINING OFF 
GO

--***********************
USE [RbrDb_269]
GO
/****** Object:  User [NT AUTHORITY\NETWORK SERVICE]    Script Date: 03/22/2009 02:49:30 ******/
--CREATE USER [NT AUTHORITY\NETWORK SERVICE] FOR LOGIN [NT AUTHORITY\NETWORK SERVICE] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  UserDefinedFunction [dbo].[JToDay]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--***************************************************************************************
CREATE FUNCTION [dbo].[JToDay]
 (
 @JulianDate int
 )
RETURNS int
AS
BEGIN
 RETURN (@JulianDate % 100000) / 100
END
GO
/****** Object:  UserDefinedFunction [dbo].[DateToJ]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--***************************************************************************************
CREATE FUNCTION [dbo].[DateToJ]
 (
 @dt datetime
 )
RETURNS int --yyyyjjjhh 200612517
BEGIN
 DECLARE @JDate int
 SET @JDate = DATEPART(year, @dt) * 100000 + DATEPART(dayofyear, @dt) * 100 + DATEPART(hour, @dt) 
 RETURN @JDate
END
GO
/****** Object:  UserDefinedFunction [dbo].[JToDate]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--***************************************************************************************
CREATE FUNCTION [dbo].[JToDate]
 (
 @JulianDate int
 )
RETURNS datetime
AS
BEGIN
 RETURN
 DateAdd(hh, @JulianDate % 100,
  DateAdd(d, dbo.JToDay(@JulianDate) - 1,
   '1/1/' + CAST(dbo.JToYear(@JulianDate) as varchar(10)))
  )
END
GO
/****** Object:  UserDefinedFunction [dbo].[JToHour]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--***************************************************************************************
CREATE FUNCTION [dbo].[JToHour]
 (
 @JulianDate int
 )
RETURNS int
AS
BEGIN
 RETURN @JulianDate % 100
END
GO
/****** Object:  UserDefinedFunction [dbo].[JToYear]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--***************************************************************************************
CREATE FUNCTION [dbo].[JToYear]
 (
 @JulianDate int
 )
RETURNS int
AS
BEGIN
 RETURN @JulianDate / 100000
END
GO
/****** Object:  Table [dbo].[CdrAggregate]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CdrAggregate](
	[date_hour] [int] NOT NULL,
	[node_id] [smallint] NOT NULL,
	[orig_end_point_IP] [int] NOT NULL,
	[orig_end_point_id] [smallint] NOT NULL,
	[customer_acct_id] [smallint] NOT NULL,
	[customer_route_id] [int] NOT NULL,
	[term_end_point_IP] [int] NOT NULL,
	[term_end_point_id] [smallint] NOT NULL,
	[calls_attempted] [int] NOT NULL,
	[calls_completed] [int] NOT NULL,
	[setup_seconds] [int] NOT NULL,
	[alert_seconds] [int] NOT NULL,
	[connected_seconds] [int] NOT NULL,
	[connected_minutes] [decimal](9, 1) NOT NULL,
	[carrier_cost] [decimal](9, 2) NOT NULL,
	[carrier_rounded_minutes] [decimal](9, 1) NOT NULL,
	[wholesale_price] [decimal](9, 2) NOT NULL,
	[wholesale_rounded_minutes] [decimal](9, 1) NOT NULL,
	[end_user_price] [decimal](9, 2) NOT NULL,
	[end_user_rounded_minutes] [decimal](9, 1) NOT NULL,
	[carrier_acct_id] [smallint] NOT NULL,
	[carrier_route_id] [int] NOT NULL,
	[access_number] [bigint] NOT NULL
) ON [CdrAggFg_269]
GO
/****** Object:  View [dbo].[TerminationRouteView]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TerminationRouteView]
AS
SELECT 
0 AS carrier_route_id, 
'' AS route_name, 
CAST(0 AS smallint) AS carrier_acct_id, 
'' AS carrier_acct_name, 
0 AS calling_plan_id, 
CAST(0 AS tinyint) AS rating_type, 
0 AS base_route_id,
CAST(0 AS tinyint) AS partial_coverage
GO
/****** Object:  Table [dbo].[BalanceAdjustmentReason]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BalanceAdjustmentReason](
	[balance_adjustment_reason_id] [int] NOT NULL,
	[description] [varchar](50) NOT NULL,
	[type] [tinyint] NOT NULL,
 CONSTRAINT [XPKBalanceAdjustmentReason] PRIMARY KEY NONCLUSTERED 
(
	[balance_adjustment_reason_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Box]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Box](
	[box_id] [int] NOT NULL,
	[date_created] [smalldatetime] NOT NULL,
	[date_activated] [smalldatetime] NULL,
 CONSTRAINT [XPKBox] PRIMARY KEY NONCLUSTERED 
(
	[box_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CdrExportMap]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CdrExportMap](
	[map_id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[delimiter] [tinyint] NOT NULL,
	[target_dest_folder] [varchar](500) NOT NULL,
 CONSTRAINT [XPKCdrExportMap] PRIMARY KEY NONCLUSTERED 
(
	[map_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContactInfo]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContactInfo](
	[contact_info_id] [int] NOT NULL,
	[address1] [varchar](50) NOT NULL,
	[address2] [varchar](50) NOT NULL,
	[city] [varchar](50) NOT NULL,
	[state] [varchar](50) NOT NULL,
	[zip_code] [varchar](12) NOT NULL,
	[email] [varchar](256) NOT NULL,
	[home_phone_number] [bigint] NOT NULL,
	[cell_phone_number] [bigint] NOT NULL,
	[work_phone_number] [bigint] NOT NULL,
 CONSTRAINT [PKContactInfo] PRIMARY KEY NONCLUSTERED 
(
	[contact_info_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Country]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Country](
	[country_id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[country_code] [int] NOT NULL,
	[status] [tinyint] NOT NULL,
	[version] [int] NOT NULL,
 CONSTRAINT [PKCountry] PRIMARY KEY NONCLUSTERED 
(
	[country_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InventoryHistory]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryHistory](
	[service_id] [smallint] NOT NULL,
	[batch_id] [int] NOT NULL,
	[timestamp] [datetime] NOT NULL,
	[command] [tinyint] NOT NULL,
	[number_of_cards] [int] NOT NULL,
	[denomination] [decimal](9, 2) NOT NULL,
	[customer_acct_id] [smallint] NULL,
	[reseller_partner_id] [int] NULL,
	[reseller_agent_id] [int] NULL,
	[person_id] [int] NOT NULL,
 CONSTRAINT [XPKInventoryHistory] PRIMARY KEY NONCLUSTERED 
(
	[service_id] ASC,
	[batch_id] ASC,
	[timestamp] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InventoryUsage]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryUsage](
	[service_id] [smallint] NOT NULL,
	[customer_acct_id] [smallint] NOT NULL,
	[timestamp] [smalldatetime] NOT NULL,
	[first_used] [int] NOT NULL,
	[total_used] [int] NOT NULL,
	[balance_depleted] [int] NOT NULL,
	[expired] [int] NOT NULL,
 CONSTRAINT [XPKInventoryUsage] PRIMARY KEY NONCLUSTERED 
(
	[service_id] ASC,
	[customer_acct_id] ASC,
	[timestamp] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayphoneSurcharge]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayphoneSurcharge](
	[payphone_surcharge_id] [int] NOT NULL,
	[surcharge] [decimal](9, 7) NOT NULL,
	[surcharge_type] [tinyint] NOT NULL,
 CONSTRAINT [XPKPayphoneSurcharge] PRIMARY KEY NONCLUSTERED 
(
	[payphone_surcharge_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Platform]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Platform](
	[platform_id] [smallint] NOT NULL,
	[location] [varchar](50) NOT NULL,
	[status] [tinyint] NOT NULL,
	[platform_config] [int] NOT NULL,
 CONSTRAINT [PKPlatform] PRIMARY KEY NONCLUSTERED 
(
	[platform_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrefixInType]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrefixInType](
	[prefix_in_type_id] [smallint] NOT NULL,
	[description] [varchar](50) NOT NULL,
	[length] [tinyint] NOT NULL,
	[delimiter] [tinyint] NOT NULL,
 CONSTRAINT [PKPrefixType] PRIMARY KEY NONCLUSTERED 
(
	[prefix_in_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RateInfo]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RateInfo](
	[rate_info_id] [int] NOT NULL,
 CONSTRAINT [XPKRateInfo] PRIMARY KEY NONCLUSTERED 
(
	[rate_info_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[schedule_id] [int] NOT NULL,
	[type] [tinyint] NOT NULL,
	[day_of_week] [smallint] NOT NULL,
	[day_of_the_month_1] [int] NOT NULL,
	[day_of_the_month_2] [int] NOT NULL,
 CONSTRAINT [XPKSchedule] PRIMARY KEY NONCLUSTERED 
(
	[schedule_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[ListOfDates]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--*************************************************************************
CREATE FUNCTION [dbo].[ListOfDates] ()  
RETURNS @DateListTab TABLE
(
 dateAlias varchar(20),
 dateDescr varchar(40)
)
AS  
BEGIN 
 INSERT INTO @DateListTab VALUES ('thishour', 'Current hour')
 INSERT INTO @DateListTab VALUES ('previoushour', 'Previous hour')
 --INSERT INTO @DateListTab VALUES ('twohoursago', 'Last 2 Hours')
 INSERT INTO @DateListTab VALUES ('today', 'Today')
 INSERT INTO @DateListTab VALUES ('last2days', 'Last 2 Days')
 INSERT INTO @DateListTab VALUES ('last3days', 'Last 3 Days')
 INSERT INTO @DateListTab VALUES ('thisweek', 'This Week')
 INSERT INTO @DateListTab VALUES ('last2weeks', 'Last 2 Weeks')
 INSERT INTO @DateListTab VALUES ('last3weeks', 'Last 3 Weeks')
 INSERT INTO @DateListTab VALUES ('thismonth', 'This Month')
 INSERT INTO @DateListTab VALUES ('last2months', 'Last 2 Months')
-- INSERT INTO @DateListTab VALUES ('twomonthsago', 'Last 3 Months')

 RETURN
END
GO
/****** Object:  Table [dbo].[TableSequence]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TableSequence](
	[TableName] [varchar](255) NOT NULL,
	[Value] [bigint] NOT NULL,
 CONSTRAINT [XPKTableSequence] PRIMARY KEY NONCLUSTERED 
(
	[TableName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TimeOfDayPolicy]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TimeOfDayPolicy](
	[time_of_day_policy] [tinyint] NOT NULL,
	[name] [varchar](20) NOT NULL,
 CONSTRAINT [XPKTimeOfDayPolicy] PRIMARY KEY NONCLUSTERED 
(
	[time_of_day_policy] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TypeOfDayChoice]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TypeOfDayChoice](
	[type_of_day_choice] [tinyint] NOT NULL,
	[name] [varchar](20) NOT NULL,
 CONSTRAINT [XPKTypeOfDayChoice] PRIMARY KEY NONCLUSTERED 
(
	[type_of_day_choice] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[IntToDottedIPAddress]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[IntToDottedIPAddress] 
(
 @IPAddressAsInt int
)

/**
--if you need to drop it
if exists (select 1
          from sysobjects
          where  id = object_id('dbo.IntToDottedIPAddress)
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.IntToDottedIPAddress
--go
*/
/**************************************************************************
DESCRIPTION: Returns dotted IPAddress

PARAMETERS:
  (@IPAddressAsInt int) - The int number containing a valid IP
  
RETURNS: IP converted to varchar dot-notation
  
USAGE:         SELECT  dbo.IntToDottedIPAddress(-2037012288)
***************************************************************************/

RETURNS varchar(20)
BEGIN

DECLARE 
 @biOctetA bigint,
 @biOctetB bigint,
 @biOctetC bigint,
 @biOctetD bigint,
 @bIp  bigint,
 @dottedIPAddress varchar(20)
        
SET @bIp = CONVERT(bigint, @IPAddressAsInt)

SET @biOctetD = (@bIp & 0x00000000FF000000) / 256 / 256 / 256
SET @biOctetC = (@bIp & 0x0000000000FF0000) / 256 / 256
SET @biOctetB = (@bIp & 0x000000000000FF00) / 256
SET @biOctetA = (@bIp & 0x00000000000000FF)
        
SET @dottedIPAddress =  
  CONVERT(varchar(4), @biOctetA) + '.' +
  CONVERT(varchar(4), @biOctetB) + '.' +
  CONVERT(varchar(4), @biOctetC) + '.' +
  CONVERT(varchar(4), @biOctetD)
                
RETURN @dottedIPAddress
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetCommandList]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetCommandList] ()  
RETURNS @CommandList TABLE
(
 command tinyint,
 description varchar(20)
)
AS  
BEGIN 

 INSERT INTO @CommandList VALUES (0, 'Generated')
 INSERT INTO @CommandList VALUES (1, 'Loaded')
 INSERT INTO @CommandList VALUES (2, 'Activated')
 INSERT INTO @CommandList VALUES (3, 'Deactivated')
 INSERT INTO @CommandList VALUES (4, 'Archived')

 RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetDateRange]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--***************************************************************************************
CREATE FUNCTION [dbo].[GetDateRange]
 (
 @date varchar(50) = 'today',
 @now datetime = GETDATE
 )
RETURNS datetime
AS
 /*
 @date
 - can be from a list of known relative dates (e.g. 'today', 'yesterday', ...)
 - can be a date in the form 'yyyy-mm-dd' (a CAST will be attempted)

 @now
 - the starting date from which to compute an offset
 - usually GetDate() is used
 */
 BEGIN
  DECLARE @ret datetime
  SET @ret = NULL

  IF @date IS NULL OR @now IS NULL
   RETURN @ret

  IF 'today' = @date
   SET @ret = @now
   
  ELSE IF 'thishour' = @date
   SET @ret = @now
  ELSE IF 'previoushour' = @date
   SET @ret = DateAdd(hh, -1, @now)

  ELSE IF 'last2days' = @date
   SET @ret = DateAdd(day, -1, @now)
  ELSE IF 'last3days' = @date
   SET @ret = DateAdd(day, -2, @now)
  ELSE IF 'thisweek' = @date
   SET @ret = dbo.F_START_OF_WEEK(@now, 2) -- 2 = Monday is the start of the week
  ELSE IF 'last2weeks' = @date
   SET @ret = dbo.F_START_OF_WEEK(DateAdd(day, -7, @now), 2) -- 2 = Monday is the start of the week
  ELSE IF 'last3weeks' = @date
   SET @ret = dbo.F_START_OF_WEEK(DateAdd(day, -14, @now), 2) -- 2 = Monday is the start of the week
  ELSE IF 'thismonth' = @date
   SET @ret = DATEADD(mm, DATEDIFF(mm, 0, @now), 0)
  ELSE IF 'last2months' = @date
   SET @ret = DATEADD(mm, DATEDIFF(mm, 0, @now) - 1, 0)
--  ELSE IF 'twomonthsago' = @date
--   SET @ret = DATEADD(mm, DATEDIFF(mm, 0, @now) - 2, 0)
--  ELSE IF 'thisyear' = @date
--   SET @ret = DATEADD(yy, DATEDIFF(yy, 0, @now), 0)
  -- TODO: need more here!!!
  ELSE
   SET @ret = @now --Cast(@Date AS datetime)
 RETURN @ret
END
GO
/****** Object:  UserDefinedFunction [dbo].[F_START_OF_WEEK]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--***************************************************************************************
CREATE FUNCTION [dbo].[F_START_OF_WEEK]
(
 @DATE   datetime,
 -- Sun = 1, Mon = 2, Tue = 3, Wed = 4
 -- Thu = 5, Fri = 6, Sat = 7
 -- Default to Sunday
 @WEEK_START_DAY  int = 1 
)
/*
Find the fisrt date on or before @DATE that matches 
day of week of @WEEK_START_DAY.
*/
returns  datetime
as
begin
declare  @START_OF_WEEK_DATE datetime
declare  @FIRST_BOW  datetime

-- Check for valid day of week
if @WEEK_START_DAY between 1 and 7
 begin
 -- Find first day on or after 1753/1/1 (-53690)
 -- matching day of week of @WEEK_START_DAY
 -- 1753/1/1 is earliest possible SQL Server date.
 select @FIRST_BOW = convert(datetime,-53690+((@WEEK_START_DAY+5) % 7))
 -- Verify beginning of week not before 1753/1/1
 if @DATE >= @FIRST_BOW
  begin
  select @START_OF_WEEK_DATE = 
  dateadd(dd,(datediff(dd,@FIRST_BOW,@DATE)/7)*7,@FIRST_BOW)
  end
 end

return @START_OF_WEEK_DATE

END
GO
/****** Object:  UserDefinedFunction [dbo].[F_DateTable]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--***************************************************************************************
CREATE FUNCTION [dbo].[F_DateTable](@startdate datetime, @enddate datetime)
RETURNS @retDateTable TABLE ([date] datetime, [julian] integer)
AS
BEGIN
 DECLARE @start INT, @end INT
 SET @start = dbo.DateToJ(@startdate)
 SET @end = dbo.DateToJ(@enddate)
 WHILE @start <= @end
 BEGIN 
  INSERT @retDateTable
  VALUES (dbo.JToDate(@start), @start) 
  SET @start = @start + 100
 END 
 RETURN
END
GO
/****** Object:  Table [dbo].[WholesaleRateHistory]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WholesaleRateHistory](
	[wholesale_route_id] [int] NOT NULL,
	[date_on] [smalldatetime] NOT NULL,
	[date_off] [smalldatetime] NOT NULL,
	[rate_info_id] [int] NOT NULL,
 CONSTRAINT [XPKWholesaleRateHistory] PRIMARY KEY NONCLUSTERED 
(
	[wholesale_route_id] ASC,
	[date_on] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResellRateHistory]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResellRateHistory](
	[resell_acct_id] [smallint] NOT NULL,
	[wholesale_route_id] [int] NOT NULL,
	[date_on] [smalldatetime] NOT NULL,
	[date_off] [smalldatetime] NOT NULL,
	[rate_info_id] [int] NOT NULL,
	[commision_type] [tinyint] NOT NULL,
	[markup_dollar] [decimal](11, 8) NOT NULL,
	[markup_percent] [decimal](11, 8) NOT NULL,
	[markup_per_call] [decimal](11, 8) NOT NULL,
	[markup_per_minute] [decimal](11, 8) NOT NULL,
 CONSTRAINT [PKResellerRateInfo] PRIMARY KEY NONCLUSTERED 
(
	[resell_acct_id] ASC,
	[wholesale_route_id] ASC,
	[date_on] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RetailAccountPayment]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RetailAccountPayment](
	[retail_acct_id] [int] NOT NULL,
	[date_time] [datetime] NOT NULL,
	[previous_amount] [decimal](9, 2) NOT NULL,
	[payment_amount] [decimal](9, 2) NOT NULL,
	[previous_bonus_minutes] [smallint] NOT NULL,
	[added_bonus_minutes] [smallint] NOT NULL,
	[comments] [varchar](max) NOT NULL,
	[person_id] [int] NOT NULL,
	[balance_adjustment_reason_id] [int] NOT NULL,
	[cdr_key] [varchar](128) NOT NULL,
 CONSTRAINT [XPKRetailAccountPayment] PRIMARY KEY NONCLUSTERED 
(
	[retail_acct_id] ASC,
	[date_time] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerAcctPayment]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerAcctPayment](
	[customer_acct_id] [smallint] NOT NULL,
	[date_time] [datetime] NOT NULL,
	[previous_amount] [decimal](18, 5) NOT NULL,
	[payment_amount] [decimal](9, 2) NOT NULL,
	[comments] [varchar](max) NOT NULL,
	[person_id] [int] NOT NULL,
	[balance_adjustment_reason_id] [int] NOT NULL,
 CONSTRAINT [XPKCustomerAcctPayment] PRIMARY KEY NONCLUSTERED 
(
	[customer_acct_id] ASC,
	[date_time] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Batch]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Batch](
	[batch_id] [int] NOT NULL,
	[status] [tinyint] NOT NULL,
	[first_serial] [bigint] NOT NULL,
	[last_serial] [bigint] NOT NULL,
	[request_id] [int] NOT NULL,
	[box_id] [int] NULL,
	[customer_acct_id] [smallint] NULL,
 CONSTRAINT [XPKBatch] PRIMARY KEY NONCLUSTERED 
(
	[batch_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarrierAcct]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CarrierAcct](
	[carrier_acct_id] [smallint] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[status] [tinyint] NOT NULL,
	[rating_type] [tinyint] NOT NULL,
	[prefix_out] [varchar](10) NOT NULL,
	[strip_1plus] [smallint] NOT NULL,
	[intl_dial_code] [varchar](5) NOT NULL,
	[partner_id] [int] NOT NULL,
	[calling_plan_id] [int] NOT NULL,
	[max_call_length] [smallint] NOT NULL,
 CONSTRAINT [PKCarrierAcct] PRIMARY KEY CLUSTERED 
(
	[carrier_acct_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerAcct]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerAcct](
	[customer_acct_id] [smallint] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[status] [tinyint] NOT NULL,
	[default_bonus_minutes_type] [tinyint] NOT NULL,
	[default_start_bonus_minutes] [smallint] NOT NULL,
	[is_prepaid] [tinyint] NOT NULL,
	[current_amount] [decimal](18, 5) NOT NULL,
	[limit_amount] [decimal](18, 5) NOT NULL,
	[warning_amount] [decimal](18, 5) NOT NULL,
	[allow_rerouting] [tinyint] NOT NULL,
	[concurrent_use] [tinyint] NOT NULL,
	[prefix_in_type_id] [smallint] NOT NULL,
	[prefix_in] [varchar](10) NOT NULL,
	[prefix_out] [varchar](10) NOT NULL,
	[partner_id] [int] NOT NULL,
	[service_id] [smallint] NOT NULL,
	[retail_calling_plan_id] [int] NULL,
	[retail_markup_type] [tinyint] NOT NULL,
	[retail_markup_dollar] [decimal](11, 8) NOT NULL,
	[retail_markup_percent] [decimal](11, 8) NOT NULL,
	[max_call_length] [smallint] NOT NULL,
	[routing_plan_id] [int] NOT NULL,
 CONSTRAINT [PKCustomerAcct] PRIMARY KEY NONCLUSTERED 
(
	[customer_acct_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Route]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Route](
	[route_id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[status] [tinyint] NOT NULL,
	[calling_plan_id] [int] NOT NULL,
	[country_id] [int] NOT NULL,
	[version] [int] NOT NULL,
	[routing_number] [int] NULL,
 CONSTRAINT [PKRoute] PRIMARY KEY NONCLUSTERED 
(
	[route_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoutingPlan]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoutingPlan](
	[routing_plan_id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[calling_plan_id] [int] NOT NULL,
	[virtual_switch_id] [int] NOT NULL,
 CONSTRAINT [XPKRoutingPlan] PRIMARY KEY NONCLUSTERED 
(
	[routing_plan_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DialCode]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DialCode](
	[calling_plan_id] [int] NOT NULL,
	[dial_code] [bigint] NOT NULL,
	[route_id] [int] NOT NULL,
	[version] [int] NOT NULL,
 CONSTRAINT [PKDialCode] PRIMARY KEY NONCLUSTERED 
(
	[calling_plan_id] ASC,
	[dial_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Service](
	[service_id] [smallint] NOT NULL,
	[name] [varchar](64) NOT NULL,
	[virtual_switch_id] [int] NOT NULL,
	[calling_plan_id] [int] NOT NULL,
	[default_routing_plan_id] [int] NOT NULL,
	[status] [tinyint] NOT NULL,
	[type] [tinyint] NOT NULL,
	[retail_type] [int] NOT NULL,
	[is_shared] [tinyint] NOT NULL,
	[rating_type] [tinyint] NOT NULL,
	[pin_length] [int] NOT NULL,
	[payphone_surcharge_id] [int] NULL,
	[sweep_schedule_id] [int] NULL,
	[sweep_fee] [decimal](9, 7) NOT NULL,
	[sweep_rule] [int] NOT NULL,
	[balance_prompt_type] [tinyint] NOT NULL,
	[balance_prompt_per_unit] [decimal](9, 7) NOT NULL,
 CONSTRAINT [XPKService] PRIMARY KEY NONCLUSTERED 
(
	[service_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LCRBlackList]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LCRBlackList](
	[routing_plan_id] [int] NOT NULL,
	[route_id] [int] NOT NULL,
	[carrier_acct_id] [smallint] NOT NULL,
	[version] [int] NOT NULL,
 CONSTRAINT [XPKLCRBlackList] PRIMARY KEY NONCLUSTERED 
(
	[routing_plan_id] ASC,
	[route_id] ASC,
	[carrier_acct_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarrierRoute]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarrierRoute](
	[carrier_route_id] [int] NOT NULL,
	[carrier_acct_id] [smallint] NOT NULL,
	[route_id] [int] NULL,
	[status] [tinyint] NOT NULL,
	[asr_time_window] [int] NOT NULL,
	[asr_target] [smallint] NOT NULL,
	[acd_time_window] [int] NOT NULL,
	[acd_target] [smallint] NOT NULL,
	[next_ep] [tinyint] NOT NULL,
 CONSTRAINT [XPKCarrierRoute] PRIMARY KEY NONCLUSTERED 
(
	[carrier_route_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarrierAcctEPMap]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarrierAcctEPMap](
	[carrier_acct_EP_map_id] [int] NOT NULL,
	[carrier_route_id] [int] NOT NULL,
	[end_point_id] [smallint] NOT NULL,
	[priority] [tinyint] NOT NULL,
	[carrier_acct_id] [smallint] NOT NULL,
 CONSTRAINT [PKCarrierAcctEPMap] PRIMARY KEY NONCLUSTERED 
(
	[carrier_acct_EP_map_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarrierRateHistory]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarrierRateHistory](
	[carrier_route_id] [int] NOT NULL,
	[date_on] [smalldatetime] NOT NULL,
	[date_off] [smalldatetime] NOT NULL,
	[rate_info_id] [int] NOT NULL,
 CONSTRAINT [XPKCarrierRateHistory] PRIMARY KEY NONCLUSTERED 
(
	[carrier_route_id] ASC,
	[date_on] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OutboundANI]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OutboundANI](
	[outbound_ani_id] [int] NOT NULL,
	[ANI] [bigint] NOT NULL,
	[carrier_route_id] [int] NOT NULL,
	[version] [int] NOT NULL,
 CONSTRAINT [PKOutboundANI] PRIMARY KEY NONCLUSTERED 
(
	[outbound_ani_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TerminationChoice]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TerminationChoice](
	[termination_choice_id] [int] NOT NULL,
	[routing_plan_id] [int] NOT NULL,
	[route_id] [int] NOT NULL,
	[priority] [tinyint] NOT NULL,
	[carrier_route_id] [int] NOT NULL,
	[version] [int] NOT NULL,
 CONSTRAINT [PKTerminationChoice] PRIMARY KEY CLUSTERED 
(
	[termination_choice_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CdrExportMapDetail]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CdrExportMapDetail](
	[map_detail_id] [int] NOT NULL,
	[map_id] [int] NOT NULL,
	[sequence] [int] NOT NULL,
	[field_name] [varchar](500) NOT NULL,
	[format_type] [varchar](500) NULL,
 CONSTRAINT [XPKCdrExportMapDetail] PRIMARY KEY NONCLUSTERED 
(
	[map_detail_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Partner]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Partner](
	[partner_id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[status] [tinyint] NOT NULL,
	[contact_info_id] [int] NOT NULL,
	[billing_schedule_id] [int] NULL,
	[virtual_switch_id] [int] NOT NULL,
 CONSTRAINT [PKPartner] PRIMARY KEY NONCLUSTERED 
(
	[partner_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Person]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Person](
	[person_id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[login] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[permission] [tinyint] NOT NULL,
	[is_reseller] [tinyint] NOT NULL,
	[status] [tinyint] NOT NULL,
	[registration_status] [tinyint] NOT NULL,
	[salt] [varchar](50) NOT NULL,
	[partner_id] [int] NULL,
	[retail_acct_id] [int] NULL,
	[group_id] [int] NULL,
	[virtual_switch_id] [int] NULL,
	[contact_info_id] [int] NULL,
 CONSTRAINT [PKPerson] PRIMARY KEY NONCLUSTERED 
(
	[person_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerSupportVendor]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerSupportVendor](
	[vendor_id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[contact_info_id] [int] NOT NULL,
 CONSTRAINT [XPKCustomerSupportVendor] PRIMARY KEY NONCLUSTERED 
(
	[vendor_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VirtualSwitch]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VirtualSwitch](
	[virtual_switch_id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[status] [tinyint] NOT NULL,
	[contact_info_id] [int] NOT NULL,
 CONSTRAINT [XPKVirtualSwitch] PRIMARY KEY NONCLUSTERED 
(
	[virtual_switch_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RetailRoute]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RetailRoute](
	[retail_route_id] [int] NOT NULL,
	[customer_acct_id] [smallint] NOT NULL,
	[route_id] [int] NULL,
	[status] [tinyint] NOT NULL,
	[start_bonus_minutes] [smallint] NOT NULL,
	[bonus_minutes_type] [tinyint] NOT NULL,
	[multiplier] [smallint] NOT NULL,
 CONSTRAINT [XPKRetailRoute] PRIMARY KEY NONCLUSTERED 
(
	[retail_route_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RetailAccount]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RetailAccount](
	[retail_acct_id] [int] NOT NULL,
	[customer_acct_id] [smallint] NOT NULL,
	[date_created] [smalldatetime] NOT NULL,
	[date_active] [smalldatetime] NOT NULL,
	[date_to_expire] [smalldatetime] NOT NULL,
	[date_expired] [smalldatetime] NOT NULL,
	[status] [tinyint] NOT NULL,
	[start_balance] [decimal](9, 2) NOT NULL,
	[current_balance] [decimal](9, 2) NOT NULL,
	[start_bonus_minutes] [smallint] NOT NULL,
	[current_bonus_minutes] [smallint] NOT NULL,
 CONSTRAINT [XPKRetailAccount] PRIMARY KEY NONCLUSTERED 
(
	[retail_acct_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResellAcct]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResellAcct](
	[resell_acct_id] [smallint] NOT NULL,
	[partner_id] [int] NOT NULL,
	[customer_acct_id] [smallint] NOT NULL,
	[person_id] [int] NOT NULL,
	[per_route] [tinyint] NOT NULL,
	[commision_type] [tinyint] NOT NULL,
	[markup_dollar] [decimal](11, 8) NOT NULL,
	[markup_percent] [decimal](11, 8) NOT NULL,
	[fee_per_call] [decimal](11, 8) NOT NULL,
	[fee_per_minute] [decimal](11, 8) NOT NULL,
 CONSTRAINT [PKReseller] PRIMARY KEY NONCLUSTERED 
(
	[resell_acct_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoadBalancingMap]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoadBalancingMap](
	[node_id] [smallint] NOT NULL,
	[customer_acct_id] [smallint] NOT NULL,
	[max_calls] [int] NOT NULL,
	[current_calls] [int] NOT NULL,
 CONSTRAINT [PKLoadBalancingMap] PRIMARY KEY NONCLUSTERED 
(
	[node_id] ASC,
	[customer_acct_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerAcctSupportMap]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerAcctSupportMap](
	[customer_acct_id] [smallint] NOT NULL,
	[vendor_id] [int] NOT NULL,
 CONSTRAINT [XPKCustomerAcctSupportMap] PRIMARY KEY NONCLUSTERED 
(
	[customer_acct_id] ASC,
	[vendor_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DialPeer]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DialPeer](
	[end_point_id] [smallint] NOT NULL,
	[prefix_in] [varchar](10) NOT NULL,
	[customer_acct_id] [smallint] NOT NULL,
 CONSTRAINT [PKDialPeer] PRIMARY KEY CLUSTERED 
(
	[end_point_id] ASC,
	[prefix_in] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AccessNumberList]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessNumberList](
	[access_number] [bigint] NOT NULL,
	[service_id] [smallint] NULL,
	[language] [tinyint] NOT NULL,
	[surcharge] [decimal](9, 7) NOT NULL,
	[surcharge_type] [tinyint] NOT NULL,
	[customer_acct_id] [smallint] NULL,
	[script_type] [int] NOT NULL,
 CONSTRAINT [XPKAccessNumberList] PRIMARY KEY NONCLUSTERED 
(
	[access_number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerSupportGroup]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerSupportGroup](
	[group_id] [int] NOT NULL,
	[description] [varchar](128) NOT NULL,
	[role] [int] NOT NULL,
	[max_amount] [decimal](9, 2) NOT NULL,
	[allow_status_change] [tinyint] NOT NULL,
	[vendor_id] [int] NOT NULL,
 CONSTRAINT [XPKCustomerSupportGroup] PRIMARY KEY NONCLUSTERED 
(
	[group_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IPAddress]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IPAddress](
	[IP_address] [int] NOT NULL,
	[end_point_id] [smallint] NOT NULL,
 CONSTRAINT [PKOrigEP] PRIMARY KEY NONCLUSTERED 
(
	[IP_address] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GenerationRequest]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenerationRequest](
	[request_id] [int] NOT NULL,
	[date_requested] [datetime] NOT NULL,
	[date_to_process] [smalldatetime] NOT NULL,
	[date_completed] [smalldatetime] NULL,
	[number_of_batches] [int] NOT NULL,
	[batch_size] [int] NOT NULL,
	[lot_id] [int] NOT NULL,
 CONSTRAINT [XPKGenerationRequest] PRIMARY KEY NONCLUSTERED 
(
	[request_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Node]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Node](
	[node_id] [smallint] NOT NULL,
	[platform_id] [smallint] NOT NULL,
	[description] [varchar](50) NOT NULL,
	[node_config] [int] NOT NULL,
	[transport_type] [tinyint] NOT NULL,
	[user_name] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[ip_address] [int] NOT NULL,
	[port] [int] NOT NULL,
	[status] [tinyint] NOT NULL,
	[billing_export_frequency] [int] NOT NULL,
	[cdr_publishing_frequency] [int] NOT NULL,
 CONSTRAINT [PKNode] PRIMARY KEY NONCLUSTERED 
(
	[node_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EndPoint]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EndPoint](
	[end_point_id] [smallint] NOT NULL,
	[alias] [varchar](50) NOT NULL,
	[with_alias_authentication] [tinyint] NOT NULL,
	[status] [tinyint] NOT NULL,
	[type] [tinyint] NOT NULL,
	[protocol] [tinyint] NOT NULL,
	[port] [int] NOT NULL,
	[registration] [tinyint] NOT NULL,
	[is_registered] [tinyint] NOT NULL,
	[ip_address_range] [varchar](19) NOT NULL,
	[max_calls] [int] NOT NULL,
	[password] [varchar](16) NOT NULL,
	[prefix_in_type_id] [smallint] NOT NULL,
	[virtual_switch_id] [int] NOT NULL,
 CONSTRAINT [PKOrigTG] PRIMARY KEY CLUSTERED 
(
	[end_point_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HolidayCalendar]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HolidayCalendar](
	[rate_info_id] [int] NOT NULL,
	[holiday_day] [smalldatetime] NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [XPKHolidayCalendar] PRIMARY KEY NONCLUSTERED 
(
	[rate_info_id] ASC,
	[holiday_day] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RetailRateHistory]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RetailRateHistory](
	[retail_route_id] [int] NOT NULL,
	[date_on] [smalldatetime] NOT NULL,
	[date_off] [smalldatetime] NOT NULL,
	[rate_info_id] [int] NOT NULL,
	[connect_fee] [decimal](9, 7) NOT NULL,
	[disconnect_fee] [decimal](9, 7) NOT NULL,
	[per_call_cost] [decimal](9, 7) NOT NULL,
	[cost_increase_per_call] [int] NOT NULL,
	[cost_increase_per_call_start] [int] NOT NULL,
	[cost_increase_per_call_stop] [int] NOT NULL,
	[tax_first_incr_cost] [decimal](9, 7) NOT NULL,
	[tax_add_incr_cost] [decimal](9, 7) NOT NULL,
	[surcharge_delay] [tinyint] NOT NULL,
	[rating_delay] [tinyint] NOT NULL,
 CONSTRAINT [XPKRetailRateHistory] PRIMARY KEY NONCLUSTERED 
(
	[retail_route_id] ASC,
	[date_on] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeOfDay]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfDay](
	[rate_info_id] [int] NOT NULL,
	[type_of_day_choice] [tinyint] NOT NULL,
	[time_of_day_policy] [tinyint] NOT NULL,
 CONSTRAINT [XPKTypeOfDay] PRIMARY KEY NONCLUSTERED 
(
	[rate_info_id] ASC,
	[type_of_day_choice] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResidentialPSTN]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResidentialPSTN](
	[service_id] [smallint] NOT NULL,
	[ANI] [bigint] NOT NULL,
	[status] [tinyint] NOT NULL,
	[date_first_used] [smalldatetime] NULL,
	[date_last_used] [smalldatetime] NULL,
	[retail_acct_id] [int] NOT NULL,
 CONSTRAINT [XPKResidentialPSTN] PRIMARY KEY NONCLUSTERED 
(
	[service_id] ASC,
	[ANI] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RetailRouteBonusMinutes]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RetailRouteBonusMinutes](
	[retail_acct_id] [int] NOT NULL,
	[retail_route_id] [int] NOT NULL,
	[start_bonus_minutes] [smallint] NOT NULL,
	[current_bonus_minutes] [smallint] NOT NULL,
 CONSTRAINT [XPKRetailRouteBonusMinutes] PRIMARY KEY NONCLUSTERED 
(
	[retail_acct_id] ASC,
	[retail_route_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhoneCard]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneCard](
	[service_id] [smallint] NOT NULL,
	[pin] [bigint] NOT NULL,
	[retail_acct_id] [int] NOT NULL,
	[serial_number] [bigint] NOT NULL,
	[status] [tinyint] NOT NULL,
	[inventory_status] [tinyint] NOT NULL,
	[date_loaded] [smalldatetime] NOT NULL,
	[date_to_expire] [smalldatetime] NOT NULL,
	[date_active] [smalldatetime] NULL,
	[date_first_used] [smalldatetime] NULL,
	[date_last_used] [smalldatetime] NULL,
	[date_deactivated] [smalldatetime] NULL,
	[date_archived] [smalldatetime] NULL,
 CONSTRAINT [XPKPhoneCard] PRIMARY KEY NONCLUSTERED 
(
	[service_id] ASC,
	[pin] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResidentialVoIP]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ResidentialVoIP](
	[user_id] [varchar](50) NOT NULL,
	[status] [tinyint] NOT NULL,
	[date_first_used] [smalldatetime] NULL,
	[date_last_used] [smalldatetime] NULL,
	[password] [varchar](16) NOT NULL,
	[allow_inbound_calls] [tinyint] NOT NULL,
	[service_id] [smallint] NULL,
	[retail_acct_id] [int] NOT NULL,
 CONSTRAINT [XPKResidentialVoIP] PRIMARY KEY NONCLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CallCenterCabina]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CallCenterCabina](
	[service_id] [smallint] NOT NULL,
	[serial_number] [bigint] NOT NULL,
	[cabina_label] [varchar](16) NOT NULL,
	[status] [tinyint] NOT NULL,
	[date_first_used] [smalldatetime] NULL,
	[date_last_used] [smalldatetime] NULL,
	[retail_acct_id] [int] NOT NULL,
 CONSTRAINT [XPKCallCenterCabina] PRIMARY KEY NONCLUSTERED 
(
	[service_id] ASC,
	[serial_number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoutingPlanDetail]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoutingPlanDetail](
	[routing_plan_id] [int] NOT NULL,
	[route_id] [int] NOT NULL,
	[routing_algorithm] [tinyint] NOT NULL,
 CONSTRAINT [XPKRoutingPlanDetail] PRIMARY KEY NONCLUSTERED 
(
	[routing_plan_id] ASC,
	[route_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WholesaleRoute]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WholesaleRoute](
	[wholesale_route_id] [int] NOT NULL,
	[service_id] [smallint] NOT NULL,
	[route_id] [int] NULL,
	[status] [tinyint] NOT NULL,
 CONSTRAINT [XPKWholesaleRoute] PRIMARY KEY NONCLUSTERED 
(
	[wholesale_route_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InventoryLot]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryLot](
	[lot_id] [int] NOT NULL,
	[service_id] [smallint] NOT NULL,
	[denomination] [decimal](9, 2) NOT NULL,
 CONSTRAINT [XPKInventoryLot] PRIMARY KEY NONCLUSTERED 
(
	[lot_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rate]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rate](
	[rate_info_id] [int] NOT NULL,
	[type_of_day_choice] [tinyint] NOT NULL,
	[time_of_day] [tinyint] NOT NULL,
	[first_incr_length] [tinyint] NOT NULL,
	[add_incr_length] [tinyint] NOT NULL,
	[first_incr_cost] [decimal](9, 7) NOT NULL,
	[add_incr_cost] [decimal](9, 7) NOT NULL,
 CONSTRAINT [XPKRate] PRIMARY KEY NONCLUSTERED 
(
	[rate_info_id] ASC,
	[type_of_day_choice] ASC,
	[time_of_day] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeOfDayPeriod]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeOfDayPeriod](
	[rate_info_id] [int] NOT NULL,
	[type_of_day_choice] [tinyint] NOT NULL,
	[start_hour] [smallint] NOT NULL,
	[stop_hour] [smallint] NOT NULL,
	[time_of_day] [tinyint] NOT NULL,
 CONSTRAINT [XPKTimeOfDayPeriod] PRIMARY KEY NONCLUSTERED 
(
	[rate_info_id] ASC,
	[type_of_day_choice] ASC,
	[start_hour] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeOfDay]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TimeOfDay](
	[time_of_day] [tinyint] NOT NULL,
	[name] [varchar](32) NOT NULL,
	[time_of_day_policy] [tinyint] NOT NULL,
 CONSTRAINT [XPKTimeOfDay] PRIMARY KEY NONCLUSTERED 
(
	[time_of_day] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CallingPlan]    Script Date: 03/22/2009 02:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CallingPlan](
	[calling_plan_id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[virtual_switch_id] [int] NOT NULL,
	[version] [int] NOT NULL,
 CONSTRAINT [XPKCallingPlan] PRIMARY KEY NONCLUSTERED 
(
	[calling_plan_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[GetJDateRange]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--***************************************************************************************
CREATE PROCEDURE [dbo].[GetJDateRange]
( 
 @DateName VARCHAR(50) = 'thishour', 
 @StartJDateHour INT OUTPUT,
 @EndJDateHour INT OUTPUT

)
AS
BEGIN
 SET NOCOUNT ON;

 IF @DateName IS NULL
  SET @DateName = 'thishour'

 IF @DateName = 'thishour'
  SET @StartJDateHour = dbo.DateToJ(dbo.GetDateRange(@DateName, GETDATE()))     --YYYYJJJHH
 ELSE IF @DateName = 'previoushour'
  SET @StartJDateHour = dbo.DateToJ(dbo.GetDateRange(@DateName, GETDATE()))     --YYYYJJJHH
 ELSE
  SET @StartJDateHour = dbo.DateToJ(dbo.GetDateRange(@DateName, GETDATE())) / 100 * 100 --YYYYJJJ00

 IF @DateName = 'previoushour'
  SET @EndJDateHour = @StartJDateHour   --YYYYJJJHH
 ELSE
  SET @EndJDateHour = dbo.DateToJ(GETDATE()) --YYYYJJJHH


 --SELECT @StartJDateHour, @EndJDateHour
END
GO
/****** Object:  View [dbo].[AggrBase]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AggrBase] 
AS
SELECT     TOP 100 PERCENT 

node_id, 
customer_acct_id, 
date_hour,
(date_hour / 100) as day,
customer_route_id,
term_end_point_IP,
calls_attempted,
calls_completed,
setup_seconds,
connected_minutes,
carrier_cost,
term_end_point_id,
carrier_acct_id 

FROM dbo.CdrAggregate
GO
/****** Object:  View [dbo].[OutDialPeerView]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[OutDialPeerView] 
AS
SELECT     
dbo.CarrierAcctEPMap.end_point_id, 
dbo.CarrierAcctEPMap.carrier_acct_id, 
dbo.EndPoint.alias, 
dbo.CarrierAcct.name AS carrier_acct_name, 
dbo.CarrierAcct.status AS carrier_acct_status, 
dbo.CarrierAcct.prefix_out, 
dbo.CarrierAcct.partner_id, 
dbo.Partner.name AS partner_name, 
dbo.Partner.status AS partner_status 

FROM  CarrierAcctEPMap INNER JOIN
CarrierAcct ON 
CarrierAcctEPMap.carrier_acct_id = CarrierAcct.carrier_acct_id 
INNER JOIN EndPoint ON 
CarrierAcctEPMap.end_point_id = EndPoint.end_point_id 
INNER JOIN Partner ON CarrierAcct.partner_id = Partner.partner_id

GROUP BY 
dbo.CarrierAcctEPMap.end_point_id, 
dbo.CarrierAcctEPMap.carrier_acct_id, 
dbo.EndPoint.alias, 
dbo.CarrierAcct.name, 
dbo.CarrierAcct.status, 
dbo.CarrierAcct.prefix_out, 
dbo.CarrierAcct.partner_id, 
dbo.Partner.name, 
dbo.Partner.status
GO
/****** Object:  UserDefinedFunction [dbo].[ListOfCarriers]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ListOfCarriers]()  
RETURNS @CarrierListTab TABLE
(
 carrierID smallint,
 carrierDescr varchar(50)
)
AS  
BEGIN 
 INSERT @CarrierListTab
 SELECT NULL [carrier_acct_id], ' All Carriers' [name]
 UNION
 SELECT [carrier_acct_id], [name]
 FROM CarrierAcct AS [carrier]
 ORDER BY [name]

 RETURN
END
GO
/****** Object:  View [dbo].[DialPeerView]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DialPeerView] 
AS 
SELECT     
dbo.DialPeer.end_point_id, 
dbo.DialPeer.prefix_in, 
dbo.DialPeer.customer_acct_id, 
dbo.EndPoint.alias, 
dbo.PrefixInType.description AS prefix_type_descr, 
dbo.PrefixInType.length AS prefix_type_length, 
dbo.PrefixInType.delimiter AS prefix_type_delimiter, 
dbo.CustomerAcct.name AS customer_acct_name, 
dbo.CustomerAcct.service_id, 
dbo.CustomerAcct.status AS customer_acct_status, 
dbo.CustomerAcct.prefix_out, 
dbo.CustomerAcct.partner_id, 
dbo.CustomerAcct.allow_rerouting, 
dbo.Service.name AS service_name, 
dbo.Service.type AS service_type, 
dbo.Service.retail_type AS service_retail_type, 
dbo.Partner.name AS partner_name, 
dbo.Partner.status AS partner_status 

FROM dbo.DialPeer 
LEFT OUTER JOIN dbo.Service 
INNER JOIN dbo.CustomerAcct ON 
dbo.Service.service_id = dbo.CustomerAcct.service_id 
INNER JOIN dbo.Partner ON 
dbo.CustomerAcct.partner_id = dbo.Partner.partner_id 
ON dbo.DialPeer.customer_acct_id = dbo.CustomerAcct.customer_acct_id 
LEFT OUTER JOIN dbo.PrefixInType 
INNER JOIN dbo.EndPoint ON 
dbo.PrefixInType.prefix_in_type_id = dbo.EndPoint.prefix_in_type_id ON 
dbo.DialPeer.end_point_id = dbo.EndPoint.end_point_id
GO
/****** Object:  View [dbo].[LoadBalancingMapView]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[LoadBalancingMapView] 
AS 
SELECT 
LoadBalancingMap.node_id, 
LoadBalancingMap.customer_acct_id, 
LoadBalancingMap.max_calls, 
LoadBalancingMap.current_calls, 
Node.platform_id, 
Node.description AS node_description, 
Node.node_config, 
Node.transport_type, 
Node.status AS node_status, 
Node.ip_address, 
dbo.IntToDottedIPAddress(Node.ip_address) AS dot_ip_address, 
Platform.location AS platform_location, 
Platform.status AS platform_status, 
Platform.platform_config, 
CustomerAcct.name AS customer_acct_name, 
CustomerAcct.status AS customer_acct_status, 
CustomerAcct.prefix_in, 
CustomerAcct.prefix_out, 
CustomerAcct.prefix_in_type_id, 
CustomerAcct.partner_id, 
Partner.name AS partner_name, 
Partner.status AS partner_status, 
Service.type AS service_type, 
Service.retail_type AS service_retail_type, 
CustomerAcct.is_prepaid, 
Service.is_shared AS is_shared_service

FROM  Service 
INNER JOIN CustomerAcct 
INNER JOIN Partner ON 
CustomerAcct.partner_id = Partner.partner_id ON 
Service.service_id = CustomerAcct.service_id 
RIGHT OUTER JOIN LoadBalancingMap ON 
CustomerAcct.customer_acct_id = LoadBalancingMap.customer_acct_id 
LEFT OUTER JOIN Platform 
INNER JOIN Node ON 
Platform.platform_id = Node.platform_id ON 
LoadBalancingMap.node_id = Node.node_id
GO
/****** Object:  UserDefinedFunction [dbo].[ListOfCustomers]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--*************************************************************************
CREATE FUNCTION [dbo].[ListOfCustomers] ()  
RETURNS @CustListTab TABLE
(
 custID smallint,
 custDescr varchar(50)
)
AS  
BEGIN 
 INSERT @CustListTab
 SELECT NULL [customer_acct_id], ' All Customers' [name]
 UNION
 SELECT [customer_acct_id], [name]
 FROM CustomerAcct AS [customer]
 ORDER BY [name]

 RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[ListOfCustomersByPartnerOrResellAgent]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--*************************************************************************
CREATE FUNCTION [dbo].[ListOfCustomersByPartnerOrResellAgent] (@partnerId int, @resellAgentId int)  
RETURNS @CustomerListTabble TABLE
(
 customer_acct_id smallint,
 customer_acct_name varchar(50)
)
AS  
BEGIN 
IF (@resellAgentId IS NOT NULL AND @resellAgentId > 0)
 BEGIN
  INSERT @CustomerListTabble
  SELECT NULL customer_acct_id, ' All Accounts' name
  UNION
  SELECT customer_acct_id, name
  FROM CustomerAcct
  WHERE customer_acct_id IN (
  SELECT customer_acct_id FROM ResellAcct WHERE person_id = @resellAgentId
  )
  ORDER BY name
 END
ELSE
 BEGIN
  INSERT @CustomerListTabble
  SELECT NULL customer_acct_id, ' All Accounts' name
  UNION
  SELECT customer_acct_id, name
  FROM CustomerAcct
  WHERE partner_id = @partnerId
  ORDER BY name
 END

 RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[ListOfPhoneCardCustomers]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--*************************************************************************
CREATE FUNCTION [dbo].[ListOfPhoneCardCustomers] ()  
RETURNS @CustListTab TABLE
(
 custID smallint,
 custDescr varchar(50)
)
AS  
BEGIN 
 INSERT @CustListTab
 SELECT NULL AS [customer_acct_id], ' All Customers' AS [name]
 UNION
 SELECT CustomerAcct.customer_acct_id, CustomerAcct.name
 FROM  CustomerAcct INNER JOIN
       Service ON CustomerAcct.service_id = Service.service_id
 WHERE (Service.retail_type = 1)--PhoneCard
 ORDER BY [name]
 RETURN
END
GO
/****** Object:  StoredProcedure [dbo].[InventoryUsageByCustomer]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryUsageByCustomer]
( 
 @customer_acct_id smallint = 0,
 @from_date datetime,
 @to_date datetime
)
AS
BEGIN
--DECLARE @customer_acct_id smallint SET @customer_acct_id = 0
--DECLARE @from_date datetime SET @from_date = DATEADD(day, -33, GETDATE())
--DECLARE @to_date datetime SET @to_date = GETDATE()
IF (@from_date IS NULL) 
 SET @from_date = GETDATE()
IF (@to_date IS NULL) 
 SET @to_date = GETDATE()

SET @from_date = CONVERT(varchar(50), @from_date, 101);
SET @to_date = CONVERT(varchar(50), @to_date, 101) + ' 23:59:59.998';

IF (@customer_acct_id IS NULL) 
 SET @customer_acct_id = 0;

 IF (@customer_acct_id <= 0)--GET FOR ALL Customers
  BEGIN
 SELECT 
  IU.service_id, 
  SV.name AS service_name, 
  IU.customer_acct_id, 
  CA.name AS customer_acct_name, 
  CAST(CONVERT(varchar(10), IU.timestamp, 101) AS smalldatetime) AS date, 
  SUM(IU.first_used) AS total_first_used, 
  SUM(IU.total_used) AS total_total_used, 
  SUM(IU.balance_depleted) AS total_balance_depleted, 
  SUM(IU.expired) AS total_expired

 FROM  InventoryUsage AS IU LEFT OUTER JOIN
 Service AS SV ON IU.service_id = SV.service_id LEFT OUTER JOIN
 CustomerAcct AS CA ON IU.customer_acct_id = CA.customer_acct_id
 
 WHERE (IU.timestamp BETWEEN @from_date AND @to_date)
 
 GROUP BY IU.service_id, IU.customer_acct_id, CAST(CONVERT(varchar(10), IU.timestamp, 101) AS smalldatetime), SV.name, CA.name
 ORDER BY service_name, customer_acct_name, date
END
 ELSE --GET FOR Selected Customer
  BEGIN
 SELECT 
  IU.service_id, 
  SV.name AS service_name, 
  IU.customer_acct_id, 
  CA.name AS customer_acct_name, 
  CAST(CONVERT(varchar(10), IU.timestamp, 101) AS smalldatetime) AS date, 
  SUM(IU.first_used) AS total_first_used, 
  SUM(IU.total_used) AS total_total_used, 
  SUM(IU.balance_depleted) AS total_balance_depleted, 
  SUM(IU.expired) AS total_expired

 FROM  InventoryUsage AS IU LEFT OUTER JOIN
 Service AS SV ON IU.service_id = SV.service_id LEFT OUTER JOIN
 CustomerAcct AS CA ON IU.customer_acct_id = CA.customer_acct_id
 
 WHERE (IU.timestamp BETWEEN @from_date AND @to_date) AND IU.customer_acct_id = @customer_acct_id
 
 GROUP BY IU.service_id, IU.customer_acct_id, CAST(CONVERT(varchar(10), IU.timestamp, 101) AS smalldatetime), SV.name, CA.name
 ORDER BY service_name, customer_acct_name, date

  END
END;
GO
/****** Object:  View [dbo].[AccessListView]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AccessListView] 
AS
SELECT     
dbo.DialPeer.end_point_id, 
dbo.DialPeer.prefix_in, 
dbo.DialPeer.customer_acct_id, 
dbo.EndPoint.alias, 
dbo.EndPoint.with_alias_authentication, 
dbo.EndPoint.status, 
dbo.EndPoint.type, 
dbo.EndPoint.protocol, 
dbo.EndPoint.port, 
dbo.EndPoint.registration, 
dbo.EndPoint.is_registered, 
dbo.EndPoint.ip_address_range, 
dbo.EndPoint.max_calls, 
dbo.EndPoint.password, 
dbo.EndPoint.prefix_in_type_id, 
dbo.PrefixInType.description AS prefix_type_descr, 
dbo.PrefixInType.length AS prefix_type_length, 
dbo.PrefixInType.delimiter AS prefix_type_delimiter

FROM         
dbo.DialPeer INNER JOIN dbo.EndPoint ON 
dbo.DialPeer.end_point_id = dbo.EndPoint.end_point_id 
INNER JOIN dbo.PrefixInType ON 
dbo.EndPoint.prefix_in_type_id = dbo.PrefixInType.prefix_in_type_id
GO
/****** Object:  View [dbo].[IPAddressView]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[IPAddressView] 
AS 
SELECT 
dbo.IPAddress.IP_address, 
dbo.IntToDottedIPAddress(dbo.IPAddress.IP_address) AS dot_IP_address, 
dbo.EndPoint.end_point_id, 
dbo.EndPoint.alias, 
dbo.EndPoint.with_alias_authentication, 
dbo.EndPoint.status, 
dbo.EndPoint.type, 
dbo.EndPoint.protocol, 
dbo.EndPoint.port, 
dbo.EndPoint.registration, 
dbo.EndPoint.is_registered, 
dbo.EndPoint.IP_address_range, 
dbo.EndPoint.max_calls,
dbo.EndPoint.password,
dbo.EndPoint.prefix_in_type_id, 
dbo.PrefixInType.description AS prefix_type_descr, 
dbo.PrefixInType.length AS prefix_length, 
dbo.PrefixInType.delimiter AS prefix_delimiter

FROM 
dbo.EndPoint INNER JOIN dbo.IPAddress 
ON dbo.IPAddress.end_point_id = dbo.EndPoint.end_point_id 
INNER JOIN dbo.PrefixInType 
ON dbo.EndPoint.prefix_in_type_id = dbo.PrefixInType.prefix_in_type_id
GO
/****** Object:  StoredProcedure [dbo].[InventoryStateByService]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryStateByService]
( 
 @service_id smallint = 0
)
AS
BEGIN
IF (@service_id IS NULL) 
 SET @service_id = 0;

 DECLARE @stats TABLE (service_name varchar(50), Generated int, Loaded int, Activated int, Deactivated int, Archived int)
 DECLARE @service_name varchar(50);
 IF (@service_id <= 0)--GET FOR ALL Services
  BEGIN
   SET @service_name = NULL
   INSERT @stats SELECT @service_name AS [service_name], [0] AS Generated, [1] AS Loaded, [2] AS Activated, [3] AS Deactivated, [4] AS Archived
   FROM 
   (SELECT IH.command, IH.number_of_cards
   FROM  InventoryHistory IH RIGHT OUTER JOIN
   Service AS SV ON IH.service_id = SV.service_id
   ) history
   PIVOT
   (
   SUM (number_of_cards)
   FOR command IN
   ( [0], [1], [2], [3], [4] )
   ) AS pvt
  END
 ELSE --GET FOR Selected Service
  BEGIN
   SET @service_name = (SELECT REPLACE(name, 'CU_S_', '') FROM Service WHERE service_id = @service_id)
   INSERT @stats SELECT @service_name AS [service_name], [0] AS Generated, [1] AS Loaded, [2] AS Activated, [3] AS Deactivated, [4] AS Archived
   FROM 
   (SELECT IH.command, IH.number_of_cards
   FROM  InventoryHistory IH RIGHT OUTER JOIN
   Service AS SV ON IH.service_id = SV.service_id 
   WHERE IH.service_id = @service_id
   ) history
   PIVOT
   (
   SUM (number_of_cards)
   FOR command IN
   ( [0], [1], [2], [3], [4] )
   ) AS pvt
  END

 IF ((SELECT COUNT(*) FROM @stats) = 0) 
 BEGIN
  INSERT INTO @stats VALUES(@service_name,0,0,0,0,0)
 END
 SELECT * FROM @stats 
END;
GO
/****** Object:  StoredProcedure [dbo].[InventoryHistoryByService]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryHistoryByService] 
( 
 @service_id smallint = 0,
 @from_date datetime,
 @to_date datetime
)
AS
BEGIN 

--DECLARE @service_id smallint SET @service_id = 0
--DECLARE @from_date datetime SET @from_date = DATEADD(day, -33, GETDATE())
--DECLARE @to_date datetime SET @to_date = GETDATE()

IF (@service_id IS NULL) 
 SET @service_id = 0;

SET @from_date = CONVERT(varchar(50), @from_date, 101);
SET @to_date = CONVERT(varchar(50), @to_date, 101) + ' 23:59:59.998';

IF (@service_id = 0)
 BEGIN
  SELECT 
  NULL AS service_id, 
  NULL AS service_name, 
  CAST(CONVERT(varchar(10), [timestamp], 101) AS smalldatetime) AS date, 
  IH.command, 
  SUM(IH.number_of_cards) AS total_cards, 
  CMD.description AS cmd_descr

  FROM  InventoryHistory IH INNER JOIN dbo.GetCommandList() AS CMD ON 
  IH.command = CMD.command
  
  WHERE (IH.timestamp BETWEEN @from_date AND @to_date)
  GROUP BY 
  CAST(CONVERT(varchar(10), [timestamp], 101) AS smalldatetime), 
  IH.command, 
  CMD.description

  ORDER BY date, IH.command
 END
 ELSE 
 BEGIN
  SELECT 
  IH.service_id, 
  REPLACE(SV.name, 'CU_S_', '') AS service_name, 
  CAST(CONVERT(varchar(10), [timestamp], 101) AS smalldatetime) AS date, 
  IH.command, 
  SUM(IH.number_of_cards) AS total_cards, 
  CMD.description AS cmd_descr

  FROM  InventoryHistory IH INNER JOIN Service SV ON 
  IH.service_id = SV.service_id 
  INNER JOIN dbo.GetCommandList() AS CMD ON 
  IH.command = CMD.command
  
  WHERE (IH.timestamp BETWEEN @from_date AND @to_date) AND (IH.service_id = @service_id)
  GROUP BY 
  IH.service_id, 
  SV.name, 
  CONVERT(VARCHAR(10), IH.timestamp, 101), 
  IH.command, 
  CMD.description

  ORDER BY date, IH.command
 END
END;
GO
/****** Object:  View [dbo].[NodeView]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[NodeView] 
AS 
SELECT     
dbo.Node.node_id, 
dbo.Node.platform_id, 
dbo.Node.description, 
dbo.Node.node_config, 
dbo.Node.transport_type, 
dbo.Node.user_name, 
dbo.Node.password, 
dbo.Node.ip_address, 
dbo.Node.port, 
dbo.IntToDottedIPAddress(dbo.Node.ip_address) AS dot_ip_address, 
dbo.Node.status AS node_status, 
dbo.Node.billing_export_frequency, 
dbo.Node.cdr_publishing_frequency, 
dbo.Platform.location AS platform_location, 
dbo.Platform.status AS platform_status, 
dbo.Platform.platform_config


FROM dbo.Node 
INNER JOIN dbo.Platform ON 
dbo.Node.platform_id = dbo.Platform.platform_id
GO
/****** Object:  UserDefinedFunction [dbo].[ListOfNodes]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--*************************************************************************
CREATE FUNCTION [dbo].[ListOfNodes] ()  
RETURNS @NodeListTab TABLE
(
 nodeID smallint,
 nodeDescr varchar(50)
)
AS  
BEGIN 
 INSERT @NodeListTab
 SELECT NULL [node_id], ' All Nodes' [description]
 UNION
 SELECT [node_id], [description]
 FROM node
 WHERE node_id in (SELECT node_id FROM Node WHERE node_config != 1)
 ORDER BY [description]

 RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[ListOfPhoneCardServices]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--*************************************************************************
CREATE FUNCTION [dbo].[ListOfPhoneCardServices] ()  
RETURNS @ServiceList TABLE
(
 service_id smallint,
 name varchar(50)
)
AS  
BEGIN 
 INSERT @ServiceList
 SELECT 0 service_id, ' All Services' [name]
 UNION
 SELECT service_id, REPLACE(name, 'CU_S_', '')
 FROM [Service] WHERE retail_type = 1--PhoneCard
 ORDER BY name

 RETURN
END
GO
/****** Object:  StoredProcedure [dbo].[proc_q_customer]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_q_customer] 
(
 @node varchar(50), 
 @date varchar(50), 
 @cust varchar(50) 
)
AS

DECLARE @StartJDateHour INT
DECLARE @EndJDateHour INT
DECLARE @CustID as smallint
DECLARE @NodeID as smallint

SET @CustID = CAST(@cust AS smallint)
SET @nodeID = CAST(@node AS smallint)

--get jdate range (start and end) based on date name, to be used in BETWEEN
EXECUTE dbo.GetJDateRange @date, @StartJDateHour OUTPUT, @EndJDateHour OUTPUT

IF @cust != ''
BEGIN
 IF @node != ''
 BEGIN
  PRINT 'WHICH: 1'
  SELECT     TOP 100 PERCENT
  1 AS Tag, NULL AS Parent
  ,@nodeID AS [aggr!1!node_id]
  ,customer_acct_id AS [aggr!1!customer_acct_id]
  ,carrier_acct_id AS [aggr!1!carrier_acct_id]
  ,day AS [aggr!1!Day]
  ,SUM(calls_attempted) AS [aggr!1!Total]
  ,SUM(calls_completed) AS [aggr!1!Completed]
  ,SUM(setup_seconds) AS [aggr!1!InMinutes]
  ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
  ,SUM(carrier_cost) AS [aggr!1!Cost]
  ,(SELECT name FROM CustomerAcct AS cust WHERE cust.customer_acct_id = a.customer_acct_id) AS [aggr!1!Name]
  ,(SELECT name FROM CarrierAcct AS cust WHERE cust.carrier_acct_id = a.carrier_acct_id) AS [aggr!1!PopName]
  ,1 as [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour) 
  AND customer_acct_id = @CustID 
  AND node_id = @nodeID
  GROUP BY [day], customer_acct_id, carrier_acct_id 
  ORDER BY [day], customer_acct_id, [aggr!1!PopName]
 END
 ELSE
 BEGIN
   PRINT 'WHICH: 2'
   SELECT     TOP 100 PERCENT
  1 AS Tag, NULL AS Parent
  ,1 AS [aggr!1!node_id]
  ,customer_acct_id AS [aggr!1!customer_acct_id]
  ,carrier_acct_id AS [aggr!1!carrier_acct_id]
  ,day AS [aggr!1!Day]
  ,SUM(calls_attempted) AS [aggr!1!Total]
  ,SUM(calls_completed) AS [aggr!1!Completed]
  ,SUM(setup_seconds) AS [aggr!1!InMinutes]
  ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
  ,SUM(carrier_cost) AS [aggr!1!Cost]
  ,(SELECT name FROM CustomerAcct AS cust WHERE cust.customer_acct_id = a.customer_acct_id) AS [aggr!1!Name]
  ,(SELECT name FROM CarrierAcct AS cust WHERE cust.carrier_acct_id = a.carrier_acct_id) AS [aggr!1!PopName]
  ,2 as [aggr!1!which]
   FROM AggrBase a
   WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour) 
   AND customer_acct_id = @CustID 
   AND node_id in (SELECT node_id FROM Node WHERE node_config != 1)-- SKIP ADMIN Node
   GROUP BY [day], customer_acct_id, carrier_acct_id 
   ORDER BY [day], customer_acct_id, [aggr!1!PopName]
 END
END
ELSE
BEGIN
 IF @node != ''
 BEGIN
  PRINT 'WHICH: 3'
  SELECT     TOP 100 PERCENT
  1 AS Tag, NULL AS Parent
  ,@nodeID AS [aggr!1!node_id]
  ,customer_acct_id AS [aggr!1!customer_acct_id]
  ,day AS [aggr!1!Day]
  ,SUM(calls_attempted) AS [aggr!1!Total]
  ,SUM(calls_completed) AS [aggr!1!Completed]
  ,SUM(setup_seconds) AS [aggr!1!InMinutes]
  ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
  ,SUM(carrier_cost) AS [aggr!1!Cost]
  ,(SELECT name FROM CustomerAcct AS cust WHERE cust.customer_acct_id = a.customer_acct_id) AS [aggr!1!Name]
  ,3 as [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour) 
  AND node_id = @nodeID
  GROUP BY [day], customer_acct_id  
  ORDER BY [day], [aggr!1!Name] --, customer_acct_id
 END
 ELSE
 BEGIN
  PRINT 'WHICH: 4'
  SELECT     TOP 100 PERCENT
  1 AS Tag, NULL AS Parent
  ,1 AS [aggr!1!node_id]
  ,customer_acct_id AS [aggr!1!customer_acct_id]
  ,day AS [aggr!1!Day]
  ,SUM(calls_attempted) AS [aggr!1!Total]
  ,SUM(calls_completed) AS [aggr!1!Completed]
  ,SUM(setup_seconds) AS [aggr!1!InMinutes]
  ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
  ,SUM(carrier_cost) AS [aggr!1!Cost]
  ,(SELECT name FROM CustomerAcct AS cust WHERE cust.customer_acct_id = a.customer_acct_id) AS [aggr!1!Name]
  ,4 as [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour) 
  AND node_id in (SELECT node_id FROM Node WHERE node_config != 1)-- SKIP ADMIN Node
  GROUP BY [day], customer_acct_id 
  ORDER BY [day], [aggr!1!Name] --, customer_acct_id
 END
END
GO
/****** Object:  StoredProcedure [dbo].[rep_GetPartnerCustomerAcctSummary]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rep_GetPartnerCustomerAcctSummary] 
(
 @partnerId INT, 
 @resellAgentId INT, -- person_id (AccessScope=ResellAgent)
 @date VARCHAR(50)
)
AS

DECLARE @StartJDateHour INT
DECLARE @EndJDateHour INT

--get jdate range (start and end) based on date name, to be used in BETWEEN
EXECUTE dbo.GetJDateRange @date, @StartJDateHour OUTPUT, @EndJDateHour OUTPUT

IF (@resellAgentId IS NOT NULL AND @resellAgentId > 0)
BEGIN
 SELECT     --TOP 100 PERCENT
  customer_acct_id
  ,day AS JDay
  ,(SELECT name FROM CustomerAcct C WHERE C.customer_acct_id = A.customer_acct_id) AS customer_acct_name
  ,SUM(calls_attempted) AS calls_attempted
  ,SUM(calls_completed) AS calls_completed
  ,SUM(setup_seconds) AS setup_seconds
  ,SUM(connected_minutes) AS connected_minutes
  ,'by resell agent' AS [by]
 FROM AggrBase A
 WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
  AND 
  customer_acct_id IN (
   SELECT customer_acct_id FROM ResellAcct WHERE person_id = @resellAgentId
  )
 GROUP BY customer_acct_id, day
 ORDER BY day, customer_acct_name
END
ELSE 
BEGIN
 SELECT     --TOP 100 PERCENT
  customer_acct_id
  ,day AS JDay
  ,(SELECT name FROM CustomerAcct C WHERE C.customer_acct_id = A.customer_acct_id) AS customer_acct_name
  ,SUM(calls_attempted) AS calls_attempted
  ,SUM(calls_completed) AS calls_completed
  ,SUM(setup_seconds) AS setup_seconds
  ,SUM(connected_minutes) AS connected_minutes
  ,'by partner' AS [by]
 FROM AggrBase A
 WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
  AND 
  customer_acct_id IN (
   SELECT customer_acct_id FROM CustomerAcct WHERE partner_id = @partnerId
  )
 GROUP BY customer_acct_id, day
 ORDER BY day, customer_acct_name
END
GO
/****** Object:  StoredProcedure [dbo].[proc_q_daily]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_q_daily] 
(
 @node varchar(50) = '', 
 @date varchar(50) --date name
)
AS

DECLARE @StartJDateHour INT
DECLARE @EndJDateHour INT
DECLARE @NodeID smallint

SET @nodeID = CAST(@node AS smallint)
--get jdate range (start and end) based on date name, to be used in BETWEEN
EXECUTE dbo.GetJDateRange @date, @StartJDateHour OUTPUT, @EndJDateHour OUTPUT

IF @node != ''
 BEGIN --Get for specified Node
  SELECT     --TOP 100 PERCENT
    1 AS Tag, NULL AS Parent
    ,@nodeID AS [aggr!1!node_id]
    ,date_hour AS [aggr!1!date_hour] --YYYYJJJHH
    ,[day] AS [aggr!1!Day]   --YYYYJJJ
    ,SUM(calls_attempted) AS [aggr!1!Total]
    ,SUM(calls_completed) AS [aggr!1!Completed]
    ,SUM(setup_seconds) AS [aggr!1!InMinutes]
    ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
    ,1 AS [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
   AND node_id = @nodeID
  GROUP BY [Day], date_hour
  ORDER BY [Day], date_hour
 END
ELSE
 BEGIN --Get for All Nodes
  SELECT     --TOP 100 PERCENT
    1 AS Tag, NULL as Parent
    ,1 AS [aggr!1!node_id]
    ,date_hour AS [aggr!1!date_hour]
    ,[day] AS [aggr!1!Day]
    ,SUM(calls_attempted) AS [aggr!1!Total]
    ,SUM(calls_completed) AS [aggr!1!Completed]
    ,SUM(setup_seconds) AS [aggr!1!InMinutes]
    ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
    ,2 as [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
   AND node_id in (SELECT node_id FROM Node WHERE node_config != 1)-- SKIP ADMIN Node
  GROUP BY [Day], date_hour
  ORDER BY [Day], date_hour
 END
GO
/****** Object:  StoredProcedure [dbo].[proc_q_trunk]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_q_trunk] 
(
 @node varchar(50), 
 @date varchar(50), 
 @carrier varchar(50) 
)
AS

DECLARE @StartJDateHour INT
DECLARE @EndJDateHour INT
DECLARE @nodeID as smallint
DECLARE @CarrierID as SMALLINT

SET @nodeID = CAST(@node as smallint)
SET @CarrierID = CAST(@carrier as SMALLINT)
--get jdate range (start and end) based on date name, to be used in BETWEEN
EXECUTE dbo.GetJDateRange @date, @StartJDateHour OUTPUT, @EndJDateHour OUTPUT

PRINT 'node: ' + CAST(@nodeID as varchar(20))
PRINT 'carrier: ' + CAST(@CarrierID as varchar(20))
PRINT 'end jdate: ' + CAST(@StartJDateHour as varchar(40))
PRINT 'start jdate: ' + CAST(@EndJDateHour as varchar(40))

IF @carrier != ''
BEGIN
 IF @node != ''
 BEGIN
  SELECT     TOP 100 PERCENT 
    1 AS Tag, NULL AS Parent
    ,@nodeID AS [aggr!1!node_id]
    ,carrier_acct_id AS [aggr!1!carrier_acct_id]
    ,customer_acct_id AS [aggr!1!customer_acct_id]
    ,day AS [aggr!1!Day]
    ,SUM(calls_attempted) AS [aggr!1!Total]
    ,SUM(calls_completed) AS [aggr!1!Completed]
    ,SUM(setup_seconds) AS [aggr!1!InMinutes]
    ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
    ,SUM(carrier_cost) AS [aggr!1!Cost]
    ,(SELECT name FROM CarrierAcct AS cust WHERE cust.carrier_acct_id = a.carrier_acct_id) AS [aggr!1!Name]
    ,(SELECT name FROM CustomerAcct AS cust WHERE cust.customer_acct_id = a.customer_acct_id) AS [aggr!1!CustName]
    ,1 as [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
  AND carrier_acct_id = @CarrierID 
  AND node_id = @nodeID
  GROUP BY [day], carrier_acct_id, customer_acct_id 
  ORDER BY [day], [aggr!1!Name], [aggr!1!CustName] --, [aggr!1!Name], [aggr!1!PopName]
 END
 ELSE -- node
 BEGIN
  SELECT     TOP 100 PERCENT 
    1 AS Tag, NULL AS Parent
    ,1 AS [aggr!1!node_id]
    ,carrier_acct_id AS [aggr!1!carrier_acct_id]
    ,customer_acct_id AS [aggr!1!customer_acct_id]
    ,day AS [aggr!1!Day]
    ,SUM(calls_attempted) AS [aggr!1!Total]
    ,SUM(calls_completed) AS [aggr!1!Completed]
    ,SUM(setup_seconds) AS [aggr!1!InMinutes]
    ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
    ,SUM(carrier_cost) AS [aggr!1!Cost]
    ,(SELECT name FROM CarrierAcct AS cust WHERE cust.carrier_acct_id = a.carrier_acct_id) AS [aggr!1!Name]
    ,(SELECT name FROM CustomerAcct AS cust WHERE cust.customer_acct_id = a.customer_acct_id) AS [aggr!1!CustName]
    ,2 as [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
  AND @CarrierID = carrier_acct_id 
  AND node_id in (SELECT node_id FROM Node WHERE node_config != 1)-- SKIP ADMIN Node
  GROUP BY [day], carrier_acct_id, customer_acct_id
  ORDER BY [day], [aggr!1!Name], [aggr!1!CustName]
 END
END
ELSE -- carrier
BEGIN
 IF @node != ''
 BEGIN
  SELECT     TOP 100 PERCENT 
    1 AS Tag, NULL AS Parent
    ,@nodeID AS [aggr!1!node_id]
    ,carrier_acct_id AS [aggr!1!carrier_acct_id]
--!!    ,customer_acct_id AS [aggr!1!customer_acct_id]
    ,day AS [aggr!1!Day]
    ,SUM(calls_attempted) AS [aggr!1!Total]
    ,SUM(calls_completed) AS [aggr!1!Completed]
    ,SUM(setup_seconds) AS [aggr!1!InMinutes]
    ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
    ,SUM(carrier_cost) AS [aggr!1!Cost]
    ,(SELECT name FROM CarrierAcct AS cust WHERE cust.carrier_acct_id = a.carrier_acct_id) AS [aggr!1!Name]
--!!    ,(SELECT name FROM CustomerAcct AS cust WHERE cust.customer_acct_id = a.customer_acct_id) AS [aggr!1!CustName]
    ,3 as [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
  AND node_id = @nodeID
  GROUP BY [day], carrier_acct_id --!!, customer_acct_id 
  ORDER BY [day], [aggr!1!Name] --!!, [aggr!1!CustName]
 END
 ELSE -- node
 BEGIN
  SELECT     TOP 100 PERCENT 
    1 AS Tag, NULL AS Parent
    ,1 AS [aggr!1!node_id]
    ,carrier_acct_id AS [aggr!1!carrier_acct_id]
--!!    ,customer_acct_id AS [aggr!1!customer_acct_id]
    ,day AS [aggr!1!Day]
    ,SUM(calls_attempted) AS [aggr!1!Total]
    ,SUM(calls_completed) AS [aggr!1!Completed]
    ,SUM(setup_seconds) AS [aggr!1!InMinutes]
    ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
    ,SUM(carrier_cost) AS [aggr!1!Cost]
    ,(SELECT name FROM CarrierAcct AS cust WHERE cust.carrier_acct_id = a.carrier_acct_id) AS [aggr!1!Name]
--!!    ,(SELECT name FROM CustomerAcct AS cust WHERE cust.customer_acct_id = a.customer_acct_id) AS [aggr!1!CustName]
    ,4 as [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
  AND node_id in (SELECT node_id FROM Node WHERE node_config != 1)-- SKIP ADMIN Node
  GROUP BY [day], carrier_acct_id --!!, customer_acct_id
  ORDER BY [day], [aggr!1!Name] --!!, [aggr!1!CustName]
 END
END
GO
/****** Object:  StoredProcedure [dbo].[proc_q_route]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_q_route] 
(
 @node varchar(50), 
 @date varchar(50), 
 @cust varchar(50) 
)
AS

DECLARE @StartJDateHour INT
DECLARE @EndJDateHour INT
DECLARE @CustID SMALLINT
DECLARE @NodeID smallint

SET @CustID = CAST(@cust AS SMALLINT)
SET @nodeID = CAST(@node AS smallint)
--get jdate range (start and end) based on date name, to be used in BETWEEN
EXECUTE dbo.GetJDateRange @date, @StartJDateHour OUTPUT, @EndJDateHour OUTPUT

IF @cust != ''
BEGIN
 IF @node != ''
 BEGIN
  SELECT     TOP 100 PERCENT 
  1 AS Tag, NULL AS Parent
  ,@nodeID AS [aggr!1!node_id]
  ,customer_route_id AS [aggr!1!route_id] --!! [aggr!1!customer_route_id]
  ,carrier_acct_id AS [aggr!1!carrier_acct_id]
  ,day AS [aggr!1!Day]
  ,SUM(calls_attempted) AS [aggr!1!Total]
  ,SUM(calls_completed) AS [aggr!1!Completed]
  ,SUM(setup_seconds) AS [aggr!1!InMinutes]
  ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
  ,SUM(carrier_cost) AS [aggr!1!Cost]
  ,(SELECT name FROM Route AS r WHERE r.route_id = a.customer_route_id) AS [aggr!1!Name]
  ,(SELECT name FROM CarrierAcct AS cust WHERE cust.carrier_acct_id = a.carrier_acct_id) AS [aggr!1!PopName]
  ,1 as [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
  AND customer_acct_id = @CustID 
  AND node_id = @nodeID
  GROUP BY [day], customer_route_id, carrier_acct_id 
  ORDER BY [day], [aggr!1!Name], [aggr!1!PopName]
 END
 ELSE
 BEGIN
  SELECT     TOP 100 PERCENT 
  1 AS Tag, NULL AS Parent
  ,1 AS [aggr!1!node_id]
  ,customer_route_id AS [aggr!1!route_id] --!![aggr!1!customer_route_id]
  ,carrier_acct_id AS [aggr!1!carrier_acct_id]
  ,day AS [aggr!1!Day]
  ,SUM(calls_attempted) AS [aggr!1!Total]
  ,SUM(calls_completed) AS [aggr!1!Completed]
  ,SUM(setup_seconds) AS [aggr!1!InMinutes]
  ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
  ,SUM(carrier_cost) AS [aggr!1!Cost]
  ,(SELECT name FROM Route AS r WHERE r.route_id = a.customer_route_id) AS [aggr!1!Name]
  ,(SELECT name FROM CarrierAcct AS cust WHERE cust.carrier_acct_id = a.carrier_acct_id) AS [aggr!1!PopName]
  ,2 as [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
  AND customer_acct_id = @CustID 
  AND node_id in (SELECT node_id FROM Node WHERE node_config != 1)-- SKIP ADMIN Node
  GROUP BY [day], customer_route_id, carrier_acct_id 
  ORDER BY [day], [aggr!1!Name], [aggr!1!PopName]
 END
END
ELSE -- '' != @cust
BEGIN
 IF @node != ''
 BEGIN
  -- NOTE: in route report, we aggregate on carrier acct too so we can show the POP.
  SELECT     TOP 100 PERCENT 
  1 AS Tag, NULL AS Parent
  ,@nodeID AS [aggr!1!node_id]
  ,customer_route_id AS [aggr!1!route_id] --!![aggr!1!customer_route_id]
  ,carrier_acct_id AS [aggr!1!carrier_acct_id]
  ,day AS [aggr!1!Day]
  ,SUM(calls_attempted) AS [aggr!1!Total]
  ,SUM(calls_completed) AS [aggr!1!Completed]
  ,SUM(setup_seconds) AS [aggr!1!InMinutes]
  ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
  ,SUM(carrier_cost) AS [aggr!1!Cost]
  ,(SELECT name FROM Route AS r WHERE r.route_id = a.customer_route_id) AS [aggr!1!Name]
  ,(SELECT name FROM CarrierAcct AS cust WHERE cust.carrier_acct_id = a.carrier_acct_id) AS [aggr!1!PopName]
  ,3 as [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
  AND node_id = @nodeID
  GROUP BY [day], customer_route_id, carrier_acct_id  
  ORDER BY [day], [aggr!1!Name], [aggr!1!PopName]
 END
 ELSE
 BEGIN
  SELECT     TOP 100 PERCENT 
  1 AS Tag, NULL AS Parent
  ,1 AS [aggr!1!node_id]
  ,customer_route_id AS [aggr!1!route_id] --!![aggr!1!customer_route_id]
  ,carrier_acct_id AS [aggr!1!carrier_acct_id]
  ,day AS [aggr!1!Day]
  ,SUM(calls_attempted) AS [aggr!1!Total]
  ,SUM(calls_completed) AS [aggr!1!Completed]
  ,SUM(setup_seconds) AS [aggr!1!InMinutes]
  ,SUM(connected_minutes) AS [aggr!1!OutMinutes]
  ,SUM(carrier_cost) AS [aggr!1!Cost]
  ,(SELECT name FROM Route AS r WHERE r.route_id = a.customer_route_id) AS [aggr!1!Name]
  ,(SELECT name FROM CarrierAcct AS cust WHERE cust.carrier_acct_id = a.carrier_acct_id) AS [aggr!1!PopName]
  ,4 as [aggr!1!which]
  FROM AggrBase a
  WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
  AND node_id in (SELECT node_id FROM Node WHERE node_config != 1)-- SKIP ADMIN Node
  GROUP BY [day], customer_route_id, carrier_acct_id  
  ORDER BY [day], [aggr!1!Name], [aggr!1!PopName]
 END
END
GO
/****** Object:  StoredProcedure [dbo].[rep_GetPartnerRouteSummary]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rep_GetPartnerRouteSummary] 
(
 @partnerId INT, 
 @resellAgentId INT, -- person_id (AccessScope=ResellAgent)
 @customerAcctId SMALLINT,
 @date VARCHAR(50)
)
AS

DECLARE @StartJDateHour INT
DECLARE @EndJDateHour INT

--get jdate range (start and end) based on date name, to be used in BETWEEN
EXECUTE dbo.GetJDateRange @date, @StartJDateHour OUTPUT, @EndJDateHour OUTPUT

IF (@resellAgentId IS NOT NULL AND @resellAgentId > 0)
BEGIN
--*** BY ResellAgent ******************************************************
 IF (@customerAcctId IS NOT NULL AND @customerAcctId > 0)
 BEGIN
 --*** BY Selected CustomerAcct ******************************************************
 SELECT     --TOP 100 PERCENT
 day AS JDay
 ,customer_route_id
 ,(SELECT name FROM Route R WHERE R.route_id = A.customer_route_id) AS customer_route_name
 ,SUM(calls_attempted) AS calls_attempted
 ,SUM(calls_completed) AS calls_completed
 ,SUM(setup_seconds) AS setup_seconds
 ,SUM(connected_minutes) AS connected_minutes
 ,'by resell agent, selected acct' AS [by]
 FROM AggrBase A
 WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
 AND 
 --NOTE: this one is just to make sure we show only acct which belong to the ResellAgent
 customer_acct_id IN (
  SELECT customer_acct_id FROM ResellAcct WHERE customer_acct_id = @customerAcctId AND person_id = @resellAgentId
 )
 GROUP BY customer_route_id, day
 ORDER BY day, customer_route_name
 END
 ELSE
 BEGIN
 --*** ALL ResellAgent's CustomerAccts ******************************************************
 SELECT     --TOP 100 PERCENT
 day AS JDay
 ,customer_route_id
 ,(SELECT name FROM Route R WHERE R.route_id = A.customer_route_id) AS customer_route_name
 ,SUM(calls_attempted) AS calls_attempted
 ,SUM(calls_completed) AS calls_completed
 ,SUM(setup_seconds) AS setup_seconds
 ,SUM(connected_minutes) AS connected_minutes
 ,'by resell agent, all accts' AS [by]
 FROM AggrBase A
 WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
 AND 
 customer_acct_id IN (
  SELECT customer_acct_id FROM ResellAcct WHERE person_id = @resellAgentId
 )
 GROUP BY customer_route_id, day
 ORDER BY day, customer_route_name
 END
END
ELSE 
BEGIN
--*** BY Partner ******************************************************
 IF (@customerAcctId IS NOT NULL AND @customerAcctId > 0)
 BEGIN
 --*** BY Selected CustomerAcct ******************************************************
  SELECT     --TOP 100 PERCENT
 day AS JDay
 ,customer_route_id
 ,(SELECT name FROM Route R WHERE R.route_id = A.customer_route_id) AS customer_route_name
 ,SUM(calls_attempted) AS calls_attempted
 ,SUM(calls_completed) AS calls_completed
 ,SUM(setup_seconds) AS setup_seconds
 ,SUM(connected_minutes) AS connected_minutes
 ,'by partner, selected acct' AS [by]
 FROM AggrBase A
 WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
 AND 
 --NOTE: this one is just to make sure we show only acct which belong to the Partner
 customer_acct_id IN (
  SELECT customer_acct_id FROM CustomerAcct WHERE customer_acct_id = @customerAcctId AND partner_id = @partnerId 
 )
 GROUP BY customer_route_id, day
 ORDER BY day, customer_route_name
END
 ELSE
 BEGIN
 --*** ALL Partner's CustomerAccts ******************************************************
 SELECT     --TOP 100 PERCENT
 day AS JDay
 ,customer_route_id
 ,(SELECT name FROM Route R WHERE R.route_id = A.customer_route_id) AS customer_route_name
 ,SUM(calls_attempted) AS calls_attempted
 ,SUM(calls_completed) AS calls_completed
 ,SUM(setup_seconds) AS setup_seconds
 ,SUM(connected_minutes) AS connected_minutes
 ,'by partner, all accts' AS [by]
 FROM AggrBase A
 WHERE (date_hour BETWEEN @StartJDateHour AND @EndJDateHour)
 AND 
 customer_acct_id IN (
  SELECT customer_acct_id FROM CustomerAcct WHERE partner_id = @partnerId
 )
 GROUP BY customer_route_id, day
 ORDER BY day, customer_route_name
 END
END
GO
/****** Object:  StoredProcedure [dbo].[AccessNumberByCustomer]    Script Date: 03/22/2009 02:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AccessNumberByCustomer] 
(
 @date_interval varchar(50) = 'today',
 @customer_acct_id smallint = 0,
 @node_id smallint = 0
)
AS
BEGIN

-- FOR TESTING ONLY
--DECLARE @date_interval varchar(50);
--DECLARE @customer_acct_id int
--DECLARE @node_id smallint
--SET @date_interval = 'twomonthsago'
--SET @customer_acct_id = 0--1026
--SET @node_id = 1--1026
-- end FOR TESTING ONLY


DECLARE @StartJDateHour INT
DECLARE @EndJDateHour INT

--get jdate range (start and end) based on date name, to be used in BETWEEN
EXECUTE dbo.GetJDateRange @date_interval, @StartJDateHour OUTPUT, @EndJDateHour OUTPUT

IF (@node_id IS NULL)
 SET @node_id = 0
IF (@customer_acct_id IS NULL)
 SET @customer_acct_id = 0


IF (@customer_acct_id > 0 AND @node_id > 0) 
 BEGIN
  SELECT 
   aggr.date_hour, 
   aggr.date_hour / 100 AS day, 
   aggr.customer_acct_id, 
   CA.name AS customer_acct_name, 
   aggr.access_number, 
   SUM(aggr.calls_attempted) AS total_calls_attempted, 
   SUM(aggr.calls_completed) AS total_calls_completed, 
   SUM(aggr.setup_seconds) AS total_setup_seconds, 
   SUM(aggr.alert_seconds) AS total_alert_seconds, 
   SUM(aggr.connected_seconds) AS total_connected_seconds, 
   SUM(aggr.connected_minutes) AS total_connected_minutes,
 1 AS [check]

  FROM CdrAggregate AS aggr LEFT OUTER JOIN
    CustomerAcct AS CA ON aggr.customer_acct_id = CA.customer_acct_id 

  WHERE (aggr.date_hour BETWEEN @StartJDateHour AND @EndJDateHour) AND (aggr.customer_acct_id = @customer_acct_id) AND (aggr.node_id = @node_id)

  GROUP BY aggr.date_hour, aggr.customer_acct_id, CA.name, aggr.access_number
  ORDER BY aggr.date_hour, customer_acct_name, aggr.access_number 

 END
ELSE IF (@customer_acct_id > 0 AND @node_id = 0)
 BEGIN
  SELECT 
   aggr.date_hour, 
   aggr.date_hour / 100 AS day, 
   aggr.customer_acct_id, 
   CA.name AS customer_acct_name, 
   aggr.access_number, 
   SUM(aggr.calls_attempted) AS total_calls_attempted, 
   SUM(aggr.calls_completed) AS total_calls_completed, 
   SUM(aggr.setup_seconds) AS total_setup_seconds, 
   SUM(aggr.alert_seconds) AS total_alert_seconds, 
   SUM(aggr.connected_seconds) AS total_connected_seconds, 
   SUM(aggr.connected_minutes) AS total_connected_minutes,
 1 AS [check]

  FROM CdrAggregate AS aggr LEFT OUTER JOIN
    CustomerAcct AS CA ON aggr.customer_acct_id = CA.customer_acct_id 

  WHERE (aggr.date_hour BETWEEN @StartJDateHour AND @EndJDateHour) AND (aggr.customer_acct_id = @customer_acct_id)

  GROUP BY aggr.date_hour, aggr.customer_acct_id, CA.name, aggr.access_number
  ORDER BY aggr.date_hour, customer_acct_name, aggr.access_number 
 END

ELSE IF (@customer_acct_id = 0 AND @node_id > 0)
 BEGIN
  SELECT 
   aggr.date_hour, 
   aggr.date_hour / 100 AS day, 
   aggr.customer_acct_id, 
   CA.name AS customer_acct_name, 
   aggr.access_number, 
   SUM(aggr.calls_attempted) AS total_calls_attempted, 
   SUM(aggr.calls_completed) AS total_calls_completed, 
   SUM(aggr.setup_seconds) AS total_setup_seconds, 
   SUM(aggr.alert_seconds) AS total_alert_seconds, 
   SUM(aggr.connected_seconds) AS total_connected_seconds, 
   SUM(aggr.connected_minutes) AS total_connected_minutes,
 1 AS [check]

  FROM CdrAggregate AS aggr LEFT OUTER JOIN
    CustomerAcct AS CA ON aggr.customer_acct_id = CA.customer_acct_id 

  WHERE (aggr.date_hour BETWEEN @StartJDateHour AND @EndJDateHour) AND (aggr.node_id = @node_id)

  GROUP BY aggr.date_hour, aggr.customer_acct_id, CA.name, aggr.access_number
  ORDER BY aggr.date_hour, customer_acct_name, aggr.access_number 
 END

ELSE 
 BEGIN
  SELECT 
   aggr.date_hour, 
   aggr.date_hour / 100 AS day, 
   aggr.customer_acct_id, 
   CA.name AS customer_acct_name, 
   aggr.access_number, 
   SUM(aggr.calls_attempted) AS total_calls_attempted, 
   SUM(aggr.calls_completed) AS total_calls_completed, 
   SUM(aggr.setup_seconds) AS total_setup_seconds, 
   SUM(aggr.alert_seconds) AS total_alert_seconds, 
   SUM(aggr.connected_seconds) AS total_connected_seconds, 
   SUM(aggr.connected_minutes) AS total_connected_minutes,
 1 AS [check]

  FROM CdrAggregate AS aggr LEFT OUTER JOIN
    CustomerAcct AS CA ON aggr.customer_acct_id = CA.customer_acct_id 

  WHERE (aggr.date_hour BETWEEN @StartJDateHour AND @EndJDateHour)

  GROUP BY aggr.date_hour, aggr.customer_acct_id, CA.name, aggr.access_number
  ORDER BY aggr.date_hour, customer_acct_name, aggr.access_number 
 END

END
GO
/****** Object:  Check [CDRExportFileDelimiters320]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CdrExportMap]  WITH NOCHECK ADD  CONSTRAINT [CDRExportFileDelimiters320] CHECK  (([delimiter]=(124) OR [delimiter]=(44) OR [delimiter]=(9)))
GO
ALTER TABLE [dbo].[CdrExportMap] CHECK CONSTRAINT [CDRExportFileDelimiters320]
GO
/****** Object:  ForeignKey [FK_AccessNumberList_Service]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[AccessNumberList]  WITH NOCHECK ADD  CONSTRAINT [FK_AccessNumberList_Service] FOREIGN KEY([service_id])
REFERENCES [dbo].[Service] ([service_id])
GO
ALTER TABLE [dbo].[AccessNumberList] CHECK CONSTRAINT [FK_AccessNumberList_Service]
GO
/****** Object:  ForeignKey [R_254]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[AccessNumberList]  WITH NOCHECK ADD  CONSTRAINT [R_254] FOREIGN KEY([customer_acct_id])
REFERENCES [dbo].[CustomerAcct] ([customer_acct_id])
GO
ALTER TABLE [dbo].[AccessNumberList] CHECK CONSTRAINT [R_254]
GO
/****** Object:  ForeignKey [R_282]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[Batch]  WITH NOCHECK ADD  CONSTRAINT [R_282] FOREIGN KEY([box_id])
REFERENCES [dbo].[Box] ([box_id])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [R_282]
GO
/****** Object:  ForeignKey [R_290]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[Batch]  WITH NOCHECK ADD  CONSTRAINT [R_290] FOREIGN KEY([request_id])
REFERENCES [dbo].[GenerationRequest] ([request_id])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [R_290]
GO
/****** Object:  ForeignKey [R_316]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CallCenterCabina]  WITH NOCHECK ADD  CONSTRAINT [R_316] FOREIGN KEY([retail_acct_id])
REFERENCES [dbo].[RetailAccount] ([retail_acct_id])
GO
ALTER TABLE [dbo].[CallCenterCabina] CHECK CONSTRAINT [R_316]
GO
/****** Object:  ForeignKey [R_363]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CallCenterCabina]  WITH NOCHECK ADD  CONSTRAINT [R_363] FOREIGN KEY([service_id])
REFERENCES [dbo].[Service] ([service_id])
GO
ALTER TABLE [dbo].[CallCenterCabina] CHECK CONSTRAINT [R_363]
GO
/****** Object:  ForeignKey [R_298]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CallingPlan]  WITH NOCHECK ADD  CONSTRAINT [R_298] FOREIGN KEY([virtual_switch_id])
REFERENCES [dbo].[VirtualSwitch] ([virtual_switch_id])
GO
ALTER TABLE [dbo].[CallingPlan] CHECK CONSTRAINT [R_298]
GO
/****** Object:  ForeignKey [R_332]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CarrierAcct]  WITH NOCHECK ADD  CONSTRAINT [R_332] FOREIGN KEY([calling_plan_id])
REFERENCES [dbo].[CallingPlan] ([calling_plan_id])
GO
ALTER TABLE [dbo].[CarrierAcct] CHECK CONSTRAINT [R_332]
GO
/****** Object:  ForeignKey [R_5]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CarrierAcct]  WITH NOCHECK ADD  CONSTRAINT [R_5] FOREIGN KEY([partner_id])
REFERENCES [dbo].[Partner] ([partner_id])
GO
ALTER TABLE [dbo].[CarrierAcct] CHECK CONSTRAINT [R_5]
GO
/****** Object:  ForeignKey [R_347]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CarrierAcctEPMap]  WITH NOCHECK ADD  CONSTRAINT [R_347] FOREIGN KEY([carrier_acct_id])
REFERENCES [dbo].[CarrierAcct] ([carrier_acct_id])
GO
ALTER TABLE [dbo].[CarrierAcctEPMap] CHECK CONSTRAINT [R_347]
GO
/****** Object:  ForeignKey [R_348]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CarrierAcctEPMap]  WITH NOCHECK ADD  CONSTRAINT [R_348] FOREIGN KEY([carrier_route_id])
REFERENCES [dbo].[CarrierRoute] ([carrier_route_id])
GO
ALTER TABLE [dbo].[CarrierAcctEPMap] CHECK CONSTRAINT [R_348]
GO
/****** Object:  ForeignKey [R_349]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CarrierAcctEPMap]  WITH NOCHECK ADD  CONSTRAINT [R_349] FOREIGN KEY([end_point_id])
REFERENCES [dbo].[EndPoint] ([end_point_id])
GO
ALTER TABLE [dbo].[CarrierAcctEPMap] CHECK CONSTRAINT [R_349]
GO
/****** Object:  ForeignKey [R_336]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CarrierRateHistory]  WITH NOCHECK ADD  CONSTRAINT [R_336] FOREIGN KEY([carrier_route_id])
REFERENCES [dbo].[CarrierRoute] ([carrier_route_id])
GO
ALTER TABLE [dbo].[CarrierRateHistory] CHECK CONSTRAINT [R_336]
GO
/****** Object:  ForeignKey [R_337]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CarrierRateHistory]  WITH NOCHECK ADD  CONSTRAINT [R_337] FOREIGN KEY([rate_info_id])
REFERENCES [dbo].[RateInfo] ([rate_info_id])
GO
ALTER TABLE [dbo].[CarrierRateHistory] CHECK CONSTRAINT [R_337]
GO
/****** Object:  ForeignKey [R_329]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CarrierRoute]  WITH NOCHECK ADD  CONSTRAINT [R_329] FOREIGN KEY([route_id])
REFERENCES [dbo].[Route] ([route_id])
GO
ALTER TABLE [dbo].[CarrierRoute] CHECK CONSTRAINT [R_329]
GO
/****** Object:  ForeignKey [R_333]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CarrierRoute]  WITH NOCHECK ADD  CONSTRAINT [R_333] FOREIGN KEY([carrier_acct_id])
REFERENCES [dbo].[CarrierAcct] ([carrier_acct_id])
GO
ALTER TABLE [dbo].[CarrierRoute] CHECK CONSTRAINT [R_333]
GO
/****** Object:  ForeignKey [R_69]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CdrExportMapDetail]  WITH NOCHECK ADD  CONSTRAINT [R_69] FOREIGN KEY([map_id])
REFERENCES [dbo].[CdrExportMap] ([map_id])
GO
ALTER TABLE [dbo].[CdrExportMapDetail] CHECK CONSTRAINT [R_69]
GO
/****** Object:  ForeignKey [R_164]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CustomerAcct]  WITH NOCHECK ADD  CONSTRAINT [R_164] FOREIGN KEY([prefix_in_type_id])
REFERENCES [dbo].[PrefixInType] ([prefix_in_type_id])
GO
ALTER TABLE [dbo].[CustomerAcct] CHECK CONSTRAINT [R_164]
GO
/****** Object:  ForeignKey [R_195]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CustomerAcct]  WITH NOCHECK ADD  CONSTRAINT [R_195] FOREIGN KEY([service_id])
REFERENCES [dbo].[Service] ([service_id])
GO
ALTER TABLE [dbo].[CustomerAcct] CHECK CONSTRAINT [R_195]
GO
/****** Object:  ForeignKey [R_341]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CustomerAcct]  WITH NOCHECK ADD  CONSTRAINT [R_341] FOREIGN KEY([routing_plan_id])
REFERENCES [dbo].[RoutingPlan] ([routing_plan_id])
GO
ALTER TABLE [dbo].[CustomerAcct] CHECK CONSTRAINT [R_341]
GO
/****** Object:  ForeignKey [R_358]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CustomerAcct]  WITH NOCHECK ADD  CONSTRAINT [R_358] FOREIGN KEY([retail_calling_plan_id])
REFERENCES [dbo].[CallingPlan] ([calling_plan_id])
GO
ALTER TABLE [dbo].[CustomerAcct] CHECK CONSTRAINT [R_358]
GO
/****** Object:  ForeignKey [R_42]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CustomerAcct]  WITH NOCHECK ADD  CONSTRAINT [R_42] FOREIGN KEY([partner_id])
REFERENCES [dbo].[Partner] ([partner_id])
GO
ALTER TABLE [dbo].[CustomerAcct] CHECK CONSTRAINT [R_42]
GO
/****** Object:  ForeignKey [R_248]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CustomerAcctPayment]  WITH NOCHECK ADD  CONSTRAINT [R_248] FOREIGN KEY([customer_acct_id])
REFERENCES [dbo].[CustomerAcct] ([customer_acct_id])
GO
ALTER TABLE [dbo].[CustomerAcctPayment] CHECK CONSTRAINT [R_248]
GO
/****** Object:  ForeignKey [R_251]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CustomerAcctPayment]  WITH NOCHECK ADD  CONSTRAINT [R_251] FOREIGN KEY([person_id])
REFERENCES [dbo].[Person] ([person_id])
GO
ALTER TABLE [dbo].[CustomerAcctPayment] CHECK CONSTRAINT [R_251]
GO
/****** Object:  ForeignKey [R_284]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CustomerAcctPayment]  WITH NOCHECK ADD  CONSTRAINT [R_284] FOREIGN KEY([balance_adjustment_reason_id])
REFERENCES [dbo].[BalanceAdjustmentReason] ([balance_adjustment_reason_id])
GO
ALTER TABLE [dbo].[CustomerAcctPayment] CHECK CONSTRAINT [R_284]
GO
/****** Object:  ForeignKey [R_257]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CustomerAcctSupportMap]  WITH NOCHECK ADD  CONSTRAINT [R_257] FOREIGN KEY([customer_acct_id])
REFERENCES [dbo].[CustomerAcct] ([customer_acct_id])
GO
ALTER TABLE [dbo].[CustomerAcctSupportMap] CHECK CONSTRAINT [R_257]
GO
/****** Object:  ForeignKey [R_315]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CustomerAcctSupportMap]  WITH NOCHECK ADD  CONSTRAINT [R_315] FOREIGN KEY([vendor_id])
REFERENCES [dbo].[CustomerSupportVendor] ([vendor_id])
GO
ALTER TABLE [dbo].[CustomerAcctSupportMap] CHECK CONSTRAINT [R_315]
GO
/****** Object:  ForeignKey [R_313]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CustomerSupportGroup]  WITH NOCHECK ADD  CONSTRAINT [R_313] FOREIGN KEY([vendor_id])
REFERENCES [dbo].[CustomerSupportVendor] ([vendor_id])
GO
ALTER TABLE [dbo].[CustomerSupportGroup] CHECK CONSTRAINT [R_313]
GO
/****** Object:  ForeignKey [R_314]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[CustomerSupportVendor]  WITH NOCHECK ADD  CONSTRAINT [R_314] FOREIGN KEY([contact_info_id])
REFERENCES [dbo].[ContactInfo] ([contact_info_id])
GO
ALTER TABLE [dbo].[CustomerSupportVendor] CHECK CONSTRAINT [R_314]
GO
/****** Object:  ForeignKey [FK_DialCode_CallingPlan]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[DialCode]  WITH NOCHECK ADD  CONSTRAINT [FK_DialCode_CallingPlan] FOREIGN KEY([calling_plan_id])
REFERENCES [dbo].[CallingPlan] ([calling_plan_id])
GO
ALTER TABLE [dbo].[DialCode] CHECK CONSTRAINT [FK_DialCode_CallingPlan]
GO
/****** Object:  ForeignKey [FK_DialCode_Route]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[DialCode]  WITH NOCHECK ADD  CONSTRAINT [FK_DialCode_Route] FOREIGN KEY([route_id])
REFERENCES [dbo].[Route] ([route_id])
GO
ALTER TABLE [dbo].[DialCode] CHECK CONSTRAINT [FK_DialCode_Route]
GO
/****** Object:  ForeignKey [R_151]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[DialPeer]  WITH NOCHECK ADD  CONSTRAINT [R_151] FOREIGN KEY([end_point_id])
REFERENCES [dbo].[EndPoint] ([end_point_id])
GO
ALTER TABLE [dbo].[DialPeer] CHECK CONSTRAINT [R_151]
GO
/****** Object:  ForeignKey [R_46]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[DialPeer]  WITH NOCHECK ADD  CONSTRAINT [R_46] FOREIGN KEY([customer_acct_id])
REFERENCES [dbo].[CustomerAcct] ([customer_acct_id])
GO
ALTER TABLE [dbo].[DialPeer] CHECK CONSTRAINT [R_46]
GO
/****** Object:  ForeignKey [R_165]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[EndPoint]  WITH NOCHECK ADD  CONSTRAINT [R_165] FOREIGN KEY([prefix_in_type_id])
REFERENCES [dbo].[PrefixInType] ([prefix_in_type_id])
GO
ALTER TABLE [dbo].[EndPoint] CHECK CONSTRAINT [R_165]
GO
/****** Object:  ForeignKey [R_300]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[EndPoint]  WITH NOCHECK ADD  CONSTRAINT [R_300] FOREIGN KEY([virtual_switch_id])
REFERENCES [dbo].[VirtualSwitch] ([virtual_switch_id])
GO
ALTER TABLE [dbo].[EndPoint] CHECK CONSTRAINT [R_300]
GO
/****** Object:  ForeignKey [R_289]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[GenerationRequest]  WITH NOCHECK ADD  CONSTRAINT [R_289] FOREIGN KEY([lot_id])
REFERENCES [dbo].[InventoryLot] ([lot_id])
GO
ALTER TABLE [dbo].[GenerationRequest] CHECK CONSTRAINT [R_289]
GO
/****** Object:  ForeignKey [R_216]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[HolidayCalendar]  WITH NOCHECK ADD  CONSTRAINT [R_216] FOREIGN KEY([rate_info_id])
REFERENCES [dbo].[RateInfo] ([rate_info_id])
GO
ALTER TABLE [dbo].[HolidayCalendar] CHECK CONSTRAINT [R_216]
GO
/****** Object:  ForeignKey [R_261]    Script Date: 03/22/2009 02:49:30 ******/
ALTER TABLE [dbo].[InventoryLot]  WITH NOCHECK ADD  CONSTRAINT [R_261] FOREIGN KEY([service_id])
REFERENCES [dbo].[Service] ([service_id])
GO
ALTER TABLE [dbo].[InventoryLot] CHECK CONSTRAINT [R_261]
GO
/****** Object:  ForeignKey [R_150]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[IPAddress]  WITH NOCHECK ADD  CONSTRAINT [R_150] FOREIGN KEY([end_point_id])
REFERENCES [dbo].[EndPoint] ([end_point_id])
GO
ALTER TABLE [dbo].[IPAddress] CHECK CONSTRAINT [R_150]
GO
/****** Object:  ForeignKey [R_272]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[LCRBlackList]  WITH NOCHECK ADD  CONSTRAINT [R_272] FOREIGN KEY([carrier_acct_id])
REFERENCES [dbo].[CarrierAcct] ([carrier_acct_id])
GO
ALTER TABLE [dbo].[LCRBlackList] CHECK CONSTRAINT [R_272]
GO
/****** Object:  ForeignKey [R_344]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[LCRBlackList]  WITH NOCHECK ADD  CONSTRAINT [R_344] FOREIGN KEY([routing_plan_id], [route_id])
REFERENCES [dbo].[RoutingPlanDetail] ([routing_plan_id], [route_id])
GO
ALTER TABLE [dbo].[LCRBlackList] CHECK CONSTRAINT [R_344]
GO
/****** Object:  ForeignKey [R_23]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[LoadBalancingMap]  WITH NOCHECK ADD  CONSTRAINT [R_23] FOREIGN KEY([node_id])
REFERENCES [dbo].[Node] ([node_id])
GO
ALTER TABLE [dbo].[LoadBalancingMap] CHECK CONSTRAINT [R_23]
GO
/****** Object:  ForeignKey [R_43]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[LoadBalancingMap]  WITH NOCHECK ADD  CONSTRAINT [R_43] FOREIGN KEY([customer_acct_id])
REFERENCES [dbo].[CustomerAcct] ([customer_acct_id])
GO
ALTER TABLE [dbo].[LoadBalancingMap] CHECK CONSTRAINT [R_43]
GO
/****** Object:  ForeignKey [R_10]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Node]  WITH NOCHECK ADD  CONSTRAINT [R_10] FOREIGN KEY([platform_id])
REFERENCES [dbo].[Platform] ([platform_id])
GO
ALTER TABLE [dbo].[Node] CHECK CONSTRAINT [R_10]
GO
/****** Object:  ForeignKey [R_369]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[OutboundANI]  WITH NOCHECK ADD  CONSTRAINT [R_369] FOREIGN KEY([carrier_route_id])
REFERENCES [dbo].[CarrierRoute] ([carrier_route_id])
GO
ALTER TABLE [dbo].[OutboundANI] CHECK CONSTRAINT [R_369]
GO
/****** Object:  ForeignKey [R_138]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Partner]  WITH NOCHECK ADD  CONSTRAINT [R_138] FOREIGN KEY([billing_schedule_id])
REFERENCES [dbo].[Schedule] ([schedule_id])
GO
ALTER TABLE [dbo].[Partner] CHECK CONSTRAINT [R_138]
GO
/****** Object:  ForeignKey [R_140]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Partner]  WITH NOCHECK ADD  CONSTRAINT [R_140] FOREIGN KEY([contact_info_id])
REFERENCES [dbo].[ContactInfo] ([contact_info_id])
GO
ALTER TABLE [dbo].[Partner] CHECK CONSTRAINT [R_140]
GO
/****** Object:  ForeignKey [R_301]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Partner]  WITH NOCHECK ADD  CONSTRAINT [R_301] FOREIGN KEY([virtual_switch_id])
REFERENCES [dbo].[VirtualSwitch] ([virtual_switch_id])
GO
ALTER TABLE [dbo].[Partner] CHECK CONSTRAINT [R_301]
GO
/****** Object:  ForeignKey [R_238]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Person]  WITH NOCHECK ADD  CONSTRAINT [R_238] FOREIGN KEY([retail_acct_id])
REFERENCES [dbo].[RetailAccount] ([retail_acct_id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [R_238]
GO
/****** Object:  ForeignKey [R_240]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Person]  WITH NOCHECK ADD  CONSTRAINT [R_240] FOREIGN KEY([partner_id])
REFERENCES [dbo].[Partner] ([partner_id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [R_240]
GO
/****** Object:  ForeignKey [R_312]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Person]  WITH NOCHECK ADD  CONSTRAINT [R_312] FOREIGN KEY([virtual_switch_id])
REFERENCES [dbo].[VirtualSwitch] ([virtual_switch_id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [R_312]
GO
/****** Object:  ForeignKey [R_382]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Person]  WITH NOCHECK ADD  CONSTRAINT [R_382] FOREIGN KEY([contact_info_id])
REFERENCES [dbo].[ContactInfo] ([contact_info_id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [R_382]
GO
/****** Object:  ForeignKey [R_555]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Person]  WITH NOCHECK ADD  CONSTRAINT [R_555] FOREIGN KEY([group_id])
REFERENCES [dbo].[CustomerSupportGroup] ([group_id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [R_555]
GO
/****** Object:  ForeignKey [R_221]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[PhoneCard]  WITH NOCHECK ADD  CONSTRAINT [R_221] FOREIGN KEY([retail_acct_id])
REFERENCES [dbo].[RetailAccount] ([retail_acct_id])
GO
ALTER TABLE [dbo].[PhoneCard] CHECK CONSTRAINT [R_221]
GO
/****** Object:  ForeignKey [R_365]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[PhoneCard]  WITH NOCHECK ADD  CONSTRAINT [R_365] FOREIGN KEY([service_id])
REFERENCES [dbo].[Service] ([service_id])
GO
ALTER TABLE [dbo].[PhoneCard] CHECK CONSTRAINT [R_365]
GO
/****** Object:  ForeignKey [R_172]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Rate]  WITH NOCHECK ADD  CONSTRAINT [R_172] FOREIGN KEY([time_of_day])
REFERENCES [dbo].[TimeOfDay] ([time_of_day])
GO
ALTER TABLE [dbo].[Rate] CHECK CONSTRAINT [R_172]
GO
/****** Object:  ForeignKey [R_209]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Rate]  WITH NOCHECK ADD  CONSTRAINT [R_209] FOREIGN KEY([rate_info_id], [type_of_day_choice])
REFERENCES [dbo].[TypeOfDay] ([rate_info_id], [type_of_day_choice])
GO
ALTER TABLE [dbo].[Rate] CHECK CONSTRAINT [R_209]
GO
/****** Object:  ForeignKey [R_288]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[ResellAcct]  WITH NOCHECK ADD  CONSTRAINT [R_288] FOREIGN KEY([customer_acct_id])
REFERENCES [dbo].[CustomerAcct] ([customer_acct_id])
GO
ALTER TABLE [dbo].[ResellAcct] CHECK CONSTRAINT [R_288]
GO
/****** Object:  ForeignKey [R_291]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[ResellAcct]  WITH NOCHECK ADD  CONSTRAINT [R_291] FOREIGN KEY([person_id])
REFERENCES [dbo].[Person] ([person_id])
GO
ALTER TABLE [dbo].[ResellAcct] CHECK CONSTRAINT [R_291]
GO
/****** Object:  ForeignKey [R_370]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[ResellAcct]  WITH NOCHECK ADD  CONSTRAINT [R_370] FOREIGN KEY([partner_id])
REFERENCES [dbo].[Partner] ([partner_id])
GO
ALTER TABLE [dbo].[ResellAcct] CHECK CONSTRAINT [R_370]
GO
/****** Object:  ForeignKey [R_339]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[ResellRateHistory]  WITH NOCHECK ADD  CONSTRAINT [R_339] FOREIGN KEY([rate_info_id])
REFERENCES [dbo].[RateInfo] ([rate_info_id])
GO
ALTER TABLE [dbo].[ResellRateHistory] CHECK CONSTRAINT [R_339]
GO
/****** Object:  ForeignKey [R_360]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[ResellRateHistory]  WITH NOCHECK ADD  CONSTRAINT [R_360] FOREIGN KEY([wholesale_route_id])
REFERENCES [dbo].[WholesaleRoute] ([wholesale_route_id])
GO
ALTER TABLE [dbo].[ResellRateHistory] CHECK CONSTRAINT [R_360]
GO
/****** Object:  ForeignKey [R_362]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[ResellRateHistory]  WITH NOCHECK ADD  CONSTRAINT [R_362] FOREIGN KEY([resell_acct_id])
REFERENCES [dbo].[ResellAcct] ([resell_acct_id])
GO
ALTER TABLE [dbo].[ResellRateHistory] CHECK CONSTRAINT [R_362]
GO
/****** Object:  ForeignKey [R_220]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[ResidentialPSTN]  WITH NOCHECK ADD  CONSTRAINT [R_220] FOREIGN KEY([retail_acct_id])
REFERENCES [dbo].[RetailAccount] ([retail_acct_id])
GO
ALTER TABLE [dbo].[ResidentialPSTN] CHECK CONSTRAINT [R_220]
GO
/****** Object:  ForeignKey [R_366]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[ResidentialPSTN]  WITH NOCHECK ADD  CONSTRAINT [R_366] FOREIGN KEY([service_id])
REFERENCES [dbo].[Service] ([service_id])
GO
ALTER TABLE [dbo].[ResidentialPSTN] CHECK CONSTRAINT [R_366]
GO
/****** Object:  ForeignKey [R_222]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[ResidentialVoIP]  WITH NOCHECK ADD  CONSTRAINT [R_222] FOREIGN KEY([retail_acct_id])
REFERENCES [dbo].[RetailAccount] ([retail_acct_id])
GO
ALTER TABLE [dbo].[ResidentialVoIP] CHECK CONSTRAINT [R_222]
GO
/****** Object:  ForeignKey [R_364]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[ResidentialVoIP]  WITH NOCHECK ADD  CONSTRAINT [R_364] FOREIGN KEY([service_id])
REFERENCES [dbo].[Service] ([service_id])
GO
ALTER TABLE [dbo].[ResidentialVoIP] CHECK CONSTRAINT [R_364]
GO
/****** Object:  ForeignKey [R_218]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RetailAccount]  WITH NOCHECK ADD  CONSTRAINT [R_218] FOREIGN KEY([customer_acct_id])
REFERENCES [dbo].[CustomerAcct] ([customer_acct_id])
GO
ALTER TABLE [dbo].[RetailAccount] CHECK CONSTRAINT [R_218]
GO
/****** Object:  ForeignKey [R_249]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RetailAccountPayment]  WITH NOCHECK ADD  CONSTRAINT [R_249] FOREIGN KEY([retail_acct_id])
REFERENCES [dbo].[RetailAccount] ([retail_acct_id])
GO
ALTER TABLE [dbo].[RetailAccountPayment] CHECK CONSTRAINT [R_249]
GO
/****** Object:  ForeignKey [R_253]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RetailAccountPayment]  WITH NOCHECK ADD  CONSTRAINT [R_253] FOREIGN KEY([person_id])
REFERENCES [dbo].[Person] ([person_id])
GO
ALTER TABLE [dbo].[RetailAccountPayment] CHECK CONSTRAINT [R_253]
GO
/****** Object:  ForeignKey [R_280]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RetailAccountPayment]  WITH NOCHECK ADD  CONSTRAINT [R_280] FOREIGN KEY([balance_adjustment_reason_id])
REFERENCES [dbo].[BalanceAdjustmentReason] ([balance_adjustment_reason_id])
GO
ALTER TABLE [dbo].[RetailAccountPayment] CHECK CONSTRAINT [R_280]
GO
/****** Object:  ForeignKey [R_326]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RetailRateHistory]  WITH NOCHECK ADD  CONSTRAINT [R_326] FOREIGN KEY([rate_info_id])
REFERENCES [dbo].[RateInfo] ([rate_info_id])
GO
ALTER TABLE [dbo].[RetailRateHistory] CHECK CONSTRAINT [R_326]
GO
/****** Object:  ForeignKey [R_353]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RetailRateHistory]  WITH NOCHECK ADD  CONSTRAINT [R_353] FOREIGN KEY([retail_route_id])
REFERENCES [dbo].[RetailRoute] ([retail_route_id])
GO
ALTER TABLE [dbo].[RetailRateHistory] CHECK CONSTRAINT [R_353]
GO
/****** Object:  ForeignKey [R_355]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RetailRoute]  WITH NOCHECK ADD  CONSTRAINT [R_355] FOREIGN KEY([route_id])
REFERENCES [dbo].[Route] ([route_id])
GO
ALTER TABLE [dbo].[RetailRoute] CHECK CONSTRAINT [R_355]
GO
/****** Object:  ForeignKey [R_357]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RetailRoute]  WITH NOCHECK ADD  CONSTRAINT [R_357] FOREIGN KEY([customer_acct_id])
REFERENCES [dbo].[CustomerAcct] ([customer_acct_id])
GO
ALTER TABLE [dbo].[RetailRoute] CHECK CONSTRAINT [R_357]
GO
/****** Object:  ForeignKey [R_367]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RetailRouteBonusMinutes]  WITH NOCHECK ADD  CONSTRAINT [R_367] FOREIGN KEY([retail_route_id])
REFERENCES [dbo].[RetailRoute] ([retail_route_id])
GO
ALTER TABLE [dbo].[RetailRouteBonusMinutes] CHECK CONSTRAINT [R_367]
GO
/****** Object:  ForeignKey [R_368]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RetailRouteBonusMinutes]  WITH NOCHECK ADD  CONSTRAINT [R_368] FOREIGN KEY([retail_acct_id])
REFERENCES [dbo].[RetailAccount] ([retail_acct_id])
GO
ALTER TABLE [dbo].[RetailRouteBonusMinutes] CHECK CONSTRAINT [R_368]
GO
/****** Object:  ForeignKey [FK_Route_CallingPlan]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Route]  WITH NOCHECK ADD  CONSTRAINT [FK_Route_CallingPlan] FOREIGN KEY([calling_plan_id])
REFERENCES [dbo].[CallingPlan] ([calling_plan_id])
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_Route_CallingPlan]
GO
/****** Object:  ForeignKey [FK_Route_Country]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Route]  WITH NOCHECK ADD  CONSTRAINT [FK_Route_Country] FOREIGN KEY([country_id])
REFERENCES [dbo].[Country] ([country_id])
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_Route_Country]
GO
/****** Object:  ForeignKey [R_351]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RoutingPlan]  WITH NOCHECK ADD  CONSTRAINT [R_351] FOREIGN KEY([calling_plan_id])
REFERENCES [dbo].[CallingPlan] ([calling_plan_id])
GO
ALTER TABLE [dbo].[RoutingPlan] CHECK CONSTRAINT [R_351]
GO
/****** Object:  ForeignKey [R_352]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RoutingPlan]  WITH NOCHECK ADD  CONSTRAINT [R_352] FOREIGN KEY([virtual_switch_id])
REFERENCES [dbo].[VirtualSwitch] ([virtual_switch_id])
GO
ALTER TABLE [dbo].[RoutingPlan] CHECK CONSTRAINT [R_352]
GO
/****** Object:  ForeignKey [R_342]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RoutingPlanDetail]  WITH NOCHECK ADD  CONSTRAINT [R_342] FOREIGN KEY([routing_plan_id])
REFERENCES [dbo].[RoutingPlan] ([routing_plan_id])
GO
ALTER TABLE [dbo].[RoutingPlanDetail] CHECK CONSTRAINT [R_342]
GO
/****** Object:  ForeignKey [R_350]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[RoutingPlanDetail]  WITH NOCHECK ADD  CONSTRAINT [R_350] FOREIGN KEY([route_id])
REFERENCES [dbo].[Route] ([route_id])
GO
ALTER TABLE [dbo].[RoutingPlanDetail] CHECK CONSTRAINT [R_350]
GO
/****** Object:  ForeignKey [FK_Service_CallingPlan]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Service]  WITH NOCHECK ADD  CONSTRAINT [FK_Service_CallingPlan] FOREIGN KEY([calling_plan_id])
REFERENCES [dbo].[CallingPlan] ([calling_plan_id])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_CallingPlan]
GO
/****** Object:  ForeignKey [FK_Service_PayphoneSurcharge]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Service]  WITH NOCHECK ADD  CONSTRAINT [FK_Service_PayphoneSurcharge] FOREIGN KEY([payphone_surcharge_id])
REFERENCES [dbo].[PayphoneSurcharge] ([payphone_surcharge_id])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_PayphoneSurcharge]
GO
/****** Object:  ForeignKey [R_269]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Service]  WITH NOCHECK ADD  CONSTRAINT [R_269] FOREIGN KEY([sweep_schedule_id])
REFERENCES [dbo].[Schedule] ([schedule_id])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [R_269]
GO
/****** Object:  ForeignKey [R_299]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Service]  WITH NOCHECK ADD  CONSTRAINT [R_299] FOREIGN KEY([virtual_switch_id])
REFERENCES [dbo].[VirtualSwitch] ([virtual_switch_id])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [R_299]
GO
/****** Object:  ForeignKey [R_346]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[Service]  WITH NOCHECK ADD  CONSTRAINT [R_346] FOREIGN KEY([default_routing_plan_id])
REFERENCES [dbo].[RoutingPlan] ([routing_plan_id])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [R_346]
GO
/****** Object:  ForeignKey [R_331]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[TerminationChoice]  WITH NOCHECK ADD  CONSTRAINT [R_331] FOREIGN KEY([carrier_route_id])
REFERENCES [dbo].[CarrierRoute] ([carrier_route_id])
GO
ALTER TABLE [dbo].[TerminationChoice] CHECK CONSTRAINT [R_331]
GO
/****** Object:  ForeignKey [R_343]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[TerminationChoice]  WITH NOCHECK ADD  CONSTRAINT [R_343] FOREIGN KEY([routing_plan_id], [route_id])
REFERENCES [dbo].[RoutingPlanDetail] ([routing_plan_id], [route_id])
GO
ALTER TABLE [dbo].[TerminationChoice] CHECK CONSTRAINT [R_343]
GO
/****** Object:  ForeignKey [R_173]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[TimeOfDay]  WITH NOCHECK ADD  CONSTRAINT [R_173] FOREIGN KEY([time_of_day_policy])
REFERENCES [dbo].[TimeOfDayPolicy] ([time_of_day_policy])
GO
ALTER TABLE [dbo].[TimeOfDay] CHECK CONSTRAINT [R_173]
GO
/****** Object:  ForeignKey [R_207]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[TimeOfDayPeriod]  WITH NOCHECK ADD  CONSTRAINT [R_207] FOREIGN KEY([rate_info_id], [type_of_day_choice])
REFERENCES [dbo].[TypeOfDay] ([rate_info_id], [type_of_day_choice])
GO
ALTER TABLE [dbo].[TimeOfDayPeriod] CHECK CONSTRAINT [R_207]
GO
/****** Object:  ForeignKey [R_210]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[TimeOfDayPeriod]  WITH NOCHECK ADD  CONSTRAINT [R_210] FOREIGN KEY([time_of_day])
REFERENCES [dbo].[TimeOfDay] ([time_of_day])
GO
ALTER TABLE [dbo].[TimeOfDayPeriod] CHECK CONSTRAINT [R_210]
GO
/****** Object:  ForeignKey [R_203]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[TypeOfDay]  WITH NOCHECK ADD  CONSTRAINT [R_203] FOREIGN KEY([type_of_day_choice])
REFERENCES [dbo].[TypeOfDayChoice] ([type_of_day_choice])
GO
ALTER TABLE [dbo].[TypeOfDay] CHECK CONSTRAINT [R_203]
GO
/****** Object:  ForeignKey [R_204]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[TypeOfDay]  WITH NOCHECK ADD  CONSTRAINT [R_204] FOREIGN KEY([rate_info_id])
REFERENCES [dbo].[RateInfo] ([rate_info_id])
GO
ALTER TABLE [dbo].[TypeOfDay] CHECK CONSTRAINT [R_204]
GO
/****** Object:  ForeignKey [R_208]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[TypeOfDay]  WITH NOCHECK ADD  CONSTRAINT [R_208] FOREIGN KEY([time_of_day_policy])
REFERENCES [dbo].[TimeOfDayPolicy] ([time_of_day_policy])
GO
ALTER TABLE [dbo].[TypeOfDay] CHECK CONSTRAINT [R_208]
GO
/****** Object:  ForeignKey [R_311]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[VirtualSwitch]  WITH NOCHECK ADD  CONSTRAINT [R_311] FOREIGN KEY([contact_info_id])
REFERENCES [dbo].[ContactInfo] ([contact_info_id])
GO
ALTER TABLE [dbo].[VirtualSwitch] CHECK CONSTRAINT [R_311]
GO
/****** Object:  ForeignKey [R_334]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[WholesaleRateHistory]  WITH NOCHECK ADD  CONSTRAINT [R_334] FOREIGN KEY([wholesale_route_id])
REFERENCES [dbo].[WholesaleRoute] ([wholesale_route_id])
GO
ALTER TABLE [dbo].[WholesaleRateHistory] CHECK CONSTRAINT [R_334]
GO
/****** Object:  ForeignKey [R_335]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[WholesaleRateHistory]  WITH NOCHECK ADD  CONSTRAINT [R_335] FOREIGN KEY([rate_info_id])
REFERENCES [dbo].[RateInfo] ([rate_info_id])
GO
ALTER TABLE [dbo].[WholesaleRateHistory] CHECK CONSTRAINT [R_335]
GO
/****** Object:  ForeignKey [FK_ServiceRoute_Route]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[WholesaleRoute]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceRoute_Route] FOREIGN KEY([route_id])
REFERENCES [dbo].[Route] ([route_id])
GO
ALTER TABLE [dbo].[WholesaleRoute] CHECK CONSTRAINT [FK_ServiceRoute_Route]
GO
/****** Object:  ForeignKey [R_328]    Script Date: 03/22/2009 02:49:31 ******/
ALTER TABLE [dbo].[WholesaleRoute]  WITH NOCHECK ADD  CONSTRAINT [R_328] FOREIGN KEY([service_id])
REFERENCES [dbo].[Service] ([service_id])
GO
ALTER TABLE [dbo].[WholesaleRoute] CHECK CONSTRAINT [R_328]
GO
