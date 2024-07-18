using System.Collections.Generic;

namespace CSQLQueryExpress
{
    public interface ISQLQueryParametersBuilder
    {
        IDictionary<string, SQLQueryParameter> Parameters { get; }

        string AddParameter(object value);

        string AddStoredProcedureParameter(string name, object value, SQLQueryParameterDirection direction);
    }
}
