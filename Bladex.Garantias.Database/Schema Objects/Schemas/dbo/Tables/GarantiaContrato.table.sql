CREATE TABLE [dbo].[GarantiaContrato] (
    [ID]                       INT             IDENTITY (1, 1) NOT NULL,
    [DealReference]            VARCHAR (50)    NULL,
    [PorcUtilization]          DECIMAL (18, 4) NULL,
    [GarantiaId]               INT             NULL,
    [FechaRegistroInicial]     DATETIME        NULL,
    [FechaVencimientoGarantia] DATETIME        NULL,
    [FechaVencimientoRiesgo]   DATETIME        NULL,
    [NetBalancePrincipal]      DECIMAL (18, 4) NULL
);




