
CREATE TABLE [dbo].[IMPORT_TH_ATOMO_GARANTIAS] (
    [ID]                         NUMERIC (18)    NOT NULL,
    [Fecha_corte]                DATETIME        NULL,
    [FECHA_SIB]                  VARCHAR (10)    NULL,
    [CODIGO_BANCO]               VARCHAR (3)     NULL,
    [RUC_DEUDOR]                 VARCHAR (45)    NULL,
    [IDENTIFICACION_FIDEICOMISO] VARCHAR (30)    NULL,
    [NOMBRE_FIDUCIARIA]          VARCHAR (3)     NULL,
    [ORIGEN_GARANTIA]            VARCHAR (1)     NULL,
    [TIPO_GARANTIA]              VARCHAR (4)     NULL,
    [IDENTIFICACION_GARANTIA]    VARCHAR (30)    NULL,
    [NOMBRE_ORGANISMO]           VARCHAR (30)    NULL,
    [VALOR_INICIAL]              NUMERIC (18, 3) NULL,
    [VALOR_GARANTIA]             NUMERIC (18, 3) NULL,
    [TIPO_INSTRUMENTO]           VARCHAR (2)     NULL,
    [CALIFICACION_INSTRUMENTO]   VARCHAR (2)     NULL,
    [CALIFICACION_EMISION]       VARCHAR (2)     NULL,
    [PAIS_EMISION]               VARCHAR (3)     NULL,
    [FECHA_ULTIMA_ACT]           VARCHAR (10)    NULL,
    [FECHA_VENCIMIENTO]          VARCHAR (10)    NULL,
    [id_sib]                     VARCHAR (50)    NULL,
    [numero_prestamo]            VARCHAR (50)    NULL,
    [cliente_garantia]           VARCHAR (50)    NULL,
    [nombre_cliente_garantia]    NVARCHAR (110)  NULL,
    [cliente_prestamo]           VARCHAR (50)    NULL,
    [nombre_cliente_prestamo]    NVARCHAR (110)  NULL,
    [APLICANTE]                  NVARCHAR (200)  NULL,
    [BENEFICIARIO]               NVARCHAR (200)  NULL,
    [ORIGEN_TRANS]               VARCHAR (50)    NULL,
    [IND_ATOMO]                  VARCHAR (50)    NULL,
    [DEAL_STAT_FROM_DT]          DATETIME        NULL,
    [DEAL_CLOSURE_DATE]          DATETIME        NULL,
    [EFFECTIVE_DATE]             DATETIME        NULL
);




