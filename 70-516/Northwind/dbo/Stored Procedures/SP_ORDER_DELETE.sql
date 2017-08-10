-- =============================================
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