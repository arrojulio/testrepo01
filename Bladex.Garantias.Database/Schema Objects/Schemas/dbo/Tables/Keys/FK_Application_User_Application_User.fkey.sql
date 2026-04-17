ALTER TABLE [dbo].[Application_User]
    ADD CONSTRAINT [FK_Application_User_Application_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Application_User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

