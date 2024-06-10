using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SQLQueryBuilder.Fragments;

namespace SQLQueryBuilder
{
    public static class SQLQueryCompiler
    {
        public static SQLQueryCompiled Compile(ISQLQuery query)
        {
            var cteList = query.Where(f => f.FragmentType == SQLQueryFragmentType.SelectCte).ToList();
            if (cteList.Count > 1 && cteList.Select(t => t.GetType().GenericTypeArguments[0]).Distinct().Count() != cteList.Count)
            {
                throw new NotSupportedException("Multiple declaration of 'WITH TABLE AS..' for the same Entity is not supported");
            }

            var parameterBuilder = new SQLQueryExpressionParametersBuilder();
            var tableNameResolver = new SQLQueryExpressionTableNameResolver();
            var queryExpressionTranslator = new SQLQueryExpressionTranslator(parameterBuilder, tableNameResolver);

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
                            translatedQueryBuilder.Append(Environment.NewLine);
                        }

                        translatedQueryBuilder.Append(fragment.Translate(queryExpressionTranslator));
                    }

                    if (idx-- > 0)
                    {
                        translatedQueryBuilder.Append($"{Environment.NewLine}), ");
                    }
                    else
                    {
                        translatedQueryBuilder.Append($"{Environment.NewLine}) {Environment.NewLine}");
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

                if (translatedQueryBuilder.Length > 0)
                {
                    translatedQueryBuilder.Append(Environment.NewLine);
                }
                                
                translatedQueryBuilder.Append(translatedFragment);
            }

            var translatedQuery = translatedQueryBuilder.ToString();

            return new SQLQueryCompiled { Statement = translatedQuery, Parameters = parameterBuilder.Parameters.Select(p => p.Value).ToList() };
        }
    }

    public static class SQLQueryBuilderExtensions
    {
        public static SQLQueryCompiled Compile(this ISQLQuery query)
        {
            return SQLQueryCompiler.Compile(query);
        }
    }

    public class SQLQueryCompiled
    {
        public string Statement { get; set; }

        public IList<SQLQueryParameter> Parameters { get; set; }
    }
}
