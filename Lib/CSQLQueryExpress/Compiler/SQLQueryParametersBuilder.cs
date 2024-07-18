using System.Collections.Generic;

namespace CSQLQueryExpress
{
    internal class SQLQueryParametersBuilder : ISQLQueryParametersBuilder
    {
        public IDictionary<string, SQLQueryParameter> Parameters { get; } = new Dictionary<string, SQLQueryParameter>();

        public string AddParameter(object value)
        {
            var parameterName = $"@p{Parameters.Count}";
            Parameters.Add(parameterName, new SQLQueryParameter(parameterName, value));
            return parameterName;
        }

        public string AddStoredProcedureParameter(string name, object value, SQLQueryParameterDirection direction)
        {
            var parameterName = $"@{name}";
            Parameters.Add(parameterName, new SQLQueryParameter(parameterName, value, direction));
            return parameterName;
        }

    }
}
