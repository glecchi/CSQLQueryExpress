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

        [Test]
        public void TestInsertedExpression()
        {
            var query = new SQLQuery()
                .Insert(new dbo.Products())
                .Output(p => p.ProductID.Inserted());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.GreaterThan(0));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty), 
                Is.EqualTo(
                @"INSERT INTO [dbo].[Products] (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued) OUTPUT INSERTED.[ProductID] VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)"));
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
                Is.EqualTo(@"DELETE  FROM [dbo].[Products] OUTPUT DELETED.[ProductID]"));
        }
    }
}