ALTER TABLE [dbo].[GarantiaDepositoOtroBanco]
    ADD CONSTRAINT [FK_GarantiaDepositoOtroBanco_GarantiaBase] FOREIGN KEY ([ID]) REFERENCES [dbo].[GarantiaBase] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;





