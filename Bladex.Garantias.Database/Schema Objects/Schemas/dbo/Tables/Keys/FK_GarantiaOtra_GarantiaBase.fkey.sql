ALTER TABLE [dbo].[GarantiaOtra]
    ADD CONSTRAINT [FK_GarantiaOtra_GarantiaBase] FOREIGN KEY ([ID]) REFERENCES [dbo].[GarantiaBase] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;

