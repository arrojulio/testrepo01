using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.Infrastructure.Repositories.SqlMappers
{
    public interface ISqlQueryBuilder
    {
        StringBuilder GetSqlSelectQuery();
        StringBuilder GetSqlUpdateQuery();
        StringBuilder GetSqlInsertQuery();
        StringBuilder GetSqlDeleteQuery();
    }
}
