SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CUSTOMERS_GET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CUSTOMERS_GET] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
      FROM CUSTOMERS;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CUSTOMER_BY_ID_GET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CUSTOMER_BY_ID_GET] 
	-- Add the parameters for the stored procedure here
	@CUSTOMER_ID NCHAR(5) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
      FROM CUSTOMERS
     WHERE CUSTOMERID = @CUSTOMER_ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CUSTOMER_UPD]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CUSTOMER_UPD]
	-- Add the parameters for the stored procedure here
	@PVI_CUSTOMER_ID NCHAR(5),
	@PVI_COMPANY_NAME NVARCHAR(40) = NULL, 
	@PVI_CONTACT_NAME NVARCHAR(30) = NULL, 
	@PVI_CONTACT_TITLE NVARCHAR(30) = NULL,
	@PVI_ADDRESS NVARCHAR(60) = NULL,
	@PVI_CITY NVARCHAR(15) = NULL,
	@PVI_REGION NVARCHAR(15) = NULL,
	@PVI_POSTAL_CODE NVARCHAR(10) = NULL,
	@PVI_COUNTRY NVARCHAR(15) = NULL,
	@PVI_PHONE NVARCHAR(24) = NULL,
	@PVI_FAX NVARCHAR(24) = NULL
AS
BEGIN
	DECLARE @N_EXISTS INT;

	SELECT @N_EXISTS = COUNT(*)
	  FROM CUSTOMERS
     WHERE CUSTOMERID = @PVI_CUSTOMER_ID;

	IF 1 = @N_EXISTS
		UPDATE CUSTOMERS
		   SET COMPANYNAME = @PVI_COMPANY_NAME ,
			   CONTACTNAME = @PVI_CONTACT_NAME,
			   CONTACTTITLE = @PVI_CONTACT_TITLE,
			   ADDRESS = @PVI_ADDRESS,
			   CITY = @PVI_CITY,
			   REGION = @PVI_REGION,
			   POSTALCODE = @PVI_POSTAL_CODE,
			   COUNTRY = @PVI_COUNTRY,
			   PHONE = @PVI_PHONE,
			   FAX = @PVI_FAX
		 WHERE CUSTOMERID = @PVI_CUSTOMER_ID;
	ELSE
		INSERT INTO CUSTOMERS (CUSTOMERID,
							   COMPANYNAME, 
							   CONTACTNAME, 
							   CONTACTTITLE,
							   ADDRESS,
							   CITY,
							   REGION,
							   POSTALCODE,
							   COUNTRY,
							   PHONE,
							   FAX)
					   VALUES (@PVI_CUSTOMER_ID,
							   @PVI_COMPANY_NAME, 
						 	   @PVI_CONTACT_NAME, 
							   @PVI_CONTACT_TITLE,
							   @PVI_ADDRESS,
							   @PVI_CITY,
							   @PVI_REGION,
							   @PVI_POSTAL_CODE,
							   @PVI_COUNTRY,
							   @PVI_PHONE,
							   @PVI_FAX);
	
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CUSTOMER_DELETE]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CUSTOMER_DELETE]
	-- Add the parameters for the stored procedure here
	@PVI_CUSTOMER_ID NCHAR(5)
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM CUSTOMERS
 		  WHERE CUSTOMERID = @PVI_CUSTOMER_ID;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CUSTOMER_COUNT_GET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CUSTOMER_COUNT_GET] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    
	SELECT COUNT(*)
      FROM CUSTOMERS;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_ORDERS_GET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ORDERS_GET] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ORDERID,
           CUSTOMERID,
           EMPLOYEEID,
           ORDERDATE,
           REQUIREDDATE,
           SHIPPEDDATE,
           FREIGHT,
           SHIPVIA,
           SHIPADDRESS,
           SHIPCITY,
           SHIPREGION,
           SHIPPOSTALCODE,
           SHIPCOUNTRY
      FROM ORDERS;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_ORDER_BY_ID_GET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ORDER_BY_ID_GET] 
	-- Add the parameters for the stored procedure here
	@PNI_ORDER_ID INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ORDERID,
           CUSTOMERID,
           EMPLOYEEID,
           ORDERDATE,
           REQUIREDDATE,
           SHIPPEDDATE,
           FREIGHT,
           SHIPVIA,
           SHIPADDRESS,
           SHIPCITY,
           SHIPREGION,
           SHIPPOSTALCODE,
           SHIPCOUNTRY
      FROM ORDERS
     WHERE ORDERID = @PNI_ORDER_ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_ORDER_COUNT_GET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ORDER_COUNT_GET] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	SELECT COUNT(*)
      FROM ORDERS;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_ORDER_DELETE]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ORDER_DELETE]
	-- Add the parameters for the stored procedure here
	@PNI_ORDER_ID INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM ORDERS
 		  WHERE ORDERID = @PNI_ORDER_ID;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_ORDER_UPD]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ORDER_UPD]
	-- Add the parameters for the stored procedure here
	@PNI_ORDER_ID INT,
	@PVI_CUSTOMER_ID NCHAR(5), 
	@PNI_EMPLOYEE_ID INT = NULL, 
	@PNI_SHIPPER_ID INT = NULL,
	@PVI_ADDRESS NVARCHAR(60) = NULL,
	@PVI_CITY NVARCHAR(15) = NULL,
	@PVI_REGION NVARCHAR(15) = NULL,
	@PVI_POSTAL_CODE NVARCHAR(10) = NULL,
	@PVI_COUNTRY NVARCHAR(15) = NULL,
	@PDI_ORDER_DATE DATETIME = NULL,
	@PDI_REQUIRED_DATE DATETIME = NULL,
    @PDI_SHIPPED_DATE DATETIME = NULL
AS
BEGIN
	DECLARE @N_EXISTS INT;

	SELECT @N_EXISTS = COUNT(*)
	  FROM ORDERS
     WHERE ORDERID = @PNI_ORDER_ID;

	IF 1 = @N_EXISTS
		UPDATE ORDERS
		   SET CUSTOMERID = @PVI_CUSTOMER_ID ,
			   EMPLOYEEID = @PNI_EMPLOYEE_ID,
			   SHIPVIA = @PNI_SHIPPER_ID,
			   SHIPADDRESS = @PVI_ADDRESS,
			   SHIPCITY = @PVI_CITY,
			   SHIPREGION = @PVI_REGION,
			   SHIPPOSTALCODE = @PVI_POSTAL_CODE,
			   SHIPCOUNTRY = @PVI_COUNTRY,
			   ORDERDATE = @PDI_ORDER_DATE,
			   REQUIREDDATE = @PDI_REQUIRED_DATE,
               SHIPPEDDATE = @PDI_SHIPPED_DATE
		 WHERE ORDERID = @PNI_ORDER_ID;
	ELSE
		INSERT INTO ORDERS (ORDERID,
							CUSTOMERID, 
							EMPLOYEEID, 
							SHIPVIA,
							SHIPADDRESS,
							SHIPCITY,
							SHIPREGION,
							SHIPPOSTALCODE,
							SHIPCOUNTRY,
							ORDERDATE,
							REQUIREDDATE,
                            SHIPPEDDATE)
    			    VALUES (@PNI_ORDER_ID,
				            @PVI_CUSTOMER_ID, 
				            @PNI_EMPLOYEE_ID, 
						    @PNI_SHIPPER_ID,
						    @PVI_ADDRESS,
						    @PVI_CITY,
						    @PVI_REGION,
						    @PVI_POSTAL_CODE,
						    @PVI_COUNTRY,
						    @PDI_ORDER_DATE,
						    @PDI_REQUIRED_DATE,
                            @PDI_SHIPPED_DATE);
	
END

GRANT EXECUTE ON SP_ORDER_UPD TO kosh

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_TERRITORIES_BY_REGION_GET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_TERRITORIES_BY_REGION_GET]
	-- Add the parameters for the stored procedure here
	@PNI_REGION_ID INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TERRITORYID,
		   TERRITORYDESCRIPTION
      FROM TERRITORIES
     WHERE REGIONID = @PNI_REGION_ID
		OR @PNI_REGION_ID IS NULL
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_TERRITORY_UPD]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_TERRITORY_UPD]
	-- Add the parameters for the stored procedure here
	@PVI_TERRITORY_ID NVARCHAR(20),
	@PVI_TERRITORY_DESC NCHAR(50),
	@PNI_REGION_ID INT
AS
BEGIN
	DECLARE @N_EXISTS INT;

	SELECT @N_EXISTS = COUNT(*)
	  FROM TERRITORIES
     WHERE TERRITORYID = @PVI_TERRITORY_ID;

	IF 1 = @N_EXISTS
		UPDATE TERRITORIES
		   SET TERRITORYDESCRIPTION = @PVI_TERRITORY_DESC,
			   REGIONID = @PNI_REGION_ID
		 WHERE TERRITORYID = @PVI_TERRITORY_ID;
	ELSE
		INSERT INTO TERRITORIES (TERRITORYID,
								 TERRITORYDESCRIPTION,
								 REGIONID)
						 VALUES (@PVI_TERRITORY_ID,
								 @PVI_TERRITORY_DESC,
								 @PNI_REGION_ID);
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_EMPLOYEE_TERRITORIES_GET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_EMPLOYEE_TERRITORIES_GET]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT E.FIRSTNAME,
           E.LASTNAME,
           T.TERRITORYID,
           T.TERRITORYDESCRIPTION
      FROM EMPLOYEETERRITORIES ET
     INNER JOIN EMPLOYEES E ON E.EMPLOYEEID = ET.EMPLOYEEID 
     INNER JOIN TERRITORIES AS T ON T.TERRITORYID = ET.TERRITORYID
END

GRANT EXECUTE ON SP_EMPLOYEE_TERRITORIES_GET TO kosh
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_EMPLOYEES_GET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_EMPLOYEES_GET] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT EMPLOYEEID,
           FIRSTNAME,
           LASTNAME,
           TITLE,
           BIRTHDATE,
           HIREDATE,
           ADDRESS,
           CITY,
           REGION,
           POSTALCODE,
           COUNTRY,
           NOTES
      FROM EMPLOYEES;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_EMPLOYEE_UPD]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_EMPLOYEE_UPD]
	-- Add the parameters for the stored procedure here
	@PNI_EMPLOYEE_ID INT,
	@PVI_FIRST_NAME NVARCHAR(10), 
	@PVI_LAST_NAME NVARCHAR(20), 
	@PVI_TITLE NVARCHAR(30) = NULL,
    @PDI_BIRTH_DATE DATETIME = NULL,
    @PDI_HIRE_DATE DATETIME = NULL,
	@PVI_ADDRESS NVARCHAR(60) = NULL,
	@PVI_CITY NVARCHAR(15) = NULL,
	@PVI_REGION NVARCHAR(15) = NULL,
	@PVI_POSTAL_CODE NVARCHAR(10) = NULL,
	@PVI_COUNTRY NVARCHAR(15) = NULL,
    @PVI_NOTES NTEXT = NULL
AS
BEGIN
	DECLARE @N_EXISTS INT;

	SELECT @N_EXISTS = COUNT(*)
	  FROM EMPLOYEES
     WHERE EMPLOYEEID = @PNI_EMPLOYEE_ID;

	IF 1 = @N_EXISTS
		UPDATE EMPLOYEES
		   SET FIRSTNAME = @PVI_FIRST_NAME ,
			   LASTNAME = @PVI_LAST_NAME,
			   TITLE = @PVI_TITLE,
               BIRTHDATE = @PDI_BIRTH_DATE,
               HIREDATE = @PDI_HIRE_DATE,
			   ADDRESS = @PVI_ADDRESS,
			   CITY = @PVI_CITY,
			   REGION = @PVI_REGION,
			   POSTALCODE = @PVI_POSTAL_CODE,
			   COUNTRY = @PVI_COUNTRY,
               NOTES = @PVI_NOTES
		 WHERE EMPLOYEEID = @PNI_EMPLOYEE_ID;
	ELSE
		INSERT INTO EMPLOYEES (EMPLOYEEID,
							   FIRSTNAME, 
							   LASTNAME, 
							   TITLE,
							   BIRTHDATE,
							   HIREDATE,
                               ADDRESS,
                               CITY,
							   REGION,
							   POSTALCODE,
							   COUNTRY,
                               NOTES)
					   VALUES (@PNI_EMPLOYEE_ID,
							   @PVI_FIRST_NAME, 
						 	   @PVI_LAST_NAME, 
							   @PVI_TITLE,
                               @PDI_BIRTH_DATE,
                               @PDI_HIRE_DATE,
							   @PVI_ADDRESS,
							   @PVI_CITY,
							   @PVI_REGION,
							   @PVI_POSTAL_CODE,
							   @PVI_COUNTRY,
                               @PVI_NOTES);
	
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_EMPLOYEE_DELETE]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_EMPLOYEE_DELETE]
	-- Add the parameters for the stored procedure here
	@PNI_EMPLOYEE_ID INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM EMPLOYEES
     WHERE EMPLOYEEID = @PNI_EMPLOYEE_ID;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_EMPLOYEE_COUNT_GET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_EMPLOYEE_COUNT_GET] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    
	SELECT COUNT(*)
      FROM EMPLOYEES;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_EMPLOYEE_BY_ID_GET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_EMPLOYEE_BY_ID_GET] 
    @PNI_EMPLOYEE_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT EMPLOYEEID,
           FIRSTNAME,
           LASTNAME,
           TITLE,
           BIRTHDATE,
           HIREDATE,
           ADDRESS,
           CITY,
           REGION,
           POSTALCODE,
           COUNTRY,
           NOTES
      FROM EMPLOYEES
     WHERE EMPLOYEEID = @PNI_EMPLOYEE_ID;
END
' 
END

/****** Object:  StoredProcedure [dbo].[SP_SUPPLIER_BY_ID_GET]    Script Date: 12/25/2010 19:47:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SUPPLIER_BY_ID_GET] 
    @PNI_SUPPLIER_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT SUPPLIERID,
           COMPANYNAME,
           CONTACTNAME,
           CONTACTTITLE,
           ADDRESS,
           CITY,
           COUNTRY,
           POSTALCODE,
           PHONE,
           REGION
      FROM SUPPLIERS
     WHERE SUPPLIERID = @PNI_SUPPLIER_ID;
END


GO

/****** Object:  StoredProcedure [dbo].[SP_SUPPLIER_COUNT_GET]    Script Date: 12/25/2010 19:47:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SUPPLIER_COUNT_GET] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    
	SELECT COUNT(*)
      FROM SUPPLIERS;
END


GO


/****** Object:  StoredProcedure [dbo].[SP_SUPPLIERS_GET]    Script Date: 12/25/2010 19:48:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SUPPLIERS_GET]
    @PNI_START_ROW INT,
    @PNI_ROW_COUNT INT
AS
BEGIN
	DECLARE @N_END_ROW INT;
	
	SELECT @N_END_ROW = @PNI_START_ROW + @PNI_ROW_COUNT;

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Q.*
      FROM (SELECT SUPPLIERID,
				   COMPANYNAME,
				   CONTACTNAME,
                   CONTACTTITLE,
				   ADDRESS,
				   CITY,
				   COUNTRY,
				   POSTALCODE,
				   PHONE,
				   REGION,
				   ROW_NUMBER() OVER (ORDER BY SUPPLIERID) RNUM
			  FROM SUPPLIERS) Q
     WHERE Q.RNUM BETWEEN @PNI_START_ROW + 1 AND @N_END_ROW;
     
END


GO

USE [Northwind]
GO

/****** Object:  StoredProcedure [dbo].[SP_LINES_BY_ORDER_GET]    Script Date: 01/02/2011 00:18:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_LINES_BY_ORDER_GET]
	-- Add the parameters for the stored procedure here
	@PNI_ORDER_ID INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ORDERID,
		   PRODUCTID,
		   DISCOUNT,
		   QUANTITY,
		   UNITPRICE
      FROM [ORDER DETAILS]
     WHERE ORDERID = @PNI_ORDER_ID
		OR @PNI_ORDER_ID IS NULL
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_PRODUCT_STATISTICS_GET]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT TOP(5)
		   PRODUCTNAME,
		   UNITSINSTOCK
	  FROM DBO.PRODUCTS
	 WHERE DISCONTINUED = 'FALSE'
END
GO


/****** Object:  StoredProcedure [dbo].[SP_REGIONS_GET]    Script Date: 04/10/2011 23:42:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REGIONS_GET] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT *
      FROM REGION;
END
GO

