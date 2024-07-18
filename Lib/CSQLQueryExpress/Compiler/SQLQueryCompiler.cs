using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CSQLQueryExpress
{
    /// <summary>
    /// Used to compile a <see cref="ISQLQuery"/> instance in a <see cref="SQLQueryCompiled"/>.
    /// </summary>
    public static class SQLQueryCompiler
    {
        /// <summary>
        /// Compile a <see cref="ISQLQuery"/> instance in a <see cref="SQLQueryCompiled"/>.
        /// </summary>
        /// <param name="query">SQL query expression built with <see cref="SQLQuery"/>,</param>
        /// <returns>A compiled expression <see cref="SQLQueryCompiled"/>.</returns>
        public static SQLQueryCompiled Compile(ISQLQuery query)
        {            
            var parameterBuilder = new SQLQueryParametersBuilder();
            var tableNameResolver = new SQLQueryTableNameResolver();
            
            return Compile(query, parameterBuilder, tableNameResolver);
        }

        /// <summary>
        /// Compile a <see cref="ISQLQuery"/> instance in a <see cref="SQLQueryCompiled"/>.
        /// </summary>
        /// <param name="query">SQL query expression built with <see cref="SQLQuery"/>,</param>
        /// <param name="parameterBuilder">The SQL query parameter builder.</param>
        /// <param name="tableNameResolver">The SQL query table name resolver.</param>
        /// <returns>A compiled expression <see cref="SQLQueryCompiled"/>.</returns>
        public static SQLQueryCompiled Compile(ISQLQuery query,
            ISQLQueryParametersBuilder parameterBuilder,
            ISQLQueryTableNameResolver tableNameResolver)
        {
            var translatedQuery = CompileQuery(query, parameterBuilder, tableNameResolver);

            return new SQLQueryCompiled(translatedQuery, new ReadOnlyCollection<SQLQueryParameter>(parameterBuilder.Parameters.Select(p => p.Value).ToList()));
        }

        internal static string CompileQuery(
            ISQLQuery query, 
            ISQLQueryParametersBuilder parameterBuilder,
            ISQLQueryTableNameResolver tableNameResolver)
        {
            var cteList = query.Where(f => f.FragmentType == SQLQueryFragmentType.SelectCte).ToList();
            if (cteList.Count > 1 && cteList.Select(t => t.GetType().GenericTypeArguments[0]).Distinct().Count() != cteList.Count)
            {
                throw new NotSupportedException("Multiple declaration of CTE TABLEs for the same Entity is not supported.");
            }

            var queryExpressionTranslator = new SQLQueryTranslator(parameterBuilder, tableNameResolver);

            var translatedQueryBuilder = new StringBuilder();

            if (cteList.Count > 0)
            {
                var idx = cteList.Count - 1;
                foreach (var cte in cteList)
                {
                    var withTableFragments = (ISQLQuery)cte;

                    foreach (var fragment in withTableFragments)
                    {
                        if (translatedQueryBuilder.Length == 0)
                        {
                            translatedQueryBuilder.Append("WITH ");
                        }
                        else
                        {
                            translatedQueryBuilder.Append($" {Environment.NewLine}");
                        }

                        translatedQueryBuilder.Append(fragment.Translate(queryExpressionTranslator));
                    }

                    if (idx-- > 0)
                    {
                        translatedQueryBuilder.Append($"{Environment.NewLine}),");
                    }
                    else
                    {
                        translatedQueryBuilder.Append($"{Environment.NewLine}){Environment.NewLine}");
                    }
                }
            }

            var allFragments = query.Where(f => cteList.Count == 0 || !cteList.Contains(f));

            foreach (var fragment in allFragments)
            {
                var translatedFragment = fragment.Translate(queryExpressionTranslator);

                if (string.IsNullOrWhiteSpace(translatedFragment))
                {
                    continue;
                }

                if (translatedQueryBuilder.Length > 0 &&
                    fragment.FragmentType != SQLQueryFragmentType.Batch &&
                    fragment.FragmentType != SQLQueryFragmentType.MultipleResultSets)
                {
                    translatedQueryBuilder.Append($" {Environment.NewLine}");
                }

                translatedQueryBuilder.Append(translatedFragment);
            }

            var translatedQuery = translatedQueryBuilder.ToString();

            return translatedQuery;
        }
    }
}
