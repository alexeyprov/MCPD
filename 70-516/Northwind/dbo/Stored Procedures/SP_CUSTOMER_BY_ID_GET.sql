-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CUSTOMER_BY_ID_GET] 
    -- Add the parameters for the stored procedure here
    @CUSTOMER_ID NCHAR(5) 
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT *
      FROM CUSTOMERS
     WHERE CUSTOMERID = @CUSTOMER_ID
END