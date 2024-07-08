using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryTruncateUnitTest : UnitTestBase
    {
        [Test]
        public void TestTruncate()
        {
            var query = new SQLQuery()
                .Truncate<dbo.Shippers>();

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));
            
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"TRUNCATE TABLE [dbo].[Shippers]"));
        }
    }
}
