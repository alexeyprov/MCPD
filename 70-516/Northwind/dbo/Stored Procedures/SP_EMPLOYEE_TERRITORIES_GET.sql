-- =============================================
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