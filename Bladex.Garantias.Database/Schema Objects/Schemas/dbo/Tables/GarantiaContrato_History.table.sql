CREATE TABLE [dbo].[GarantiaContrato_History] (
    [BusinessDate]             DATETIME        NOT NULL,
    [ID]                       INT             NOT NULL,
    [DealReference]            VARCHAR (50)    NULL,
    [PorcUtilization]          DECIMAL (18, 4) NULL,
    [GarantiaId]               INT             NULL,
    [FechaRegistroInicial]     DATETIME        NULL,
    [FechaVencimientoGarantia] DATETIME        NULL,
    [FechaVencimientoRiesgo]   DATETIME        NULL,
    [NetBalancePrincipal]      DECIMAL (18, 4) NULL
);

