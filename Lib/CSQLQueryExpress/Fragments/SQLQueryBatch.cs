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

            if (queries.Any(q => q.IsHierarchicalSelectFromCte()))
            {
                throw new NotSupportedException($"Queries with CTE TABLEs is not supported in {nameof(SQLQueryBatch<T>)}");
            }

            _fragments = fragments;
            _queries = queries;

            _fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Batch; } }

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var qry in _queries)
            {
                foreach (var fragment in qry)
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