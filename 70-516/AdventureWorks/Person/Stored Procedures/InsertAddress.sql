CREATE PROCEDURE [Person].[InsertAddress] (
    @addressLine1 NVARCHAR(60),
    @addressLine2 NVARCHAR(60),
    @city NVARCHAR(30),
    @stateProvinceID INT,
    @postalCode NVARCHAR(15)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [Person].[Address] (
                [AddressLine1],
                [AddressLine2],
                [City],
                [StateProvinceID],
                [PostalCode]
     )
         VALUES (
                @addressLine1,
                @addressLine2,
                @city,
                @stateProvinceID,
                @postalCode
    );

    SELECT ModifiedDate,
           rowguid,
           AddressID
      FROM Person.[Address]
     WHERE AddressID = SCOPE_IDENTITY()
       AND @@ROWCOUNT = 1;

END