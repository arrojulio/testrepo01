ALTER TABLE [dbo].[GarantiaBase]
    ADD CONSTRAINT [FK_GarantiaBase_InternalStatus] FOREIGN KEY ([InternalStatus]) REFERENCES [dbo].[InternalStatus] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;

