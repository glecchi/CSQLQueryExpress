using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using System.Runtime.CompilerServices;

namespace CSQLQueryExpress.Tests.UnitTests
{

    [TestFixture]
    public class QueryJoinUnitTest : UnitTestBase
    {
        /// <summary>
        /// The table alias is based on the type of the data model class. To join a table with itself requires a different data model class that maps the same table.
        /// </summary>
        [Test]
        public void TestJoinBeetweenTheSameTable()
        {
            var query = new SQLQuery()
                .From<dbo.Shippers>()
                .InnerJoin<ShipperJoined>((s1, s2) => s1.ShipperID == s2.ShipperID)
                .Select((s1, s2) => s1.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Shippers] AS _t0 INNER JOIN [dbo].[Shippers] AS _t1 ON (_t0.[ShipperID] = _t1.[ShipperID])"));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        /// <summary>
        /// The table alias is based on the type of the data model class. To join a table with itself requires a different data model class that maps the same table.
        /// </summary>
        [Test]
        public void TestJoinBeetweenTheSameTableBySubquery()
        {
            var subQuery = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == "XXX")
                .Select<ShipperJoined>((s, res) => s.All());

            var query = new SQLQuery()
                .From<dbo.Shippers>()
                .InnerJoin(subQuery, (s1, s2) => s1.ShipperID == s2.ShipperID)
                .Select((s1, s2) => s1.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo("XXX"));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Shippers] AS _t0 INNER JOIN (SELECT _t0.* FROM [dbo].[Shippers] AS _t0 WHERE (_t0.[CompanyName] = @p0)) AS _t1 ON (_t0.[ShipperID] = _t1.[ShipperID])"));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        class ShipperJoined : dbo.Shippers
        {

        }

        [Test]
        public void TestInnerJoin()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .InnerJoin<dbo.Categories>((p, c) => p.CategoryID == c.CategoryID)
                .Select((p, c) => c.CategoryName, (p, c) => p.ProductName);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestCrossApply()
        {
            var queryCrApply = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.UnitsInStock > 20)
                .OrderBy(p => p.ProductName.Desc())
                .Select(c => c.All())
                .Top(1);

            var query = new SQLQuery()
                .From<dbo.Categories>()
                .CrossApply(queryCrApply, (c, p) => p.CategoryID == c.CategoryID)
                .Select((c, p) => c.CategoryName, (c, p) => p.ProductName);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(20));

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestOuterApply()
        {
            var queryOtrApply = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.UnitsInStock > 100)
                .OrderBy(p => p.ProductName.Desc())
                .Select(c => c.All())
                .Top(1);

            var query = new SQLQuery()
                .From<dbo.Categories>()
                .OuterApply(queryOtrApply, (c, p) => p.CategoryID == c.CategoryID)
                .Select((c, p) => c.CategoryName, (c, p) => p.ProductName);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(100));

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestLeftOuterJoin()
        {
            var query = new SQLQuery()
                .From<dbo.Categories>()
                .LeftOuterJoin<dbo.Products>((c, p) => c.CategoryID == p.CategoryID)
                .Where((c, p) => p.ProductID.IsNotNull())
                .Select((c, p) => c.CategoryName, (c, p) => p.ProductName);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestRightOuterJoin()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .RightOuterJoin<dbo.Categories>((p, c) => c.CategoryID == p.CategoryID)
                .Where((p, c) => p.ProductID.IsNotNull())
                .Select((p, c) => c.CategoryName, (p, c) => p.ProductName);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestJoinHints()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .InnerJoin<dbo.Categories>((p, c) => c.CategoryID == p.CategoryID).With(JoinHints.LOOP)
                .Select((p, c) => c.CategoryName, (p, c) => p.ProductName);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.[CategoryName], _t1.[ProductName] FROM [dbo].[Products] AS _t1 INNER LOOP JOIN [dbo].[Categories] AS _t0 ON (_t0.[CategoryID] = _t1.[CategoryID])"));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        private string GetSQLStatement([CallerMemberName] string memeberName = null)
        {
            var statement = File.ReadAllText($@"UnitTests\JoinStatements\{memeberName}.txt");

            return statement;
        }
    }
}
