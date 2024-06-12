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

        [Test]
        public void TestCastExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(c => c.CategoryID == 0)
                 .Select(c => c.ProductID.Cast<string>(System.Data.SqlDbType.VarChar.Max()));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT CAST(_t0.[ProductID] AS VARCHAR(MAX)) FROM [dbo].[Products] AS _t0 WHERE (_t0.[CategoryID] = @p0)"));
        }

        [Test]
        public void TestConvertExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(c => c.CategoryID == 0)
                 .Select(c => c.ProductID.Convert<string>(System.Data.SqlDbType.VarChar.Size(50)));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT CONVERT(VARCHAR(50), _t0.[ProductID]) FROM [dbo].[Products] AS _t0 WHERE (_t0.[CategoryID] = @p0)"));
        }

        [Test]
        public void TestUnicodeExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(c => c.CategoryID == 0)
                 .Select(c => c.ProductID.Convert<string>(System.Data.SqlDbType.VarChar.Size(50)).Unicode());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT UNICODE(CONVERT(VARCHAR(50), _t0.[ProductID])) FROM [dbo].[Products] AS _t0 WHERE (_t0.[CategoryID] = @p0)"));
        }

        [Test]
        public void TestAsciiExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(c => c.CategoryID == 0)
                 .Select(c => c.ProductID.Convert<string>(System.Data.SqlDbType.VarChar.Size(50)).Ascii());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT ASCII(CONVERT(VARCHAR(50), _t0.[ProductID])) FROM [dbo].[Products] AS _t0 WHERE (_t0.[CategoryID] = @p0)"));
        }

        [Test]
        public void TestCollateExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(c => c.ProductName.Collate("Latin1_General_CS_AS") == "XXX")
                 .Select(c => c.ProductID);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.[ProductID] FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductName] COLLATE Latin1_General_CS_AS = @p0)"));
        }
    }
}