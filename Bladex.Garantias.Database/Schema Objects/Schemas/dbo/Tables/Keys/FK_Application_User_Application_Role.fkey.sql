ALTER TABLE [dbo].[Application_User]
    ADD CONSTRAINT [FK_Application_User_Application_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Application_Role] ([RoleId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

