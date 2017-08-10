
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