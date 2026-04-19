CREATE TABLE [dbo].[Customer] (
    [ID]                   VARCHAR (10)  NOT NULL,
    [Nombre]               VARCHAR (150) NOT NULL,
    [CountryID]            VARCHAR (3)   NULL,
    [Rating]               DECIMAL (18)  NULL,
    [EconomicGroup]        VARCHAR (150) NULL,
    [NationalID]           VARCHAR (20)  NULL,
    [BUSINESS_LINE_DESC_2] VARCHAR (150) NULL,
    [AccountOfficer]       VARCHAR (150) NULL,
    [GLOBAL_LINE_DESC]     VARCHAR (150) NULL,
    [LIMIT_EXP_DATE]       VARCHAR (150) NULL,
    [RECORD_STAT]          VARCHAR (50)  NULL,
    [AUTH_STAT]            VARCHAR (50)  NULL,
    [CUSTOMER_ACTIVE]      BIT           NULL,
    [Internal]             BIT           NOT NULL
);















