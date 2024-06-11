using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSQLQueryExpress.Fragments
{
    public class SQLQueryBatch<T> : ISQLQuery, ISQLQueryFragment
    {
        private readonly IList<ISQLQueryFragment> _fragments;
        private readonly ISQLQuery[] _queries;
        
        internal SQLQueryBatch(IList<ISQLQueryFragment> fragments, params ISQLQuery[] queries)
        {
            if (queries == null || queries.Length == 0)
            {
                throw new ArgumentNullException(nameof(queries));
            }

            _fragments = fragments;
            _queries = queries;

            _fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Batch; } }

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            var idx = _queries.Length - 1;
            foreach (var sel in _queries)
            {
                foreach (var fragment in sel)
                {
                    yield return fragment;
                }

                foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer(FragmentType)))
                {
                    yield return fragment;
                }
            }
        }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            return $";{Environment.NewLine} ";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}