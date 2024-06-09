using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SQLQueryBuilder.Fragments
{
    public class SQLQueryDrop<T> : ISQLQueryFragment, ISQLQuery
        where T : ISQLQueryEntity
    {
        private readonly IList<ISQLQueryFragment> _fragments;

        internal SQLQueryDrop(IList<ISQLQueryFragment> fragments)
        {
            _fragments = fragments;

            _fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Drop; } }

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer(FragmentType)))
            {
                yield return fragment;
            }
        }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            return $"DROP TABLE {expressionTranslator.GetTableName(typeof(T))}";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}