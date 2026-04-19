using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Xml;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Components.MakerChecker
{
    /// <summary>
    /// Class used to perform CRUD operations againts Maker and Checker Core Table. It allows a custom object to be stored.
    /// </summary>
    /// <typeparam name="T">ISerializable object to store into the database.</typeparam>
    public class MakerCheckerCore<T> where T : Bladex.Garantias.Infrastructure.DomainBase.EntityBase
    {
        #region Delegates & Events
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of type <see cref="System.Object"/></param>
        /// <param name="e">The <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerCoreCommitEventArgs"/> instance containing the event data.</param>
        public delegate void CommitChangesetHandler(object sender, MakerCheckerCoreCommitEventArgs<T> e);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of type <see cref="System.Object"/></param>
        /// <param name="e">The <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerCoreSaveOperationEventArgs"/> instance containing the event data.</param>
        public delegate void SaveOperationHandler(object sender, MakerCheckerCoreSaveOperationEventArgs<T> e);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of type <see cref="System.Object"/></param>
        /// <param name="e">The <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerCoreDeleteOperationEventArgs"/> instance containing the event data.</param>
        public delegate void DeleteOperationHandler(object sender, MakerCheckerCoreDeleteOperationEventArgs<T> e);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of type <see cref="System.Object"/></param>
        /// <param name="e">The <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerCoreUpdateOperationEventArgs"/> instance containing the event data.</param>
        public delegate void UpdateOperationHandler(object sender, MakerCheckerCoreUpdateOperationEventArgs<T> e);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of type <see cref="System.Object"/></param>
        /// <param name="e">The <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerCorePersistOperationEventArgs"/> instance containing the event data.</param>
        public delegate void PersistObjectHandler(object sender, MakerCheckerCorePersistOperationEventArgs<T> e);

        /// <summary>
        /// Fires when the changeset has been commited.
        /// </summary>
        public event CommitChangesetHandler OnCommitChangeset;

        /// <summary>
        /// Occurs when [on save operation].
        /// </summary>
        public event SaveOperationHandler OnSaveOperation;
        /// <summary>
        /// Occurs when [on delete operation].
        /// </summary>
        public event DeleteOperationHandler OnDeleteOperation;
        /// <summary>
        /// Occurs when [on update operation].
        /// </summary>
        public event UpdateOperationHandler OnUpdateOperation;
        /// <summary>
        /// Occurs when [on persist object]. Should suscribe to access to the object to persist.
        /// </summary>
        public event PersistObjectHandler OnPersistObject;

        #endregion

        protected readonly IMakerCheckerUserRepository MakerCheckerUserRepository;
        protected readonly IMakerCheckerRoleRepository MakerCheckerRoleRepository;
        protected readonly IMakerCheckerOperationStatusRepository MakerCheckerOperationStatusRepository;
        protected readonly IMakerCheckerOperationRepository MakerCheckerOperationRepository;
        protected readonly IMakerCheckerChangesetRepository MakerCheckerChangesetRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerCore&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="MakerCheckerUserRepository">The maker checker user repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerUserRepository"/></param>
        /// <param name="MakerCheckerRoleRepository">The maker checker role repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerRoleRepository"/></param>
        /// <param name="MakerCheckerOperationStatusRepository">The maker checker operation status repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerOperationStatusRepository"/></param>
        /// <param name="MakerCheckerChangesetRepository">The maker checker changeset repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerChangesetRepository"/></param>
        /// <param name="MakerCheckerOperationRepository">The maker checker operation repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker.IMakerCheckerOperationRepository"/></param>
        public MakerCheckerCore(IMakerCheckerUserRepository MakerCheckerUserRepository, IMakerCheckerRoleRepository MakerCheckerRoleRepository, IMakerCheckerOperationStatusRepository MakerCheckerOperationStatusRepository, IMakerCheckerChangesetRepository MakerCheckerChangesetRepository, IMakerCheckerOperationRepository MakerCheckerOperationRepository)
        {
            this.MakerCheckerUserRepository = MakerCheckerUserRepository;
            this.MakerCheckerRoleRepository = MakerCheckerRoleRepository;
            this.MakerCheckerOperationStatusRepository = MakerCheckerOperationStatusRepository;
            this.MakerCheckerChangesetRepository = MakerCheckerChangesetRepository;
            this.MakerCheckerOperationRepository = MakerCheckerOperationRepository;
            _operationStatus = new Dictionary<int, string>();
            var operationStatus = this.MakerCheckerOperationStatusRepository.GetAll();
            foreach (var op in operationStatus)
            {
                if (!_operationStatus.ContainsKey(op.OperationStatusId))
                {
                    _operationStatus.Add(op.OperationStatusId, op.OperationStatusDescription);
                }
            }

        }

        private readonly Dictionary<int, string> _operationStatus;

        /// <summary>
        /// Gets the operation status.
        /// </summary>
        public Dictionary<int, string> OperationStatus
        {
            get { return _operationStatus; }
        }

        /// <summary>
        /// Perform save of maker and checker object
        /// </summary>
        /// <param name="MakerUserId">Maker User ID</param>
        /// <param name="OperationStatusId">Operation Status (New, Pending, Approved, etc)</param>
        /// <param name="MakerCheckerObjectToStore">Data to serialize</param>
        /// <param name="Action"><code>MatrixXmlSerializer.MakerAndCheckerAction</code> enumerator</param>
        /// <returns>Operation ID generated</returns>
        public int Save(string MakerUserId, int OperationStatusId, T MakerCheckerObjectToStore, MakerAndCheckerActionEnum Action)
        {
            return this.Save(MakerUserId, OperationStatusId, MakerCheckerObjectToStore, Action, null);
        }

        public int Save(string MakerUserId, int OperationStatusId, T MakerCheckerObjectToStore, MakerAndCheckerActionEnum Action, Dictionary<string, object> AdditionalAttributes)
        {
            int operationId = -1;
            try
            {
                MakerCheckerObject<T> makerCheckerObject = new MakerCheckerObject<T>();
                makerCheckerObject.Action = Action;
                makerCheckerObject.Object = MakerCheckerObjectToStore;
                
                if(AdditionalAttributes != null)
                    makerCheckerObject.AdditionalAttributes = new Infrastructure.Serialization.SerializableDictionary<string, object>(AdditionalAttributes);

                makerCheckerObject.Serialize();

                Guid changesetId = GetChangesetIdentifier(MakerUserId);
                var operation = SaveToDb(changesetId, MakerUserId, OperationStatusId, makerCheckerObject.ObjectSerialized, MakerCheckerObjectToStore);
                operationId = operation.OperationId;
                if (OnSaveOperation != null)
                {
                    MakerCheckerCoreSaveOperationEventArgs<T> eargs = this.GetOperationEventArg<MakerCheckerCoreSaveOperationEventArgs<T>>(operationId);
                    eargs.MakerCheckerObject = makerCheckerObject;
                    OnSaveOperation.Invoke(this, eargs);
                }
            }
            catch (Exception ex)
            {
                operationId = -1;
            }
            return operationId;
        }

        /// <summary>
        /// Updates the specified operation with a modified maker checker object.
        /// </summary>
        /// <param name="operationId">The operation id of type <see cref="System.Int32"/></param>
        /// <param name="makerCheckerObject">The maker checker object of type <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerObject&lt;T&gt;"/></param>
        public void Update(int operationId, MakerCheckerObject<T> makerCheckerObject)
        {

            var operation = this.MakerCheckerOperationRepository.FindBy(operationId);
            makerCheckerObject.Serialize();
            operation.Item = makerCheckerObject.ObjectSerialized;
            if (makerCheckerObject.Object != null)
                operation.ItemType = makerCheckerObject.Object.GetType().AssemblyQualifiedName;
            this.MakerCheckerOperationRepository.Add(operation);
        }

        /// <summary>
        /// Perform update of Maker And Checker object.
        /// </summary>
        /// <param name="OperationId">Operation ID</param>
        /// <param name="CheckerUserId">Checker User ID</param>
        /// <param name="OperationStatusId">Operation Status (New, Pending, Approved, etc)</param>
        /// <param name="Comment">Comment about the change.</param>
        /// <returns>Affected Rows</returns>
        public int Update(int OperationId, string CheckerUserId, int OperationStatusId, string Comment)
        {
            int res = -1;
            try
            {
                var operation = UpdateToDb(OperationId, CheckerUserId, OperationStatusId, Comment);
                if (operation != null) res = 1;
                if (OnUpdateOperation != null)
                {
                    MakerCheckerCoreUpdateOperationEventArgs<T> eargs = GetOperationEventArg<MakerCheckerCoreUpdateOperationEventArgs<T>>(OperationId);
                    OnUpdateOperation(this, eargs);
                }
            }
            catch (SqlException sqlEx)
            {
                res = -1;
                if (sqlEx.Number == 50000)
                {
                    throw new System.Data.DBConcurrencyException(sqlEx.Message, sqlEx);
                }
            }
            return res;
        }

        public U GetOperationEventArg<U>(Guid ChangesetId) where U : MakerCheckerCoreCommitEventArgs<T>, new()
        {
            U result = new U();
            Type t = typeof(U);

            MakerCheckerChangeset dt = this.SelectChangesetById(ChangesetId);
            if (dt != null)
            {
                if (t.FullName == typeof(MakerCheckerCoreCommitEventArgs<T>).FullName)
                {

                    (result as MakerCheckerCoreCommitEventArgs<T>).ChangesetId = dt.ChangesetId;
                    (result as MakerCheckerCoreCommitEventArgs<T>).MakerUserId = dt.MakerUserId;
                    (result as MakerCheckerCoreCommitEventArgs<T>).ChangesetCommitDate = dt.ChangesetCommitDate;
                    (result as MakerCheckerCoreCommitEventArgs<T>).ChangesetDate = dt.ChangesetDate;
                    List<MakerCheckerOperation> dtOperations = this.SelectAllByChangesetId(ChangesetId);
                    if (dtOperations != null)
                    {
                        (result as MakerCheckerCoreCommitEventArgs<T>).Operations = new MakerCheckerCoreSaveOperationEventArgs<T>[dtOperations.Count];
                        for (int c = 0; c < dtOperations.Count; c++)
                        {
                            (result as MakerCheckerCoreCommitEventArgs<T>).Operations[c] = this.GetOperationEventArg<MakerCheckerCoreSaveOperationEventArgs<T>>(dtOperations[c].OperationId);
                        }
                    }
                }
            }

            return result;
        }

        public U GetOperationEventArg<U>(int OperationId) where U : MakerCheckerCoreEventArgs<T>, new()
        {
            U result = new U();
            Type t = typeof(U);

            MakerCheckerOperation operation = this.SelectOne(OperationId);
            if (operation != null)
            {
                if (t.FullName == typeof(MakerCheckerCoreSaveOperationEventArgs<T>).FullName)
                {
                    (result as MakerCheckerCoreSaveOperationEventArgs<T>).OperationStatusId = operation.OperationStatusId;
                    (result as MakerCheckerCoreSaveOperationEventArgs<T>).OperationStatus = this.OperationStatus[operation.OperationStatusId];
                    (result as MakerCheckerCoreSaveOperationEventArgs<T>).ChangesetId = operation.ChangesetId;
                    (result as MakerCheckerCoreSaveOperationEventArgs<T>).CheckerUserId = operation.CheckerUserId;
                    (result as MakerCheckerCoreSaveOperationEventArgs<T>).MakerUserId = operation.Changeset.MakerUserId;
                    (result as MakerCheckerCoreSaveOperationEventArgs<T>).ChangesetCommitDate = operation.Changeset.ChangesetCommitDate;
                    (result as MakerCheckerCoreSaveOperationEventArgs<T>).MakerDate = operation.MakerDate;
                    (result as MakerCheckerCoreSaveOperationEventArgs<T>).OperationId = operation.OperationId;
                    (result as MakerCheckerCoreSaveOperationEventArgs<T>).ChangesetDate = operation.Changeset.ChangesetDate;
                }
                else if (t.FullName == typeof(MakerCheckerCoreUpdateOperationEventArgs<T>).FullName)
                {
                    (result as MakerCheckerCoreUpdateOperationEventArgs<T>).OperationStatusId = operation.OperationStatusId;
                    (result as MakerCheckerCoreUpdateOperationEventArgs<T>).OperationStatus = this.OperationStatus[operation.OperationStatusId];
                    (result as MakerCheckerCoreUpdateOperationEventArgs<T>).ChangesetId = operation.ChangesetId;
                    (result as MakerCheckerCoreUpdateOperationEventArgs<T>).CheckerUserId = operation.CheckerUserId;
                    (result as MakerCheckerCoreUpdateOperationEventArgs<T>).MakerUserId = operation.Changeset.MakerUserId;
                    (result as MakerCheckerCoreUpdateOperationEventArgs<T>).ChangesetCommitDate = operation.Changeset.ChangesetCommitDate;
                    (result as MakerCheckerCoreUpdateOperationEventArgs<T>).MakerDate = operation.MakerDate;
                    (result as MakerCheckerCoreUpdateOperationEventArgs<T>).CheckerDate = operation.CheckerDate;
                    (result as MakerCheckerCoreUpdateOperationEventArgs<T>).OperationId = operation.OperationId;
                    (result as MakerCheckerCoreUpdateOperationEventArgs<T>).OperationComment = operation.Comment;
                    (result as MakerCheckerCoreUpdateOperationEventArgs<T>).ChangesetDate = operation.Changeset.ChangesetDate;

                }
                else if (t.FullName == typeof(MakerCheckerCoreDeleteOperationEventArgs<T>).FullName)
                {
                    (result as MakerCheckerCoreDeleteOperationEventArgs<T>).OperationStatusId = operation.OperationStatusId;
                    (result as MakerCheckerCoreDeleteOperationEventArgs<T>).OperationStatus = this.OperationStatus[operation.OperationStatusId];
                    (result as MakerCheckerCoreDeleteOperationEventArgs<T>).ChangesetId = operation.ChangesetId;
                    (result as MakerCheckerCoreDeleteOperationEventArgs<T>).CheckerUserId = operation.CheckerUserId;
                    (result as MakerCheckerCoreDeleteOperationEventArgs<T>).MakerUserId = operation.Changeset.MakerUserId;
                    (result as MakerCheckerCoreDeleteOperationEventArgs<T>).ChangesetCommitDate = operation.Changeset.ChangesetCommitDate;
                    //(result as MakerCheckerCoreDeleteOperationEventArgs).MakerDate = operationRow.Field<DateTime?>("MakerDate");
                    //(result as MakerCheckerCoreDeleteOperationEventArgs).CheckerDate = operationRow.Field<DateTime?>("CheckerDate");
                    //(result as MakerCheckerCoreDeleteOperationEventArgs).OperationId = OperationId;
                    (result as MakerCheckerCoreDeleteOperationEventArgs<T>).OperationComment = operation.Comment;
                    (result as MakerCheckerCoreDeleteOperationEventArgs<T>).ChangesetDate = operation.Changeset.ChangesetDate;
                }


            }

            return result;
        }

        /// <summary>
        /// Returns all maker and checker operations
        /// </summary>
        /// <returns>Table with all maker and checker operations. MakerCheckerObject column is missing.</returns>
        public List<MakerCheckerOperation> SelectAll()
        {
            return this.MakerCheckerOperationRepository.GetAll().OrderBy(o=>o.OperationStatusId).OrderByDescending(o=>o.MakerDate).ToList();
        }

        public List<MakerCheckerChangesetSummary> SelectAllChangesetsSummary()
        {
            return this.MakerCheckerChangesetRepository.GetChangesetSummary();
        }

        /// <summary>
        /// Returns the list of checker users.
        /// </summary>
        /// <returns>Checker User ID</returns>
        public IEnumerable<MakerCheckerUser> GetCheckerUsersList()
        {
            return this.MakerCheckerUserRepository.GetAll().Where(o => o.Role.Role == MakerCheckerRole.MakerCheckerAvailableRoles.Checker);
        }

        /// <summary>
        /// Returns all changesets.
        /// </summary>
        /// <returns>Table with changesets.</returns>
        public List<MakerCheckerChangeset> SelectAllChangesets()
        {
            return this.MakerCheckerChangesetRepository.GetAll().OrderByDescending(o => o.ChangesetDate).ToList();
            
        }

        /// <summary>
        /// Selects the changeset by id.
        /// </summary>
        /// <param name="ChangesetId">The changeset id of type <see cref="System.Guid"/></param>
        /// <returns></returns>
        public MakerCheckerChangeset SelectChangesetById(Guid ChangesetId)
        {
            return this.MakerCheckerChangesetRepository.FindBy(ChangesetId);
        }

        /// <summary>
        /// Returns all maker and checker operations for the specified Maker.
        /// </summary>
        /// <param name="CheckerUserId">The Checker User ID (optional) used to retrieve operations.</param>
        /// <param name="MakerUserId">The Maker UserID (required) used to retrieve operations.</param>
        /// <returns>Collection with all maker and checker operations. MakerCheckerObject column is missing.</returns>
        public List<MakerCheckerOperation> SelectAll(string MakerUserId, string CheckerUserId)
        {
            if (string.IsNullOrEmpty(CheckerUserId))
            {
                return this.MakerCheckerOperationRepository.GetOperationsByMaker(MakerUserId).OrderBy(o => o.MakerDate).ToList();
            }
            else
            {
                return this.MakerCheckerOperationRepository.GetOperationsByChecker(CheckerUserId).ToList();
            }
        }

        public MakerCheckerChangesetSummary SelectAllChangesetsSummary(Guid ChangesetId)
        {
            return this.MakerCheckerChangesetRepository.GetChangesetSummary(ChangesetId);
        }

        /// <summary>
        /// Returns all maker and checker operations for the specified changeset.
        /// </summary>
        /// <param name="ChangesetId">The Changeset ID used to retrieve operations.</param>
        /// <returns>Table with all maker and checker operations. MakerCheckerObject column is missing.</returns>
        public List<MakerCheckerOperation> SelectAllByChangesetId(Guid ChangesetId)
        {
            return this.MakerCheckerOperationRepository.GetOperationsByChangeset(ChangesetId).OrderByDescending(o => o.MakerDate).ToList();
        }

        /// <summary>
        /// Returns a desired Maker and Checker Operation
        /// </summary>
        /// <param name="OperationId">ID of operation to retrieve.</param>
        /// <returns>Table with operations. MakerCheckerObject column missing</returns>
        public MakerCheckerOperation SelectOne(int OperationId)
        {
            return this.MakerCheckerOperationRepository.FindBy(OperationId);
        }

        /// <summary>
        /// Deletes the specified operation.
        /// </summary>
        /// <param name="OperationId">Operation ID to delete.</param>
        /// <returns>1 if delete was ok, -1 if delete was wrong.</returns>
        public int Delete(int OperationId)
        {
            try
            {
                var operation = this.MakerCheckerOperationRepository.FindBy(OperationId);
                this.MakerCheckerOperationRepository.Remove(operation);
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// Deletes the specified operation by Changeset.
        /// </summary>
        /// <param name="ChangesetId">Changeset ID to delete.</param>
        /// <returns>1 if delete was ok, -1 if delete was wrong.</returns>
        public int DeleteByChangesetId(Guid ChangesetId)
        {
            try
            {
                var operations = this.MakerCheckerOperationRepository.GetOperationsByChangeset(ChangesetId);
                operations.ForEach( o=> this.MakerCheckerOperationRepository.Remove(o));
                operations = null;
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }

        public bool CheckItemExistence(int itemId)
        {
            var operations = this.MakerCheckerOperationRepository.GetOperationsByItemId(itemId);
            return operations.FirstOrDefault(o => o.OperationStatus.OperationStatusId == (int)MakerCheckerOperationStatus.OperationStatus.New) != null;
        }

        public MakerCheckerOperation GetNewOperationForAnItem(int itemId)
        {
            var operations = this.MakerCheckerOperationRepository.GetOperationsByItemId(itemId);
            return operations.FirstOrDefault(o => o.OperationStatus.OperationStatusId == (int)MakerCheckerOperationStatus.OperationStatus.New);
        }

        /// <summary>
        /// Returns true if an item with same ItemID already exists.
        /// </summary>
        /// <param name="item">ID of the Item stored as XML</param>
        /// <returns>True if exists, False if not.</returns>
        public bool CheckItemExistence(EntityBase item)
        {
            return this.MakerCheckerOperationRepository.GetAll().Where(o => o.ItemId == item.GetKeyAs<int>()).Count() == 0 ? false : true;
        }

        private MakerCheckerOperation UpdateToDb(int OperationId, string CheckerUserId, int OperationStatusId, string Comment)
        {
            var operation = this.MakerCheckerOperationRepository.FindBy(OperationId);
            operation.CheckerUserId = CheckerUserId;
            operation.OperationStatusId = OperationStatusId;
            operation.Comment = Comment;
            return this.MakerCheckerOperationRepository.Add(operation);
        }

        /// <summary>
        /// Marks a changeset as commited.
        /// </summary>
        /// <param name="MakerUserId">Maker User ID</param>
        /// <param name="ChangesetId">Changeset Identifier</param>
        public void CommitChangeset(string MakerUserId, Guid ChangesetId)
        {
            try
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Information, "Commiting changeset operation...", new Dictionary<string, object>() { {"MakerUserId", MakerUserId},{"ChangesetId",ChangesetId}});
                var changeset = this.MakerCheckerChangesetRepository.FindBy(ChangesetId);
                changeset.ChangesetCommitDate = DateTime.Now;
                this.MakerCheckerChangesetRepository.Add(changeset);
                if (OnCommitChangeset != null)
                {
                    MakerCheckerCoreCommitEventArgs<T> eargs = this.GetOperationEventArg<MakerCheckerCoreCommitEventArgs<T>>(ChangesetId);
                    OnCommitChangeset.Invoke(this, eargs);
                }
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Information, "Changeset operation commited successfully.", new Dictionary<string, object>() { { "MakerUserId", MakerUserId }, { "ChangesetId", ChangesetId } });
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("An error has ocurred trying to commit changeset {0}", ChangesetId), new Dictionary<string, object>() { { "MakerUserId", MakerUserId }, { "ChangesetId", ChangesetId } }, ex);
            }
        }

        /// <summary>
        /// Returns the changeset identifier for the specified MakerUser 
        /// </summary>
        /// <param name="MakerUserId">Maker User ID</param>
        /// <returns>Changeset Identifier assigned to the Maker User</returns>
        public Guid GetChangesetIdentifier(string MakerUserId)
        {
            var changeset = this.MakerCheckerChangesetRepository.GetAvailableChangeset(MakerUserId);
            return changeset.ChangesetId;
        }

        private MakerCheckerOperation SaveToDb(Guid ChangesetId, string MakerUserId, int OperationStatusId, string MakerCheckerObjectSerialized, EntityBase MakerCheckerObject)
        {
            MakerCheckerOperation operation = new MakerCheckerOperation() { ChangesetId = ChangesetId, MakerDate = DateTime.Now, OperationStatusId = OperationStatusId };
            // Retrieve ID for MakerChecker Object.
            int? itemId = GetItemId(MakerCheckerObject);
            if (itemId.HasValue)
                operation.ItemId = itemId.Value;
            operation.ItemType = MakerCheckerObject.GetType().AssemblyQualifiedName;
            operation.Item = MakerCheckerObjectSerialized;
            return this.MakerCheckerOperationRepository.Add(operation);
            
        }
        /// <summary>
        /// Gets the item id.
        /// </summary>
        /// <param name="entity">The entity of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        /// <returns></returns>
        public int? GetItemId(EntityBase entity)
        {
            try
            {
                return entity.GetKeyAs<int>();
            }
            catch (Exception ex)
            {
                return default(int?);
            }
        }

        /// <summary>
        /// Persists the object.
        /// </summary>
        /// <param name="ParamOperationId">The param operation id of type <see cref="System.Int32"/></param>
        /// <param name="UserId">The user id of type <see cref="System.String"/></param>
        public void PersistObject(int ParamOperationId, string UserId)
        {
            var operation = this.MakerCheckerOperationRepository.FindBy(ParamOperationId);
            if (operation == null) throw new Exception(string.Format("Operation with Id {0} not found.", ParamOperationId));

            Type elementType = Type.GetType(operation.ItemType);
            Type[] types = new Type[] {elementType};
            Type listType = typeof(MakerCheckerObject<>);
            Type genericType = listType.MakeGenericType(types);
            dynamic makerCheckerObject = Activator.CreateInstance(genericType);
            string serializedObject = operation.Item;
            makerCheckerObject.ObjectSerialized = serializedObject;
            makerCheckerObject.Deserialize();
            // delegate the responsability of the storage to the caller.
            if (OnPersistObject != null)
            {
                var eventArgs = GetOperationEventArg<MakerCheckerCorePersistOperationEventArgs<T>>(ParamOperationId);
                eventArgs.MakerCheckerObject = makerCheckerObject;
                OnPersistObject.Invoke(this, eventArgs);
            }
        }

        public MakerCheckerObject<TU> GetMakerCheckerObject<TU>(int operationId) where TU: EntityBase
        {
            var operation = this.MakerCheckerOperationRepository.FindBy(operationId);
            if (operation == null) throw new Exception(string.Format("Operation with Id {0} not found.", operationId));

            Type elementType = Type.GetType(operation.ItemType);
            Type[] types = new Type[] {elementType};
            Type listType = typeof(MakerCheckerObject<>);
            Type genericType = listType.MakeGenericType(types);
            dynamic makerCheckerObject = Activator.CreateInstance(genericType);
            string serializedObject = operation.Item;
            makerCheckerObject.ObjectSerialized = serializedObject;
            makerCheckerObject.Deserialize();
            return AutoMapper.Mapper.Map(makerCheckerObject, makerCheckerObject.GetType(), typeof (MakerCheckerObject<TU>));
            return (MakerCheckerObject<TU>)makerCheckerObject;
        }

    }

    /// <summary>
    /// The maker checker core event args class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MakerCheckerCoreEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerCoreEventArgs&lt;T&gt;"/> class.
        /// </summary>
        public MakerCheckerCoreEventArgs()
        {
        }

        /// <summary>
        /// Gets or sets the changeset id.
        /// </summary>
        /// <value>
        /// The changeset id of type <see cref="System.Guid"/>
        /// </value>
        public Guid ChangesetId { get; set; }
        /// <summary>
        /// Gets or sets the changeset commit date.
        /// </summary>
        /// <value>
        /// The changeset commit date of type <see cref="System.Nullable&lt;System.DateTime&gt;"/>
        /// </value>
        public DateTime? ChangesetCommitDate { get; set; }
        /// <summary>
        /// Gets or sets the changeset comment.
        /// </summary>
        /// <value>
        /// The changeset comment of type <see cref="System.String"/>
        /// </value>
        public string ChangesetComment { get; set; }
        /// <summary>
        /// Gets or sets the changeset date.
        /// </summary>
        /// <value>
        /// The changeset date of type <see cref="System.Nullable&lt;System.DateTime&gt;"/>
        /// </value>
        public DateTime? ChangesetDate { get; set; }
    }

    /// <summary>
    /// The maker checker core commit event args class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MakerCheckerCoreCommitEventArgs<T> : MakerCheckerCoreEventArgs<T>
    {
        /// <summary>
        /// Gets or sets the maker user id.
        /// </summary>
        /// <value>
        /// The maker user id of type <see cref="System.String"/>
        /// </value>
        public string MakerUserId { get; set; }
        /// <summary>
        /// Gets or sets the operations.
        /// </summary>
        /// <value>
        /// The operations of type <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerCoreSaveOperationEventArgs&lt;T&gt;[]"/>
        /// </value>
        public MakerCheckerCoreSaveOperationEventArgs<T>[] Operations { get; set; }
    }

    /// <summary>
    /// The maker checker core save operation event args class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MakerCheckerCoreSaveOperationEventArgs<T> : MakerCheckerCoreEventArgs<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerCoreSaveOperationEventArgs&lt;T&gt;"/> class.
        /// </summary>
        public MakerCheckerCoreSaveOperationEventArgs()
        {
        }

        /// <summary>
        /// Add=1, Edit=2, Delete=3
        /// </summary>    
        public MakerAndCheckerActionEnum? Action { get; set; }


        /// <summary>
        /// Gets or sets the maker user id.
        /// </summary>
        /// <value>
        /// The maker user id of type <see cref="System.String"/>
        /// </value>
        public string MakerUserId { get; set; }
        /// <summary>
        /// Gets or sets the maker date.
        /// </summary>
        /// <value>
        /// The maker date of type <see cref="System.Nullable&lt;System.DateTime&gt;"/>
        /// </value>
        public DateTime? MakerDate { get; set; }
        /// <summary>
        /// Gets or sets the operation status id.
        /// </summary>
        /// <value>
        /// The operation status id of type <see cref="System.Int32"/>
        /// </value>
        public int OperationStatusId { get; set; }
        /// <summary>
        /// Gets or sets the operation status.
        /// </summary>
        /// <value>
        /// The operation status of type <see cref="System.String"/>
        /// </value>
        public string OperationStatus { get; set; }
        /// <summary>
        /// Gets or sets the operation id.
        /// </summary>
        /// <value>
        /// The operation id of type <see cref="System.Int32"/>
        /// </value>
        public int OperationId { get; set; }
        /// <summary>
        /// Gets or sets the checker user id.
        /// </summary>
        /// <value>
        /// The checker user id of type <see cref="System.String"/>
        /// </value>
        public string CheckerUserId { get; set; }
        /// <summary>
        /// Gets or sets the maker checker object.
        /// </summary>
        /// <value>
        /// The maker checker object of type <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerObject&lt;T&gt;"/>
        /// </value>
        public MakerCheckerObject<T> MakerCheckerObject { get; set; }
    }

    /// <summary>
    /// The maker checker core update operation event args class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MakerCheckerCoreUpdateOperationEventArgs<T> : MakerCheckerCoreEventArgs<T>
    {


        /// <summary>
        /// Add=1, Edit=2, Delete=3
        /// </summary>    
        public MakerAndCheckerActionEnum? Action { get; set; }

        /// <summary>
        /// Gets or sets the maker user id.
        /// </summary>
        /// <value>
        /// The maker user id of type <see cref="System.String"/>
        /// </value>
        public string MakerUserId { get; set; }
        /// <summary>
        /// Gets or sets the maker date.
        /// </summary>
        /// <value>
        /// The maker date of type <see cref="System.Nullable&lt;System.DateTime&gt;"/>
        /// </value>
        public DateTime? MakerDate { get; set; }
        /// <summary>
        /// Gets or sets the checker date.
        /// </summary>
        /// <value>
        /// The checker date of type <see cref="System.Nullable&lt;System.DateTime&gt;"/>
        /// </value>
        public DateTime? CheckerDate { get; set; }
        /// <summary>
        /// Gets or sets the operation status id.
        /// </summary>
        /// <value>
        /// The operation status id of type <see cref="System.Int32"/>
        /// </value>
        public int OperationStatusId { get; set; }
        /// <summary>
        /// Gets or sets the operation status.
        /// </summary>
        /// <value>
        /// The operation status of type <see cref="System.String"/>
        /// </value>
        public string OperationStatus { get; set; }
        /// <summary>
        /// Gets or sets the operation id.
        /// </summary>
        /// <value>
        /// The operation id of type <see cref="System.Int32"/>
        /// </value>
        public int OperationId { get; set; }
        /// <summary>
        /// Gets or sets the checker user id.
        /// </summary>
        /// <value>
        /// The checker user id of type <see cref="System.String"/>
        /// </value>
        public string CheckerUserId { get; set; }
        /// <summary>
        /// Gets or sets the operation comment.
        /// </summary>
        /// <value>
        /// The operation comment of type <see cref="System.String"/>
        /// </value>
        public string OperationComment { get; set; }
        /// <summary>
        /// Gets or sets the maker checker object.
        /// </summary>
        /// <value>
        /// The maker checker object of type <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerObject&lt;T&gt;"/>
        /// </value>
        public MakerCheckerObject<T> MakerCheckerObject { get; set; }
    }

    /// <summary>
    /// The maker checker core delete operation event args class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MakerCheckerCoreDeleteOperationEventArgs<T> : MakerCheckerCoreEventArgs<T>
    {
        /// <summary>
        /// Gets or sets the maker user id.
        /// </summary>
        /// <value>
        /// The maker user id of type <see cref="System.String"/>
        /// </value>
        public string MakerUserId { get; set; }
        /// <summary>
        /// Gets or sets the operation status id.
        /// </summary>
        /// <value>
        /// The operation status id of type <see cref="System.Int32"/>
        /// </value>
        public int OperationStatusId { get; set; }
        /// <summary>
        /// Gets or sets the operation status.
        /// </summary>
        /// <value>
        /// The operation status of type <see cref="System.String"/>
        /// </value>
        public string OperationStatus { get; set; }
        /// <summary>
        /// Gets or sets the operation id.
        /// </summary>
        /// <value>
        /// The operation id of type <see cref="System.Int32"/>
        /// </value>
        public int OperationId { get; set; }
        /// <summary>
        /// Gets or sets the checker user id.
        /// </summary>
        /// <value>
        /// The checker user id of type <see cref="System.String"/>
        /// </value>
        public string CheckerUserId { get; set; }
        /// <summary>
        /// Gets or sets the operation comment.
        /// </summary>
        /// <value>
        /// The operation comment of type <see cref="System.String"/>
        /// </value>
        public string OperationComment { get; set; }
        /// <summary>
        /// Gets or sets the maker checker object.
        /// </summary>
        /// <value>
        /// The maker checker object of type <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerObject&lt;T&gt;"/>
        /// </value>
        public MakerCheckerObject<T> MakerCheckerObject { get; set; }
    }

    /// <summary>
    /// The maker checker core persist operation event args class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MakerCheckerCorePersistOperationEventArgs<T> : MakerCheckerCoreEventArgs<T>
    {
        /// <summary>
        /// Gets or sets the maker user id.
        /// </summary>
        /// <value>
        /// The maker user id of type <see cref="System.String"/>
        /// </value>
        public string MakerUserId { get; set; }
        /// <summary>
        /// Gets or sets the operation status id.
        /// </summary>
        /// <value>
        /// The operation status id of type <see cref="System.Int32"/>
        /// </value>
        public int OperationStatusId { get; set; }
        /// <summary>
        /// Gets or sets the operation status.
        /// </summary>
        /// <value>
        /// The operation status of type <see cref="System.String"/>
        /// </value>
        public string OperationStatus { get; set; }
        /// <summary>
        /// Gets or sets the operation id.
        /// </summary>
        /// <value>
        /// The operation id of type <see cref="System.Int32"/>
        /// </value>
        public int OperationId { get; set; }
        /// <summary>
        /// Gets or sets the checker user id.
        /// </summary>
        /// <value>
        /// The checker user id of type <see cref="System.String"/>
        /// </value>
        public string CheckerUserId { get; set; }
        /// <summary>
        /// Gets or sets the operation comment.
        /// </summary>
        /// <value>
        /// The operation comment of type <see cref="System.String"/>
        /// </value>
        public string OperationComment { get; set; }
        /// <summary>
        /// Gets or sets the maker checker object.
        /// </summary>
        /// <value>
        /// The maker checker object of type <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerCheckerObject&lt;T&gt;"/>
        /// </value>
        public MakerCheckerObject<T> MakerCheckerObject { get; set; }
    }
}
