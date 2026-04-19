ALTER TABLE [dbo].[APP_MakerChecker_Operation]
    ADD CONSTRAINT [FK_APP_MakerChecker_Core_APP_MakerChecker_Changeset] FOREIGN KEY ([ChangesetID]) REFERENCES [dbo].[APP_MakerChecker_Changeset] ([ChangesetID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

