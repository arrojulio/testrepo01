ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_CustomerGarante] FOREIGN KEY ([Garante]) REFERENCES [dbo].[Customer] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

