CREATE VIEW [dbo].[AllUsers]
AS
SELECT u.UserId,
       u.FirstName,
       u.LastName,
       u.ts,
       am.Email,
       au.UserName
  FROM [$(AspNetDb)].dbo.aspnet_Membership am
 INNER JOIN [$(AspNetDb)].dbo.aspnet_Users au ON au.UserId = am.UserId
 INNER JOIN dbo.[User] u ON u.UserId = au.UserId;

