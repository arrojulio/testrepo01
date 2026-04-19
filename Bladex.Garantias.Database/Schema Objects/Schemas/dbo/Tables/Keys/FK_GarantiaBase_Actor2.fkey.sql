ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_Actor2] FOREIGN KEY ([Revisor]) REFERENCES [dbo].[Actor] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

