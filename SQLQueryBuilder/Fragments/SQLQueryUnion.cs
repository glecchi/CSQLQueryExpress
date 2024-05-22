using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SQLQueryBuilder.Fragments
{
    public abstract class SQLQueryUnion : ISQLQueryFragment, ISQLQuery
    {
        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Union; } }

        public abstract string Translate(ISQLQueryExpressionTranslator expressionTranslator);

        public abstract IEnumerator<ISQLQueryFragment> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SQLQueryUnion<T> : SQLQueryUnion
    {
        private readonly IList<ISQLQueryFragment> _fragments;
        private readonly SQLQuerySelect<T>[] _select;
        private readonly bool _all;

        internal SQLQueryUnion(IList<ISQLQueryFragment> fragments, bool all, params SQLQuerySelect<T>[] select)
        {
            _fragments = fragments;
            _all = all;
            _select = select;

            _fragments.Add(this);
        }

        public override IEnumerator<ISQLQueryFragment> GetEnumerator()
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
                    foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer()))
                    {
                        yield return fragment;
                    }
                }
            }
        }

        public override string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            return $"{Environment.NewLine}{(_all ? "UNION ALL" : "UNION")} {Environment.NewLine}";
        }
    }
}