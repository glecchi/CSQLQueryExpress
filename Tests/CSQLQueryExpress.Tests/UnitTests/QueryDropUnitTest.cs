using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{

    [TestFixture]
    public class QueryDropUnitTest : UnitTestBase
    {
        [Test]
        public void TestDrop()
        {
            var query = new SQLQuery()
                .Drop<dbo.Shippers>();

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"DROP TABLE [dbo].[Shippers]"));
        }
    }
}
