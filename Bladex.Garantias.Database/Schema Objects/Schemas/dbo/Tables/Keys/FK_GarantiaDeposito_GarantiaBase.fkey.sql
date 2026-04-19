ALTER TABLE [dbo].[GarantiaDeposito]
    ADD CONSTRAINT [FK_GarantiaDeposito_GarantiaBase] FOREIGN KEY ([ID]) REFERENCES [dbo].[GarantiaBase] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;





