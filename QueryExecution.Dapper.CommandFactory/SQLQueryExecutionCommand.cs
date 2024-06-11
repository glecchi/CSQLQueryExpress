using System.Collections;
using System.Reflection.PortableExecutable;
using CSQLQueryExpress;

namespace QueryExecution.Dapper.CommandFactory
{
    public abstract class SQLQueryExecutionCommand : ISQLQueryCommand, IEnumerable<string>
    {
        private readonly ISQLQueryCommandFactory _commandFactory;

        protected SQLQueryExecutionCommand(ISQLQueryCommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        protected abstract int GetResult(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled);

        public IEnumerator<string> GetEnumerator()
        {
            var result = GetResult(_commandFactory, out SQLQueryCompiled queryCompiled);

            yield return "-----------------------------------------------------------------------------------------------------------------";

            yield return "Statement SQL:";

            yield return string.Empty;

            if (queryCompiled.Parameters.Count > 0)
            {
                foreach (var p in queryCompiled.Parameters)
                {
                    yield return $"DECLARE {p}";
                }

                yield return string.Empty;
            }

            yield return queryCompiled.Statement;

            yield return string.Empty;

            yield return "-----------------------------------------------------------------------------------------------------------------";

            yield return $"Result => {result}";

            yield return "-----------------------------------------------------------------------------------------------------------------";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
