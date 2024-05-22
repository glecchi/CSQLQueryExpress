using System.Collections;
using SQLQueryBuilder.Fragments;

namespace SQLQueryBuilder.Dapper.CommandFactory.Query
{
    public abstract class SQLSelectQueryCommand<T> : IEnumerable<string>
    {
        public IEnumerator<string> GetEnumerator()
        {
            var reader = SQLQueryCommandFactory.GetReader(GetQuerySelect(), out SQLQueryCompiled queryTranslated);

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

            yield return "Risultato:";

            yield return string.Empty;

            foreach (var row in reader)
            {
                yield return row.DumpJson();
            }

            yield return string.Empty;

            yield return "-----------------------------------------------------------------------------------------------------------------";
        }

        protected abstract SQLQuerySelect<T> GetQuerySelect();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
