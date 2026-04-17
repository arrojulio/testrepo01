ALTER TABLE [dbo].[GarantiaDepositoOtroBanco]
    ADD CONSTRAINT [FK_GarantiaDepositoOtroBanco_Bancos] FOREIGN KEY ([BancoSuper]) REFERENCES [dbo].[Bancos] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;

