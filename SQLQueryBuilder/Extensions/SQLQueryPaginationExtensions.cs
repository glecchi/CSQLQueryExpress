using SQLQueryBuilder.Fragments;
using SQLQueryBuilder.Statements;
using System;
using System.Linq.Expressions;

namespace SQLQueryBuilder.Extensions
{
    public static class SQLQueryPaginationExtensions
    {
        public static SQLQueryPage Offset(this SQLQueryPage obj, int offset)
        {
            return default;
        }

        public static object Fetch(this SQLQueryPage obj, int fetch)
        {
            return default;
        }

        public static int Over(this int obj, Expression<Func<RowNumberOver, object>> expression)
        {
            return default;
        }

        public static long Over(this long obj, Expression<Func<RowNumberOver, object>> expression)
        {
            return default;
        }

        public static RowNumberOver PartitionBy(this RowNumberOver over, params Expression<Func<object>>[] partitionBy)
        {
            return default;
        }

        public static RowNumberOver OrderBy(this RowNumberOver over, params Expression<Func<object>>[] orderBy)
        {
            return default;
        }
    }
}