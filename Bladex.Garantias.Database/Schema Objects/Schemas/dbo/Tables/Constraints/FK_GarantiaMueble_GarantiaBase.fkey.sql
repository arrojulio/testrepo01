ALTER TABLE [dbo].[GarantiaMueble]
    ADD CONSTRAINT [FK_GarantiaMueble_GarantiaBase] FOREIGN KEY ([ID]) REFERENCES [dbo].[GarantiaBase] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;





