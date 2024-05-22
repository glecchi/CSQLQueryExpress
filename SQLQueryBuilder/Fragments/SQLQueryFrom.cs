using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SQLQueryBuilder.Fragments
{
    public class SQLQueryFrom<T> : ISQLQueryFragment where T : ISQLQueryEntity
    {
        private readonly SQLQuerySelect<T> _select;
        private readonly IList<ISQLQueryFragment> _fragments;
        private WithOptions? _withOptions;

        internal SQLQueryFrom(IList<ISQLQueryFragment> fragments, SQLQuerySelect<T> select = null)
        {
            _fragments = fragments;
            _select = select;

            if (_select != null && _select.UseWithTable)
            {
                _fragments.Add(_select);
            }

            _fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return _select != null ? SQLQueryFragmentType.FromBySelect : SQLQueryFragmentType.From; } }

        public SQLQueryJoin<T, TJ1> InnerJoin<TJ1>(SQLQuerySelect<TJ1> select, Expression<Func<T, TJ1, bool>> join) where TJ1 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1>(_fragments, SQLQueryJoinType.InnerJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1> InnerJoin<TJ1>(Expression<Func<T, TJ1, bool>> join) where TJ1 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1>(_fragments, SQLQueryJoinType.InnerJoin, join);
        }

        public SQLQueryJoin<T, TJ1> LeftOuterJoin<TJ1>(SQLQuerySelect<TJ1> select, Expression<Func<T, TJ1, bool>> join) where TJ1 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1>(_fragments, SQLQueryJoinType.LeftOuterJoin, join, select);
        }

        public SQLQueryJoin<T, TJ1> LeftOuterJoin<TJ1>(Expression<Func<T, TJ1, bool>> join) where TJ1 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1>(_fragments, SQLQueryJoinType.LeftOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1> RightOuterJoin<TJ1>(Expression<Func<T, TJ1, bool>> join) where TJ1 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1>(_fragments, SQLQueryJoinType.RightOuterJoin, join);
        }

        public SQLQueryJoin<T, TJ1> RightOuterJoin<TJ1>(SQLQuerySelect<TJ1> select, Expression<Func<T, TJ1, bool>> join) where TJ1 : ISQLQueryEntity
        {
            return new SQLQueryJoin<T, TJ1>(_fragments, SQLQueryJoinType.RightOuterJoin, join, select);
        }

        public SQLQueryOrder<T> OrderBy(params Expression<Func<T, object>>[] orderBy)
        {
            return new SQLQueryOrder<T>(_fragments, orderBy);
        }

        public SQLQueryGroup<T> GroupBy(params Expression<Func<T, object>>[] group)
        {
            return new SQLQueryGroup<T>(_fragments, group);
        }

        public SQLQueryWhere<T> Where(Expression<Func<T, bool>> where)
        {
            return new SQLQueryWhere<T>(_fragments, where);
        }

        public SQLQuerySelect<TS> Select<TS>(Expression<Func<T, TS>> select)
        {
            return new SQLQuerySelect<TS>(_fragments, new[] { select });
        }

        public SQLQuerySelect<T> Select(params Expression<Func<T, object>>[] select)
        {
            return new SQLQuerySelect<T>(_fragments, select);
        }

        public SQLQuerySelect<TS> Select<TS>(params Expression<Func<T, TS, object>>[] select)
        {
            return new SQLQuerySelect<TS>(_fragments, select);
        }

        public SQLQueryUpdate<T> Update(params Expression<Action<T>>[] update)
        {
            return new SQLQueryUpdate<T>(_fragments, update);
        }

        public SQLQueryInsert<T> Insert()
        {
            return new SQLQueryInsert<T>(_fragments, _select);
        }

        public SQLQueryInsert<T> Insert(IDictionary<string, object> insert)
        {
            return new SQLQueryInsert<T>(_fragments, insert);
        }

        public SQLQueryInsert<T> Insert(object insert)
        {
            return new SQLQueryInsert<T>(_fragments, insert);
        }

        public SQLQueryDelete<T> Delete()
        {
            return new SQLQueryDelete<T>(_fragments);
        }

        public SQLQueryFrom<T> With(WithOptions options)
        {
            _withOptions = options;
            return this;
        }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            var fromBuilder = new StringBuilder();

            if (_select != null)
            {
                if (_select.UseWithTable)
                {
                    fromBuilder.Append($"FROM {expressionTranslator.GetTableAlias(typeof(T))}");
                }
                else if (_fragments.All(f => f.FragmentType != SQLQueryFragmentType.Insert))
                {
                    fromBuilder.Append($"FROM {Environment.NewLine}({Environment.NewLine}{string.Join($"{Environment.NewLine} ", _select.Select(s => s.Translate(expressionTranslator)))}{Environment.NewLine}) AS {expressionTranslator.GetTableAlias(typeof(T))}");
                }
                else
                {
                    fromBuilder.Append($"{string.Join($"{Environment.NewLine} ", _select.Select(s => s.Translate(expressionTranslator)))}{Environment.NewLine}");
                }
            }
            else if (_fragments.Any(f => f.FragmentType == SQLQueryFragmentType.Select))
            {
                fromBuilder.Append($"FROM {expressionTranslator.Translate(Expression.Constant(typeof(T)))}");
            }
            else
            {
                fromBuilder.Append($"FROM {expressionTranslator.GetTableName(typeof(T))}");
            }

            if (_withOptions.HasValue)
            {
                fromBuilder.Append($" WITH ({_withOptions.Value})");
            }

            return fromBuilder.ToString();
        }
    }
}