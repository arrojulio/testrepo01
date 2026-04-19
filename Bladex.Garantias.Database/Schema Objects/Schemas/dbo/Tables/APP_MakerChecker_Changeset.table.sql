CREATE TABLE [dbo].[APP_MakerChecker_Changeset] (
    [ChangesetID]         UNIQUEIDENTIFIER NOT NULL,
    [MakerUserID]         VARCHAR (32)     NOT NULL,
    [ChangesetDate]       DATETIME         NOT NULL,
    [ChangesetCommitDate] DATETIME         NULL,
    [ChangesetComment]    NVARCHAR (MAX)   NULL
);



