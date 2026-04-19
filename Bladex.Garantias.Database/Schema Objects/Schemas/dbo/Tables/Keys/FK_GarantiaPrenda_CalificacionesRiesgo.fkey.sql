ALTER TABLE [dbo].[GarantiaPrenda]
    ADD CONSTRAINT [FK_GarantiaPrenda_CalificacionesRiesgo] FOREIGN KEY ([CalificacionEmision]) REFERENCES [dbo].[CalificacionesRiesgo] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

