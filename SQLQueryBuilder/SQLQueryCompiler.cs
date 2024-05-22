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
            var withTable = query.Where(f => f is SQLQuerySelect sel && sel.UseWithTable).ToList();
            if (withTable.Count > 1)
            {
                throw new NotSupportedException("Multiple declaration of 'WITH TABLE AS..' is not supported");
            }

            var parameterBuilder = new SQLQueryExpressionParametersBuilder();
            var tableNameResolver = new SQLQueryExpressionTableNameResolver();
            var queryExpressionTranslator = new SQLQueryExpressionTranslator(parameterBuilder, tableNameResolver);

            var translatedQueryBuilder = new StringBuilder();

            if (withTable.Count > 0)
            {
                var withTableFragments = (ISQLQuery)withTable[0];

                foreach (var fragment in withTableFragments)
                {
                    if (translatedQueryBuilder.Length > 0)
                    {
                        translatedQueryBuilder.Append(Environment.NewLine);
                    }

                    translatedQueryBuilder.Append(fragment.Translate(queryExpressionTranslator));
                }

                translatedQueryBuilder.Append($"{Environment.NewLine}) {Environment.NewLine}");
            }

            var allFragments = query.Where(f => withTable.Count == 0 || f != withTable[0]);

            foreach (var fragment in allFragments)
            {
                if (translatedQueryBuilder.Length > 0)
                {
                    translatedQueryBuilder.Append(Environment.NewLine);
                }

                translatedQueryBuilder.Append(fragment.Translate(queryExpressionTranslator));
            }

            var translatedQuery = translatedQueryBuilder.ToString();

            return new SQLQueryCompiled { Statement = translatedQuery, Parameters = parameterBuilder.Parameters };
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

        public IDictionary<string, object> Parameters { get; set; }
    }
}
