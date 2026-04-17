ALTER TABLE [dbo].[APP_MakerChecker_RoleTemplate]
    ADD CONSTRAINT [FK_APP_MakerChecker_RoleTemplate_APP_MAKERCHECKER_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[APP_MakerChecker_Role] ([RoleId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

