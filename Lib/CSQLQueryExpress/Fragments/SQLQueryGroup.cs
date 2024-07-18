using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CSQLQueryExpress.Fragments
{
    public abstract class SQLQueryGroup : ISQLQueryFragment,
        ISQLQueryWithSelect
    {
        protected IList<ISQLQueryFragment> Fragments;
        private readonly Expression[] _group;

        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, Expression group, Expression[] otherGroup)
        {
            Fragments = fragments;
            _group = group.Merge(otherGroup);

            Fragments.Add(this);
        }

        public SQLQuerySelect SelectAll()
        {
            return new SQLQuerySelect(Fragments);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Group; } }

        public string Translate(ISQLQueryTranslator expressionTranslator)
        {
            return $"GROUP BY {string.Join(", ", _group.Select(g => expressionTranslator.Translate(g)))}";
        }
    }

    public class SQLQueryGroup<T> : SQLQueryGroup, ISQLQueryFragment,
        ISQLQueryWithSelect<T>, ISQLQueryWithOrderBy<T>
        where T : ISQLQueryEntity
    {
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, object>> group, 
            Expression<Func<T, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T> Having(
            Expression<Func<T, object>> having, 
            params Expression<Func<T, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T> OrderBy(
            Expression<Func<T, object>> orderBy, 
            params Expression<Func<T, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect<T> Select(
            Expression<Func<T, object>> select,
            params Expression<Func<T, object>>[] otherSelect)
        {
            return new SQLQuerySelect<T>(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TS, object>> select,
            params Expression<Func<T, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1> : SQLQueryGroup, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1>, ISQLQueryWithOrderBy<T, TJ1>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
    {
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, object>> group, 
            Expression<Func<T, TJ1, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1> Having(
           Expression<Func<T, TJ1, object>> having,
           params Expression<Func<T, TJ1, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1> OrderBy(
            Expression<Func<T, TJ1, object>> orderBy, 
            params Expression<Func<T, TJ1, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, object>> select,
            params Expression<Func<T, TJ1, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TS, object>> select,
            params Expression<Func<T, TJ1, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2> : SQLQueryGroup, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2>, ISQLQueryWithOrderBy<T, TJ1, TJ2>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
    {
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, object>> group, 
            Expression<Func<T, TJ1, TJ2, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2> Having(
           Expression<Func<T, TJ1, TJ2, object>> having,
           params Expression<Func<T, TJ1, TJ2, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2> OrderBy(
            Expression<Func<T, TJ1, TJ2, object>> orderBy, 
            params Expression<Func<T, TJ1, TJ2, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, object>> select,
            params Expression<Func<T, TJ1, TJ2, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2, TJ3> : SQLQueryGroup, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
    {
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, object>> group, 
            Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2, TJ3> Having(
           Expression<Func<T, TJ1, TJ2, TJ3, object>> having,
           params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2, TJ3>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, object>> orderBy, 
            params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4> : SQLQueryGroup, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
    {
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>> group, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4> Having(
           Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>> having,
           params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5> : SQLQueryGroup, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
    {
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>> group, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5> Having(
           Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>> having,
           params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> : SQLQueryGroup, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
    {
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>> group, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> Having(
           Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>> having,
           params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> : SQLQueryGroup, ISQLQueryFragment,
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
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>> group, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> Having(
           Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>> having,
           params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> : SQLQueryGroup, ISQLQueryFragment,
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
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>> group, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> Having(
           Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>> having,
           params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> : SQLQueryGroup, ISQLQueryFragment,
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
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>> group, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> Having(
           Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>> having,
           params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> : SQLQueryGroup, ISQLQueryFragment,
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
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>> group, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> Having(
           Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>> having,
           params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> : SQLQueryGroup, ISQLQueryFragment,
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
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>> group, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> Having(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>> having,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> : SQLQueryGroup, ISQLQueryFragment,
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
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>> group, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> Having(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>> having,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> : SQLQueryGroup, ISQLQueryFragment,
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
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>> group, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> Having(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>> having,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }

    public class SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> : SQLQueryGroup, ISQLQueryFragment, 
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
        internal SQLQueryGroup(IList<ISQLQueryFragment> fragments,
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>> group,
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] otherGroup)
            : base(fragments, group, otherGroup)
        {

        }

        public SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> Having(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>> having,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] otherHaving)
        {
            return new SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14>(Fragments, having, otherHaving);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> OrderBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>> orderBy,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge());
        }

        public SQLQuerySelect Select(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] otherSelect)
        {
            return new SQLQuerySelect(Fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, TS, object>> select,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(Fragments, select.Merge(otherSelect));
        }
    }
}
