CREATE TABLE [dbo].[SyncLog] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [TimeStamp]    DATETIME      NOT NULL,
    [Status]       VARCHAR (50)  NOT NULL,
    [FechaCorte]   DATETIME      NULL,
    [ItemsAdded]   VARCHAR (MAX) NULL,
    [ItemsUpdated] VARCHAR (MAX) NULL,
    [ItemsDeleted] VARCHAR (MAX) NULL,
    [Message]      VARCHAR (MAX) NULL
);



