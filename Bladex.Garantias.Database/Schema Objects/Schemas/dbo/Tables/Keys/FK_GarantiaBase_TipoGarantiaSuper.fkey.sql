ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_TipoGarantiaSuper] FOREIGN KEY ([TipoGarantiaSuper]) REFERENCES [dbo].[TipoGarantiaSuper] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

