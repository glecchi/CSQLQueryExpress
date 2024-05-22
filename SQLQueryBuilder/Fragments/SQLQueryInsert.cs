using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using SQLQueryBuilder.Extensions;

namespace SQLQueryBuilder.Fragments
{
    public class SQLQueryInsert<T> : ISQLQueryFragment, ISQLQuery
        where T : ISQLQueryEntity
    {
        private readonly IList<ISQLQueryFragment> _fragments;
        private readonly SQLQuerySelect _select;
        private readonly List<string> _tableColumns;
        private IDictionary<string, object> _insertProperties;

        internal SQLQueryInsert(IList<ISQLQueryFragment> fragments, SQLQuerySelect select)
        {
            _fragments = fragments;

            if (select != null)
            {
                _select = select;
            }
            else
            {
                _select = (SQLQuerySelect<T>)fragments.Last(f => f.GetType().IsGenericType && f.GetType().GetGenericTypeDefinition() == typeof(SQLQuerySelect<T>));
            }

            _tableColumns = GetTableColumns();

            _fragments.Add(this);
        }

        internal SQLQueryInsert(IList<ISQLQueryFragment> fragments, IDictionary<string, object> insert)
        {
            _fragments = fragments;
            _insertProperties = insert;

            _tableColumns = GetTableColumns();

            if (_insertProperties.Any(p => _tableColumns.All(c => !string.Equals(c, p.Key, StringComparison.OrdinalIgnoreCase))))
            {
                throw new InvalidOperationException("Some insert values mismatch table columns");
            }

            _fragments.Add(this);

            AddSqlQueryInsertValues();
        }

        internal SQLQueryInsert(IList<ISQLQueryFragment> fragments, object insert)
        {
            _fragments = fragments;

            _tableColumns = GetTableColumns();

            _insertProperties = GetInsertProperties(insert);

            if (_insertProperties.Any(p => _tableColumns.All(c => !string.Equals(c, p.Key, StringComparison.OrdinalIgnoreCase))))
            {
                throw new InvalidOperationException("Some insert values mismatch table columns");
            }

            _fragments.Add(this);

            AddSqlQueryInsertValues();
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Insert; } }

        public SQLQueryOutput<T> Output(params Expression<Func<T, object>>[] output)
        {
            return new SQLQueryOutput<T>(QueryOutputSource.Inserted, _fragments, output);
        }

        public SQLQueryOutput<TS> Output<TS>(params Expression<Func<T, TS, object>>[] output)
        {
            return new SQLQueryOutput<TS>(QueryOutputSource.Inserted, _fragments, output);
        }

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer()))
            {
                yield return fragment;
            }
        }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            var insertBuilder = new StringBuilder();

            insertBuilder.Append($"INSERT INTO {expressionTranslator.GetTableName(typeof(T))} ");

            if (_insertProperties != null)
            {
                var properties = _insertProperties
                    .ToDictionary(p => p.Key.ToUpper(), p => p.Value);

                insertBuilder.Append($"{Environment.NewLine}({string.Join(", ", _tableColumns.Where(c => properties.ContainsKey(c.ToUpper())))}) ");
            }
            else
            {
                insertBuilder.Append($"{Environment.NewLine}({string.Join(", ", _select.Select.Select(u => expressionTranslator.GetColumnWithoutTableAlias(expressionTranslator.Translate(u))))}) ");
            }

            return insertBuilder.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void AddSqlQueryInsertValues()
        {
            var insertValues = new SQLQueryInsertValues(_tableColumns, _insertProperties);
            _fragments.Add(insertValues);
        }

        private IDictionary<string, object> GetInsertProperties(object insert)
        {
            return insert.GetType().GetReadableProperties()
                 .ToDictionary(p => p.Name, p => p.GetValue(insert));
        }

        private List<string> GetTableColumns()
        {
            return typeof(T).GetAllColumnsProperties()
                .Select(p => p.Name)
                .ToList();
        }
    }

    public class SQLQueryInsertValues : ISQLQueryFragment
    {
        private readonly List<string> _tableColumns;
        private readonly IDictionary<string, object> _insertProperties;

        internal SQLQueryInsertValues(List<string> tableColumns, IDictionary<string, object> insertProperties)
        {
            _tableColumns = tableColumns;
            _insertProperties = insertProperties;
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.InsertValues; } }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            var valuesBuilder = new StringBuilder();

            var properties = _insertProperties
                    .ToDictionary(p => p.Key.ToUpper(), p => p.Value);

            valuesBuilder.Append($"VALUES ");

            valuesBuilder.Append($"{Environment.NewLine}({string.Join(", ", _tableColumns.Where(c => properties.ContainsKey(c.ToUpper())).Select(c => expressionTranslator.MakeParameter(properties[c.ToUpper()])))})");

            return valuesBuilder.ToString();
        }
    }

}
