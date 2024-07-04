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
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT SUM(_t0.[Quantity]) FROM [dbo].[Order Details] AS _t0 WHERE (_t0.[OrderID] = @p0)"));
        }

        [Test]
        public void TestAvgExpression()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.UnitPrice.Avg());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT AVG(_t0.[UnitPrice]) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestAvgDistinctExpression()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.UnitPrice.AvgDistinct());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT AVG(DISTINCT _t0.[UnitPrice]) FROM [dbo].[Products] AS _t0"));
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
            Assert.That(compiledQuery.Parameters[0].Value, Is.TypeOf<DateTime>());
            Assert.That(compiledQuery.Parameters[1].Value, Is.TypeOf<DateTime>());
            Assert.That(compiledQuery.Parameters[2].Value, Is.EqualTo(-1));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Orders] AS _t0 WHERE (SIGN(DATEDIFF(DAY, ISNULL(_t0.[OrderDate], @p0), @p1)) = @p2)"));
        }

        [Test]
        public void TestCountExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Orders>()
                 .Select(c => c.OrderID.Count());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT COUNT(_t0.[OrderID]) FROM [dbo].[Orders] AS _t0"));
        }

        [Test]
        public void TestCountDistinctExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Order_Details>()
                 .Select(c => c.OrderID.CountDistinct());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT COUNT(DISTINCT _t0.[OrderID]) FROM [dbo].[Order Details] AS _t0"));
        }

        [Test]
        public void TestMaxExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Order_Details>()
                 .Select(c => c.Quantity.Max());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT MAX(_t0.[Quantity]) FROM [dbo].[Order Details] AS _t0"));
        }

        [Test]
        public void TestMinExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Order_Details>()
                 .Select(c => c.Quantity.Min());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT MIN(_t0.[Quantity]) FROM [dbo].[Order Details] AS _t0"));
        }

        [Test]
        public void TestLenExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductName.Len().Max());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT MAX(LEN(_t0.[ProductName])) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestLeftExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductName.Left(5));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(5));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT LEFT(_t0.[ProductName], @p0) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestRightExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductName.Right(5));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(5));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT RIGHT(_t0.[ProductName], @p0) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestSubstringExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductName.Substring(5));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(5));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT LEFT(_t0.[ProductName], @p0) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestSubstringOffsetExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductName.Substring(5, 5));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(5));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(5));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT SUBSTRING(_t0.[ProductName], @p0, @p1) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestTrimStartExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductName.TrimStart());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT LTRIM(_t0.[ProductName]) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestTrimEndExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductName.TrimEnd());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT RTRIM(_t0.[ProductName]) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestTrimExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductName.Trim());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT TRIM(_t0.[ProductName]) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestUpperExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductName.ToUpper());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT UPPER(_t0.[ProductName]) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestLowerExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductName.ToLower());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT LOWER(_t0.[ProductName]) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestReplaceExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductName.Replace("A", "X"));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo("A"));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo("X"));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT REPLACE(_t0.[ProductName], @p0, @p1) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestIndexOfExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(c => c.ProductName.IndexOf("A"));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo("A"));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT CHARINDEX(@p0, _t0.[ProductName]) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestConcatExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .InnerJoin<dbo.Categories>((p, c) => p.CategoryID == c.CategoryID)
                 .Select((p, c) => string.Concat(c.CategoryName, " - ", p.ProductName));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(" - "));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT CONCAT(_t0.[CategoryName], @p0, _t1.[ProductName]) FROM [dbo].[Products] AS _t1 INNER JOIN [dbo].[Categories] AS _t0 ON (_t1.[CategoryID] = _t0.[CategoryID])"));
        }
    }
}