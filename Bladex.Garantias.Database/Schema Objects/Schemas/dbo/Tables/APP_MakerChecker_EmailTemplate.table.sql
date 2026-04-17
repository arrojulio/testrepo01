CREATE TABLE [dbo].[APP_MakerChecker_EmailTemplate] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [TemplateName]      NVARCHAR (100) NOT NULL,
    [Subject]           NVARCHAR (100) NULL,
    [Body]              NVARCHAR (MAX) NOT NULL,
    [MakerIdentifier]   NVARCHAR (50)  NULL,
    [CheckerIdentifier] NVARCHAR (50)  NULL,
    [DataIdentifier]    NVARCHAR (200) NOT NULL,
    [Cc]                NVARCHAR (MAX) NULL,
    [Bcc]               NVARCHAR (MAX) NULL
);



