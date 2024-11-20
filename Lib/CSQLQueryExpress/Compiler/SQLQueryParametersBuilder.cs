using System.Collections.Generic;
using System.Runtime;

namespace CSQLQueryExpress
{
    public sealed class SQLQueryParametersBuilder : ISQLQueryParametersBuilder
    {
        IDictionary<string, SQLQueryParameter> ISQLQueryParametersBuilder.Parameters { get; } = new Dictionary<string, SQLQueryParameter>();
                
        private readonly SQLQueryCompilerSettings _settings;

        public SQLQueryParametersBuilder(SQLQueryCompilerSettings settings)
        {
            _settings = settings;
        }

        void ISQLQueryParametersBuilder.Initialize()
        {
            ((ISQLQueryParametersBuilder)this).Parameters.Clear();
        }

        string ISQLQueryParametersBuilder.AddParameter(object value)
        {
            var parameterName = $"{_settings.QueryParameterPrefix}{((ISQLQueryParametersBuilder)this).Parameters.Count}";
            ((ISQLQueryParametersBuilder)this).Parameters.Add(parameterName, new SQLQueryParameter(parameterName, value));
            return parameterName;
        }

        string ISQLQueryParametersBuilder.AddStoredProcedureParameter(string name, object value, SQLQueryParameterDirection direction)
        {
            var parameterName = $"{_settings.StoredProcedureParameterPrefix}{name}";
            ((ISQLQueryParametersBuilder)this).Parameters.Add(parameterName, new SQLQueryParameter(parameterName, value, direction));
            return parameterName;
        }
    }
}
