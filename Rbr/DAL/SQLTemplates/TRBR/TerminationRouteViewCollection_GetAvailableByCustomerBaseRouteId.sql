-- GET ACTIVE CARRIER/ROUTES THAT COVER CustomerDialCodes for provided Route 
-- TO SHOW IN AVAILABLE LIST for LCR 

--DEBUG
/*
--*****************************************
-- THESE HAS TO BE PASSED AS PARAMS
DECLARE @CustomerBaseRouteId int
DECLARE @IsLCRMode tinyint -- 0=false; 1=true
--*****************************************


SET @CustomerBaseRouteId = 250--151--250--250--
SET @IsLCRMode = 1

DECLARE @carr_dc_count int
*/

DECLARE @country_id int
DECLARE @country_code int
SET @country_id = (SELECT country_id FROM Route WHERE route_id = @CustomerBaseRouteId) --145--142--
SET @country_code = (SELECT country_code FROM Country WHERE country_id = @country_id) --55--52--

DECLARE @cust_dc_len int

DECLARE @cust_dcodes TABLE (dial_code bigint PRIMARY KEY) 
DECLARE @temp_cust_dcodes TABLE (dial_code bigint PRIMARY KEY) 

DECLARE @carr_dcodes TABLE (dial_code bigint, route_id int) 
DECLARE @temp_full_carr_coverage TABLE (route_id int, dial_code bigint, PRIMARY KEY (route_id, dial_code))
DECLARE @temp_part_carr_coverage TABLE (route_id int, dial_code bigint, PRIMARY KEY (route_id, dial_code))

DECLARE @full_coverage_carr_routes TABLE (route_id int, PRIMARY KEY (route_id))
DECLARE @part_coverage_carr_routes TABLE (route_id int, PRIMARY KEY (route_id))
DECLARE @carr_coverage_routes TABLE (route_id int, partial_coverage tinyint, PRIMARY KEY (route_id))


--**********************************************************************
--LOAD EXISTING CUSTOMER DIAL CODES
INSERT INTO @cust_dcodes 
	SELECT /*TOP(11)*/ DialCode.dial_code
	FROM  DialCode 
	WHERE DialCode.route_id = @CustomerBaseRouteId 
	ORDER BY DialCode.dial_code
	--ORDER BY CAST(DialCode.dial_code AS varchar)
--**********************************************************************
SET @cust_dc_len = (SELECT LEN(MAX(dial_code)) FROM @cust_dcodes)

--**********************************************************************
-- LOAD ALL ACTIVE CARRIER DIAL CODES
IF (@IsLCRMode > 0)
	BEGIN
		--LOAD ALL CARRIER DIAL CODES (for specified country) 
		--EXCLUDING CARRIERS with Disabled Rating
		--NOTE: this exlusion is done to avoid processing extra routes
		INSERT INTO @carr_dcodes 
			SELECT DISTINCT DialCode.dial_code, DialCode.route_id
			FROM  CarrierAcct INNER JOIN
			CarrierRoute ON CarrierAcct.carrier_acct_id = CarrierRoute.carrier_acct_id INNER JOIN
			Route ON CarrierRoute.route_id = Route.route_id INNER JOIN
			DialCode ON Route.route_id = DialCode.route_id
			WHERE 
			CarrierAcct.rating_type > 0 --rating anabled
			AND 
			Route.route_id <> @CustomerBaseRouteId --NOTE this carrier route should be added to the final list without any checking
			AND 
			CarrierRoute.status = 1 -- Active
			AND 
			Route.status = 1 -- Active
			AND 
			Route.country_id = @country_id
	END
ELSE --MANUAL (no filter on rating_type)
	BEGIN
		--LOAD ALL CARRIER DIAL CODES (for specified country) 
		--INCLUDING CARRIERS with Disabled Rating
		INSERT INTO @carr_dcodes 
			SELECT DISTINCT DialCode.dial_code, DialCode.route_id
			FROM  CarrierAcct INNER JOIN
			CarrierRoute ON CarrierAcct.carrier_acct_id = CarrierRoute.carrier_acct_id INNER JOIN
			Route ON CarrierRoute.route_id = Route.route_id INNER JOIN
			DialCode ON Route.route_id = DialCode.route_id
			WHERE 
			Route.route_id <> @CustomerBaseRouteId --NOTE this carrier route should be added to the final list without any checking
			AND 
			CarrierRoute.status = 1 -- Active
			AND 
			Route.status = 1 -- Active
			AND 
			Route.country_id = @country_id
	END
-- END LOAD ALL ACTIVE CARRIER DIAL CODES
--**********************************************************************

--DEBUG
--SET @carr_dc_count = (SELECT COUNT(*) FROM @carr_dcodes)
--PRINT N'TOTAL Carr DialCodes: ' + CAST(@carr_dc_count AS NVARCHAR)

--**********************************************************************
--NOTE: SKIPPED ABOVE, NOW ADD CUSTOMER ROUTE TO CARRIER ROUTE LIST in case if any carrier uses same route/callingPlan as provided customer's route
INSERT INTO @full_coverage_carr_routes VALUES(@CustomerBaseRouteId)
--**********************************************************************

WHILE (@cust_dc_len >= LEN(@country_code)) 
BEGIN
--DEBUG
--PRINT N'PROCESSING dc len: ' + CAST(@cust_dc_len AS NVARCHAR)

	--************************************************************
	-- FULL CONERAGE *********************************************
	-- CREATE CUST DC SUBSET for the @cust_dc_len
	DELETE FROM @temp_cust_dcodes

	INSERT INTO @temp_cust_dcodes
	SELECT dial_code FROM @cust_dcodes
	WHERE LEN(dial_code) = @cust_dc_len

	-- SELECT matching carr dcs into @temp_full_carr_coverage
	DELETE FROM @temp_full_carr_coverage

	INSERT INTO @temp_full_carr_coverage
	SELECT CRDC.route_id, CRDC.dial_code 
	FROM @carr_dcodes CRDC, @temp_cust_dcodes TCUDC
	WHERE CRDC.dial_code = TCUDC.dial_code

	-- INSERT matching routes into @full_coverage_carr_routes
	INSERT INTO @full_coverage_carr_routes 
	SELECT DISTINCT route_id 
	FROM @temp_full_carr_coverage

	-- DELETE @temp_full_carr_coverage codes by route FROM @carr_dcodes
	DELETE FROM @carr_dcodes 
	WHERE route_id IN (SELECT route_id FROM @temp_full_carr_coverage)
	-- END FULL CONERAGE *****************************************
	--************************************************************


	--************************************************************
	-- PARTIAL CONERAGE ******************************************
	-- CREATE CUST DC SUBSET for the @cust_dc_len

	-- SELECT matching carr dcs into @temp_full_carr_coverage
	DELETE FROM @temp_part_carr_coverage

	INSERT INTO @temp_part_carr_coverage
	SELECT DISTINCT CRDC.route_id, LEFT(CRDC.dial_code, @cust_dc_len)
	FROM @carr_dcodes CRDC, @temp_cust_dcodes TCUDC
	WHERE 
	LEN(CRDC.dial_code) > @cust_dc_len
	AND 
	LEFT(CRDC.dial_code, @cust_dc_len) = TCUDC.dial_code

	-- INSERT matching routes into @full_coverage_carr_routes
	INSERT INTO @part_coverage_carr_routes SELECT DISTINCT route_id FROM @temp_part_carr_coverage

	-- DELETE @temp_part_carr_coverage codes by route FROM @carr_dcodes
	DELETE FROM @carr_dcodes WHERE route_id IN (SELECT route_id FROM @temp_part_carr_coverage)
	-- END PARTIAL CONERAGE **************************************
	--************************************************************

	SET @cust_dc_len = @cust_dc_len - 1
END

--DEBUG
/*
SELECT * FROM @full_coverage_carr_routes
SELECT * FROM @part_coverage_carr_routes
*/
INSERT INTO @carr_coverage_routes
SELECT route_id, 0 FROM @full_coverage_carr_routes

INSERT INTO @carr_coverage_routes
SELECT route_id, 1 FROM @part_coverage_carr_routes


--DEBUG
--SELECT * FROM @carr_coverage_routes

--*****************************************************************
-- GET FINAL FULL INFO
IF (@IsLCRMode > 0)
	BEGIN
		--EXCLUDING CARRIERS with Disabled Rating
		--NOTE: this exlusion is done to avoid returning extra routes
		SELECT DISTINCT 
		CarrierRoute.carrier_route_id, Route.name AS route_name, 
		CarrierAcct.carrier_acct_id, CarrierAcct.name AS carrier_acct_name, 
		CarrierAcct.calling_plan_id, 
		CarrierAcct.rating_type, Route.route_id AS base_route_id, 
		CRCR.partial_coverage

		FROM  CarrierAcct INNER JOIN
		   CarrierRoute ON CarrierAcct.carrier_acct_id = CarrierRoute.carrier_acct_id INNER JOIN
		   Route ON CarrierRoute.route_id = Route.route_id INNER JOIN
		   @carr_coverage_routes CRCR ON Route.route_id = CRCR.route_id
		WHERE 
		CarrierAcct.rating_type > 0 --rating anabled
		AND 
		CarrierAcct.status = 1 --Active
		AND 
		CarrierRoute.status = 1 --Active
		AND 
		Route.status = 1 --Active

		ORDER BY 
		CarrierAcct.name, 
		Route.name 
	END
ELSE -- MANUAL
	BEGIN
		--INCLUDING CARRIERS with Disabled Rating
		SELECT DISTINCT 
		CarrierRoute.carrier_route_id, Route.name AS route_name, 
		CarrierAcct.carrier_acct_id, CarrierAcct.name AS carrier_acct_name, 
		CarrierAcct.calling_plan_id, 
		CarrierAcct.rating_type, Route.route_id AS base_route_id, 
		CRCR.partial_coverage
		FROM  CarrierAcct INNER JOIN
		   CarrierRoute ON CarrierAcct.carrier_acct_id = CarrierRoute.carrier_acct_id INNER JOIN
		   Route ON CarrierRoute.route_id = Route.route_id INNER JOIN
		   @carr_coverage_routes CRCR ON Route.route_id = CRCR.route_id
		WHERE 
		CarrierAcct.status = 1 --Active
		AND 
		CarrierRoute.status = 1 --Active
		AND 
		Route.status = 1 --Active

		ORDER BY 
		CarrierAcct.name, 
		Route.name 
	END
