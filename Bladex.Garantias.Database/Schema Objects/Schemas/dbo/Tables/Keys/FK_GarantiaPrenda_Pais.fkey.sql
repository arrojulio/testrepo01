ALTER TABLE [dbo].[GarantiaPrenda]
    ADD CONSTRAINT [FK_GarantiaPrenda_Pais] FOREIGN KEY ([PaisEmision]) REFERENCES [dbo].[Pais] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;

