using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryOperationUnitTest : UnitTestBase
    {
        [Test]
        public void TestSumExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Order_Details>()
                 .Where(p => p.OrderID == 1)
                 .Select(c => c.Quantity.Sum());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT SUM(_t0.[Quantity]) FROM [dbo].[Order Details] AS _t0 WHERE (_t0.[OrderID] = @p0)"));
        }

        [Test]
        public void TestSignExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Orders>()
                 .Where(p => DateTime.Now.Subtract(p.OrderDate.IsNull(DateTime.Now)).Days.Sign() == -1)
                 .Select(c => c.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(3));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Orders] AS _t0 WHERE (SIGN(DATEDIFF(DAY, ISNULL(_t0.[OrderDate], @p0), @p1)) = @p2)"));
        }
    }
}