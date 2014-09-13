ALTER TABLE [dbo].[PasswordResetTokens]
	ADD CONSTRAINT [FK_UserPasswordResetToken]
	FOREIGN KEY (UserId)
	REFERENCES [Users] (Id)
