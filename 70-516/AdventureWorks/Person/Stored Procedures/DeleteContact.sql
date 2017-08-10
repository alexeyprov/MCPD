CREATE PROCEDURE [Person].[DeleteContact]
    @contactId INT,
    @modifiedDate DATETIME
AS
BEGIN
    DELETE FROM [Person].[Contact]
          WHERE ContactID = @contactId
            AND ModifiedDate = @modifiedDate;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR(
                 'ERROR - Concurrency', 
                 11,
                 1);
    END
END
