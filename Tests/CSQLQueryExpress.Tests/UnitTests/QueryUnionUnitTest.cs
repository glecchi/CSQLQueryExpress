using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryUnionUnitTest : UnitTestBase
    {
        [Test]
        public void TestUnion()
        {
            var queryAvg = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(p => p.UnitPrice.Avg());

            var queryFirst = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.UnitPrice < queryAvg)
                .Select(p => p.ProductName, p => p.UnitPrice);

            var querySecond = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.UnitPrice > queryAvg)
                .Select(p => p.ProductName, p => p.UnitPrice);

            var query = new SQLQuery()
                .Union(queryFirst, querySecond);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestSelectFromUnion()
        {
            var queryAvg = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(p => p.UnitPrice.Avg());

            var queryFirst = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.UnitPrice < queryAvg)
                .Select(p => p.All());

            var querySecond = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.UnitPrice > queryAvg)
                .Select(p => p.All());

            var queryUnion = new SQLQuery()
                .Union(queryFirst, querySecond);

            var querySelectUnion = new SQLQuery()
                .From(queryUnion)
                .Select(u => u.All());

            var query = new SQLQuery()
                .From<dbo.Order_Details>()
                .InnerJoin(querySelectUnion, (odt, u) => odt.ProductID == u.ProductID)
                .GroupBy((odt, u) => odt.OrderID)
                .Select((odt, u) => odt.OrderID, (odt, u) => odt.Quantity.Sum());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        protected string GetSQLStatement([CallerMemberName] string memeberName = null)
        {
            var statement = File.ReadAllText($@"UnitTests\UnionStatements\{memeberName}.txt");

            return statement;
        }
    }
}
