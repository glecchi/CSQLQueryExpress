using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QuerySelectionUnitTest : UnitTestBase
    {
        [Test]
        public void TestAllExpression()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty), Is.EqualTo(
                @"SELECT _t0.* FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestAsExpression()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.ProductID.As(p.ProductID));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty), Is.EqualTo(
                @"SELECT _t0.[ProductID] AS [ProductID] FROM [dbo].[Products] AS _t0"));
        }
    }
}