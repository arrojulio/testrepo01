ALTER TABLE [dbo].[GarantiaDeposito]
    ADD CONSTRAINT [FK_GarantiaDeposito_Bancos] FOREIGN KEY ([BancoLocalSuper]) REFERENCES [dbo].[Bancos] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;

