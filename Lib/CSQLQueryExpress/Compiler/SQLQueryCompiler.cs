﻿using System;
using System.Collections.Generic;
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
        /// <param name="query">SQL query expression built with <see cref="SQLQuery"/></param>
        /// <param name="settings">[OPTIONAL] SQL query compiler setting <see cref="SQLQueryCompilerSettings"/></param>
        /// <returns>A compiled expression <see cref="SQLQueryCompiled"/>.</returns>
        public static SQLQueryCompiled Compile(ISQLQuery query, SQLQueryCompilerSettings settings = null)
        {
            var compilerSettings = settings ?? new SQLQueryCompilerSettings();
            var parameterBuilder = new SQLQueryParametersBuilder(compilerSettings);
            var tableNameResolver = new SQLQueryTableNameResolver(compilerSettings);

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
            parameterBuilder.Initialize();
            tableNameResolver.Initialize();

            var translatedQuery = CompileQuery(query, parameterBuilder, tableNameResolver);

            return new SQLQueryCompiled(translatedQuery, new ReadOnlyCollection<SQLQueryParameter>(parameterBuilder.Parameters.Select(p => p.Value).ToList()));
        }

        internal static string CompileQuery(
            ISQLQuery query, 
            ISQLQueryParametersBuilder parameterBuilder,
            ISQLQueryTableNameResolver tableNameResolver)
        {
            var cteList = query.GetHierachicalSelectCte();
            if (cteList.Count > 1 && cteList.Select(t => t.GetType().GenericTypeArguments[0]).Distinct().Count() != cteList.Count)
            {
                throw new NotSupportedException("Multiple declaration of CTE TABLEs for the same Entity is not supported.");
            }

            var queryExpressionTranslator = new SQLQueryTranslator(parameterBuilder, tableNameResolver);

            var translatedQueryBuilder = new StringBuilder();

            var cteFragmentsTranslated = new List<ISQLQueryFragment>();

            if (cteList.Count > 0)
            {
                var idx = cteList.Count - 1;
                foreach (var cte in cteList)
                {
                    var withTableFragments = cte;

                    foreach (var fragment in withTableFragments)
                    {
                        if (cteFragmentsTranslated.Contains(fragment))
                        {
                            continue;
                        }

                        if (translatedQueryBuilder.Length == 0)
                        {
                            translatedQueryBuilder.Append("WITH ");
                        }
                        else
                        {
                            translatedQueryBuilder.Append($" {Environment.NewLine}");
                        }

                        translatedQueryBuilder.Append(fragment.Translate(queryExpressionTranslator));

                        cteFragmentsTranslated.Add(fragment);
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

            var allFragments = query.Where(f => cteFragmentsTranslated.Count == 0 || !cteFragmentsTranslated.Contains(f));

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
