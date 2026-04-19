ALTER TABLE [dbo].[APP_MakerChecker_User]
    ADD CONSTRAINT [FK_APP_MakerChecker_User_Application_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Application_User] ([UserId]) ON DELETE CASCADE ON UPDATE NO ACTION;

