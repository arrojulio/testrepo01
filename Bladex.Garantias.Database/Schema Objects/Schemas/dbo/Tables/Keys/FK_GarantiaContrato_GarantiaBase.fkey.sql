ALTER TABLE [dbo].[GarantiaContrato]
    ADD CONSTRAINT [FK_GarantiaContrato_GarantiaBase] FOREIGN KEY ([GarantiaId]) REFERENCES [dbo].[GarantiaBase] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

