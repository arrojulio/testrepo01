using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.Infrastructure.Repositories.SqlMappers
{
    public class SqlQueryBuilder : ISqlQueryBuilder
    {
        
        public SqlQueryBuilder()
        {
            
        }

        public string SelectQuery;
        public string UpdateQuery;
        public string InsertQuery;
        public string DeleteQuery;

        #region ISqlQueryBuilder Members

        public StringBuilder GetSqlSelectQuery()
        {
            return new StringBuilder(this.SelectQuery);
        }

        public StringBuilder GetSqlUpdateQuery()
        {
            return new StringBuilder(this.UpdateQuery);
        }

        public StringBuilder GetSqlInsertQuery()
        {
            return new StringBuilder(this.InsertQuery);
        }

        public StringBuilder GetSqlDeleteQuery()
        {
            return new StringBuilder(this.DeleteQuery);
        }

        #endregion
    }
}
