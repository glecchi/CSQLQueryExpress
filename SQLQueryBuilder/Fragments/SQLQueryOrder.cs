using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SQLQueryBuilder.Fragments
{
    public abstract class SQLQueryOrder : ISQLQueryFragment
    {
        protected readonly IList<ISQLQueryFragment> Fragments;
        private Expression[] _orderBy;

        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression[] orderBy)
        {
            Fragments = fragments;
            _orderBy = orderBy;

            Fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Order; } }

        public void Paging(Expression paging)
        {
            Fragments.Add(new SQLQueryPage(paging));
        }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            return $"ORDER BY {string.Join(", ", _orderBy.Select(o => expressionTranslator.Translate(o)))}";
        }
    }

    public class SQLQueryOrder<T> : SQLQueryOrder, ISQLQueryFragment
        where T : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1> : SQLQueryOrder, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2> : SQLQueryOrder, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2> Page(Expression<Func<SQLQueryPage, object>> paging)
        {
            Paging(paging);
            return this;
        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3> : SQLQueryOrder, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TJ3, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4> : SQLQueryOrder, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5> : SQLQueryOrder, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> : SQLQueryOrder, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> : SQLQueryOrder, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
    {
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> : SQLQueryOrder, ISQLQueryFragment
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> : SQLQueryOrder, ISQLQueryFragment
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> : SQLQueryOrder, ISQLQueryFragment
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> : SQLQueryOrder, ISQLQueryFragment
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> : SQLQueryOrder, ISQLQueryFragment
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> : SQLQueryOrder, ISQLQueryFragment
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }

    public class SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> : SQLQueryOrder, ISQLQueryFragment
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
        internal SQLQueryOrder(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] order)
            : base(fragments, order)
        {

        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }
    }
}
