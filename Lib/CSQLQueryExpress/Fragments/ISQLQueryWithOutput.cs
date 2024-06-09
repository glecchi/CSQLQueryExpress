using System;
using System.Linq.Expressions;

namespace CSQLQueryExpress.Fragments
{
    internal interface ISQLQueryWithOutput<T> where T : ISQLQueryEntity
    {
        SQLQueryOutput<TS> Output<TS>(
            Expression<Func<T, TS>> output);

        SQLQueryOutput<T> Output(
            Expression<Func<T, object>> output,
            params Expression<Func<T, object>>[] otherOutput);

        SQLQueryOutput<TS> Output<TS>(
            Expression<Func<T, TS, object>> output,
            params Expression<Func<T, TS, object>>[] otherOutput);
    }

}
