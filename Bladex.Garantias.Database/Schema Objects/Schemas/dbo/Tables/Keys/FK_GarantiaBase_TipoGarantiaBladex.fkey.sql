ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_TipoGarantiaBladex] FOREIGN KEY ([TipoGarantiaBladex]) REFERENCES [dbo].[TipoGarantiaBladex] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

