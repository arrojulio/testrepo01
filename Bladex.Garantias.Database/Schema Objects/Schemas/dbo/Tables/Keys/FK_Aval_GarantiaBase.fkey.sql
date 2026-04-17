ALTER TABLE [dbo].[Aval]
    ADD CONSTRAINT [FK_Aval_GarantiaBase] FOREIGN KEY ([GarantiaId]) REFERENCES [dbo].[GarantiaBase] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;

