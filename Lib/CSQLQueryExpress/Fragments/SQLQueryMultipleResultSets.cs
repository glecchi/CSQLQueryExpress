using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSQLQueryExpress.Fragments
{
    public class SQLQueryMultipleResultSets : ISQLQuery, ISQLQueryFragment
    {
        private readonly IList<ISQLQueryFragment> _fragments;
        private readonly SQLQuerySelect[] _queries;

        internal SQLQueryMultipleResultSets(IList<ISQLQueryFragment> fragments, params SQLQuerySelect[] queries)
        {
            if (queries == null || queries.Length == 0)
            {
                throw new ArgumentNullException(nameof(queries));
            }

            if (queries.Any(q => q.FragmentType == SQLQueryFragmentType.SelectCte || q.IsHierarchicalSelectFromCte()))
            {
                throw new NotSupportedException($"Queries with CTE TABLEs is not supported in {nameof(SQLQueryMultipleResultSets)}");
            }

            _fragments = fragments;
            _queries = queries;

            _fragments.Add(this);
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.MultipleResultSets; } }

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
            return $"; {Environment.NewLine}";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SQLQueryMultipleResultSets<T> : SQLQueryMultipleResultSets
    {
        internal SQLQueryMultipleResultSets(IList<ISQLQueryFragment> fragments, params SQLQuerySelect<T>[] queries)
            : base(fragments, queries)
        {

        }
    }

    public class SQLQueryMultipleResultSets<T1, T2> : SQLQueryMultipleResultSets
    {
        internal SQLQueryMultipleResultSets(IList<ISQLQueryFragment> fragments, SQLQuerySelect<T1> queryT1, SQLQuerySelect<T2> queryT2)
            : base(fragments, new SQLQuerySelect[] { queryT1, queryT2 })
        {

        }
    }

    public class SQLQueryMultipleResultSets<T1, T2, T3> : SQLQueryMultipleResultSets
    {
        internal SQLQueryMultipleResultSets(IList<ISQLQueryFragment> fragments, SQLQuerySelect<T1> queryT1, SQLQuerySelect<T2> queryT2, SQLQuerySelect<T3> queryT3)
            : base(fragments, new SQLQuerySelect[] { queryT1, queryT2, queryT3 })
        {

        }
    }

    public class SQLQueryMultipleResultSets<T1, T2, T3, T4> : SQLQueryMultipleResultSets
    {
        internal SQLQueryMultipleResultSets(
            IList<ISQLQueryFragment> fragments, 
            SQLQuerySelect<T1> queryT1, 
            SQLQuerySelect<T2> queryT2, 
            SQLQuerySelect<T3> queryT3,
            SQLQuerySelect<T4> queryT4)
            : base(fragments, new SQLQuerySelect[] { queryT1, queryT2, queryT3, queryT4 })
        {

        }
    }

    public class SQLQueryMultipleResultSets<T1, T2, T3, T4, T5> : SQLQueryMultipleResultSets
    {
        internal SQLQueryMultipleResultSets(
            IList<ISQLQueryFragment> fragments,
            SQLQuerySelect<T1> queryT1,
            SQLQuerySelect<T2> queryT2,
            SQLQuerySelect<T3> queryT3,
            SQLQuerySelect<T4> queryT4,
            SQLQuerySelect<T5> queryT5)
            : base(fragments, new SQLQuerySelect[] { queryT1, queryT2, queryT3, queryT4, queryT5 })
        {

        }
    }

    public class SQLQueryMultipleResultSets<T1, T2, T3, T4, T5, T6> : SQLQueryMultipleResultSets
    {
        internal SQLQueryMultipleResultSets(
            IList<ISQLQueryFragment> fragments,
            SQLQuerySelect<T1> queryT1,
            SQLQuerySelect<T2> queryT2,
            SQLQuerySelect<T3> queryT3,
            SQLQuerySelect<T4> queryT4,
            SQLQuerySelect<T5> queryT5,
            SQLQuerySelect<T6> queryT6)
            : base(fragments, new SQLQuerySelect[] { queryT1, queryT2, queryT3, queryT4, queryT5, queryT6 })
        {

        }
    }

    public class SQLQueryMultipleResultSets<T1, T2, T3, T4, T5, T6, T7> : SQLQueryMultipleResultSets
    {
        internal SQLQueryMultipleResultSets(
            IList<ISQLQueryFragment> fragments,
            SQLQuerySelect<T1> queryT1,
            SQLQuerySelect<T2> queryT2,
            SQLQuerySelect<T3> queryT3,
            SQLQuerySelect<T4> queryT4,
            SQLQuerySelect<T5> queryT5,
            SQLQuerySelect<T6> queryT6,
            SQLQuerySelect<T7> queryT7)
            : base(fragments, new SQLQuerySelect[] { queryT1, queryT2, queryT3, queryT4, queryT5, queryT6, queryT7 })
        {

        }
    }

    public class SQLQueryMultipleResultSets<T1, T2, T3, T4, T5, T6, T7, T8> : SQLQueryMultipleResultSets
    {
        internal SQLQueryMultipleResultSets(
            IList<ISQLQueryFragment> fragments,
            SQLQuerySelect<T1> queryT1,
            SQLQuerySelect<T2> queryT2,
            SQLQuerySelect<T3> queryT3,
            SQLQuerySelect<T4> queryT4,
            SQLQuerySelect<T5> queryT5,
            SQLQuerySelect<T6> queryT6,
            SQLQuerySelect<T7> queryT7,
            SQLQuerySelect<T8> queryT8)
            : base(fragments, new SQLQuerySelect[] { queryT1, queryT2, queryT3, queryT4, queryT5, queryT6, queryT7, queryT8 })
        {

        }
    }

    public class SQLQueryMultipleResultSets<T1, T2, T3, T4, T5, T6, T7, T8, T9> : SQLQueryMultipleResultSets
    {
        internal SQLQueryMultipleResultSets(
            IList<ISQLQueryFragment> fragments,
            SQLQuerySelect<T1> queryT1,
            SQLQuerySelect<T2> queryT2,
            SQLQuerySelect<T3> queryT3,
            SQLQuerySelect<T4> queryT4,
            SQLQuerySelect<T5> queryT5,
            SQLQuerySelect<T6> queryT6,
            SQLQuerySelect<T7> queryT7,
            SQLQuerySelect<T8> queryT8,
            SQLQuerySelect<T9> queryT9)
            : base(fragments, new SQLQuerySelect[] { queryT1, queryT2, queryT3, queryT4, queryT5, queryT6, queryT7, queryT8, queryT9 })
        {

        }
    }
}