

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