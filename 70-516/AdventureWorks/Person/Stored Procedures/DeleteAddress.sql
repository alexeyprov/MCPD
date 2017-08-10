CREATE PROCEDURE [Person].[DeleteAddress]
    @addressId INT,
    @modifiedDate DATETIME
AS
BEGIN
    DELETE FROM [Person].[Address]
          WHERE AddressID = @addressId
            AND ModifiedDate = @modifiedDate;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR(
                 'ERROR - Concurrency', 
                 11,
                 1);
    END
END
