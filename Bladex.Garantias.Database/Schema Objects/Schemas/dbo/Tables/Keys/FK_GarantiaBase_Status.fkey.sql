ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_Status] FOREIGN KEY ([Status]) REFERENCES [dbo].[Status] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;





