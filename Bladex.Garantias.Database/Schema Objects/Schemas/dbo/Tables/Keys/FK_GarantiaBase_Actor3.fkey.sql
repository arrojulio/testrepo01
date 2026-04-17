ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_Actor3] FOREIGN KEY ([Administrador]) REFERENCES [dbo].[Actor] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

