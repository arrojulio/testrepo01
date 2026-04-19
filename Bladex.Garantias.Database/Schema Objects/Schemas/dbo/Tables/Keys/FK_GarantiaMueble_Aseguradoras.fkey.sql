ALTER TABLE [dbo].[GarantiaMueble]
    ADD CONSTRAINT [FK_GarantiaMueble_Aseguradoras] FOREIGN KEY ([AseguradorSuper]) REFERENCES [dbo].[Aseguradoras] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

