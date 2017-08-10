CREATE PROCEDURE [Person].[UpdateAddress] (
    @addressId INT,
    @addressLine1 NVARCHAR(60),
    @addressLine2 NVARCHAR(60),
    @city NVARCHAR(30),
    @stateProvinceID INT,
    @postalCode NVARCHAR(15),
    @modifiedDate DATETIME,
    @rowsUpdated INT OUT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Person].[Address]
       SET [AddressLine1] = @addressLine1,
           [AddressLine2] = @addressLine2,
           [City] = @city,
           [StateProvinceID] = @stateProvinceID,
           [PostalCode] = @postalCode,
           [ModifiedDate] = @modifiedDate
     WHERE AddressID = @AddressId
       AND ModifiedDate = @modifiedDate;

    SELECT @rowsUpdated = @@ROWCOUNT;

    SELECT ModifiedDate
      FROM Person.[Address]
     WHERE AddressID = @addressId
       AND @rowsUpdated = 1;

END
