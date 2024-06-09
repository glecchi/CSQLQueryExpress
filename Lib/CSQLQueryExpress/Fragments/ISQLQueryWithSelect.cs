using System;
using System.Linq.Expressions;

namespace CSQLQueryExpress.Fragments
{
    internal interface ISQLQueryWithSelect
    {
        SQLQuerySelect SelectAll();
    }

    internal interface ISQLQueryWithSelect<T> : ISQLQueryWithSelect
    {
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TS>> select);

        SQLQuerySelect<T> Select(
            Expression<Func<T, object>> select,
            params Expression<Func<T, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TS, object>> select,
            params Expression<Func<T, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1> : ISQLQueryWithSelect
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
    {
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, object>> select,
            params Expression<Func<T, TJ1, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TS, object>> select,
            params Expression<Func<T, TJ1, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2> : ISQLQueryWithSelect
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
    {
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, object>> select,
            params Expression<Func<T, TJ1, TJ2, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2, TJ3> : ISQLQueryWithSelect
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
    {
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4> : ISQLQueryWithSelect
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
    {
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5> : ISQLQueryWithSelect
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
    {
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> : ISQLQueryWithSelect
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
    {
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> : ISQLQueryWithSelect
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
    {
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> : ISQLQueryWithSelect
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
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> : ISQLQueryWithSelect
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
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> : ISQLQueryWithSelect
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
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> : ISQLQueryWithSelect
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
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> : ISQLQueryWithSelect
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
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> : ISQLQueryWithSelect
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
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>> select, 
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TS, object>> select, 
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TS, object>>[] otherSelect);
    }

    internal interface ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> : ISQLQueryWithSelect
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
        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, TS>> select);

        SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>> select, 
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] otherSelect);

        SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, TS, object>> select, 
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, TS, object>>[] otherSelect);
    }
}