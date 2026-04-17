CREATE TABLE [dbo].[Aval] (
    [ID]                  INT          IDENTITY (1, 1) NOT NULL,
    [GarantiaId]          INT          NOT NULL,
    [Nombre]              VARCHAR (50) NULL,
    [PorcentajeCobertura] FLOAT        NULL,
    [CountryId]           VARCHAR (3)  NULL,
    [EsCliente]           BIT          NULL,
    [TipoAvalId]          VARCHAR (50) NULL
);




