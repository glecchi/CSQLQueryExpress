using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CSQLQueryExpress.Fragments
{
    public abstract class SQLQueryWhere : ISQLQueryFragment,
        ISQLQueryWithSelect
    {
        protected readonly IList<ISQLQueryFragment> Fragments;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments)
        {
            Fragments = fragments;
        
            Fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Where; } }

        protected abstract Expression GetWhereCondition();

        public SQLQuerySelect SelectAll()
        {
            return new SQLQuerySelect(Fragments);
        }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            if (Fragments.Any(f => f.FragmentType == SQLQueryFragmentType.FromBySelect || f.FragmentType == SQLQueryFragmentType.Select || f.FragmentType == SQLQueryFragmentType.SelectCte))
            {
                return $"WHERE {expressionTranslator.Translate(GetWhereCondition())}";
            }

            return $"WHERE {expressionTranslator.GetColumnsWithoutTableAlias(expressionTranslator.Translate(GetWhereCondition()))}";
        }
    }

    public class SQLQueryWhere<T> : SQLQueryWhere, ISQLQueryFragment,
        ISQLQueryWithSelect<T>, ISQLQueryWithOrderBy<T>
        where T : ISQLQueryEntity
    {
        private Expression<Func<T, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T> And(Expression<Func<T, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T> Or(Expression<Func<T, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T> GroupBy(
            Expression<Func<T, object>> group,
            params Expression<Func<T, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T> OrderBy(
            Expression<Func<T, object>> orderBy, 
            params Expression<Func<T, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TS>> select)
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

        public SQLQueryUpdate<T> Update(Expression<Action<T>> update)
        {
            return new SQLQueryUpdate<T>(Fragments, update);
        }

        public SQLQueryUpdate<T> Update(params Expression<Action<T>>[] update)
        {
            return new SQLQueryUpdate<T>(Fragments, update);
        }

        public SQLQueryUpdate<T> Update(object update)
        {
            return new SQLQueryUpdate<T>(Fragments, update);
        }

        public SQLQueryUpdate<T> Update(T update)
        {
            return new SQLQueryUpdate<T>(Fragments, update);
        }

        public SQLQueryUpdate<T> Update(IDictionary<string, object> update)
        {
            return new SQLQueryUpdate<T>(Fragments, update);
        }

        public SQLQueryDelete<T> Delete()
        {
            return new SQLQueryDelete<T>(Fragments);
        }
    }

    public class SQLQueryWhere<T, TJ1> : SQLQueryWhere, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1>, ISQLQueryWithOrderBy<T, TJ1>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
    {
        private Expression<Func<T, TJ1, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1> And(Expression<Func<T, TJ1, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1> Or(Expression<Func<T, TJ1, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1> GroupBy(
            Expression<Func<T, TJ1, object>> group,
            params Expression<Func<T, TJ1, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1> OrderBy(Expression<Func<T, TJ1, object>> orderBy, params Expression<Func<T, TJ1, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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

    public class SQLQueryWhere<T, TJ1, TJ2> : SQLQueryWhere, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2>, ISQLQueryWithOrderBy<T, TJ1, TJ2>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
    {
        private Expression<Func<T, TJ1, TJ2, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2> And(Expression<Func<T, TJ1, TJ2, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2> Or(Expression<Func<T, TJ1, TJ2, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2> GroupBy(
            Expression<Func<T, TJ1, TJ2, object>> group,
            params Expression<Func<T, TJ1, TJ2, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2> OrderBy(Expression<Func<T, TJ1, TJ2, object>> orderBy, params Expression<Func<T, TJ1, TJ2, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
    public class SQLQueryWhere<T, TJ1, TJ2, TJ3> : SQLQueryWhere, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
    {
        private Expression<Func<T, TJ1, TJ2, TJ3, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3> And(Expression<Func<T, TJ1, TJ2, TJ3, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3> Or(Expression<Func<T, TJ1, TJ2, TJ3, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3> GroupBy(
            Expression<Func<T, TJ1, TJ2, TJ3, object>> group,
            params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4> : SQLQueryWhere, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
    {
        private Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4> And(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4> Or(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4> GroupBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>> group,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5> : SQLQueryWhere, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
    {
        private Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5> And(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5> Or(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5> GroupBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>> group,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> : SQLQueryWhere, ISQLQueryFragment,
        ISQLQueryWithSelect<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>, ISQLQueryWithOrderBy<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
    {
        private Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> And(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> Or(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> GroupBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>> group,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> : SQLQueryWhere, ISQLQueryFragment,
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
        private Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> And(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> Or(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> GroupBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>> group,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> : SQLQueryWhere, ISQLQueryFragment,
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
        private Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> And(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> Or(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> GroupBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>> group,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> : SQLQueryWhere, ISQLQueryFragment,
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
        private Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> And(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> Or(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> GroupBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>> group,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> : SQLQueryWhere, ISQLQueryFragment,
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
        private Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> And(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> Or(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> GroupBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>> group,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> : SQLQueryWhere, ISQLQueryFragment,
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
        private Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> And(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> Or(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> GroupBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>> group,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> : SQLQueryWhere, ISQLQueryFragment,
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
        private Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> And(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> Or(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> GroupBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>> group,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> : SQLQueryWhere, ISQLQueryFragment,
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
        private Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> And(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> Or(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> GroupBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>> group,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
    public class SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> : SQLQueryWhere, ISQLQueryFragment,
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
        private Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, bool>> _where;

        internal SQLQueryWhere(IList<ISQLQueryFragment> fragments, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, bool>> where)
            : base(fragments)
        {
            _where = where;
        }

        protected override Expression GetWhereCondition()
        {
            return _where;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> And(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, bool>> condition)
        {
            var body = Expression.AndAlso(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> Or(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, bool>> condition)
        {
            var body = Expression.Or(_where.Body, condition.Body);
            _where = Expression.Lambda<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, bool>>(body, _where.Parameters);
            return this;
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> GroupBy(
            Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>> group,
            params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] otherGroup)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14>(Fragments, group, otherGroup);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14> OrderBy(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>> orderBy, params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14, object>>[] otherOrderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, TJ14>(Fragments, orderBy, otherOrderBy);
        }

        public SQLQueryUpdate<TU> Update<TU>(Expression<Action<TU>> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(params Expression<Action<TU>>[] update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(object update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(TU update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryUpdate<TU> Update<TU>(IDictionary<string, object> update) where TU : ISQLQueryEntity
        {
            return new SQLQueryUpdate<TU>(Fragments, update);
        }

        public SQLQueryDelete<TD> Delete<TD>() where TD : ISQLQueryEntity
        {
            return new SQLQueryDelete<TD>(Fragments);
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
