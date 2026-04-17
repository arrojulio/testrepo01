using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using static Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerRole;

namespace Bladex.Garantias.DomainModel.Services.Components.MakerChecker
{
    /// <summary>
    /// The maker checker service class.
    /// </summary>
    public class MakerCheckerService
    {
        /// <summary>
        ///   <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerUserRepository"/>
        /// </summary>
        protected readonly IMakerCheckerUserRepository UserRepository;
        /// <summary>
        ///   <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerRoleRepository"/>
        /// </summary>
        protected readonly IMakerCheckerRoleRepository RoleRepository;
        /// <summary>
        ///   <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerEmailTemplateRepository"/>
        /// </summary>
        protected readonly IMakerCheckerEmailTemplateRepository EmailTemplateRepository;

        protected readonly IMakerCheckerOperationStatusRepository OperationStatusRepository;

        protected readonly IMakerCheckerOperationRepository OperationRepository;

        protected readonly IMakerCheckerChangesetRepository ChangesetRepository;


        private const string _EMAIL_NOTIFICATION_DATA_CHECKER = "<b>Changeset Commit Date:</b> {0}<br/><b>Changeset Comment:</b> {1}<br/>";
        //private const string _EMAIL_NOTIFICATION_DATA_MAKER = "<b>Changeset Identifier:</b> {4}<br/><b>Changeset Commit Date:</b> {3}<br/><b>Operations</b><br/><b>Operation Status:</b> {1}<br/><b>Comments:</b>{2}<br/>";
        private const string _EMAIL_NOTIFICATION_DATA_MAKER = "<br/><b>Changeset Commit Date:</b> {0}<br/><b>Total operations:</b> {1}.<br/><b>Approved operations:</b> {2}<br/><b>Rejected operations:</b> {3}<br/>";

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerUserRepository"/></param>
        /// <param name="roleRepository">The role repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerRoleRepository"/></param>
        /// <param name="emailTemplateRepository">The email template repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerEmailTemplateRepository"/></param>
        /// <param name="operationStatusRepository">The operation status repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerOperationStatusRepository"/></param>
        /// <param name="changesetRepository">The changeset repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerChangesetRepository"/></param>
        /// <param name="operationRepository">The operation repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerOperationRepository"/></param>
        public MakerCheckerService(IMakerCheckerUserRepository userRepository, IMakerCheckerRoleRepository roleRepository, IMakerCheckerEmailTemplateRepository emailTemplateRepository, IMakerCheckerOperationStatusRepository operationStatusRepository, IMakerCheckerChangesetRepository changesetRepository, IMakerCheckerOperationRepository operationRepository)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            EmailTemplateRepository = emailTemplateRepository;
            OperationStatusRepository = operationStatusRepository;
            ChangesetRepository = changesetRepository;
            OperationRepository = operationRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerService"/> class.
        /// </summary>
        public MakerCheckerService()
        {
            UserRepository = RepositoryFactory.GetRepository<IMakerCheckerUserRepository, MakerCheckerUser>();
            RoleRepository = RepositoryFactory.GetRepository<IMakerCheckerRoleRepository, MakerCheckerRole>();
            EmailTemplateRepository = RepositoryFactory.GetRepository<IMakerCheckerEmailTemplateRepository, MakerCheckerEmailTemplate>();
            OperationStatusRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationStatusRepository, MakerCheckerOperationStatus>(); ;
            ChangesetRepository = RepositoryFactory.GetRepository<IMakerCheckerChangesetRepository, MakerCheckerChangeset>(); ;
            OperationRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationRepository, MakerCheckerOperation>(); ; ;
        }

        #region User
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        public List<MakerCheckerUser> GetUsers()
        {
            return UserRepository.GetAll().ToList();
        }

        /// <summary>
        /// Gets the maker users.
        /// </summary>
        /// <returns></returns>
        public List<MakerCheckerUser> GetMakerUsers()
        {
            // TODO: Configurar id de role maker
            return UserRepository.GetAll().Where(o => o.Role.RoleId == (int)(MakerCheckerAvailableRoles.Maker) || o.Role.RoleId == (int)MakerCheckerAvailableRoles.SuperUser).ToList();
        }

        /// <summary>
        /// Gets the checker users.
        /// </summary>
        /// <returns></returns>
        public List<MakerCheckerUser> GetCheckerUsers()
        {
            // TODO: Configurar id de role checker
            return UserRepository.GetAll().Where(o => o.Role.RoleId == (int)(MakerCheckerAvailableRoles.Checker) || o.Role.RoleId == (int)MakerCheckerAvailableRoles.SuperUser).ToList();
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="UserId">The user id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public MakerCheckerUser GetUser(string UserId)
        {
            return UserRepository.FindBy(UserId);
        }
        #endregion

        #region Role

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <returns></returns>
        public List<MakerCheckerRole> GetRoles()
        {
            return RoleRepository.GetAll().ToList();
        }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <param name="RoleId">The role id of type <see cref="System.Int32"/></param>
        /// <returns></returns>
        public MakerCheckerRole GetRole(int RoleId)
        {
            return RoleRepository.FindBy(RoleId);
        }
        #endregion

        #region Changeset

        /// <summary>
        /// Commits the changeset.
        /// </summary>
        /// <param name="changesetId">The changeset id of type <see cref="System.Guid"/></param>
        /// <param name="changesetComment">The changeset comment of type <see cref="System.String"/></param>
        public void CommitChangeset(Guid changesetId, string changesetComment)
        {
            var changeset = this.GetChangeset(changesetId);
            changeset.ChangesetCommitDate = DateTime.Now;
            changeset.ChangesetComment = changesetComment;
            changeset = this.ChangesetRepository.Add(changeset);
            try
            {
                MakerCheckerCoreCommitEventArgs<GarantiaBase> args = new MakerCheckerCoreCommitEventArgs<GarantiaBase>();
                args.ChangesetCommitDate = changeset.ChangesetCommitDate;
                args.ChangesetDate = changeset.ChangesetDate;
                args.ChangesetComment = changeset.ChangesetComment;
                args.ChangesetId = changeset.ChangesetId;
                args.MakerUserId = changeset.MakerUserId;
                var operations = this.GetOperations(changesetId);


                List<MakerCheckerCoreSaveOperationEventArgs<GarantiaBase>> argList = new List<MakerCheckerCoreSaveOperationEventArgs<GarantiaBase>>();
                foreach (var o in operations)
                {
                    var opArgs = new MakerCheckerCoreSaveOperationEventArgs<GarantiaBase>();
                    opArgs.ChangesetCommitDate = o.Changeset.ChangesetCommitDate;
                    opArgs.ChangesetComment = o.Changeset.ChangesetComment;
                    opArgs.ChangesetDate = o.Changeset.ChangesetDate;
                    opArgs.ChangesetId = o.Changeset.ChangesetId;
                    opArgs.CheckerUserId = o.CheckerUserId;
                    AutoMapper.Mapper.CreateMap(Type.GetType(o.ItemType), typeof(MakerCheckerObject<GarantiaBase>));
                    opArgs.MakerCheckerObject = AutoMapper.Mapper.Map(o.GetMakerCheckerObject(), Type.GetType(o.ItemType), typeof(MakerCheckerObject<GarantiaBase>));
                    opArgs.MakerDate = o.MakerDate;
                    opArgs.MakerUserId = o.Changeset.MakerUserId;
                    opArgs.OperationId = o.OperationId;
                    opArgs.OperationStatus = o.OperationStatus != null ? o.OperationStatus.OperationStatusDescription : string.Empty;
                    opArgs.OperationStatusId = o.OperationStatusId;         
                    opArgs.Action = opArgs.MakerCheckerObject?.Action;
                    argList.Add(opArgs);
                }
                args.Operations = argList.ToArray();
                this.SendEmailToChecker(args);
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Warning, "Error preparando la notificacion al usuario checker para notificar un commit.", new Dictionary<string, object>() { { "changesetId", changesetId.ToString() } }, ex);
            }
        }

        /// <summary>
        /// Gets the current changeset.
        /// </summary>
        /// <param name="UserId">The user id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public MakerCheckerChangeset GetCurrentChangeset(string UserId)
        {
            return this.ChangesetRepository.GetAvailableChangeset(UserId);
        }

        /// <summary>
        /// Gets the changeset.
        /// </summary>
        /// <param name="changesetId">The changeset id of type <see cref="System.Guid"/></param>
        /// <returns></returns>
        public MakerCheckerChangeset GetChangeset(Guid changesetId)
        {
            return this.ChangesetRepository.FindBy(changesetId);
        }

        /// <summary>
        /// Gets the summary changesets.
        /// </summary>
        /// <param name="UserId">The user id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public List<MakerCheckerChangesetSummary> GetSummaryChangesets(MakerCheckerUser user)
        {
            return this.ChangesetRepository.GetChangesetSummary(user);
        }

        /// <summary>
        /// Gets the changesets.
        /// </summary>
        /// <param name="UserId">The user id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public List<MakerCheckerChangeset> GetChangesets(string UserId)
        {
            MakerCheckerUser user = this.GetUser(UserId);
            List<MakerCheckerChangeset> changesets;
            if (user.Role.RoleId == (int)MakerCheckerRole.MakerCheckerAvailableRoles.Checker)
            {
                changesets = this.ChangesetRepository.GetAll().ToList();
            }
            else
            {
                changesets = new List<MakerCheckerChangeset>();
                changesets.Add(this.ChangesetRepository.GetAvailableChangeset(UserId));
            }
            return changesets;
        }

        #endregion

        #region Operations

        /// <summary>
        /// Gets the operations.
        /// </summary>
        /// <param name="changesetId">The changeset id of type <see cref="System.Guid"/></param>
        /// <returns></returns>
        public List<MakerCheckerOperation> GetOperations(Guid changesetId)
        {
            return this.OperationRepository.GetOperationsByChangeset(changesetId).OrderBy(o => o.MakerDate).ToList();
        }

        public List<MakerCheckerOperationSummary> GetPendingSummaryOperations(MakerCheckerUser user)
        {
            GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase> commonSvc = new GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase>();
            GarantiaContratoService garantiaContratoService = new GarantiaContratoService();

            List<MakerCheckerOperationSummary> preList;
            List<MakerCheckerOperationSummary> ret = new List<MakerCheckerOperationSummary>();
            if (user.IsChecker || user.IsSuperUser)
                preList = OperationRepository.GetPendingSummaryOperations();
            else 
                preList = OperationRepository.GetPendingSummaryOperationsByUser(user.UserId);

            foreach (MakerCheckerOperationSummary elem in preList)
            {
                var obj = commonSvc.GetMakerCheckerObject(elem.OperationId);
                List<GarantiaContrato> contratos = garantiaContratoService.GetContractsFromMakerChekerObject(obj);
                
                var garantia = obj.Object;
                elem.TipoGarantia = garantia.TipoGarantiaSuper.Nombre;
                elem.CustomerName = garantia.Cliente.Nombre;
                elem.Garante = garantia.Garante.Nombre;
                elem.ValorGarantia = garantia.GetValorGarantiaSuperIntendencia();
                if (contratos != null)
                    elem.RelatedDeals = contratos.Count;

                ret.Add(elem);
            }

            return ret;
        }
        /// <summary>
        /// Gets the operation.
        /// </summary>
        /// <param name="operationId">The operation id of type <see cref="System.Int32"/></param>
        /// <returns></returns>
        public MakerCheckerOperation GetOperation(int operationId)
        {
            return this.OperationRepository.FindBy(operationId);
        }

        /// <summary>
        /// Deletes the operation.
        /// </summary>
        /// <param name="operation">The operation of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation"/></param>
        public void DeleteOperation(MakerCheckerOperation operation)
        {
            GarantiaService garantiaSVC = new GarantiaService();

            try
            {
                if (operation.ItemId != null && operation.ItemId != 0)
                    garantiaSVC.SetInternalStatus(operation.ItemId.Value, new InternalStatusService().GetActiveStatus());

                ApplicationLogger.Instance.Info(string.Format("Eliminando operacion de M&C con Id: {0} perteneciente al ChangesetId {1}", operation.OperationId, operation.ChangesetId));
                this.OperationRepository.Remove(operation);
                ApplicationLogger.Instance.Info(string.Format("Eliminacion de operacion de M&C con Id: {0} perteneciente al ChangesetId {1} efectuada correctamente.", operation.OperationId, operation.ChangesetId));
            }
            catch (Exception ex)
            {
                string message = string.Format("Eliminacion de operacion de M&C con Id: {0} perteneciente al ChangesetId {1} no realizada debido a un error.", operation != null ? operation.OperationId.ToString() : "Not Found", operation != null ? operation.ChangesetId.ToString() : "Not Found");
                ApplicationLogger.Instance.Error(message, ex);
                throw new Exception(message, ex);
            }
        }

        public bool IsCheckUserAllowedToApproveOperation(MakerCheckerOperation operation, User appUser)
        {

            if (appUser == null)
                return false;
            if (appUser.UserId == null)
                return false;
            if (operation == null || operation.Changeset == null || operation.Changeset.MakerUser == null)
                return false;
            if (operation.Changeset.MakerUser.UserId.Equals(appUser.UserId))
                return false;

            MakerCheckerUser checkerUser = GetUser(appUser.UserId);

            if (appUser.IsPowerUser)
                return true;

            if (checkerUser.IsChecker)
                return true;
            
            return true;


        }
        /// <summary>
        /// Saves the operation.
        /// </summary>
        /// <param name="operation">The operation of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation"/></param>
        /// <returns></returns>
        public MakerCheckerOperation SaveOperation(MakerCheckerOperation operation, User appUser)
        {
            // Ticket #92423529 - Garantias - QuickWins -  verificar si el usuario esres 'l
            if (!IsCheckUserAllowedToApproveOperation(operation, appUser))
            {
                string message = "El usuario no posee la capacidad de salvar la operación.";
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, message, new Dictionary<string, object>() { { "operationId", operation.OperationId } });
                throw new Exception(message);

            }

            GarantiaService garantiaSVC = new GarantiaService();

            if (operation.OperationStatusId == (int)MakerCheckerOperationStatus.OperationStatus.Rejected)
            {
                GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase> commonServices = new GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase>();
                garantiaSVC.SetInternalStatus(operation.ItemId.Value, new InternalStatusService().GetActiveStatus());
            }

            var result = this.OperationRepository.Add(operation);            

            /*
            if (operation.ItemId != null && operation.ItemId.Value > 0)
            {                
                GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase> commonServices = new GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase>();                                                
                garantiaSVC.SetInternalStatus(operation.ItemId.Value, new InternalStatusService().GetActiveStatus());
            }*/

            try
            {
                // send notification to maker
                var args = new MakerCheckerCoreUpdateOperationEventArgs<GarantiaBase>();
                args.ChangesetCommitDate = result.Changeset.ChangesetCommitDate;
                args.ChangesetComment = result.Changeset.ChangesetComment;
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
                args.Action = args.MakerCheckerObject?.Action;
                this.SendEmailToMakerOperation(args);
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Warning, "Error preparando la notificacion al usuario maker para notificar modificaciones a las operaciones.", new Dictionary<string, object>() { { "operationId", operation.OperationId}  }, ex);
            }

            return result;
        }

        #endregion

        /// <summary>
        /// Sends the email to checker.
        /// </summary>
        /// <param name="args">The <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerCoreCommitEventArgs&lt;Bladex.Garantias.DomainModel.DomainBase.GarantiaBase&gt;"/> instance containing the event data.</param>
        public void SendEmailToChecker(MakerCheckerCoreCommitEventArgs<GarantiaBase> args)
        {
            GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase> commonSvc = new GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase>();

            string EmailsCheckers = string.Empty;
            //Guid ChangesetId = args.ChangesetId;
            //MakerCheckerChangeset changeset = GetChangeset(ChangesetId);
            //List<MakerCheckerOperation> operations = GetOperations(ChangesetId);

            
            List<MakerCheckerUser> list = new List<MakerCheckerUser>();
            list = GetCheckerUsers();

            foreach (MakerCheckerUser user in list)
            {
                if (user.UserId != args.MakerUserId)    //Evita enviar email al checker si es el mismo maker quien hizo el commit
                    EmailsCheckers += user.Email + ";";
            }
            // If no recipients was specified, cancel execution
            if (string.IsNullOrEmpty(EmailsCheckers.Trim())) return;

            MakerCheckerRole mcr = new MakerCheckerRole();
            mcr = GetRoles().FirstOrDefault(o => o.Role == MakerCheckerRole.MakerCheckerAvailableRoles.Checker);
            if (mcr == null) throw new Exception("No existe rol de checker en el modulo de Maker and Checker.");
            MakerCheckerEmailTemplate emailTmpl = EmailTemplateRepository.GetEmailTemplateByRole(mcr.RoleId);
            string message = string.Format(_EMAIL_NOTIFICATION_DATA_CHECKER, args.ChangesetCommitDate, args.ChangesetComment);
            emailTmpl.Body = emailTmpl.Body.Replace(emailTmpl.MakerIdentifier, args.MakerUserId);
            emailTmpl.Body = emailTmpl.Body.Replace(emailTmpl.DataIdentifier, message);

            // Add extended information 
            string body = "";
            body += string.Format("<b>Usuario Maker:</b> {0}<br/>", args.MakerUserId ?? "(none)");
            body += string.Format("<b>Operations:</b><br/>");
            
            List<MakerCheckerOperation> operations = this.GetOperations(args.ChangesetId);

            foreach (var op in operations)
            {
                var action = op.GetMakerCheckerObject() != null && op.GetMakerCheckerObject().Action != null ? op.GetMakerCheckerObject().Action.ToString() : "No Action";

                body += string.Format("<b>----------------------------</b> <br/><br/>");
                body += string.Format("<b>Operation Type:</b> {0}<br/>", action);
                body += string.Format("<b>Usuario Checker:</b> {0}<br/>", op.CheckerUserId ?? "(none)");

                var garantia = commonSvc.GetMakerCheckerObject(op.OperationId);


                if (garantia != null && garantia.Object != null)
                {

                    var gar = garantia.Object;
                    body += string.Format("<b>Cliente:</b> {0}<br/>", gar.Cliente.Nombre);
                    body += string.Format("<b>Garante:</b> {0}<br/>", gar.Garante.Nombre);
                    body += string.Format("<b>Nro. Garantia:</b> {0}<br/>", gar.Key);
                    body += string.Format("<b>Tipo Garantia SBP:</b> {0}<br/>", gar.TipoGarantiaSuper.Nombre);
                    body += string.Format("<b>Valor Garantia:</b> {0:C}<br/>", gar.GetValorGarantiaSuperIntendencia());
                    body += string.Format("<b># Contratos Vinculados:</b> <br/>");
                }
                else
                {
                    body += "<b> - SIN INFORMACION DE GARANTIA - </b><br/>";
                }

            }
            emailTmpl.Body += body;


            // send email
            Bladex.Garantias.Infrastructure.Emailing.EmailerComponent.Instance.SendEmail(EmailsCheckers, emailTmpl.Subject, emailTmpl.Body, string.Empty, emailTmpl.Cc, emailTmpl.Bcc, null);
        }
 

        /// <summary>
        /// Sends the email to maker operation.
        /// </summary>
        /// <param name="args">The <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerCoreUpdateOperationEventArgs&lt;Bladex.Garantias.DomainModel.DomainBase.GarantiaBase&gt;"/> instance containing the event data.</param>
        public void SendEmailToMakerOperation(MakerCheckerCoreUpdateOperationEventArgs<GarantiaBase> args)
        {

            GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase> commonSvc = new GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase>();

            MakerCheckerCore<GarantiaBase> core = new MakerCheckerCore<GarantiaBase>(this.UserRepository, this.RoleRepository, this.OperationStatusRepository, this.ChangesetRepository, this.OperationRepository);
            // Get Summary for my changeset
            MakerCheckerChangesetSummary dt = core.SelectAllChangesetsSummary(args.ChangesetId);
            if (dt != null)
            {
                // For my changeset returns, retrieve summary of operations.
                int totalOperations = dt.TotalOperations;
                int newOperations = dt.NewOperations;
                int approvedOperations = dt.ApprovedOperations;
                int rejectedOperations = dt.RejectedOperations;
                // If all operations has been revised, send email.
                if (totalOperations == (approvedOperations + rejectedOperations))
                {
                    // Send Notification
                    MakerCheckerRole mcr = GetRoles().FirstOrDefault(o => o.RoleId == (int)MakerCheckerRole.MakerCheckerAvailableRoles.Maker);
                    MakerCheckerEmailTemplate emailTmpl = EmailTemplateRepository.GetEmailTemplateByRole(mcr.RoleId);

                    List<MakerCheckerOperation> operations = this.GetOperations(args.ChangesetId);

                    string message = string.Format(_EMAIL_NOTIFICATION_DATA_MAKER, args.ChangesetCommitDate, totalOperations, approvedOperations, rejectedOperations);
                    emailTmpl.Body = emailTmpl.Body.Replace(emailTmpl.CheckerIdentifier, args.CheckerUserId);
                    emailTmpl.Body = emailTmpl.Body.Replace(emailTmpl.MakerIdentifier, args.MakerUserId);
                    emailTmpl.Body = emailTmpl.Body.Replace(emailTmpl.DataIdentifier, message);

                    // Add extended information 
                    //emailTmpl.Body += CreateOperationInfoBody(args);

                    MakerCheckerUser makerUser = this.GetUser(args.MakerUserId);
                    if (makerUser != null && !string.IsNullOrEmpty(makerUser.Email))
                    {
                        ///////////////////////
                        /// Ticket #92423529 - Garantias - Quick wins
                        string body = "";
                        body += string.Format("<b>Usuario Maker:</b> {0}<br/>", args.MakerUserId ?? "(none)");
                        body += string.Format("<b>Operations:</b><br/>");
                        foreach (var op in operations)
                        {
                            var action = op.GetMakerCheckerObject() != null && op.GetMakerCheckerObject().Action != null ? op.GetMakerCheckerObject().Action.ToString() : "No Action";

                            body += string.Format("<b>----------------------------</b> <br/><br/>");
                            body += string.Format("<b>Operation Type:</b> {0}<br/>", action);
                            body += string.Format("<b>Usuario Checker:</b> {0}<br/>", op.CheckerUserId ?? "(none)");
                            body += string.Format("<b>Resolution:</b> {0}<br/>", op.OperationStatus == null? "(none)" : op.OperationStatus.OperationStatusDescription);


                            MakerCheckerObject<GarantiaBase> makerCheckObj = commonSvc.GetMakerCheckerObject(op.OperationId);

                            if (makerCheckObj != null && makerCheckObj.Object != null)
                            {
                                GarantiaBase gar = makerCheckObj.Object;
                                body += string.Format("<b>Cliente:</b> {0}<br/>", gar.Cliente.Nombre);
                                body += string.Format("<b>Garante:</b> {0}<br/>", gar.Garante.Nombre);
                                body += string.Format("<b>Nro. Garantia:</b> {0}<br/>", gar.Key);
                                body += string.Format("<b>Tipo Garantia SBP:</b> {0}<br/>", gar.TipoGarantiaSuper.Nombre);
                                body += string.Format("<b>Valor Garantia:</b> {0:C}<br/>", gar.GetValorGarantiaSuperIntendencia());
                                body += string.Format("<b># Contratos Vinculados:</b> <br/>");

                                
                            }
                            else
                            {
                                body += "<b> - SIN INFORMACION DE GARANTIA - </b><br/>";
                            }
                        }
                        emailTmpl.Body += body;

                        Bladex.Garantias.Infrastructure.Emailing.EmailerComponent.Instance.SendEmail(makerUser.Email, emailTmpl.Subject, emailTmpl.Body, string.Empty,emailTmpl.Cc, emailTmpl.Bcc, null);
                    }
                }
                
            }

            
        }

    }
}
