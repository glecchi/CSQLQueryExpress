using Dapper;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace QueryExecution.Dapper.CommandFactory
{
    public interface ISQLQueryCommandFactory
    {
        IEnumerable<T> GetReader<T>(SQLQueryForXml<T> query, out SQLQueryCompiled queryCompiled);
        IEnumerable<T> GetReader<T>(SQLQueryOutput<T> query, out SQLQueryCompiled queryCompiled);
        IEnumerable<T> GetReader<T>(SQLQuerySelect<T> query, out SQLQueryCompiled queryCompiled);
        IEnumerable<T> GetReader<T>(SQLQueryUnion<T> query, out SQLQueryCompiled queryCompiled);
        IEnumerable<T> GetReader<T>(SQLQueryStoredProcedure<T> procedure, out SQLQueryCompiled queryCompiled);
        IEnumerable GetReader(SQLQueryMultipleResultSets query, out SQLQueryCompiled queryCompiled);
        
        int GetResult<T>(SQLQueryDelete<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity;
        int GetResult<T>(SQLQueryDrop<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity;
        int GetResult<T>(SQLQueryInsert<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity;
        int GetResult<T>(SQLQueryInto<T> query, out SQLQueryCompiled queryCompiled);
        int GetResult<T>(SQLQueryTruncate<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity;
        int GetResult<T>(SQLQueryUpdate<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity;
        int GetResult(SQLQueryStoredProcedure procedure, out SQLQueryCompiled queryCompiled);
        int GetResult<T>(SQLQueryBatch<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity;
    }

    public class SQLQueryCommandFactory : ISQLQueryCommandFactory
    {
        private readonly string _connectionString;

        public SQLQueryCommandFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<T> GetReader<T>(SQLQuerySelect<T> query, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = query.Compile();

            return new SQLQueryCommandReader<T>(_connectionString, queryCompiled);
        }

        public IEnumerable<T> GetReader<T>(SQLQueryUnion<T> query, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = query.Compile();

            return new SQLQueryCommandReader<T>(_connectionString, queryCompiled);
        }

        public IEnumerable<T> GetReader<T>(SQLQueryForXml<T> query, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = query.Compile();

            return new SQLQueryCommandReader<T>(_connectionString, queryCompiled);
        }

        public IEnumerable<T> GetReader<T>(SQLQueryOutput<T> query, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = query.Compile();

            return new SQLQueryCommandReader<T>(_connectionString, queryCompiled);
        }

        public IEnumerable<T> GetReader<T>(SQLQueryStoredProcedure<T> procedure, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = procedure.Compile();

            return new SQLQueryCommandReader<T>(_connectionString, queryCompiled);
        }

        public IEnumerable GetReader(SQLQueryMultipleResultSets query, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = query.Compile();

            return new SQLQueryMultipleResultSetsCommandReader(_connectionString, queryCompiled);
        }

        public int GetResult<T>(SQLQueryInto<T> query, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = query.Compile();

            return new SqlQueryCommandExecute(_connectionString, queryCompiled).Execute();
        }

        public int GetResult<T>(SQLQueryInsert<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity
        {
            queryCompiled = query.Compile();

            return new SqlQueryCommandExecute(_connectionString, queryCompiled).Execute();
        }

        public int GetResult<T>(SQLQueryUpdate<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity
        {
            queryCompiled = query.Compile();

            return new SqlQueryCommandExecute(_connectionString, queryCompiled).Execute();
        }

        public int GetResult<T>(SQLQueryDelete<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity
        {
            queryCompiled = query.Compile();

            return new SqlQueryCommandExecute(_connectionString, queryCompiled).Execute();
        }

        public int GetResult<T>(SQLQueryTruncate<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity
        {
            queryCompiled = query.Compile();

            return new SqlQueryCommandExecute(_connectionString, queryCompiled).Execute();
        }

        public int GetResult<T>(SQLQueryDrop<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity
        {
            queryCompiled = query.Compile();

            return new SqlQueryCommandExecute(_connectionString, queryCompiled).Execute();
        }

        public int GetResult(SQLQueryStoredProcedure procedure, out SQLQueryCompiled queryCompiled)
        {
            queryCompiled = procedure.Compile();

            return new SqlQueryCommandExecute(_connectionString, queryCompiled).Execute();
        }

        public int GetResult<T>(SQLQueryBatch<T> query, out SQLQueryCompiled queryCompiled) where T : ISQLQueryEntity
        {
            queryCompiled = query.Compile();

            return new SqlQueryCommandExecute(_connectionString, queryCompiled).Execute();
        }
    }

    public class SqlQueryCommandExecute
    {
        private readonly SQLQueryCompiled _queryCompiled;
        private readonly string _connectionString;

        public SqlQueryCommandExecute(string connectionString, SQLQueryCompiled queryCompiled)
        {
            _queryCompiled = queryCompiled;
            _connectionString = connectionString;
        }

        public int Execute()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                int result;

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var parameters = new DynamicParameters();

                        if (_queryCompiled.Parameters.Count > 0)
                        {
                            foreach (var pr in _queryCompiled.Parameters)
                            {
                                ParameterDirection direction = GetDirection(pr);
                                parameters.Add(pr.Name, pr.Value, direction: direction);
                            }
                        }

                        result = connection.Execute(_queryCompiled.Statement, parameters, transaction);

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

        private ParameterDirection GetDirection(SQLQueryParameter pr)
        {
            switch (pr.Direction)
            {
                case SQLQueryParameterValueDirection.Input:
                    return ParameterDirection.Input;
                case SQLQueryParameterValueDirection.Output:
                    return ParameterDirection.Output;
                case SQLQueryParameterValueDirection.Result:
                    return ParameterDirection.ReturnValue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pr.Direction));
            }
        }
    }

    public class SQLQueryCommandReader : IEnumerable<object>
    {
        private readonly SQLQueryCompiled _queryCompiled;
        private readonly string _connectionString;

        public SQLQueryCommandReader(string connectionString, SQLQueryCompiled queryCompiled)
        {
            _queryCompiled = queryCompiled;
            _connectionString = connectionString;
        }

        public IEnumerator<object> GetEnumerator()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                IList<object> result;
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var parameters = new DynamicParameters();

                        if (_queryCompiled.Parameters.Count > 0)
                        {
                            foreach (var pr in _queryCompiled.Parameters)
                            {
                                ParameterDirection direction = GetDirection(pr);
                                parameters.Add(pr.Name, pr.Value, direction: direction);
                            }
                        }

                        result = connection.Query(_queryCompiled.Statement, parameters, transaction).ToList();

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

        private ParameterDirection GetDirection(SQLQueryParameter pr)
        {
            switch (pr.Direction)
            {
                case SQLQueryParameterValueDirection.Input:
                    return ParameterDirection.Input;
                case SQLQueryParameterValueDirection.Output:
                    return ParameterDirection.Output;
                case SQLQueryParameterValueDirection.Result:
                    return ParameterDirection.ReturnValue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pr.Direction));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SQLQueryCommandReader<T> : IEnumerable<T>
    {
        private readonly SQLQueryCompiled _queryCompiled;
        private readonly string _connectionString;

        public SQLQueryCommandReader(string connectionString, SQLQueryCompiled queryCompiled)
        {
            _queryCompiled = queryCompiled;
            _connectionString = connectionString;
        }

        public IEnumerator<T> GetEnumerator()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                IList<T> result;
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var parameters = new DynamicParameters();

                        if (_queryCompiled.Parameters.Count > 0)
                        {
                            foreach (var pr in _queryCompiled.Parameters)
                            {
                                ParameterDirection direction = GetDirection(pr);
                                parameters.Add(pr.Name, pr.Value, direction: direction);
                            }
                        }

                        result = connection.Query<T>(_queryCompiled.Statement, parameters, transaction).ToList();

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

        private ParameterDirection GetDirection(SQLQueryParameter pr)
        {
            switch (pr.Direction)
            {
                case SQLQueryParameterValueDirection.Input:
                    return ParameterDirection.Input;
                case SQLQueryParameterValueDirection.Output:
                    return ParameterDirection.Output;
                case SQLQueryParameterValueDirection.Result:
                    return ParameterDirection.ReturnValue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pr.Direction));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SQLQueryMultipleResultSetsCommandReader : IEnumerable<object>
    {
        private readonly SQLQueryCompiled _queryCompiled;
        private readonly string _connectionString;

        public SQLQueryMultipleResultSetsCommandReader(string connectionString, SQLQueryCompiled queryCompiled)
        {
            _queryCompiled = queryCompiled;
            _connectionString = connectionString;
        }

        public IEnumerator<object> GetEnumerator()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
            connectionStringBuilder.MultipleActiveResultSets = true;

            using (var connection = new SqlConnection(connectionStringBuilder.ToString()))
            {
                connection.Open();

                var results = new List<IEnumerable<object>>();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var parameters = new DynamicParameters();

                        if (_queryCompiled.Parameters.Count > 0)
                        {
                            foreach (var pr in _queryCompiled.Parameters)
                            {
                                ParameterDirection direction = GetDirection(pr);
                                parameters.Add(pr.Name, pr.Value, direction: direction);
                            }
                        }

                        var multipleResult = connection.QueryMultiple(_queryCompiled.Statement, parameters, transaction);
                        while(!multipleResult.IsConsumed) 
                        {
                            results.Add(multipleResult.Read().ToList());
                        }
                        
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                foreach (var result in results)
                {
                    foreach (var item in result)
                    {
                        yield return item;
                    }
                }
            }
        }

        private ParameterDirection GetDirection(SQLQueryParameter pr)
        {
            switch (pr.Direction)
            {
                case SQLQueryParameterValueDirection.Input:
                    return ParameterDirection.Input;
                case SQLQueryParameterValueDirection.Output:
                    return ParameterDirection.Output;
                case SQLQueryParameterValueDirection.Result:
                    return ParameterDirection.ReturnValue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pr.Direction));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
