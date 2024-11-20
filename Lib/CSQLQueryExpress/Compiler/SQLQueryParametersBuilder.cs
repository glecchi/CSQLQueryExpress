using System.Collections.Generic;
using System.Runtime;

namespace CSQLQueryExpress
{
    internal class SQLQueryParametersBuilder : ISQLQueryParametersBuilder
    {
        public IDictionary<string, SQLQueryParameter> Parameters { get; } = new Dictionary<string, SQLQueryParameter>();
                
        private readonly SQLQueryCompilerSettings _settings;

        public SQLQueryParametersBuilder(SQLQueryCompilerSettings settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            Parameters.Clear();
        }

        public string AddParameter(object value)
        {
            var parameterName = $"{_settings.QueryParameterPrefix}{Parameters.Count}";
            Parameters.Add(parameterName, new SQLQueryParameter(parameterName, value));
            return parameterName;
        }

        public string AddStoredProcedureParameter(string name, object value, SQLQueryParameterDirection direction)
        {
            var parameterName = $"{_settings.StoredProcedureParameterPrefix}{name}";
            Parameters.Add(parameterName, new SQLQueryParameter(parameterName, value, direction));
            return parameterName;
        }
    }
}
