using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryDefinitionUnitTest : UnitTestBase
    {
        [Test]
        public void TestAscDescExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .OrderBy(p => p.SupplierID.Desc(), p => p.ProductID.Asc())
                 .Select(c => c.ProductID);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.[ProductID] FROM [dbo].[Products] AS _t0 ORDER BY _t0.[SupplierID] DESC, _t0.[ProductID] ASC"));
        }

        [Test]
        public void TestSizeExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductID.Convert<string>(System.Data.SqlDbType.VarChar.Size(50)));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT CONVERT(VARCHAR(50), _t0.[ProductID]) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestMaxExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductID.Convert<string>(System.Data.SqlDbType.VarChar.Max()));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT CONVERT(VARCHAR(MAX), _t0.[ProductID]) FROM [dbo].[Products] AS _t0"));
        }
    }
}