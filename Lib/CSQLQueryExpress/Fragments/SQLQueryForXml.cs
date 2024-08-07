﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CSQLQueryExpress.Fragments
{
    public interface ISQLQueryForXml
    {

    }

    public class SQLQueryForXml<T> : ISQLQueryFragment, ISQLQuery
    {
        private readonly IList<ISQLQueryFragment> _fragments;
        private readonly Expression _forXml;

        internal SQLQueryForXml(IList<ISQLQueryFragment> fragments)
        {
            _fragments = fragments;

            _fragments.Add(this);
        }

        internal SQLQueryForXml(IList<ISQLQueryFragment> fragments, Expression forXml)
        {
            _fragments = fragments;
            _forXml = forXml;

            _fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType => SQLQueryFragmentType.ForXml;

        public IEnumerator<ISQLQueryFragment> GetEnumerator()
        {
            foreach (var fragment in _fragments.OrderBy(f => f, new SQLQueryFragmentComparer(FragmentType)))
            {
                yield return fragment;
            }
        }

        public string Translate(ISQLQueryTranslator expressionTranslator)
        {
            if (_forXml != null)
            {
                return $"FOR XML {expressionTranslator.Translate(_forXml, FragmentType)}";
            }

            return "FOR XML";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
