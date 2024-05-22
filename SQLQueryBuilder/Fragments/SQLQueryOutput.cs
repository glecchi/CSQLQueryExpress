using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace SQLQueryBuilder.Fragments
{
    public abstract class SQLQueryOutput : ISQLQueryFragment, ISQLQuery
    {
        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Output; } }

        public abstract IEnumerator<ISQLQueryFragment> GetEnumerator();
        public abstract string Translate(ISQLQueryExpressionTranslator expressionTranslator);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SQLQueryOutput<T> : SQLQueryOutput
    {
        private readonly Expression[] _output;
        private readonly QueryOutputSource _source;
        private readonly IList<ISQLQueryFragment> _fragments;

        internal SQLQueryOutput(QueryOutputSource source, IList<ISQLQueryFragment> fragments, params Expression[] output)
        {
            _source = source;
            _fragments = fragments;
            _output = output;

            _fragments.Add(this);
        }

        public override string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            return $"OUTPUT {string.Join(", ", _output.Select(u => $"{_source.ToString().ToUpper()}.{expressionTranslator.GetColumnWithoutTableAlias(expressionTranslator.Translate(u))}"))} ";
        }

        public override IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer()))
            {
                yield return fragment;
            }
        }
    }

    public enum QueryOutputSource
    {
        Inserted,

        Deleted
    }
}