ALTER TABLE [dbo].[GarantiaInmueble]
    ADD CONSTRAINT [FK_GarantiaInmueble_Avaluadoras] FOREIGN KEY ([AseguradorSuper]) REFERENCES [dbo].[Avaluadoras] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

