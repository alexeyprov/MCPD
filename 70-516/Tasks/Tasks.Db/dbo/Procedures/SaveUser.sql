CREATE PROCEDURE [dbo].[SaveUser]
    @userId uniqueidentifier,
    @firstname nvarchar(50),
    @lastname nvarchar(50)
AS
BEGIN

    SET NOCOUNT ON;

                MERGE dbo.[User]
            AS TARGET
                USING (
               SELECT @userId,
                      @firstname,
                      @lastname
) 
                   AS source (
                      UserId,
                      Firstname,
                      Lastname
)
                   ON target.UserId = source.UserId
    WHEN MATCHED THEN
           UPDATE SET Firstname = target.Firstname,
                      Lastname = target.Lastname
WHEN NOT MATCHED THEN
               INSERT (
                      UserId, 
                      Firstname, 
                      Lastname
)
               VALUES (
                      source.UserId,
                      source.Firstname,
                      source.Lastname);

END
