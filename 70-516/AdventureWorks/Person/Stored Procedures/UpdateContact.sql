CREATE PROCEDURE [Person].[UpdateContact] (
    @contactId INT,
    @nameStyle dbo.NameStyle,
    @title NVARCHAR(8),
    @firstName dbo.Name,
    @middleName dbo.Name,
    @lastName dbo.Name,
    @suffix NVARCHAR(10),
    @emailAddress NVARCHAR(50),
    @emailPromotion INT,
    @phone dbo.Phone,
    @passwordHash VARCHAR(128),
    @passwordSalt VARCHAR(10),
    @additionalContactInfo XML,
    @modifiedDate DATETIME,
    @rowsUpdated INT OUT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Person].[Contact]
       SET [NameStyle] = @nameStyle,
           [Title] = @title,
           [FirstName] = @firstName,
           [MiddleName] = @middleName,
           [LastName] = @lastName,
           [Suffix] = @suffix,
           [EmailAddress] = @emailAddress,
           [EmailPromotion] = @emailPromotion,
           [Phone] = @phone,
           [PasswordHash] = @passwordHash,
           [PasswordSalt] = @passwordSalt,
           [AdditionalContactInfo] = @additionalContactInfo,
           [ModifiedDate] = GETDATE()
     WHERE ContactID = @contactId
       AND ModifiedDate = @modifiedDate;

    SELECT @rowsUpdated = @@ROWCOUNT;

    SELECT ModifiedDate
      FROM Person.Contact
     WHERE ContactID = @contactId
       AND @rowsUpdated = 1;

END
