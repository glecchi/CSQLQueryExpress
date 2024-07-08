using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryAssignmentUnitTest : UnitTestBase
    {
        [Test]
        public void TestSetExpression()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Update(p => p.ProductID.Set(1));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"UPDATE [dbo].[Products] SET [ProductID] = @p0"));
        }

        [Test]
        public void TestSetByteArrayExpression()
        {
            var image = Enumerable.Range(0, 100).Select(i => Convert.ToByte(i)).ToArray();

            var query = new SQLQuery()
               .From<dbo.Categories>()
               .Update(p => p.Picture.Set(image));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.TypeOf<byte[]>());

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"UPDATE [dbo].[Categories] SET [Picture] = @p0"));
        }

        [Test]
        public void TestAssignExpression()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Update(p => p.ProductID.Set(p.ProductID + 1));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"UPDATE [dbo].[Products] SET [ProductID] = ([ProductID] + @p0)"));
        }

        [Test]
        public void TestSetQueryExpression()
        {
            var queryAssign = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.CategoryID == 1)
                .Select(p => p.ProductID)
                .Top(1);

            var query = new SQLQuery()
                .From<dbo.Products>()
                .Update(p => p.ProductID.Set(queryAssign))
                .Top(1);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"UPDATE TOP(1) [dbo].[Products] SET [ProductID] = (SELECT TOP(1) [ProductID] FROM [dbo].[Products] AS _t0 WHERE ([CategoryID] = @p0))"));
        }
    }
}