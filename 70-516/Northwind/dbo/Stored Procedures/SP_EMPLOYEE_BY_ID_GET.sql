
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