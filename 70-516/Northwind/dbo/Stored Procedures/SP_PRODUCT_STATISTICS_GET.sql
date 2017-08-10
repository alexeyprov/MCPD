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