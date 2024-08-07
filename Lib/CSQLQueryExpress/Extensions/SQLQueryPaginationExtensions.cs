﻿using CSQLQueryExpress.Fragments;
using System;
using System.Linq.Expressions;

namespace CSQLQueryExpress
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

        public static T Over<T>(this T obj, Expression<Func<IRowNumberOver, object>> expression) where T : struct
        {
            return default;
        }

        public static T Over<T>(this T? obj, Expression<Func<IRowNumberOver, object>> expression) where T : struct
        {
            return default;
        }

        public static IRowNumberOver PartitionBy(this IRowNumberOver over, params object[] partitionBy)
        {
            return default;
        }

        public static IRowNumberOver OrderBy(this IRowNumberOver over, params object[] orderBy)
        {
            return default;
        }
    }
}