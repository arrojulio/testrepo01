/*CREATE VIEW [dbo].[VW_Garantias_GarantiaGarantiaContrato]
AS


SELECT     G.ID AS GarantiaId, GC.ID AS ContratoId, G.ID_Atomo, G.FCCReference, G.IdentificadorGarantia, G.Cliente AS CustomerId, Cliente.Nombre AS CustomerName, 
                      G.Beneficiario, G.IdentificacionFideicomiso, G.OrigenGarantia, G.PaisGarantia, G.getIdentificacionDocumentoGarantia, G.getNombreOrganismo, G.ValorInicial, 
                      G.getValorGarantiaSuperIntendencia, G.DescripcionDeLaGarantia, G.AttachedToLine, G.FechaRegistroInicial AS FechaRegistroInicialGarantia, 
                      GC.FechaRegistroInicial AS FechaRegistroInicialContrato, G.FechaFormalizacion, G.FechaVencimientoRiesgo AS FechaVencimientoRiesgoGarantia, 
                      GC.FechaVencimientoRiesgo AS FechaVencimientoRiesgoContrato, G.FechaVencimientoGarantia, 
                      GC.FechaVencimientoGarantia AS FechaVencimientoGarantiaContrato, G.FechaUltimaRevisionEvaluacion, G.FechaProximaRevisionEvaluacion, 
                      G.FechaVencimientoSeguro, G.CategoriaRiesgoGarantia AS CategoriaRiesgoGarantiaId, CRG.Nombre AS CategoriaRiesgoGarantiaNombre, 
                      G.ReduccionDeRiesgoPorPais, G.ValorNecesarioDeGarantia, G.getRatioCoberturaGarantia, G.PorcentajeAplicableMitigacionSuperInt, 
                      G.Comentarios, G.RatingGarante, G.ValorPolizaSeguro, G.NumeroPolizaSeguro, G.ValorMercado, G.Status AS StatusID, S.Nombre AS StatusNombre, 
                      G.CodigoBanco AS BancoId, Bancos.Nombre AS BancoNombre, Bancos.Categoria AS BancoCategoria, G.FiduciariaSuper AS FiduciariaSuperId, 
                      FiduciariaSuper.Nombre AS FiduciariaSuperNombre, G.Fiduciaria AS FiduciariaId, Fiduciarias.Nombre AS FiduciariaNombre, G.Depositante AS DepositanteId, 
                      Depositante.Nombre AS DepositanteNombre, Depositante.Pais AS DepositantePais, G.Evaluador AS EvaluadorId, Evaluador.Nombre AS EvaluadorNombre, 
                      Evaluador.Pais AS EvaluadorPais, G.Administrador AS AdministradorId, Administrador.Nombre AS AdministradorNombre, Administrador.Pais AS AdministradorPais, 
                      G.Asegurador AS AseguradorId, Asegurador.Nombre AS AseguradorNombre, Asegurador.Pais AS AseguradorPais, G.Revisor AS RevisorId, 
                      Revisor.Nombre AS RevisorNombre, Revisor.Pais AS RevisorPais, G.TipoGarantiaSuper AS TipoGarantiaSuperId, 
                      TipoGarantiaSuper.Nombre AS TipoGarantiaSuperNombre, G.TipoGarantiaBladex AS TipoGarantiaBladexId, 
                      TipoGarantiaBladex.Nombre AS TipoGarantiaBladexNombre, G.Garante AS GaranteId, Garante.Nombre AS GaranteNombre, 
                      G.FrequenciaRevision AS FrecuenciaRevisionId, Frecuencias.Nombre AS FrecuenciaRevisionNombre, G.Moneda AS MonedaId, Monedas.Nombre AS MonedaNombre, 
                      G.CategoriaSuperId, CS.Nombre AS CategoriaSuperNombre
FROM         GarantiaBase AS G LEFT OUTER JOIN
                      Customer AS Cliente ON G.Cliente = Cliente.ID LEFT OUTER JOIN
                      Bancos ON G.CodigoBanco = Bancos.ID LEFT OUTER JOIN
                      Fiduciarias AS FiduciariaSuper ON G.FiduciariaSuper = FiduciariaSuper.ID LEFT OUTER JOIN
                      Fiduciarias ON G.Fiduciaria = Fiduciarias.ID LEFT OUTER JOIN
                      Actor AS Depositante ON G.Depositante = Depositante.ID LEFT OUTER JOIN
                      Actor AS Evaluador ON G.Evaluador = Evaluador.ID LEFT OUTER JOIN
                      Actor AS Administrador ON G.Administrador = Administrador.ID LEFT OUTER JOIN
                      Actor AS Asegurador ON G.Asegurador = Asegurador.ID LEFT OUTER JOIN
                      Actor AS Revisor ON G.Revisor = Revisor.ID LEFT OUTER JOIN
                      TipoGarantiaSuper ON G.TipoGarantiaSuper = TipoGarantiaSuper.ID LEFT OUTER JOIN
                      TipoGarantiaBladex ON G.TipoGarantiaBladex = TipoGarantiaBladex.ID LEFT OUTER JOIN
                      Customer AS Garante ON G.Garante = Garante.ID LEFT OUTER JOIN
                      Frecuencias ON G.FrequenciaRevision = Frecuencias.ID LEFT OUTER JOIN
                      Monedas ON G.Moneda = Monedas.ID LEFT OUTER JOIN
                      GarantiaContrato AS GC ON G.ID = GC.GarantiaId LEFT OUTER JOIN
                      CategoriaSuper AS CS ON G.CategoriaSuperId = CS.ID LEFT OUTER JOIN
                      Status AS S ON G.Status = S.ID LEFT OUTER JOIN
                      CategoriaRiesgoGarantia AS CRG ON G.CategoriaRiesgoGarantia = CRG.ID
WHERE G.Status = 1*/