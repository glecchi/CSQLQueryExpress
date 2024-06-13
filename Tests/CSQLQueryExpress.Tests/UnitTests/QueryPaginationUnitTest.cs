using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryPaginationUnitTest
    {
        [Test]
        public void TestOffsetFetchExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Orders>()
                 .OrderBy(o => o.OrderID)
                 .Page(p => p.Offset(5).Fetch(5))
                 .Select(c => c.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Orders] AS _t0 ORDER BY _t0.[OrderID] OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY"));
        }

        [Test]
        public void TestOverPartitionByOrderByExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Order_Details>()
                 .Select(c => Row.Number().Over(o => o.PartitionBy(() => c.ProductID).OrderBy(() => c.OrderID.Asc())));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT ROW_NUMBER() OVER(PARTITION BY _t0.[ProductID] ORDER BY _t0.[OrderID] ASC) FROM [dbo].[Order Details] AS _t0"));
        }
    }
}
