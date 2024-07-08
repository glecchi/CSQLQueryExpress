using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryStoreProcedureUnitTest : UnitTestBase
    {
        [Test]
        public void TestStoreProcedure()
        {
            var storeProcedure = new dbo.Proc_CustOrderHist { CustomerID = "ALFKI" };

            var query = new SQLQuery()
                .StoredProcedure(storeProcedure);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(storeProcedure.CustomerID));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"EXECUTE [dbo].[CustOrderHist] @CustomerID"));
        }

        [Test]
        public void TestStoreProcedureResultEnabled()
        {
            var storeProcedure = new dbo.Proc_CustOrderHist { CustomerID = "ALFKI" };

            var query = new SQLQuery()
                .StoredProcedure(storeProcedure)
                .AddResultParameter();

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(storeProcedure.CustomerID));
            Assert.That(compiledQuery.Parameters[1].Name, Is.EqualTo("@RC"));
            Assert.That(compiledQuery.Parameters[1].Value, Is.Null);

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"DECLARE @RC AS INT EXECUTE @RC = [dbo].[CustOrderHist] @CustomerID"));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());

            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(0));
        }
    }
}
