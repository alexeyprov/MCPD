CREATE PROCEDURE [Person].[InsertContact] (
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
    @additionalContactInfo XML
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [Person].[Contact] (
                [NameStyle],
                [Title],
                [FirstName],
                [MiddleName],
                [LastName],
                [Suffix],
                [EmailAddress],
                [EmailPromotion],
                [Phone],
                [PasswordHash],
                [PasswordSalt],
                [AdditionalContactInfo]
    )
         VALUES (
                @nameStyle,
                @title,
                @firstName,
                @middleName,
                @lastName,
                @suffix,
                @emailAddress,
                @emailPromotion,
                @phone,
                @passwordHash,
                @passwordSalt,
                @additionalContactInfo
    );

    SELECT ModifiedDate,
           rowguid,
           ContactID
      FROM Person.Contact
     WHERE ContactID = SCOPE_IDENTITY()
       AND @@ROWCOUNT = 1;

END
