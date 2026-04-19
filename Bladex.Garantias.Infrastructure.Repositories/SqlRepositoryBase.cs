namespace Bladex.Garantias.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;
    using DomainBase;
    using EntityFactoryFramework;
    using Logging;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using RepositoryFramework;

    /// <summary>
    /// Provides the base functionality for all sql repositories
    /// </summary>
    /// <typeparam name="T">An <see cref="EntityBase"/> which will be treated by the _repository.</typeparam>
    public abstract class SqlRepositoryBase<T> : RepositoryBase<T>
        where T : EntityBase
    {

        protected ILogger _logger = Bladex.Garantias.Infrastructure.Logging.ApplicationLogger.Instance;

        #region AppendChildData Delegate

        /// <summary>
        /// The delegate signature required for callback methods
        /// </summary>
        /// <param name="entityAggregate"></param>
        /// <param name="childEntityKey"></param>
        public delegate void AppendChildData(T entityAggregate, 
            object childEntityKeyValue);

        #endregion

        #region Private Fields

        private Database database;
        private IEntityFactory<T> entityFactory;
        private Dictionary<string, AppendChildData> childCallbacks;
        private string baseQuery;
        private string baseWhereClause;
        private string entityName;
        private string keyFieldName;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlRepositoryBase&lt;T&gt;"/> class.
        /// </summary>
        protected SqlRepositoryBase() 
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlRepositoryBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        protected SqlRepositoryBase(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            this.database = DatabaseFactory.CreateDatabase("BLX_GARANTIAS");
            this.entityFactory = EntityFactoryBuilder.BuildFactory<T>();
            this.childCallbacks = new Dictionary<string, AppendChildData>();
            this.BuildChildCallbacks();
            this.baseQuery = this.GetBaseQuery();
            this.baseWhereClause = this.GetBaseWhereClause();
            this.entityName = this.GetEntityName();
            this.keyFieldName = this.GetKeyFieldName();
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Get the base query to retrieve entities.
        /// </summary>
        /// <returns>SQL Select Query to retrieve entities</returns>
        protected abstract string GetBaseQuery();

        /// <summary>
        /// Get the where clause used to retrieve one entity.
        /// </summary>
        /// <returns>SQL Where Clases to retrieve one entity.</returns>
        protected abstract string GetBaseWhereClause();

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected abstract string GetEntityName();

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected abstract string GetKeyFieldName();

        /// <summary>
        /// Builds the child callbacks.
        /// </summary>
        protected abstract void BuildChildCallbacks();
        
        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        protected abstract override T PersistNewItem(T item);
        
        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        protected abstract override T PersistUpdatedItem(T item);
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets the database.
        /// </summary>
        protected Database Database
        {
            get { return this.database; }
        }

        /// <summary>
        /// Gets the child callbacks.
        /// </summary>
        protected Dictionary<string, AppendChildData> ChildCallbacks
        {
            get { return this.childCallbacks; }
        }

        /// <summary>
        /// Gets the base query.
        /// </summary>
        internal string BaseQuery
        {
            get { return this.baseQuery; }
        }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        protected string EntityName
        {
            get { return this.entityName; }
        }

        /// <summary>
        /// Gets the name of the key field.
        /// </summary>
        /// <value>
        /// The name of the key field.
        /// </value>
        protected string KeyFieldName
        {
            get { return this.keyFieldName; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public override long Count()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT COUNT(*) FROM {0}", this.GetEntityName());
            builder.Append(";");
            return Convert.ToInt32(this.Database.ExecuteScalar(this.Database.GetSqlStringCommand(builder.ToString())));
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public override IList<T> GetAll()
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(this.baseQuery);
                builder.Append(";");
                return this.BuildEntitiesFromSql(builder.ToString());
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Error en GetAll() method of {0}. Devolviendo lista vacia.", this.GetType().Name), ex);
                return new List<T>();
            }

        }

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override T FindBy(object key)
        {
            try
            {
                StringBuilder builder = this.GetBaseQueryBuilder();
                builder.Append(this.BuildBaseWhereClause(key));
                return this.BuildEntityFromSql(builder.ToString());
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Error en FindBy({0}) method of {1}. Devolviendo null.", key, this.GetType().Name), ex);
                return null;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        protected IDataReader ExecuteReader(string sql)
        {
            DbCommand command = this.database.GetSqlStringCommand(sql);
            return this.database.ExecuteReader(command);
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        protected IDataReader ExecuteReader(DbCommand command)
        {
            return this.database.ExecuteReader(command);
        }

        /// <summary>
        /// Builds the entity from SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        protected virtual T BuildEntityFromSql(string sql)
        {
            T entity = default(T);
            using (IDataReader reader = this.ExecuteReader(sql))
            {
                if (reader.Read())
                {
                    entity = this.BuildEntityFromReader(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// Builds the entity from SQL.
        /// </summary>
        /// <param name="command">The SQL.</param>
        /// <returns></returns>
        protected virtual T BuildEntityFromSql(DbCommand command)
        {
            T entity = default(T);
            using (IDataReader reader = this.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    entity = this.BuildEntityFromReader(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// Builds the entity from reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        protected virtual T BuildEntityFromReader(IDataReader reader)
        {
            DataTable schemaTable = (this.childCallbacks != null && this.childCallbacks.Count > 0)
                ? reader.GetSchemaTable()
                : null;
            return BuildEntityFromReader(reader, schemaTable);
        }

        protected virtual T BuildEntityFromReader(IDataReader reader, DataTable schemaTable)
        {
            T entity = this.entityFactory.BuildEntity(reader);
            if (this.childCallbacks != null && this.childCallbacks.Count > 0 && schemaTable != null)
            {
                object childKeyValue = null;
                foreach (string childKeyName in this.childCallbacks.Keys)
                {
                    if (DataHelper.ReaderContainsColumnName(schemaTable, childKeyName))
                    {
                        if (reader.IsDBNull(reader.GetOrdinal(childKeyName)))
                        {
                            // Skip the lookup if FK is null.
                            continue;
                        }
                        childKeyValue = reader[childKeyName];
                    }
                    else
                    {
                        childKeyValue = null;
                    }
                    this.childCallbacks[childKeyName](entity, childKeyValue);
                }
            }
            entity.IsDirty = false;
            return entity;
        }

        /// <summary>
        /// Builds the entities from SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        protected virtual List<T> BuildEntitiesFromSql(string sql)
        {
            List<T> entities = new List<T>();
            _logger.Debug(sql);

            using (IDataReader reader = this.ExecuteReader(this.Database.GetSqlStringCommand(sql)))
            {
                DataTable schemaTable = (this.childCallbacks != null && this.childCallbacks.Count > 0)
                    ? reader.GetSchemaTable()
                    : null;

                while (reader.Read())
                {
                    try
                    {
                        entities.Add(this.BuildEntityFromReader(reader, schemaTable));
                    }
                    catch(Exception ex)
                    {
                        _logger.Error(string.Format("Error building entity of type {0} at SqlRepositoryBase. Omitting entity.", typeof(T)), ex);
                    }
                }
            }
            return entities;
        }

        /// <summary>
        /// Builds the entities from SQL.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        protected virtual List<T> BuildEntitiesFromSql(DbCommand command)
        {
            List<T> entities = new List<T>();
            using (IDataReader reader = this.ExecuteReader(command))
            {
                DataTable schemaTable = (this.childCallbacks != null && this.childCallbacks.Count > 0)
                    ? reader.GetSchemaTable()
                    : null;

                while (reader.Read())
                {
                    entities.Add(this.BuildEntityFromReader(reader, schemaTable));
                }
            }
            return entities;
        }

        /// <summary>
        /// Builds the base where clause.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected virtual string BuildBaseWhereClause(object key)
        {
            string whereClause = string.Format(this.baseWhereClause, DataHelper.GetSqlValue(key));
            return whereClause;
        }

        /// <summary>
        /// Gets the base query builder.
        /// </summary>
        /// <returns></returns>
        protected virtual StringBuilder GetBaseQueryBuilder()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.baseQuery);
            return builder;
        }

        /// <summary>
        /// Persists the deleted item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected override void PersistDeletedItem(T item)
        {
            // Delete the Entity
            
            string query = string.Format("DELETE FROM {0} {1}",
                this.entityName,
                this.BuildBaseWhereClause(item.Key));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(query));
            _logger.Debug(string.Format("Entity of type {0} with identifier {1} has been deleted.", item.GetType(), item.Key));
        }

        #endregion
    }
}