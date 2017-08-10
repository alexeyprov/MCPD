-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_TERRITORY_UPD]
	-- Add the parameters for the stored procedure here
	@PVI_TERRITORY_ID NVARCHAR(20),
	@PVI_TERRITORY_DESC NCHAR(50),
	@PNI_REGION_ID INT
AS
BEGIN
	DECLARE @N_EXISTS INT;

	SELECT @N_EXISTS = COUNT(*)
	  FROM TERRITORIES
     WHERE TERRITORYID = @PVI_TERRITORY_ID;

	IF 1 = @N_EXISTS
		UPDATE TERRITORIES
		   SET TERRITORYDESCRIPTION = @PVI_TERRITORY_DESC,
			   REGIONID = @PNI_REGION_ID
		 WHERE TERRITORYID = @PVI_TERRITORY_ID;
	ELSE
		INSERT INTO TERRITORIES (TERRITORYID,
								 TERRITORYDESCRIPTION,
								 REGIONID)
						 VALUES (@PVI_TERRITORY_ID,
								 @PVI_TERRITORY_DESC,
								 @PNI_REGION_ID);
END