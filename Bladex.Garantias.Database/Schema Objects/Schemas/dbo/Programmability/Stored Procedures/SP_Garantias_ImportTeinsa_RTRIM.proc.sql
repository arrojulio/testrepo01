-- =============================================
-- Author:		Noivaris
-- Create date: 2/16/2011
-- Description:	Elimina los espacios en blanco a la derecha para los campos en blanco de la informacion proveniente de Teinsa
-- =============================================
CREATE PROCEDURE SP_Garantias_ImportTeinsa_RTRIM
AS
BEGIN
	
	UPDATE [BLX_GARANTIAS].[dbo].[IMPORT_TH_ATOMO_GARANTIAS]
	   SET [FECHA_SIB] = RTRIM([FECHA_SIB])
		  ,[CODIGO_BANCO] = RTRIM([CODIGO_BANCO])
		  ,[RUC_DEUDOR] = RTRIM([RUC_DEUDOR])
		  ,[IDENTIFICACION_FIDEICOMISO] = RTRIM([IDENTIFICACION_FIDEICOMISO])
		  ,[NOMBRE_FIDUCIARIA] = RTRIM([NOMBRE_FIDUCIARIA])
		  ,[ORIGEN_GARANTIA] = RTRIM([ORIGEN_GARANTIA])
		  ,[TIPO_GARANTIA] = RTRIM([TIPO_GARANTIA])
		  ,[IDENTIFICACION_GARANTIA] = RTRIM([IDENTIFICACION_GARANTIA])
		  ,[NOMBRE_ORGANISMO] = RTRIM([NOMBRE_ORGANISMO])
		  ,[TIPO_INSTRUMENTO] = RTRIM([TIPO_INSTRUMENTO])
		  ,[CALIFICACION_INSTRUMENTO] = RTRIM([CALIFICACION_INSTRUMENTO])
		  ,[CALIFICACION_EMISION] = RTRIM([CALIFICACION_EMISION])
		  ,[PAIS_EMISION] = RTRIM([PAIS_EMISION])
		  ,[FECHA_ULTIMA_ACT] = RTRIM([FECHA_ULTIMA_ACT])
		  ,[FECHA_VENCIMIENTO] = RTRIM([FECHA_VENCIMIENTO])
		  ,[id_sib] = RTRIM([id_sib])
		  ,[numero_prestamo] = RTRIM([numero_prestamo])
		  ,[cliente_garantia] = RTRIM([cliente_garantia])
		  ,[nombre_cliente_garantia] = RTRIM([nombre_cliente_garantia])
		  ,[cliente_prestamo] = RTRIM([cliente_prestamo])
		  ,[nombre_cliente_prestamo] = RTRIM([nombre_cliente_prestamo])
		  ,[APLICANTE] = RTRIM([APLICANTE])
		  ,[BENEFICIARIO] = RTRIM([BENEFICIARIO])
		  ,[ORIGEN_TRANS] = RTRIM([ORIGEN_TRANS])


 
END