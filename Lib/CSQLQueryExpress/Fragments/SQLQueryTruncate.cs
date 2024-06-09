using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSQLQueryExpress.Fragments
{
    public class SQLQueryTruncate<T> : ISQLQueryFragment, ISQLQuery
        where T : ISQLQueryEntity
    {
        private readonly IList<ISQLQueryFragment> _fragments;

        internal SQLQueryTruncate(IList<ISQLQueryFragment> fragments)
        {
            _fragments = fragments;

            _fragments.Add(this);
        }

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer(FragmentType)))
            {
                yield return fragment;
            }
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Truncate; } }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            return $"TRUNCATE TABLE {expressionTranslator.GetTableName(typeof(T))}";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}