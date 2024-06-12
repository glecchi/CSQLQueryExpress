using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryConversionUnitTest : UnitTestBase
    {
        [Test]
        public void TestCompressExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Categories>()
                 .Where(c => c.CategoryID == 0)
                 .Select(c => c.Picture.Compress());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT COMPRESS(_t0.[Picture]) FROM [dbo].[Categories] AS _t0 WHERE (_t0.[CategoryID] = @p0)"));
        }

        [Test]
        public void TestDecompressExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Categories>()
                 .Where(c => c.CategoryID == 0)
                 .Select(c => c.Picture.Decompress());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT DECOMPRESS(_t0.[Picture]) FROM [dbo].[Categories] AS _t0 WHERE (_t0.[CategoryID] = @p0)"));
        }
    }
}