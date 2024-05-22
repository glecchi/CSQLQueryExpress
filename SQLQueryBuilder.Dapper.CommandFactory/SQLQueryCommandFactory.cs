using Dapper;
using SQLQueryBuilder.Fragments;
using System.Collections;
using System.Data.SqlClient;

namespace SQLQueryBuilder.Dapper.CommandFactory
{
    public static class SQLQueryCommandFactory
    {
        private static string ConnectionString;

        public static void SetConnectionString(string connectinString)
        {
            ConnectionString = connectinString;
        }

        public static IEnumerable<T> GetReader<T>(SQLQuerySelect<T> query, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = query.Compile();

            return new SqlQueryCommandFactory<T>(ConnectionString, queryCompiled);
        }

        public static IEnumerable<T> GetReader<T>(SQLQueryUnion<T> query, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = query.Compile();

            return new SqlQueryCommandFactory<T>(ConnectionString, queryCompiled);
        }

        public static IEnumerable<T> GetReader<T>(SQLQueryForXml<T> query, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = query.Compile();

            return new SqlQueryCommandFactory<T>(ConnectionString, queryCompiled);
        }

        public static IEnumerable<T> GetReader<T>(SQLQueryOutput<T> query, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = query.Compile();

            return new SqlQueryCommandFactory<T>(ConnectionString, queryCompiled);
        }

        public static int GetResult<T>(SQLQueryInto<T> query, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = query.Compile();

            return new SqlCommand(ConnectionString, queryCompiled).Execute();
        }

        public static int GetResult<T>(SQLQueryInsert<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity
        {
            queryCompiled = query.Compile();

            return new SqlCommand(ConnectionString, queryCompiled).Execute();
        }

        public static int GetResult<T>(SQLQueryUpdate<T> query, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = query.Compile();

            return new SqlCommand(ConnectionString, queryCompiled).Execute();
        }

        public static int GetResult<T>(SQLQueryDelete<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity
        {
            queryCompiled = query.Compile();

            return new SqlCommand(ConnectionString, queryCompiled).Execute();
        }

        public static int GetResult<T>(SQLQueryTruncate<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity
        {
            queryCompiled = query.Compile();

            return new SqlCommand(ConnectionString, queryCompiled).Execute();
        }

        public static int GetResult<T>(SQLQueryDrop<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity
        {
            queryCompiled = query.Compile();

            return new SqlCommand(ConnectionString, queryCompiled).Execute();
        }
    }

    internal class SqlCommand
    {
        private readonly SQLQueryCompiled _queryCompiled;
        private readonly string _connectionString;

        public SqlCommand(string connectionString, SQLQueryCompiled queryCompiled)
        {
            _queryCompiled = queryCompiled;
            _connectionString = connectionString;
        }

        public int Execute()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                Int32 result;

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        result = connection.Execute(_queryCompiled.Statement, _queryCompiled.Parameters, transaction);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                return result;
            }
        }
    }

    internal class SqlQueryCommandFactory<T> : IEnumerable<T>
    {
        private readonly SQLQueryCompiled _queryCompiled;
        private readonly string _connectionString;

        public SqlQueryCommandFactory(string connectionString, SQLQueryCompiled queryCompiled)
        {
            _queryCompiled = queryCompiled;
            _connectionString = connectionString;
        }

        public IEnumerator<T> GetEnumerator()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                IEnumerable<T> result;
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        result = connection.Query<T>(_queryCompiled.Statement, _queryCompiled.Parameters, transaction);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                foreach (var item in result)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
