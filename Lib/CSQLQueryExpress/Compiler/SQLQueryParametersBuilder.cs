using System.Collections.Generic;

namespace CSQLQueryExpress
{
    public sealed class SQLQueryParametersBuilder : ISQLQueryParametersBuilder
    {
        IDictionary<string, SQLQueryParameter> ISQLQueryParametersBuilder.Parameters { get; } = new Dictionary<string, SQLQueryParameter>();

        private readonly string _parameterPrefix;

        public SQLQueryParametersBuilder(string parameterPrefix = "@")
        {
            _parameterPrefix = parameterPrefix;
        }

        string ISQLQueryParametersBuilder.AddParameter(object value)
        {
            var parameterName = $"{_parameterPrefix}p{((ISQLQueryParametersBuilder)this).Parameters.Count}";
            ((ISQLQueryParametersBuilder)this).Parameters.Add(parameterName, new SQLQueryParameter(parameterName, value));
            return parameterName;
        }

        string ISQLQueryParametersBuilder.AddStoredProcedureParameter(string name, object value, SQLQueryParameterDirection direction)
        {
            var parameterName = $"{_parameterPrefix}{name}";
            ((ISQLQueryParametersBuilder)this).Parameters.Add(parameterName, new SQLQueryParameter(parameterName, value, direction));
            return parameterName;
        }        
    }
}
