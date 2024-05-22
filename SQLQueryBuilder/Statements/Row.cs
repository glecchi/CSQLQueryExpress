using System;
using System.Linq.Expressions;

namespace SQLQueryBuilder.Statements
{
    public static class Row
    {
        public static int Number()
        {
            return default;
        }
    }

    public class RowNumber
    {
        public int Over(Expression<Func<RowNumberOver, object>> expression)
        {
            return default;
        }
    }

    public class RowNumberOver
    {

    }
}
