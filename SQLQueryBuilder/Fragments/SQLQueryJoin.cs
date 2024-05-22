using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SQLQueryBuilder.Fragments
{
    public enum SQLQueryJoinType
    {
        InnerJoin,

        LeftOuterJoin,

        RightOuterJoin,
    }

    public abstract class SQLQueryJoin : ISQLQueryFragment
    {
        protected readonly IList<ISQLQueryFragment> Fragments;
        private readonly SQLQueryJoinType _joinType;
        private readonly Expression _join;
        private readonly Type _type;
        private readonly SQLQuerySelect _select;

        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression join, Type type, SQLQuerySelect select = null)
        {
            Fragments = fragments;
            _join = join;
            _joinType = joinType;
            _type = type;
            _select = select;

            if (_select != null && _select.UseWithTable && !Fragments.Contains(_select))
            {
                Fragments.Add(_select);
            }

            Fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return _select != null ? SQLQueryFragmentType.JoinBySelect : SQLQueryFragmentType.Join; } }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            var joinBuilder = new StringBuilder(); ;
            switch (_joinType)
            {
                case SQLQueryJoinType.InnerJoin:
                    joinBuilder.Append("INNER JOIN");
                    break;
                case SQLQueryJoinType.LeftOuterJoin:
                    joinBuilder.Append("LEFT OUTER JOIN");
                    break;
                case SQLQueryJoinType.RightOuterJoin:
                    joinBuilder.Append("OUTER OUTER JOIN");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_select != null)
            {
                if (_select.UseWithTable)
                {
                    joinBuilder.Append($" {expressionTranslator.GetTableAlias(_type)} ON {expressionTranslator.Translate(_join)}");
                }
                else
                {
                    joinBuilder.Append($" {Environment.NewLine}({Environment.NewLine}{string.Join($"{Environment.NewLine} ", _select.Select(s => s.Translate(expressionTranslator)))}{Environment.NewLine}) AS {expressionTranslator.GetTableAlias(_type)} ON {expressionTranslator.Translate(_join)}");
                }
            }
            else
            {
                joinBuilder.Append($" {expressionTranslator.Translate(Expression.Constant(_type))} ON {expressionTranslator.Translate(_join)}");
            }

            return joinBuilder.ToString();
        }
    }

    public class SQLQueryJoin<T, TJ1> : SQLQueryJoin, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
    {
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, bool>> join, SQLQuerySelect<TJ1> select = null)
            : base(fragments, joinType, join, typeof(TJ1), select)
        {

        }

        public SQLQueryJoin<T, TJ1, TJ2> InnerJoin<TJ2>(Expression<Func<T, TJ1, TJ2, bool>> join) where TJ2 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2>(Fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2> LeftOuterJoin<TJ2>(Expression<Func<T, TJ1, TJ2, bool>> join) where TJ2 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2>(Fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2> RightOuterJoin<TJ2>(Expression<Func<T, TJ1, TJ2, bool>> join) where TJ2 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2>(Fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2> InnerJoin<TJ2>(SQLQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, bool>> join) where TJ2 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2>(Fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2> LeftOuterJoin<TJ2>(SQLQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, bool>> join) where TJ2 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2>(Fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2> RightOuterJoin<TJ2>(SQLQuerySelect<TJ2> select, Expression<Func<T, TJ1, TJ2, bool>> join) where TJ2 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2>(Fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryGroup<T, TJ1> GroupBy(params Expression<Func<T, TJ1, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1>(Fragments, group);
        }

        public SQLQueryOrder<T, TJ1> OrderBy(params Expression<Func<T, TJ1, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1>(Fragments, orderBy);
        }

        public SQLQueryWhere<T, TJ1> Where(Expression<Func<T, TJ1, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1>(Fragments, where);
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

    public class SQLQueryJoin<T, TJ1, TJ2> : SQLQueryJoin, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
    {
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, TJ2, bool>> join, SQLQuerySelect<TJ2> select = null)
            : base(fragments, joinType, join, typeof(TJ2), select)
        {

        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3> InnerJoin<TJ3>(Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) where TJ3 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3>(Fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3> LeftOuterJoin<TJ3>(Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) where TJ3 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3>(Fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3> RightOuterJoin<TJ3>(Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) where TJ3 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3>(Fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3> InnerJoin<TJ3>(SQLQuerySelect<TJ3> select, Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) where TJ3 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3>(Fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3> LeftOuterJoin<TJ3>(SQLQuerySelect<TJ3> select, Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) where TJ3 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3>(Fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3> RightOuterJoin<TJ3>(SQLQuerySelect<TJ3> select, Expression<Func<T, TJ1, TJ2, TJ3, bool>> join) where TJ3 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3>(Fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryGroup<T, TJ1, TJ2> GroupBy(params Expression<Func<T, TJ1, TJ2, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2>(Fragments, group);
        }

        public SQLQueryOrder<T, TJ1, TJ2> OrderBy(params Expression<Func<T, TJ1, TJ2, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2>(Fragments, orderBy);
        }

        public SQLQueryWhere<T, TJ1, TJ2> Where(Expression<Func<T, TJ1, TJ2, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1, TJ2>(Fragments, where);
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

    public class SQLQueryJoin<T, TJ1, TJ2, TJ3> : SQLQueryJoin, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
    {
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, TJ2, TJ3, bool>> join, SQLQuerySelect<TJ3> select = null)
            : base(fragments, joinType, join, typeof(TJ3), select)
        {

        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4> InnerJoin<TJ4>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join) where TJ4 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4>(Fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4> LeftOuterJoin<TJ4>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join) where TJ4 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4>(Fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4> RightOuterJoin<TJ4>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join) where TJ4 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4>(Fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4> InnerJoin<TJ4>(SQLQuerySelect<TJ4> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join) where TJ4 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4>(Fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4> LeftOuterJoin<TJ4>(SQLQuerySelect<TJ4> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join) where TJ4 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4>(Fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4> RightOuterJoin<TJ4>(SQLQuerySelect<TJ4> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join) where TJ4 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4>(Fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3>(Fragments, group);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3>(Fragments, orderBy);
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3> Where(Expression<Func<T, TJ1, TJ2, TJ3, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1, TJ2, TJ3>(Fragments, where);
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

    public class SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4> : SQLQueryJoin, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
    {
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> join, SQLQuerySelect<TJ4> select = null)
            : base(fragments, joinType, join, typeof(TJ4), select)
        {

        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5> InnerJoin<TJ5>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> join) where TJ5 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5> LeftOuterJoin<TJ5>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> join) where TJ5 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5> RightOuterJoin<TJ5>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> join) where TJ5 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5> InnerJoin<TJ5>(SQLQuerySelect<TJ5> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> join) where TJ5 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5> LeftOuterJoin<TJ5>(SQLQuerySelect<TJ5> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> join) where TJ5 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5> RightOuterJoin<TJ5>(SQLQuerySelect<TJ5> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> join) where TJ5 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4>(Fragments, group);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4>(Fragments, orderBy);
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4> Where(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4>(Fragments, where);
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

    public class SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5> : SQLQueryJoin, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
    {
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> join, SQLQuerySelect<TJ5> select = null)
            : base(fragments, joinType, join, typeof(TJ5), select)
        {

        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> InnerJoin<TJ6>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> join) where TJ6 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> LeftOuterJoin<TJ6>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> join) where TJ6 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> RightOuterJoin<TJ6>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> join) where TJ6 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> InnerJoin<TJ6>(SQLQuerySelect<TJ6> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> join) where TJ6 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> LeftOuterJoin<TJ6>(SQLQuerySelect<TJ6> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> join) where TJ6 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> RightOuterJoin<TJ6>(SQLQuerySelect<TJ6> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> join) where TJ6 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, group);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, orderBy);
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5> Where(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5>(Fragments, where);
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

    public class SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> : SQLQueryJoin, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
    {
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> join, SQLQuerySelect<TJ6> select = null)
            : base(fragments, joinType, join, typeof(TJ6), select)
        {

        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> InnerJoin<TJ7>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> join) where TJ7 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> LeftOuterJoin<TJ7>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> join) where TJ7 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> RightOuterJoin<TJ7>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> join) where TJ7 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> InnerJoin<TJ7>(SQLQuerySelect<TJ7> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> join) where TJ7 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> LeftOuterJoin<TJ7>(SQLQuerySelect<TJ7> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> join) where TJ7 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> RightOuterJoin<TJ7>(SQLQuerySelect<TJ7> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> join) where TJ7 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, group);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, orderBy);
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6> Where(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6>(Fragments, where);
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

    public class SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> : SQLQueryJoin, ISQLQueryFragment
        where T : ISQLQueryEntity
        where TJ1 : ISQLQueryEntity
        where TJ2 : ISQLQueryEntity
        where TJ3 : ISQLQueryEntity
        where TJ4 : ISQLQueryEntity
        where TJ5 : ISQLQueryEntity
        where TJ6 : ISQLQueryEntity
        where TJ7 : ISQLQueryEntity
    {
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> join, SQLQuerySelect<TJ7> select = null)
            : base(fragments, joinType, join, typeof(TJ7), select)
        {

        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> InnerJoin<TJ8>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> join) where TJ8 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> LeftOuterJoin<TJ8>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> join) where TJ8 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> RightOuterJoin<TJ8>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> join) where TJ8 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> InnerJoin<TJ8>(SQLQuerySelect<TJ8> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> join) where TJ8 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> LeftOuterJoin<TJ8>(SQLQuerySelect<TJ8> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> join) where TJ8 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> RightOuterJoin<TJ8>(SQLQuerySelect<TJ8> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> join) where TJ8 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, group);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, orderBy);
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7> Where(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7>(Fragments, where);
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

    public class SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> : SQLQueryJoin, ISQLQueryFragment
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
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> join, SQLQuerySelect<TJ8> select = null)
            : base(fragments, joinType, join, typeof(TJ8), select)
        {

        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> InnerJoin<TJ9>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> join) where TJ9 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> LeftOuterJoin<TJ9>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> join) where TJ9 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> RightOuterJoin<TJ9>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> join) where TJ9 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> InnerJoin<TJ9>(SQLQuerySelect<TJ9> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> join) where TJ9 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> LeftOuterJoin<TJ9>(SQLQuerySelect<TJ9> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> join) where TJ9 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> RightOuterJoin<TJ9>(SQLQuerySelect<TJ9> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> join) where TJ9 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, group);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, orderBy);
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8> Where(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8>(Fragments, where);
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

    public class SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> : SQLQueryJoin, ISQLQueryFragment
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
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> join, SQLQuerySelect<TJ9> select = null)
            : base(fragments, joinType, join, typeof(TJ9), select)
        {

        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> InnerJoin<TJ10>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> join) where TJ10 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> LeftOuterJoin<TJ10>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> join) where TJ10 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> RightOuterJoin<TJ10>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> join) where TJ10 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> InnerJoin<TJ10>(SQLQuerySelect<TJ10> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> join) where TJ10 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> LeftOuterJoin<TJ10>(SQLQuerySelect<TJ10> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> join) where TJ10 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> RightOuterJoin<TJ10>(SQLQuerySelect<TJ10> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> join) where TJ10 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, group);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, orderBy);
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9> Where(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9>(Fragments, where);
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

    public class SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> : SQLQueryJoin, ISQLQueryFragment
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
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> join, SQLQuerySelect<TJ10> select = null)
            : base(fragments, joinType, join, typeof(TJ10), select)
        {

        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> InnerJoin<TJ11>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> join) where TJ11 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> LeftOuterJoin<TJ11>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> join) where TJ11 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> RightOuterJoin<TJ11>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> join) where TJ11 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> InnerJoin<TJ11>(SQLQuerySelect<TJ11> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> join) where TJ11 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> LeftOuterJoin<TJ11>(SQLQuerySelect<TJ11> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> join) where TJ11 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> RightOuterJoin<TJ11>(SQLQuerySelect<TJ11> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> join) where TJ11 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, group);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, orderBy);
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10> Where(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10>(Fragments, where);
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

    public class SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> : SQLQueryJoin, ISQLQueryFragment
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
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> join, SQLQuerySelect<TJ11> select = null)
            : base(fragments, joinType, join, typeof(TJ11), select)
        {

        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> InnerJoin<TJ12>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> join) where TJ12 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> LeftOuterJoin<TJ12>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> join) where TJ12 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> RightOuterJoin<TJ12>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> join) where TJ12 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> InnerJoin<TJ12>(SQLQuerySelect<TJ12> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> join) where TJ12 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> LeftOuterJoin<TJ12>(SQLQuerySelect<TJ12> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> join) where TJ12 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> RightOuterJoin<TJ12>(SQLQuerySelect<TJ12> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> join) where TJ12 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, group);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, orderBy);
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11> Where(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11>(Fragments, where);
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

    public class SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> : SQLQueryJoin, ISQLQueryFragment
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
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> join, SQLQuerySelect<TJ12> select = null)
            : base(fragments, joinType, join, typeof(TJ12), select)
        {

        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> InnerJoin<TJ13>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> join) where TJ13 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> LeftOuterJoin<TJ13>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> join) where TJ13 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> RightOuterJoin<TJ13>(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> join) where TJ13 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> InnerJoin<TJ13>(SQLQuerySelect<TJ13> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> join) where TJ13 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> LeftOuterJoin<TJ13>(SQLQuerySelect<TJ13> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> join) where TJ13 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> RightOuterJoin<TJ13>(SQLQuerySelect<TJ13> select, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> join) where TJ13 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> GroupBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] group)
        {
            return new SQLQueryGroup<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, group);
        }

        public SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> OrderBy(params Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, object>>[] orderBy)
        {
            return new SQLQueryOrder<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, orderBy);
        }

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12> Where(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12>(Fragments, where);
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

    public class SQLQueryJoin<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> : SQLQueryJoin, ISQLQueryFragment
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
        internal SQLQueryJoin(IList<ISQLQueryFragment> fragments, SQLQueryJoinType joinType, Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> join, SQLQuerySelect<TJ13> select = null)
            : base(fragments, joinType, join, typeof(TJ13), select)
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

        public SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13> Where(Expression<Func<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13, bool>> where)
        {
            return new SQLQueryWhere<T, TJ1, TJ2, TJ3, TJ4, TJ5, TJ6, TJ7, TJ8, TJ9, TJ10, TJ11, TJ12, TJ13>(Fragments, where);
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
}
