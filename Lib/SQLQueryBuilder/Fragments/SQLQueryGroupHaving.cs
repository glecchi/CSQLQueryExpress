using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SQLQueryBuilder.Fragments
{
    public abstract class SQLQueryGroupHaving : ISQLQueryFragment,
        ISQLQueryWithSelect
    {
        protected IList<ISQLQueryFragment> Fragments;
        private readonly Expression[] _having;

        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression having, Expression[] otherHaving)
        {
            Fragments = fragments;
            _having = having.Merge(otherHaving);

            Fragments.Add(this);
        }

        public SQLQuerySelect SelectAll()
        {
            return new SQLQuerySelect(Fragments);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.GroupHaving; } }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            return $"HAVING {string.Join(", ", _having.Select(h => expressionTranslator.Translate(h)))}";
        }
    }

    public class SQLQueryGroupHaving<T> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T>, ISQLQueryWithOrderBy<T>
        where T : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, object>> having, Expression<Func<T, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T> OrderBy(Expression<Func<T, object>> orderBy, params Expression<Func<T, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect<T> Select(Expression<Func<T, object>> select, params Expression<Func<T, object>>[] otherSelect)
        {
            return new SQLQuerySelect<T>(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TS, object>> select, params Expression<Func<T, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroupHaving<T, TJ1> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1>, ISQLQueryWithOrderBy<T, TJ1>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, object>> having, Expression<Func<T, TJ1, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1> OrderBy(Expression<Func<T, TJ1, object>> orderBy, params Expression<Func<T, TJ1, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, object>> select, params Expression<Func<T, TJ1, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TS, object>> select, params Expression<Func<T, TJ1, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroupHaving<T, TJ1, TJ2> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2>, ISQLQueryWithOrderBy<T, TJ1, TJ2>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, object>> having, Expression<Func<T, TJ1, TJ2, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2> OrderBy(Expression<Func<T, TJ1, TJ2, object>> orderBy, params Expression<Func<T, TJ1, TJ2, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, object>> select, params Expression<Func<T, TJ1, TJ2, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, object>> having, Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, TJ3, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>> having, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>> having, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>> having, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>> having, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>> having, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>> having, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>> having, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>> having, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>> having, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>> having, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> : SQLQueryGroupHaving, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14>
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>> having, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] otherHaving)
            : base(fragments, having, otherHaving)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, TS, object>> select, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
}
