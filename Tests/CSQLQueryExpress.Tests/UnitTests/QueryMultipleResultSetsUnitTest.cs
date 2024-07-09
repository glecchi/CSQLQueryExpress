using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using System.Runtime.CompilerServices;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryMultipleResultSetsUnitTest : UnitTestBase
    {
        [Test]
        public void TestMultipleResultSets()
        {
            var queryRegion = new SQLQuery()
                .From<dbo.Region>()
                .Select(r => r.All());

            var queryCategories = new SQLQuery()
                .From<dbo.Categories>()
                .Select(c => c.All());

            var queryShippers = new SQLQuery()
                .From<dbo.Shippers>()
                .Select(s => s.All());

            var query = new SQLQuery()
                .MultipleResultSets(queryRegion, queryCategories, queryShippers);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        protected string GetSQLStatement([CallerMemberName] string memeberName = null)
        {
            var statement = File.ReadAllText($@"UnitTests\MultipleResultSetsStatements\{memeberName}.txt");

            return statement;
        }
    }
}
