using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Services.Components.MakerChecker;
using Bladex.Garantias.Presentation.Website.Models;
using Bladex.Garantias.Presentation.Website.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bladex.Garantias.UnitTests.DomainModel.Services
{
    [TestClass]
    public class MakerCheckerServiceTest
    {
        [TestInitialize()]
        public void Initialize()
        {
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            Bladex.Garantias.Presentation.Website.Bootstrapper.Setup();
        }

        [TestMethod]
        public void CreateModelFromGarantiaOtra()
        {
            var operacion = ServiceFacade.Instance.MakerCheckerService.GetOperation(87);
            Assert.IsNotNull(operacion);
            operacion.GetMakerCheckerObject();
            var garantia = ServiceFacade.Instance.GarantiaOtraService.GetById(10217);
            Assert.IsNotNull(garantia);
            GarantiaOtraModel model = (GarantiaOtraModel)garantia.CreateModel();
            Assert.IsNotNull(model);
            List<Aval> avales = garantia.Avales;
            Assert.IsNotNull(avales);
            List<AvalViewModel> avalesModel = model.AvalList;
            Assert.IsNotNull(avalesModel);
            Console.WriteLine("Garantia Key: {0}", garantia.Key);
            Console.WriteLine("Garantia Model Key: {0}", model.Key);
            Console.WriteLine("Garantia Avales: {0}", avales.Count);
            Console.WriteLine("Garantia Model Avales: {0}", avalesModel.Count);
        }

        [TestMethod]
        public void SendEmailToCheckerTest()
        {
            MakerCheckerService svc = new MakerCheckerService();
            var changesets = svc.GetChangesets("maker");
            MakerCheckerCoreCommitEventArgs<GarantiaBase> checkerArgs = new MakerCheckerCoreCommitEventArgs<GarantiaBase>();
            checkerArgs.ChangesetId = changesets.First().ChangesetId;
            checkerArgs.ChangesetDate = changesets.First().ChangesetDate;
            checkerArgs.ChangesetCommitDate = changesets.First().ChangesetCommitDate;
            checkerArgs.MakerUserId = changesets.First().MakerUserId;
            var operations = svc.GetOperations(changesets.First().ChangesetId);
            List<MakerCheckerCoreSaveOperationEventArgs<GarantiaBase>> opArgs = new List<MakerCheckerCoreSaveOperationEventArgs<GarantiaBase>>();
            foreach (var o in operations)
            {
                MakerCheckerCoreSaveOperationEventArgs<GarantiaBase> operationArgument = new MakerCheckerCoreSaveOperationEventArgs<GarantiaBase>();
                
                operationArgument.ChangesetCommitDate = o.Changeset.ChangesetCommitDate;
                operationArgument.ChangesetDate = o.Changeset.ChangesetDate;
                operationArgument.ChangesetId = o.Changeset.ChangesetId;
                operationArgument.CheckerUserId = o.CheckerUserId;
                AutoMapper.Mapper.CreateMap(Type.GetType(o.ItemType), typeof(MakerCheckerObject<GarantiaBase>));
                operationArgument.MakerCheckerObject = AutoMapper.Mapper.Map(o.GetMakerCheckerObject(), Type.GetType(o.ItemType), typeof(MakerCheckerObject<GarantiaBase>));
                operationArgument.MakerDate = o.MakerDate;
                operationArgument.MakerUserId = o.Changeset.MakerUserId;
                operationArgument.OperationId = o.OperationId;
                operationArgument.OperationStatus = o.OperationStatus.OperationStatusDescription;
                operationArgument.OperationStatusId = o.OperationStatusId;
                opArgs.Add(operationArgument);
            }
            checkerArgs.Operations = opArgs.ToArray();
            svc.SendEmailToChecker(checkerArgs);


            
        }

        [TestMethod]
        public void SendEmailToMakerOperationTest()
        {
            MakerCheckerService svc = new MakerCheckerService();
            // send notification to maker
            var args = new MakerCheckerCoreUpdateOperationEventArgs<GarantiaBase>();
            var result = svc.GetOperations(svc.GetChangesets("maker").First().ChangesetId).First();
            args.ChangesetCommitDate = result.Changeset.ChangesetCommitDate;
            args.ChangesetDate = result.Changeset.ChangesetDate;
            args.ChangesetId = result.Changeset.ChangesetId;
            args.CheckerDate = result.CheckerDate;
            args.CheckerUserId = result.CheckerUserId;
            AutoMapper.Mapper.CreateMap(Type.GetType(result.ItemType), typeof(MakerCheckerObject<GarantiaBase>));
            args.MakerCheckerObject = AutoMapper.Mapper.Map(result.GetMakerCheckerObject(), Type.GetType(result.ItemType), typeof(MakerCheckerObject<GarantiaBase>));
            args.MakerDate = result.MakerDate;
            args.MakerUserId = result.Changeset.MakerUserId;
            args.OperationComment = result.Comment;
            args.OperationId = result.OperationId;
            args.OperationStatus = result.OperationStatus != null ? result.OperationStatus.OperationStatusDescription : string.Empty;
            args.OperationStatusId = result.OperationStatusId;
           
            svc.SendEmailToMakerOperation(args);
        }
    }
}
