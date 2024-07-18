using System.Collections.Generic;
using System.Linq;

namespace CSQLQueryExpress
{
    /// <summary>
    /// A compiled expression of <see cref="ISQLQuery"/> instance.
    /// </summary>
    public sealed class SQLQueryCompiled
    {
        internal SQLQueryCompiled(string statement, IList<SQLQueryParameter> parameters)
        {
            Statement = statement;
            Parameters = parameters;
            ParametersKeyValue = parameters.ToDictionary(p => p.Name, p => p.Value);
        }

        /// <summary>
        /// The TSQL statement.
        /// </summary>
        public string Statement { get; }

        /// <summary>
        /// The list of parameters.
        /// </summary>
        public IList<SQLQueryParameter> Parameters { get; }

        /// <summary>
        /// The KeyValue parameters list. 
        /// </summary>
        public IDictionary<string, object> ParametersKeyValue { get; }
    }
}
