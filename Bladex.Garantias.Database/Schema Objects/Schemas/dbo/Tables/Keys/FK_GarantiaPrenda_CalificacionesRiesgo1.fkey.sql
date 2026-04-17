ALTER TABLE [dbo].[GarantiaPrenda]
    ADD CONSTRAINT [FK_GarantiaPrenda_CalificacionesRiesgo1] FOREIGN KEY ([CalificacionEmisor]) REFERENCES [dbo].[CalificacionesRiesgo] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

