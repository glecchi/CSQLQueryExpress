using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CSQLQueryExpress.Fragments
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
        private readonly SQLQueryFragmentType _owner;
        private readonly IList<ISQLQueryFragment> _fragments;

        internal SQLQueryOutput(SQLQueryFragmentType owner, IList<ISQLQueryFragment> fragments, params Expression[] output)
        {
            _owner = owner;
            _fragments = fragments;
            _output = output;

            _fragments.Add(this);
        }

        public override string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            return $"OUTPUT {Environment.NewLine}{string.Join($"{Environment.NewLine}, ", _output.Select(u => expressionTranslator.Translate(u)))}";
        }

        public override IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer(_owner)))
            {
                yield return fragment;
            }
        }
    }
}