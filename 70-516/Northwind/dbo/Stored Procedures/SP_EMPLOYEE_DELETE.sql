-- =============================================
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