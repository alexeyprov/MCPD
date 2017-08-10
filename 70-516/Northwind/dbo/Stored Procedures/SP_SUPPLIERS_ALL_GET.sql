CREATE PROCEDURE [dbo].[SP_SUPPLIERS_ALL_GET]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT SUPPLIERID,
           COMPANYNAME,
           CONTACTNAME,
           CONTACTTITLE,
           [ADDRESS],
           CITY,
           COUNTRY,
           POSTALCODE,
           PHONE,
           REGION
      FROM dbo.SUPPLIERS
END
