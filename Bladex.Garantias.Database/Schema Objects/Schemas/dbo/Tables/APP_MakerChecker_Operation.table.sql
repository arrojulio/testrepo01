CREATE TABLE [dbo].[APP_MakerChecker_Operation] (
    [OperationID]       INT              IDENTITY (1, 1) NOT NULL,
    [ChangesetID]       UNIQUEIDENTIFIER NOT NULL,
    [CheckerUserID]     VARCHAR (32)     NULL,
    [CheckerDate]       DATETIME         NULL,
    [MakerDate]         DATETIME         NOT NULL,
    [OperationStatusID] INT              NOT NULL,
    [ItemID]            INT              NULL,
    [Item]              NVARCHAR (MAX)   NOT NULL,
    [ItemType]          NVARCHAR (255)   NULL,
    [Comment]           NVARCHAR (MAX)   NULL
);





