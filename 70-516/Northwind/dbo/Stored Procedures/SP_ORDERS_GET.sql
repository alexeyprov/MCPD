-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ORDERS_GET] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ORDERID,
           CUSTOMERID,
           EMPLOYEEID,
           ORDERDATE,
           REQUIREDDATE,
           SHIPPEDDATE,
           FREIGHT,
           SHIPVIA,
           SHIPADDRESS,
           SHIPCITY,
           SHIPREGION,
           SHIPPOSTALCODE,
           SHIPCOUNTRY
      FROM ORDERS;
END