using System;
using System.Linq.Expressions;

namespace CSQLQueryExpress
{
    public interface ISQLQueryTranslator
    {
        string Translate(Expression expression, SQLQueryFragmentType fragmentType);

        string MakeParameter(object value);

        string MakeStoredProcedureParameter(string name, object value, SQLQueryParameterDirection direction);

        string GetTableAlias(Type GetTableAlias);

        string GetTableName(Type tableType);

        string GetColumnsWithoutTableAlias(string columns);
    }
}