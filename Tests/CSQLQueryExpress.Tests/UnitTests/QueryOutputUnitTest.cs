using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryOutputUnitTest : UnitTestBase
    {
        [Test]
        public void TestInsertedExpression()
        {
            var query = new SQLQuery()
                .Insert(new dbo.Products())
                .Output(p => p.ProductID.Inserted());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(9));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(
                @"INSERT INTO [dbo].[Products] ([ProductName], [SupplierID], [CategoryID], [QuantityPerUnit], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued]) OUTPUT INSERTED.[ProductID] VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)"));
        }

        [Test]
        public void TestDeletedExpression()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Delete()
                .Output(p => p.ProductID.Deleted());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"DELETE FROM [dbo].[Products] OUTPUT DELETED.[ProductID]"));
        }
    }
}