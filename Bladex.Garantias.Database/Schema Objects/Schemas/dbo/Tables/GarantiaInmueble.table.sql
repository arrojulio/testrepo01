CREATE TABLE [dbo].[GarantiaInmueble] (
    [ID]                         INT           NOT NULL,
    [AseguradorSuper]            VARCHAR (5)   NULL,
    [ValorEvaluacionVentaRapida] MONEY         NULL,
    [ValorAvaluo]                MONEY         NULL,
    [InscripcionRegistroPublico] VARCHAR (100) NULL,
    [NumeroDeFinca]              VARCHAR (100) NULL,
    [FechaInicialAvaluo] [datetime] NULL,
	[FechaVencimientoAvaluo] [datetime] NULL,
	[ValorTotalAvaluo] [money] NULL
);







