using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQueryBuilder
{
    public interface ISQLQueryExpressionParametersBuilder
    {
        IDictionary<string, object> Parameters { get; }

        string AddParameter(object value);
    }

    internal class SQLQueryExpressionParametersBuilder : ISQLQueryExpressionParametersBuilder
    {
        public IDictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        public string AddParameter(object value)
        {
            var parameterName = $"@p{Parameters.Count}";
            Parameters.Add(parameterName, value);
            return parameterName;
        }

    }
}
