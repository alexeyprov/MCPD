


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SUPPLIERS_GET]
    @PNI_START_ROW INT,
    @PNI_ROW_COUNT INT
AS
BEGIN
	DECLARE @N_END_ROW INT;
	
	SELECT @N_END_ROW = @PNI_START_ROW + @PNI_ROW_COUNT;

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Q.*
      FROM (SELECT SUPPLIERID,
				   COMPANYNAME,
				   CONTACTNAME,
                   CONTACTTITLE,
				   ADDRESS,
				   CITY,
				   COUNTRY,
				   POSTALCODE,
				   PHONE,
				   REGION,
				   ROW_NUMBER() OVER (ORDER BY SUPPLIERID) RNUM
			  FROM SUPPLIERS) Q
     WHERE Q.RNUM BETWEEN @PNI_START_ROW + 1 AND @N_END_ROW;
     
END