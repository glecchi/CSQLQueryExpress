using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SQLQueryBuilder.Fragments
{
    public class SQLQueryFromUnion<T> : ISQLQueryFragment, 
        ISQLQueryWithSelect<T>
    {
        private readonly IList<ISQLQueryFragment> _fragments;
        private readonly SQLQueryUnion<T> _union;

        public SQLQueryFromUnion(IList<ISQLQueryFragment> fragments, SQLQueryUnion<T> union)
        {
            _fragments = fragments;
            _union = union;

            foreach (var cteFragment in union.Where(f => f.FragmentType == SQLQueryFragmentType.SelectCte))
            {
                _fragments.Add(cteFragment);
            }
                
            _fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType => SQLQueryFragmentType.FromUnion;

        public SQLQuerySelect SelectAll()
        {
            return new SQLQuerySelect(_fragments);
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TS>> select)
        {
            return new SQLQuerySelect<TS>(_fragments, select.Merge());
        }

        public SQLQuerySelect<T> Select(
            Expression<Func<T, object>> select,
            params Expression<Func<T, object>>[] otherSelect)
        {
            return new SQLQuerySelect<T>(_fragments, select.Merge(otherSelect));
        }

        public SQLQuerySelect<TS> Select<TS>(
            Expression<Func<T, TS, object>> select,
            params Expression<Func<T, TS, object>>[] otherSelect)
        {
            return new SQLQuerySelect<TS>(_fragments, select.Merge(otherSelect));
        }

        public string Translate(ISQLQueryExpressionTranslator expressionTranslator)
        {
            return $"FROM {Environment.NewLine}({Environment.NewLine}{string.Join($" {Environment.NewLine}", _union.Select(u => u.Translate(expressionTranslator)))}{Environment.NewLine}) AS {expressionTranslator.GetTableAlias(typeof(T))} ";
        }
    }
}