using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryDeleteUnitTest : UnitTestBase
    {
        [Test]
        public void TestDelete()
        {
            var whereCompanyName = "World Trust";

            var query = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == whereCompanyName)
                .Delete()
                .Top(1);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(whereCompanyName));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"DELETE TOP(1) FROM [dbo].[Shippers] WHERE ([CompanyName] = @p0)"));
        }

        [Test]
        public void TestDeleteSubquery()
        {
            var whereCompanyName = "World Trust";

            var subQuery = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == whereCompanyName)
                .Select(s => s.All())
                .Top(1);

            var query = new SQLQuery()
                .From(subQuery)
                .Delete();

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(whereCompanyName));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"DELETE _t0 FROM (SELECT TOP(1) _t0.* FROM [dbo].[Shippers] AS _t0 WHERE (_t0.[CompanyName] = @p0)) AS _t0"));
        }
    }
}
