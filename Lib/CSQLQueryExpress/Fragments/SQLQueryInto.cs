using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSQLQueryExpress.Fragments
{
    public abstract class SQLQueryInto : ISQLQueryFragment, ISQLQuery
    {
        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Into; } }

        public abstract IEnumerator<ISQLQueryFragment> GetEnumerator();

        public abstract string Translate(ISQLQueryExpressionTranslator expressionTranslator);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SQLQueryInto<T> : SQLQueryInto
    {
        private readonly IList<ISQLQueryFragment> _fragments;

        internal SQLQueryInto(IList<ISQLQueryFragment> fragments)
        {
            _fragments = fragments;

            _fragments.Add(this);
        }

        public override string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            return $"INTO {expressionTranslator.GetTableName(typeof(T))}";
        }

        public override IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer(FragmentType)))
            {
                yield return fragment;
            }
        }
    }
}
