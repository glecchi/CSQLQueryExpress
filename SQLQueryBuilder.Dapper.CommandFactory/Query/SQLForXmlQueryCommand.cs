﻿using System.Collections;

namespace SQLQueryBuilder.Dapper.CommandFactory.Query
{
    //public abstract class SQLForXmlQueryCommand<T> : IEnumerable<string>
    //{
    //    public IEnumerator<string> GetEnumerator()
    //    {
    //        var reader = SQLQueryCommandFactory.GetReader(GetQueryForXml(), out SQLQueryCompiled queryTranslated);

    //        yield return "-----------------------------------------------------------------------------------------------------------------";

    //        yield return "Statement SQL eseguito:";

    //        yield return string.Empty;

    //        if (queryTranslated.Parameters.Count > 0)
    //        {
    //            foreach (var p in queryTranslated.Parameters)
    //            {
    //                yield return $"DECLARE {p.Key} = {p.Value}";
    //            }

    //            yield return string.Empty;
    //        }

    //        yield return queryTranslated.Statement;

    //        yield return string.Empty;

    //        yield return "-----------------------------------------------------------------------------------------------------------------";

    //        yield return "Risultato:";

    //        yield return string.Empty;

    //        foreach (var row in reader)
    //        {
    //            yield return row.DumpJson();
    //        }

    //        yield return string.Empty;

    //        yield return "-----------------------------------------------------------------------------------------------------------------";
    //    }

    //    protected abstract SQLQueryForXml<T> GetQueryForXml();

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}
}
