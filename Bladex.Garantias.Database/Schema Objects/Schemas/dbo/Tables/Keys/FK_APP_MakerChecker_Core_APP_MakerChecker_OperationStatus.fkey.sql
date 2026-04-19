ALTER TABLE [dbo].[APP_MakerChecker_Operation]
    ADD CONSTRAINT [FK_APP_MakerChecker_Core_APP_MakerChecker_OperationStatus] FOREIGN KEY ([OperationStatusID]) REFERENCES [dbo].[APP_MakerChecker_OperationStatus] ([OperationStatusID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

