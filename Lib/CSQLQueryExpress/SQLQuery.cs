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

        private IList<ISQLQueryFragment> InitNewFragmentsList()
        {
             return new List<ISQLQueryFragment>();
        }
    }
}