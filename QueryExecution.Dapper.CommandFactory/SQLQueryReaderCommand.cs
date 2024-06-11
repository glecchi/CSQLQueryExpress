using System.Collections;
using CSQLQueryExpress;

namespace QueryExecution.Dapper.CommandFactory
{
    public abstract class SQLQueryReaderCommand : ISQLQueryCommand, IEnumerable<string>
    {
        private readonly ISQLQueryCommandFactory _commandFactory;

        protected SQLQueryReaderCommand(ISQLQueryCommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        protected abstract IEnumerable GetReader(ISQLQueryCommandFactory commandFactory, out SQLQueryCompiled queryCompiled);

        public IEnumerator<string> GetEnumerator()
        {
            var reader = GetReader(_commandFactory, out SQLQueryCompiled queryCompiled);

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

            yield return string.Empty;

            yield return "Press ENTER to continue..";
            yield return "BREAK";

            yield return "Result:";
            yield return string.Empty;

            foreach (var row in reader)
            {
                yield return row.DumpJson();
            }

            yield return string.Empty;

            yield return "-----------------------------------------------------------------------------------------------------------------";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
