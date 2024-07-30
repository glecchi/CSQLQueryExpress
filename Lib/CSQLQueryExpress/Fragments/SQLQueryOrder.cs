using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CSQLQueryExpress.Fragments
{
    public abstract class SQLQueryOrder : ISQLQueryFragment,
        ISQLQueryWithSelect
    {
        protected readonly IList<ISQLQueryFragment> Fragments;
        private readonly Expression[] _orderBy;

        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression orderBy, Expression[] otherOrderBy)
        {
            Fragments = fragments;
            _orderBy = orderBy.Merge(otherOrderBy);

            Fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Order; } }

        public SQLQuerySelect SelectAll()
        {
            return new SQLQuerySelect(Fragments);
        }

        protected void Paging(Expression paging)
        {
            Fragments.Add(new SQLQueryPage(paging));
        }

        public string Translate(ISQLQueryTranslator expressionTranslator)
        {
            return $"ORDER BY {string.Join(", ", _orderBy.Select(o => expressionTranslator.Translate(o, FragmentType)))}";
        }
    }

    public class SQLQueryOrder<T> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T>
        where T : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, object>> order, 
            Expression<Func<T, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, object>> order, 
            Expression<Func<T, TJ1, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, object>> order, 
            Expression<Func<T, TJ1, TJ2, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, object>> order, 
            Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>> order, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>> order, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>> order, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>> order, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>> order, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>> order, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>> order, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>> order, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>> order, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>> order, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> : SQLQueryOrder, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14>
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>> order, 
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] otherOrder)
            : base(fragments, order, otherOrder)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
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
