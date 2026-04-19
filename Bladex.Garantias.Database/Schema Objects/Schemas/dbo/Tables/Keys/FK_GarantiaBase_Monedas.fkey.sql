ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_Monedas] FOREIGN KEY ([Moneda]) REFERENCES [dbo].[Monedas] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;

