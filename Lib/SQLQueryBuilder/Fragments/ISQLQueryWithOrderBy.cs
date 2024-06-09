using System;
using System.Linq.Expressions;

namespace SQLQueryBuilder.Fragments
{
    internal interface ISQLQueryWithOrderBy<T>
        where T : ISQLQueryEntity
    {
        SQLQueryOrder<T> OrderBy(
            Expression<Func<T, object>> orderBy,
            params Expression<Func<T, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1> OrderBy(
            Expression<Func<T, TJ1, object>> orderBy,
            params Expression<Func<T, TJ1, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2> OrderBy(
            Expression<Func<T, TJ1, TJ2, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2, TJ3> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
        where TJ8 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
        where TJ8 : ISQLQueryEntity
        where TJ9 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
        where TJ8 : ISQLQueryEntity
        where TJ9 : ISQLQueryEntity
        where TJ10 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
        where TJ8 : ISQLQueryEntity
        where TJ9 : ISQLQueryEntity
        where TJ10 : ISQLQueryEntity
        where TJ11 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
        where TJ8 : ISQLQueryEntity
        where TJ9 : ISQLQueryEntity
        where TJ10 : ISQLQueryEntity
        where TJ11 : ISQLQueryEntity
        where TJ12 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
        where TJ8 : ISQLQueryEntity
        where TJ9 : ISQLQueryEntity
        where TJ10 : ISQLQueryEntity
        where TJ11 : ISQLQueryEntity
        where TJ12 : ISQLQueryEntity
        where TJ13 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>> orderBy, 
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] otherOrderBy);
    }

    internal interface ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
        where TJ8 : ISQLQueryEntity
        where TJ9 : ISQLQueryEntity
        where TJ10 : ISQLQueryEntity
        where TJ11 : ISQLQueryEntity
        where TJ12 : ISQLQueryEntity
        where TJ13 : ISQLQueryEntity
        where TJ14 : ISQLQueryEntity
    {
        SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>> orderBy, 
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] otherOrderBy);
    }
}