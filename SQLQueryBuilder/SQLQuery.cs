using System.Collections.Generic;
using SQLQueryBuilder.Fragments;

namespace SQLQueryBuilder
{
    public class SQLQuery
    {
        private IList<ISQLQueryFragment> _fragments = new List<ISQLQueryFragment>();

        public SQLQueryFrom<T> From<T>() where T : ISQLQueryEntity
        {
            return new SQLQueryFrom<T>(_fragments);
        }

        public SQLQueryFrom<T> From<T>(SQLQuerySelect<T> select) where T : ISQLQueryEntity
        {
            return new SQLQueryFrom<T>(_fragments, select);
        }

        public SQLQueryInsert<T> Insert<T>(object insert) where T : ISQLQueryEntity
        {
            return new SQLQueryInsert<T>(_fragments, insert);
        }

        public SQLQueryInsert<T> Insert<T>(IDictionary<string, object> insert) where T : ISQLQueryEntity
        {
            return new SQLQueryInsert<T>(_fragments, insert);
        }

        public SQLQueryUnion<T> Union<T>(params SQLQuerySelect<T>[] select)
        {
            return new SQLQueryUnion<T>(_fragments, false, select);
        }

        public SQLQueryUnion<T> UnionAll<T>(params SQLQuerySelect<T>[] select)
        {
            return new SQLQueryUnion<T>(_fragments, true, select);
        }

        public SQLQueryTruncate<T> Truncate<T>() where T : ISQLQueryEntity
        {
            return new SQLQueryTruncate<T>(_fragments);
        }

        public SQLQueryDrop<T> Drop<T>() where T : ISQLQueryEntity
        {
            return new SQLQueryDrop<T>(_fragments);
        }
    }
}