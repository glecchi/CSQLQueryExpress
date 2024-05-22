using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SQLQueryBuilder.Fragments
{
    public class SQLQueryUpdate<T> : ISQLQueryFragment, ISQLQuery
    {
        private readonly Expression<Action<T>>[] _update;
        private readonly IList<ISQLQueryFragment> _fragments;
        private readonly bool _inLine;
        private int? _top;

        internal SQLQueryUpdate(IList<ISQLQueryFragment> fragments, params Expression<Action<T>>[] update)
        {
            _fragments = fragments;
            _update = update;
            _inLine = fragments.All(f => f.FragmentType != SQLQueryFragmentType.FromBySelect);

            _fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Update; } }

        public SQLQueryUpdate<T> Top(int count)
        {
            _top = count;
            return this;
        }

        public SQLQueryOutput<T> Output(params Expression<Func<T, object>>[] output)
        {
            return new SQLQueryOutput<T>(QueryOutputSource.Inserted, _fragments, output);
        }

        public SQLQueryOutput<TS> Output<TS>(params Expression<Func<T, TS, object>>[] output)
        {
            return new SQLQueryOutput<TS>(QueryOutputSource.Inserted, _fragments, output);
        }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            var updateBuilder = new StringBuilder();

            if (!_inLine)
            {
                updateBuilder.Append($"UPDATE {(_top.HasValue ? $"TOP({_top}) " : string.Empty)}{expressionTranslator.GetTableAlias(typeof(T))} ");

                updateBuilder.Append($"{Environment.NewLine}SET {string.Join(", ", _update.Select(u => expressionTranslator.Translate(u)))} ");
            }
            else
            {
                updateBuilder.Append($"UPDATE {(_top.HasValue ? $"TOP({_top}) " : string.Empty)}{expressionTranslator.GetTableName(typeof(T))} ");

                updateBuilder.Append($"{Environment.NewLine}SET {string.Join(", ", _update.Select(u => expressionTranslator.GetColumnWithoutTableAlias(expressionTranslator.Translate(u))))} ");
            }

            return updateBuilder.ToString();
        }

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer()))
            {
                yield return fragment;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}