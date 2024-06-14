using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    internal class QueryMathematicalUnitTest : UnitTestBase
    {
        [Test]
        public void TestAbsExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ReorderLevel.Abs());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT ABS(_t0.[ReorderLevel]) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestCeilingExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ReorderLevel.Ceiling());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT CEILING(_t0.[ReorderLevel]) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestFloorExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ReorderLevel.Floor());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT FLOOR(_t0.[ReorderLevel]) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestRountExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.UnitPrice.Round(1));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT ROUND(_t0.[UnitPrice], @p0) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestSqrtExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.UnitPrice.Sqrt());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT SQRT(_t0.[UnitPrice]) FROM [dbo].[Products] AS _t0"));
        }
    }
}
