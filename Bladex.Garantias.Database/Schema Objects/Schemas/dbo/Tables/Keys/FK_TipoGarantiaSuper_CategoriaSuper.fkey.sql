ALTER TABLE [dbo].[TipoGarantiaSuper]
    ADD CONSTRAINT [FK_TipoGarantiaSuper_CategoriaSuper] FOREIGN KEY ([IdCategoriaSuper]) REFERENCES [dbo].[CategoriaSuper] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

