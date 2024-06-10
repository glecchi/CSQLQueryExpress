using System.Collections.Generic;

namespace CSQLQueryExpress
{

    public interface ISQLQueryExpressionParametersBuilder
    {
        IDictionary<string, SQLQueryParameter> Parameters { get; }

        string AddParameter(object value);

        string AddStoredProcedureParameter(string name, object value, SQLQueryParameterValueDirection direction);
    }

    internal class SQLQueryExpressionParametersBuilder : ISQLQueryExpressionParametersBuilder
    {
        public IDictionary<string, SQLQueryParameter> Parameters { get; } = new Dictionary<string, SQLQueryParameter>();

        public string AddParameter(object value)
        {
            var parameterName = $"@p{Parameters.Count}";
            Parameters.Add(parameterName, new SQLQueryParameter(parameterName, value));
            return parameterName;
        }

        public string AddStoredProcedureParameter(string name, object value, SQLQueryParameterValueDirection direction)
        {
            var parameterName = $"@{name}";
            Parameters.Add(parameterName, new SQLQueryParameter(parameterName, value, direction));
            return parameterName;
        }

    }
}
