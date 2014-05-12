USE CdrDb_$(DB_NAME_SUFFIX)
GO

CREATE FUNCTION dbo.IntToDottedIPAddress 
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
