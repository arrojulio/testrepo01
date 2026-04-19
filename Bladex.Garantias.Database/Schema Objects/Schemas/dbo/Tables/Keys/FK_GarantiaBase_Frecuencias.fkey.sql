ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_Frecuencias] FOREIGN KEY ([FrequenciaRevision]) REFERENCES [dbo].[Frecuencias] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

