using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLQueryBuilder.Fragments;

namespace SQLQueryBuilder.Dapper.CommandFactory.Query
{

    public abstract class SQLDropQueryCommand<T> : IEnumerable<string> where T : ISQLQueryEntity
    {
        public IEnumerator<string> GetEnumerator()
        {
            var result = SQLQueryCommandFactory.GetResult(GetQueryDrop(), out SQLQueryCompiled queryTranslated);

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

        protected abstract SQLQueryDrop<T> GetQueryDrop();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
