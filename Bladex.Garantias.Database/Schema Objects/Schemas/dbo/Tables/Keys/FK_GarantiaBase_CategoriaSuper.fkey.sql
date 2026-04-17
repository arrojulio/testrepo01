ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_CategoriaSuper] FOREIGN KEY ([CategoriaSuperId]) REFERENCES [dbo].[CategoriaSuper] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;

