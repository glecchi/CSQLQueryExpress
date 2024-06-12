using System.Collections.Generic;
using CSQLQueryExpress.Fragments;

namespace CSQLQueryExpress
{
    /// <summary>
    /// Used to create a SQL query expression.
    /// </summary>
    public sealed class SQLQuery
    {
        public SQLQueryFrom<T> From<T>() where T : ISQLQueryEntity
        {
            return new SQLQueryFrom<T>(InitNewFragmentsList());
        }

        public SQLQueryFrom<T> From<T>(SQLQuerySelect<T> select) where T : ISQLQueryEntity
        {
            return new SQLQueryFrom<T>(InitNewFragmentsList(), select);
        }

        public SQLQueryFromUnion<T> From<T>(SQLQueryUnion<T> union)
        {
            return new SQLQueryFromUnion<T>(InitNewFragmentsList(), union);
        }

        public SQLQueryInsert<T> Insert<T>(T insert) where T : ISQLQueryEntity
        {
            return new SQLQueryInsert<T>(InitNewFragmentsList(), insert);
        }

        public SQLQueryInsert<T> Insert<T>(object insert) where T : ISQLQueryEntity
        {
            return new SQLQueryInsert<T>(InitNewFragmentsList(), insert);
        }

        public SQLQueryInsert<T> Insert<T>(IDictionary<string, object> insert) where T : ISQLQueryEntity
        {
            return new SQLQueryInsert<T>(InitNewFragmentsList(), insert);
        }

        public SQLQueryUnion<T> Union<T>(SQLQuerySelect<T> firstSelect, SQLQuerySelect<T> secondSelect, params SQLQuerySelect<T>[] otherSelect)
        {
            return new SQLQueryUnion<T>(InitNewFragmentsList(), false, firstSelect, secondSelect, otherSelect);
        }

        public SQLQueryUnion<T> UnionAll<T>(SQLQuerySelect<T> firstSelect, SQLQuerySelect<T> secondSelect, params SQLQuerySelect<T>[] otherSelect)
        {
            return new SQLQueryUnion<T>(InitNewFragmentsList(), true, firstSelect, secondSelect, otherSelect);
        }

        public SQLQueryBatch<T> Batch<T>(params SQLQueryInsert<T>[] insert) where T : ISQLQueryEntity
        {
            return new SQLQueryBatch<T>(InitNewFragmentsList(), insert);
        }

        public SQLQueryBatch<T> Batch<T>(params SQLQueryUpdate<T>[] update) where T : ISQLQueryEntity
        {
            return new SQLQueryBatch<T>(InitNewFragmentsList(), update);
        }

        public SQLQueryBatch<T> Batch<T>(params SQLQueryDelete<T>[] delete) where T : ISQLQueryEntity
        {
            return new SQLQueryBatch<T>(InitNewFragmentsList(), delete);
        }

        public SQLQueryTruncate<T> Truncate<T>() where T : ISQLQueryEntity
        {
            return new SQLQueryTruncate<T>(InitNewFragmentsList());
        }

        public SQLQueryDrop<T> Drop<T>() where T : ISQLQueryEntity
        {
            return new SQLQueryDrop<T>(InitNewFragmentsList());
        }

        public SQLQueryStoredProcedure StoredProcedure(ISQLStoredProcedure procedure)
        {
            return new SQLQueryStoredProcedure(InitNewFragmentsList(), procedure);
        }

        public SQLQueryStoredProcedure<T> StoredProcedure<T>(ISQLStoredProcedure<T> procedure)
        {
            return new SQLQueryStoredProcedure<T>(InitNewFragmentsList(), procedure);
        }

        public SQLQueryMultipleResultSets MultipleResultSets(params SQLQuerySelect[] select)
        {
            return new SQLQueryMultipleResultSets(InitNewFragmentsList(), select);
        }

        public SQLQueryMultipleResultSets<T> MultipleResultSets<T>(params SQLQuerySelect<T>[] select)
        {
            return new SQLQueryMultipleResultSets<T>(InitNewFragmentsList(), select);
        }

        public SQLQueryMultipleResultSets<T1, T2> MultipleResultSets<T1, T2>(
            SQLQuerySelect<T1> selectT1, 
            SQLQuerySelect<T2> selectT2)
        {
            return new SQLQueryMultipleResultSets<T1, T2>(InitNewFragmentsList(), selectT1, selectT2);
        }

        public SQLQueryMultipleResultSets<T1, T2, T3> MultipleResultSets<T1, T2, T3>(
            SQLQuerySelect<T1> selectT1, 
            SQLQuerySelect<T2> selectT2, 
            SQLQuerySelect<T3> selectT3)
        {
            return new SQLQueryMultipleResultSets<T1, T2, T3>(InitNewFragmentsList(), selectT1, selectT2, selectT3);
        }

        public SQLQueryMultipleResultSets<T1, T2, T3, T4> MultipleResultSets<T1, T2, T3, T4>(
            SQLQuerySelect<T1> selectT1, 
            SQLQuerySelect<T2> selectT2, 
            SQLQuerySelect<T3> selectT3,
            SQLQuerySelect<T4> selectT4)
        {
            return new SQLQueryMultipleResultSets<T1, T2, T3, T4>(InitNewFragmentsList(), selectT1, selectT2, selectT3, selectT4);
        }

        public SQLQueryMultipleResultSets<T1, T2, T3, T4, T5> MultipleResultSets<T1, T2, T3, T4, T5>(
            SQLQuerySelect<T1> selectT1,
            SQLQuerySelect<T2> selectT2,
            SQLQuerySelect<T3> selectT3,
            SQLQuerySelect<T4> selectT4,
            SQLQuerySelect<T5> selectT5)
        {
            return new SQLQueryMultipleResultSets<T1, T2, T3, T4, T5>(InitNewFragmentsList(), selectT1, selectT2, selectT3, selectT4, selectT5);
        }

        public SQLQueryMultipleResultSets<T1, T2, T3, T4, T5, T6> MultipleResultSets<T1, T2, T3, T4, T5, T6>(
            SQLQuerySelect<T1> selectT1,
            SQLQuerySelect<T2> selectT2,
            SQLQuerySelect<T3> selectT3,
            SQLQuerySelect<T4> selectT4,
            SQLQuerySelect<T5> selectT5,
            SQLQuerySelect<T6> selectT6)
        {
            return new SQLQueryMultipleResultSets<T1, T2, T3, T4, T5, T6>(InitNewFragmentsList(), selectT1, selectT2, selectT3, selectT4, selectT5, selectT6);
        }

        public SQLQueryMultipleResultSets<T1, T2, T3, T4, T5, T6, T7> MultipleResultSets<T1, T2, T3, T4, T5, T6, T7>(
            SQLQuerySelect<T1> selectT1,
            SQLQuerySelect<T2> selectT2,
            SQLQuerySelect<T3> selectT3,
            SQLQuerySelect<T4> selectT4,
            SQLQuerySelect<T5> selectT5,
            SQLQuerySelect<T6> selectT6,
            SQLQuerySelect<T7> selectT7)
        {
            return new SQLQueryMultipleResultSets<T1, T2, T3, T4, T5, T6, T7>(InitNewFragmentsList(), selectT1, selectT2, selectT3, selectT4, selectT5, selectT6, selectT7);
        }

        public SQLQueryMultipleResultSets<T1, T2, T3, T4, T5, T6, T7, T8> MultipleResultSets<T1, T2, T3, T4, T5, T6, T7, T8>(
            SQLQuerySelect<T1> selectT1,
            SQLQuerySelect<T2> selectT2,
            SQLQuerySelect<T3> selectT3,
            SQLQuerySelect<T4> selectT4,
            SQLQuerySelect<T5> selectT5,
            SQLQuerySelect<T6> selectT6,
            SQLQuerySelect<T7> selectT7,
            SQLQuerySelect<T8> selectT8)
        {
            return new SQLQueryMultipleResultSets<T1, T2, T3, T4, T5, T6, T7, T8>(InitNewFragmentsList(), selectT1, selectT2, selectT3, selectT4, selectT5, selectT6, selectT7, selectT8);
        }

        public SQLQueryMultipleResultSets<T1, T2, T3, T4, T5, T6, T7, T8, T9> MultipleResultSets<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            SQLQuerySelect<T1> selectT1,
            SQLQuerySelect<T2> selectT2,
            SQLQuerySelect<T3> selectT3,
            SQLQuerySelect<T4> selectT4,
            SQLQuerySelect<T5> selectT5,
            SQLQuerySelect<T6> selectT6,
            SQLQuerySelect<T7> selectT7,
            SQLQuerySelect<T8> selectT8,
            SQLQuerySelect<T9> selectT9)
        {
            return new SQLQueryMultipleResultSets<T1, T2, T3, T4, T5, T6, T7, T8, T9>(InitNewFragmentsList(), selectT1, selectT2, selectT3, selectT4, selectT5, selectT6, selectT7, selectT8, selectT9);
        }

        private IList<ISQLQueryFragment> InitNewFragmentsList()
        {
             return new List<ISQLQueryFragment>();
        }
    }
}