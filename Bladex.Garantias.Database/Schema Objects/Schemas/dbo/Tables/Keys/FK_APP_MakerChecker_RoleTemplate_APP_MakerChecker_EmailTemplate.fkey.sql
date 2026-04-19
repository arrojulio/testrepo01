ALTER TABLE [dbo].[APP_MakerChecker_RoleTemplate]
    ADD CONSTRAINT [FK_APP_MakerChecker_RoleTemplate_APP_MakerChecker_EmailTemplate] FOREIGN KEY ([EmailTemplateId]) REFERENCES [dbo].[APP_MakerChecker_EmailTemplate] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

