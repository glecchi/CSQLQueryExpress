using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SQLQueryBuilder.Fragments
{
    public abstract class SQLQueryWhere : ISQLQueryFragment
    {
        protected readonly IList<ISQLQueryFragment> Fragments;

        private readonly Expression _where;
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression where)
        {
            Fragments = fragments;
            _where = where;

            Fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Where; } }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            if (Fragments.All(f => f.FragmentType != SQLQueryFragmentType.FromBySelect && f.FragmentType != SQLQueryFragmentType.Update && f.FragmentType == SQLQueryFragmentType.Delete))
            {
                return $"WHERE {expressionTranslator.GetColumnsWithoutTableAlias(expressionTranslator.Translate(_where))}";
            }

            return $"WHERE {expressionTranslator.Translate(_where)}";
        }
    }

    public class SQLQueryWhere<T> : SQLQueryWhere, ISQLQueryFragment
        where T : ISQLQueryEntity
    {
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T> GroupBy(params Expression<Func<T, object>>[] group)
        {
            return new SQLQueryGroup<T>(Fragments, group);
        }

        public SQLQueryOrder<T> OrderBy(params Expression<Func<T, object>>[] orderBy)
        {
            return new SQLQueryOrder<T>(Fragments, orderBy);
        }

        public SQLQuerySelect<T> Select(params Expression<Func<T, object>>[] select)
        {
            return new SQLQuerySelect<T>(Fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TS>> select)
        {
            return new SQLQuerySelect<TS>(Fragments, new[] { select });
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(Fragments, select);
        }

        public SQLQueryUpdate<T> Update(params Expression<Action<T>>[] update)
        {
            return new SQLQueryUpdate<T>(Fragments, update);
        }

        public SQLQueryDelete<T> Delete()
        {
            return new SQLQueryDelete<T>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1> : SQLQueryWhere, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
    {
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1> GroupBy(params Expression<Func<T, TJ1, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2> : SQLQueryWhere, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
    {
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2> GroupBy(params Expression<Func<T, TJ1, TJ2, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2, TJ3> : SQLQueryWhere, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
    {
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4> : SQLQueryWhere, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
    {
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5> : SQLQueryWhere, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
    {
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> : SQLQueryWhere, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
    {
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> : SQLQueryWhere, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
    {
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> : SQLQueryWhere, ISQLQueryFragment
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
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> : SQLQueryWhere, ISQLQueryFragment
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
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> : SQLQueryWhere, ISQLQueryFragment
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
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> : SQLQueryWhere, ISQLQueryFragment
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
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> : SQLQueryWhere, ISQLQueryFragment
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
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> : SQLQueryWhere, ISQLQueryFragment
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
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> : SQLQueryWhere, ISQLQueryFragment
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
        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, bool>> where)
            : base(fragments, where)
        {

        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14>(Fragments, group);
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

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
        }
    }
}
