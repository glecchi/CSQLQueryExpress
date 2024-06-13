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
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"UPDATE [dbo].[Products] SET [ProductID] = ([ProductID] + @p0)"));
        }
    }
}