ALTER TABLE [dbo].[GarantiaPrenda]
    ADD CONSTRAINT [FK_GarantiaPrenda_InstrumentoFinanciero] FOREIGN KEY ([TipoInstrumentoFinanciero]) REFERENCES [dbo].[InstrumentoFinanciero] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;

