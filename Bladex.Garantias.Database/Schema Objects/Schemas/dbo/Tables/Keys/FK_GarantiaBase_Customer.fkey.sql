ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_Customer] FOREIGN KEY ([Cliente]) REFERENCES [dbo].[Customer] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;

