﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CSQLQueryExpress.Fragments
{
    public class SQLQueryInsert<T> : ISQLQueryFragment, ISQLQuery, ISQLQueryFragmentFromSelect, ISQLQueryWithOutput<T> where T : ISQLQueryEntity
    {
        private readonly IList<ISQLQueryFragment> _fragments;
        private readonly SQLQuerySelect _select;
        private readonly List<string> _tableColumns;
        private readonly IDictionary<string, Expression> _insertParameters;
        private readonly IDictionary<string, object> _insertProperties;
        private readonly Expression[] _insertColumns;

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

            AddSqlQueryInsertSelect();
        }

        internal SQLQueryInsert(IList<ISQLQueryFragment> fragments, IDictionary<string, object> insert)
        {
            _fragments = fragments;
            _insertProperties = insert;

            _tableColumns = GetTableColumns();

            _fragments.Add(this);

            AddSqlQueryInsertValues();
        }

        internal SQLQueryInsert(IList<ISQLQueryFragment> fragments, object insert)
        {
            _fragments = fragments;

            _tableColumns = GetTableColumns();

            _insertProperties = GetInsertProperties(insert);

            _fragments.Add(this);

            AddSqlQueryInsertValues();
        }

        internal SQLQueryInsert(IList<ISQLQueryFragment> fragments, Expression<Action<T>>[] insert)
        {
            _fragments = fragments;

            _tableColumns = GetTableColumns();

            _insertParameters = SQLQueryUtils.GetInsertParameters(insert);

            _fragments.Add(this);

            AddSqlQueryInsertParameters();
        }

        internal SQLQueryInsert(IList<ISQLQueryFragment> fragments, SQLQuerySelect select, Expression<Func<T, object>>[] columns)
        {
            _fragments = fragments;

            _select = select;

            _insertColumns = columns;

            _tableColumns = GetTableColumns();

            _fragments.Add(this);

            AddSqlQueryInsertSelect();
        }

        public SQLQuerySelect FromSelect { get { return _select; } }

        public SQLQueryFragmentType FragmentType { get { return _select != null ? SQLQueryFragmentType.InsertBySelect : SQLQueryFragmentType.Insert; } }

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

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer(FragmentType)))
            {
                yield return fragment;
            }
        }

        public string Translate(ISQLQueryTranslator expressionTranslator)
        {
            var insertBuilder = new StringBuilder();

            insertBuilder.Append($"INSERT INTO {expressionTranslator.GetTableName(typeof(T))} ");

            if (_insertProperties != null)
            {
                var properties = _insertProperties
                    .ToDictionary(p => p.Key.ToUpper(), p => p.Value);

                insertBuilder.Append($"{Environment.NewLine}({string.Join(", ", _tableColumns.Where(c => properties.ContainsKey(c.ToUpper())).Select(c => $"[{c}]"))})");
            }
            else if (_insertParameters != null)
            {
                insertBuilder.Append($"{Environment.NewLine}({string.Join(", ", _tableColumns.Where(c => _insertParameters.ContainsKey(c)).Select(c => $"[{c}]"))})");
            }
            else if (_insertColumns != null)
            {
                insertBuilder.Append($"{Environment.NewLine}({string.Join(", ", _insertColumns.Select(u => expressionTranslator.GetColumnsWithoutTableAlias(expressionTranslator.Translate(u, FragmentType))))})");
            }
            else
            {
                insertBuilder.Append($"{Environment.NewLine}({string.Join(", ", _select.Select.Select(u => expressionTranslator.GetColumnsWithoutTableAlias(expressionTranslator.Translate(u, FragmentType))))})");
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

        private void AddSqlQueryInsertSelect()
        {
            var insertSelect = new SQLQueryInsertValuesFromSelect<T>(_select);
            _fragments.Add(insertSelect);
        }

        private void AddSqlQueryInsertParameters()
        {
            var inserParameters = new SQLQueryInsertParameters<T>(_tableColumns, _insertParameters);
            _fragments.Add(inserParameters);
        }

        private IDictionary<string, object> GetInsertProperties(object insert)
        {
            return insert.GetType().GetReadableProperties()
                 .ToDictionary(p => p.Name, p => p.GetValue(insert));
        }

        private List<string> GetTableColumns()
        {
            return typeof(T).GetWritableColumnsProperties()
                .Select(p => p.Name)
                .ToList();
        }
    }

    internal class SQLQueryInsertValues : ISQLQueryFragment
    {
        private readonly List<string> _tableColumns;
        private readonly IDictionary<string, object> _insertProperties;

        internal SQLQueryInsertValues(List<string> tableColumns, IDictionary<string, object> insertProperties)
        {
            _tableColumns = tableColumns;
            _insertProperties = insertProperties;
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.InsertValues; } }

        public string Translate(ISQLQueryTranslator expressionTranslator)
        {
            var valuesBuilder = new StringBuilder();

            var properties = _insertProperties
                    .ToDictionary(p => p.Key.ToUpper(), p => p.Value);

            valuesBuilder.Append($"VALUES ");

            valuesBuilder.Append($"{Environment.NewLine}({string.Join(", ", _tableColumns.Where(c => properties.ContainsKey(c.ToUpper())).Select(c => expressionTranslator.MakeParameter(properties[c.ToUpper()])))})");

            return valuesBuilder.ToString();
        }
    }

    internal class SQLQueryInsertValuesFromSelect<T> : ISQLQueryFragment
    {
        private readonly SQLQuerySelect _select;

        internal SQLQueryInsertValuesFromSelect(SQLQuerySelect select)
        {
            _select = select;
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.InsertValues; } }

        public string Translate(ISQLQueryTranslator expressionTranslator)
        {
            var selectBuilder = new StringBuilder();

            if (_select.FragmentType == SQLQueryFragmentType.SelectCte)
            {
                selectBuilder.Append($"SELECT ");

                selectBuilder.Append($"{Environment.NewLine}{string.Join($", {Environment.NewLine}", _select.Select.Select(s => expressionTranslator.Translate(s, FragmentType)))} {Environment.NewLine}");

                selectBuilder.Append($"FROM {expressionTranslator.GetTableAlias(typeof(T))}");
            }
            else
            {
                var cteFragments = _select.GetHierachicalSelectCte();

                selectBuilder.Append(string.Join($"{Environment.NewLine} ", _select.Where(f => !(f is ISQLQuery fSQLQuery) || !cteFragments.Contains(fSQLQuery)).Select(s => s.Translate(expressionTranslator))));
            }

            return selectBuilder.ToString();
        }
    }

    internal class SQLQueryInsertParameters<T> : ISQLQueryFragment
    {
        private readonly List<string> _tableColumns;
        private IDictionary<string, Expression> _insertParameters;

        public SQLQueryInsertParameters(List<string> tableColumns, IDictionary<string, Expression> insertParameters)
        {
            _tableColumns = tableColumns;
            _insertParameters = insertParameters;
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.InsertValues; } }

        public string Translate(ISQLQueryTranslator expressionTranslator)
        {
            var valuesBuilder = new StringBuilder();

            valuesBuilder.Append($"VALUES ");

            valuesBuilder.Append($"{Environment.NewLine}({string.Join(", ", _tableColumns.Where(c => _insertParameters.ContainsKey(c)).Select(c => expressionTranslator.Translate(_insertParameters[c], FragmentType)))})");

            return valuesBuilder.ToString();
        }
    }
}
