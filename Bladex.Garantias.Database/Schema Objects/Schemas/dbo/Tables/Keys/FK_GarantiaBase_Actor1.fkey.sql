ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_Actor1] FOREIGN KEY ([Evaluador]) REFERENCES [dbo].[Actor] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

