-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_TERRITORIES_BY_REGION_GET]
	-- Add the parameters for the stored procedure here
	@PNI_REGION_ID INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TERRITORYID,
		   TERRITORYDESCRIPTION
      FROM TERRITORIES
     WHERE REGIONID = @PNI_REGION_ID
		OR @PNI_REGION_ID IS NULL
END