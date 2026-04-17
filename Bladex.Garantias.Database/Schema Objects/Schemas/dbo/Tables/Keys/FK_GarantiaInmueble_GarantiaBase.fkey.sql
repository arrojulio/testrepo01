ALTER TABLE [dbo].[GarantiaInmueble]
    ADD CONSTRAINT [FK_GarantiaInmueble_GarantiaBase] FOREIGN KEY ([ID]) REFERENCES [dbo].[GarantiaBase] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;





