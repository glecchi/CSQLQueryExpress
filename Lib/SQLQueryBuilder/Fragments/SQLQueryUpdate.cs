using SQLQueryBuilder.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SQLQueryBuilder.Fragments
{
    public class SQLQueryUpdate<T> : ISQLQueryFragment, ISQLQuery,
        ISQLQueryWithOutput<T>
        where T : ISQLQueryEntity
    {
        private readonly IList<ISQLQueryFragment> _fragments;
        private readonly Expression<Action<T>>[] _update;
        private readonly IDictionary<string, object> _updateProperties;
        private readonly bool _inLine;
        private int? _top;

        internal SQLQueryUpdate(IList<ISQLQueryFragment> fragments, params Expression<Action<T>>[] update)
        {
            _fragments = fragments;
            _update = update;
            _inLine = fragments.All(f => f.FragmentType != SQLQueryFragmentType.FromBySelect);

            _fragments.Add(this);
        }

        internal SQLQueryUpdate(IList<ISQLQueryFragment> fragments, object update)
        {
            _fragments = fragments;
            _updateProperties = GetUpdateProperties(update);
            _inLine = fragments.All(f => f.FragmentType != SQLQueryFragmentType.FromBySelect);

            _fragments.Add(this);
        }

        internal SQLQueryUpdate(IList<ISQLQueryFragment> fragments, IDictionary<string, object> update)
        {
            _fragments = fragments;
            _updateProperties = update;
            _inLine = fragments.All(f => f.FragmentType != SQLQueryFragmentType.FromBySelect);

            _fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Update; } }

        public SQLQueryUpdate<T> Top(int count)
        {
            _top = count;
            return this;
        }

        public SQLQueryOutput<TS> Output<TS>(Expression<Func<T, TS>> output)
        {
            return new SQLQueryOutput<TS>(FragmentType, _fragments, output.Merge());
        }

        public SQLQueryOutput<T> Output(
            Expression<Func<T, object>> output,
            params Expression<Func<T, object>>[] otherOutput)
        {
            return new SQLQueryOutput<T>(FragmentType, _fragments, output.Merge(otherOutput));
        }

        public SQLQueryOutput<TS> Output<TS>(
            Expression<Func<T, TS, object>> output,
            params Expression<Func<T, TS, object>>[] otherOutput)
        {
            return new SQLQueryOutput<TS>(FragmentType, _fragments, output.Merge(otherOutput));
        }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            var updateBuilder = new StringBuilder();

            if (!_inLine)
            {
                updateBuilder.Append($"UPDATE {(_top.HasValue ? $"TOP({_top}) " : string.Empty)}{expressionTranslator.GetTableAlias(typeof(T))} ");

                if (_updateProperties == null)
                {
                    updateBuilder.Append($"{Environment.NewLine}SET {string.Join(", ", _update.Select(u => expressionTranslator.Translate(u)))} ");
                }
                else
                {
                    var tableColumns = GetTableColumns();

                    var properties = _updateProperties
                        .ToDictionary(p => p.Key.ToUpper(), p => p.Value);

                    updateBuilder.Append($"{Environment.NewLine}SET {string.Join(", ", tableColumns.Where(c => properties.ContainsKey(c.ToUpper())).Select(c => $"{expressionTranslator.GetTableAlias(typeof(T))}.[{c}] = {expressionTranslator.MakeParameter(properties[c.ToUpper()])}"))} ");
                }
            }
            else
            {
                updateBuilder.Append($"UPDATE {(_top.HasValue ? $"TOP({_top}) " : string.Empty)}{expressionTranslator.GetTableName(typeof(T))} ");

                if (_updateProperties == null)
                {
                    updateBuilder.Append($"{Environment.NewLine}SET {string.Join(", ", _update.Select(u => expressionTranslator.GetColumnWithoutTableAlias(expressionTranslator.Translate(u))))} ");
                }
                else
                {
                    var tableColumns = GetTableColumns();

                    var properties = _updateProperties
                        .ToDictionary(p => p.Key.ToUpper(), p => p.Value);

                    updateBuilder.Append($"{Environment.NewLine}SET {string.Join(", ", tableColumns.Where(c => properties.ContainsKey(c.ToUpper())).Select(c => $"[{c}] = {expressionTranslator.MakeParameter(properties[c.ToUpper()])}"))} ");
                }
            }

            return updateBuilder.ToString();
        }

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer(FragmentType)))
            {
                yield return fragment;
            }
        }

        private IDictionary<string, object> GetUpdateProperties(object update)
        {
            return update.GetType().GetReadableProperties()
                 .ToDictionary(p => p.Name, p => p.GetValue(update));
        }

        private List<string> GetTableColumns()
        {
            return typeof(T).GetWritableColumnsProperties()
                .Select(p => p.Name)
                .ToList();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}