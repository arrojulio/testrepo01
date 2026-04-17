ALTER TABLE [dbo].[GarantiaPrenda]
    ADD CONSTRAINT [FK_GarantiaPrenda_GarantiaBase] FOREIGN KEY ([ID]) REFERENCES [dbo].[GarantiaBase] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;





