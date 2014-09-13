CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] NVARCHAR(50) NOT NULL, 
	[EmailAddress] NVARCHAR(100) NOT NULL,
	[PasswordSalt] BINARY(32) NOT NULL,
    [PasswordHash] BINARY(32) NOT NULL, 
    [FailedLoginAttempts] INT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] DATETIME NULL 
    
)
