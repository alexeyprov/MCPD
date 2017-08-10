
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