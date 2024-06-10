using System.Collections.Generic;
using CSQLQueryExpress.Fragments;

namespace CSQLQueryExpress
{
    /// <summary>
    /// Used to create a SQL query expression.
    /// </summary>
    public sealed class SQLQuery
    {
        private readonly IList<ISQLQueryFragment> _fragments = new List<ISQLQueryFragment>();

        public SQLQueryFrom<T> From<T>() where T : ISQLQueryEntity
        {
            return new SQLQueryFrom<T>(_fragments);
        }

        public SQLQueryFrom<T> From<T>(SQLQuerySelect<T> select) where T : ISQLQueryEntity
        {
            return new SQLQueryFrom<T>(_fragments, select);
        }

        public SQLQueryFromUnion<T> From<T>(SQLQueryUnion<T> union)
        {
            return new SQLQueryFromUnion<T>(_fragments, union);
        }

        public SQLQueryInsert<T> Insert<T>(T insert) where T : ISQLQueryEntity
        {
            return new SQLQueryInsert<T>(_fragments, insert);
        }

        public SQLQueryInsert<T> Insert<T>(object insert) where T : ISQLQueryEntity
        {
            return new SQLQueryInsert<T>(_fragments, insert);
        }

        public SQLQueryInsert<T> Insert<T>(IDictionary<string, object> insert) where T : ISQLQueryEntity
        {
            return new SQLQueryInsert<T>(_fragments, insert);
        }

        public SQLQueryUnion<T> Union<T>(SQLQuerySelect<T> firstSelect, SQLQuerySelect<T> secondSelect, params SQLQuerySelect<T>[] otherSelect)
        {
            return new SQLQueryUnion<T>(_fragments, false, firstSelect, secondSelect, otherSelect);
        }

        public SQLQueryUnion<T> UnionAll<T>(SQLQuerySelect<T> firstSelect, SQLQuerySelect<T> secondSelect, params SQLQuerySelect<T>[] otherSelect)
        {
            return new SQLQueryUnion<T>(_fragments, true, firstSelect, secondSelect, otherSelect);
        }

        public SQLQueryTruncate<T> Truncate<T>() where T : ISQLQueryEntity
        {
            return new SQLQueryTruncate<T>(_fragments);
        }

        public SQLQueryDrop<T> Drop<T>() where T : ISQLQueryEntity
        {
            return new SQLQueryDrop<T>(_fragments);
        }

        public SQLQueryStoredProcedure StoredProcedure(ISQLStoredProcedure procedure)
        {
            return new SQLQueryStoredProcedure(_fragments, procedure);
        }

        public SQLQueryStoredProcedure<T> StoredProcedure<T>(ISQLStoredProcedure<T> procedure)
        {
            return new SQLQueryStoredProcedure<T>(_fragments, procedure);
        }
    }
}