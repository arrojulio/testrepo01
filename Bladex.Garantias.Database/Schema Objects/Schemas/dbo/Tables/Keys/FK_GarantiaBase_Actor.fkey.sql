ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_Actor] FOREIGN KEY ([Depositante]) REFERENCES [dbo].[Actor] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

