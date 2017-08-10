-- =============================================
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