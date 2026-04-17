ALTER TABLE [dbo].[GarantiaPrenda]
    ADD CONSTRAINT [FK_GarantiaPrenda_Actor] FOREIGN KEY ([Emisor]) REFERENCES [dbo].[Actor] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;

