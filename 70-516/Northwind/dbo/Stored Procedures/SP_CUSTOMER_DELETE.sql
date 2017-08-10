-- =============================================
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