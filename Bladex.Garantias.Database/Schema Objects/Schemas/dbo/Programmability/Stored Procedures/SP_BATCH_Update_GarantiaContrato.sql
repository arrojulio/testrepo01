

-- =============================================
-- Author:		Novaris FS
-- Create date: 12/23/2021
-- Description:	Proceso de carga en lote de la relacion Garantia-Contrato
-- =============================================

CREATE PROCEDURE [dbo].[SP_BATCH_Update_GarantiaContrato] 
	
AS
BEGIN

DECLARE @GarantiaId INT, @DealReference VARCHAR(50), @ImportId INT, @PorcCobertura DECIMAL(18,4), @countGarantiaRecords int, @countDealRecords int, @countGarantiaContratoRecords int

-- Establece id de carga
SET @ImportId = (SELECT isnull(MAX(ImportId),0) + 1 FROM BATCH_GarantiaContrato_Log)

-- Para cada registro por cargar
DECLARE garantia_cursor CURSOR  
    FOR SELECT GarantiaId, DealReference, PorcCobertura FROM BATCH_GarantiaContrato_Import ORDER BY GarantiaId, DealReference  
OPEN garantia_cursor  
FETCH NEXT FROM garantia_cursor into @GarantiaId, @DealReference, @PorcCobertura;

WHILE @@FETCH_STATUS = 0
BEGIN
	-- Verificacion garantiaId
	SET @countGarantiaRecords = (select count(*) from GarantiaBase where ID = @GarantiaId)
	IF @countGarantiaRecords = 0
	BEGIN
		INSERT INTO BATCH_GarantiaContrato_Log VALUES (getdate(), @ImportId, 'Error', 'GarantiaId '+ convert(varchar,@GarantiaId) +' no existente.')
	END

	--Verificacion dealReference
	SET @countDealRecords = (select count(*) from IMPORT_Finance_TransactionalInfo_FT where DEAL_REF = @DealReference)
	IF @countDealRecords = 0
	BEGIN
		INSERT INTO BATCH_GarantiaContrato_Log VALUES (getdate(), @ImportId, 'Error', 'Deal Reference '+ @DealReference +' no existente.')
	END

	-- Si existe garantiaId y dealReference
	IF @countGarantiaRecords > 0 AND @countDealRecords > 0
	--IF @countGarantiaRecords > 0	--Verificacion solo de garantiaId
	BEGIN
		--Verificacion si existe la relacion garantia-contrato
		SET @countGarantiaContratoRecords = (select count(*) from GarantiaContrato where garantiaid = @GarantiaId AND DealReference = @DealReference)
		IF @countGarantiaContratoRecords = 0
		BEGIN
			--Inserto garantia-contrato
			INSERT INTO BATCH_GarantiaContrato_Log VALUES (getdate(), @ImportId, 'New', 'Nuevo registro ingresado GarantiaId '+ convert(varchar,@GarantiaId) + ' Deal Reference '+ @DealReference +'.')
			INSERT INTO GarantiaContrato (DealReference, PorcUtilization, GarantiaId) VALUES (@DealReference, @PorcCobertura, @GarantiaId)
		END
		ELSE
		BEGIN
			--actualizo garantia-contrato
			UPDATE GarantiaContrato
			SET PorcUtilization = @PorcCobertura
			WHERE  garantiaid = @GarantiaId AND DealReference = @DealReference

			INSERT INTO BATCH_GarantiaContrato_Log VALUES (getdate(), @ImportId, 'Update', 'Registro actualizado GarantiaId '+ convert(varchar,@GarantiaId) + ' Deal Reference '+ @DealReference +'.')
		END

	END

	--proximo registro para cargar
	FETCH NEXT FROM garantia_cursor into @GarantiaId, @DealReference, @PorcCobertura;
END

CLOSE garantia_cursor  
DEALLOCATE garantia_cursor

END
