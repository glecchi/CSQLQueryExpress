using System.Collections;
using SQLQueryBuilder.Fragments;

namespace SQLQueryBuilder.Dapper.CommandFactory.Query
{
    public abstract class SQLUpdateQueryCommand<T> : IEnumerable<string> where T : ISQLQueryEntity
    {
        public IEnumerator<string> GetEnumerator()
        {
            var result = SQLQueryCommandFactory.GetResult(GetQueryUpdate(), out SQLQueryCompiled queryTranslated);

            yield return "-----------------------------------------------------------------------------------------------------------------";

            yield return "Statement SQL eseguito:";

            yield return string.Empty;

            if (queryTranslated.Parameters.Count > 0)
            {
                foreach (var p in queryTranslated.Parameters)
                {
                    yield return $"DECLARE {p.Key} = {p.Value}";
                }

                yield return string.Empty;
            }

            yield return queryTranslated.Statement;

            yield return string.Empty;

            yield return "-----------------------------------------------------------------------------------------------------------------";

            yield return $"Risultato => {result}";

            yield return "-----------------------------------------------------------------------------------------------------------------";
        }

        protected abstract SQLQueryUpdate<T> GetQueryUpdate();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
