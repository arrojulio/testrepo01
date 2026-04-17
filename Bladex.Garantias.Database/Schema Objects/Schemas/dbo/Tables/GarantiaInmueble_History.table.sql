CREATE TABLE [dbo].[GarantiaInmueble_History] (
    [BusinessDate]               DATETIME      NOT NULL,
    [ID]                         INT           NOT NULL,
    [AseguradorSuper]            VARCHAR (5)   NULL,
    [ValorEvaluacionVentaRapida] MONEY         NULL,
    [ValorAvaluo]                MONEY         NULL,
    [InscripcionRegistroPublico] VARCHAR (100) NULL,
    [NumeroDeFinca]              VARCHAR (100) NULL
);



