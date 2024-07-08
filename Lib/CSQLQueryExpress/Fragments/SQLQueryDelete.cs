using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CSQLQueryExpress.Fragments
{
    public class SQLQueryDelete<T> : ISQLQueryFragment, ISQLQuery ,
        ISQLQueryWithOutput<T>
        where T : ISQLQueryEntity
    {
        private readonly IList<ISQLQueryFragment> _fragments;
        private readonly bool _inLine;
        private int? _top;

        internal SQLQueryDelete(IList<ISQLQueryFragment> fragments)
        {
            _fragments = fragments;
            _inLine = fragments.All(f => f.FragmentType != SQLQueryFragmentType.FromBySelect);

            _fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Delete; } }

        public SQLQueryDelete<T> Top(int count)
        {
            _top = count;
            return this;
        }

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

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            var deleteBuilder = new StringBuilder();

            if (_top.HasValue)
            {
                if (!_inLine)
                {
                    deleteBuilder.Append($"DELETE TOP({_top}) {expressionTranslator.GetTableAlias(typeof(T))}");
                }
                else
                {
                    deleteBuilder.Append($"DELETE TOP({_top})");
                }
            }
            else
            {
                if (!_inLine)
                {
                    deleteBuilder.Append($"DELETE {expressionTranslator.GetTableAlias(typeof(T))}");
                }
                else
                {
                    deleteBuilder.Append($"DELETE");
                }
            }

            return deleteBuilder.ToString();
        }

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer(FragmentType)))
            {
                yield return fragment;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}