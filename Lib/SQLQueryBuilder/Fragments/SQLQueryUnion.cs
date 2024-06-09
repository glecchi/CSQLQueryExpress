using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SQLQueryBuilder.Fragments
{
    public class SQLQueryUnion<T> : ISQLQuery, ISQLQueryFragment
    {
        private readonly IList<ISQLQueryFragment> _fragments;
        private readonly SQLQuerySelect<T>[] _select;
        private readonly bool _all;

        internal SQLQueryUnion(IList<ISQLQueryFragment> fragments, bool all, SQLQuerySelect<T> firstSelect, SQLQuerySelect<T> secondSelect, params SQLQuerySelect<T>[] otherSelect)
        {
            _fragments = fragments;
            _all = all;
            _select = firstSelect.Merge(secondSelect, otherSelect);

            _fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Union; } }

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            var idx = _select.Length - 1;
            foreach (var sel in _select)
            {
                foreach (var fragment in sel)
                {
                    yield return fragment;
                }

                if (idx-- > 0)
                {
                    foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer(FragmentType)))
                    {
                        yield return fragment;
                    }
                }
            }
        }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            return $"{Environment.NewLine}{(_all ? "UNION ALL" : "UNION")} {Environment.NewLine}";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}