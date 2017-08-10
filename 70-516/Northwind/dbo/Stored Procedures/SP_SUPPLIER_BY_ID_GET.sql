


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SUPPLIER_BY_ID_GET] 
    @PNI_SUPPLIER_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT SUPPLIERID,
           COMPANYNAME,
           CONTACTNAME,
           CONTACTTITLE,
           ADDRESS,
           CITY,
           COUNTRY,
           POSTALCODE,
           PHONE,
           REGION
      FROM SUPPLIERS
     WHERE SUPPLIERID = @PNI_SUPPLIER_ID;
END