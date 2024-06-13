using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryConditionUnitTest : UnitTestBase
    {
        [Test]
        public void TestIsNullExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => p.ProductID.IsNull())
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductID] IS NULL)"));
        }

        [Test]
        public void TestIsNotNullExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => p.ProductID.IsNotNull())
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductID] IS NOT NULL)"));
        }

        [Test]
        public void TestInExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => p.ProductID.In(new List<int> { 1, 2, 3 }))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(3));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductID] IN (@p0, @p1, @p2))"));
        }

        [Test]
        public void TestNotInExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => p.ProductID.NotIn(new List<int> { 1, 2, 3 }))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(3));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductID] NOT IN (@p0, @p1, @p2))"));
        }

        [Test]
        public void TestIsNullWithReplacementExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(p => p.ProductID.IsNull(0));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT ISNULL(_t0.[ProductID], @p0) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestCaseWhenExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(p => Case.When(() => p.ProductID.IsNotNull()).Then(() => p.ProductID).Else(() => 0));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT CASE WHEN (_t0.[ProductID] IS NOT NULL) THEN _t0.[ProductID] ELSE @p0 END FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestCaseMultipleWhenExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(p => Case.When(() => p.CategoryID == 1).Then(() => "CATEGORIA 1").When(() => p.CategoryID == 2).Then(() => "CATEGORIA 2").Else(() => "CATETORIA N"));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(5));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT CASE WHEN (_t0.[CategoryID] = @p0) THEN @p1 WHEN (_t0.[CategoryID] = @p2) THEN @p3 ELSE @p4 END FROM [dbo].[Products] AS _t0"));
        }
    }
}