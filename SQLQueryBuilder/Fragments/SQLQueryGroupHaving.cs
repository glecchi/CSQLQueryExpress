using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SQLQueryBuilder.Fragments
{
    public abstract class SQLQueryGroupHaving : ISQLQueryFragment
    {
        protected IList<ISQLQueryFragment> Fragments;
        private readonly Expression[] _having;

        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression[] having)
        {
            Fragments = fragments;
            _having = having;

            Fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.GroupHaving; } }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            return $"HAVING {string.Join(", ", _having.Select(h => expressionTranslator.Translate(h)))}";
        }
    }

    public class SQLQueryGroupHaving<T> : SQLQueryGroupHaving, ISQLQueryFragment
        where T : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T> OrderBy(params Expression<Func<T, object>>[] orderBy)
        {
            return new SQLQueryOrder<T>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1> : SQLQueryGroupHaving, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1> OrderBy(params Expression<Func<T, TJ1, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2> : SQLQueryGroupHaving, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2> OrderBy(params Expression<Func<T, TJ1, TJ2, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3> : SQLQueryGroupHaving, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4> : SQLQueryGroupHaving, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5> : SQLQueryGroupHaving, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> : SQLQueryGroupHaving, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> : SQLQueryGroupHaving, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
    {
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> : SQLQueryGroupHaving, ISQLQueryFragment
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> : SQLQueryGroupHaving, ISQLQueryFragment
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> : SQLQueryGroupHaving, ISQLQueryFragment
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, orderBy);
        }

        public SQLQuerySelect Select(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] select)
        {
            return new SQLQuerySelect(Fragments, select);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> : SQLQueryGroupHaving, ISQLQueryFragment
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> : SQLQueryGroupHaving, ISQLQueryFragment
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> : SQLQueryGroupHaving, ISQLQueryFragment
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, orderBy);
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

    public class SQLQueryGroupHaving<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> : SQLQueryGroupHaving, ISQLQueryFragment
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
        internal SQLQueryGroupHaving(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] group)
            : base(fragments, group)
        {

        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14>(Fragments, orderBy);
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
