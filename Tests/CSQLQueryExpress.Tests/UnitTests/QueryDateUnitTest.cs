﻿using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using System.Runtime.CompilerServices;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryDateUnitTest : UnitTestBase
    {
        [Test]
        public void TestDatePartWithDateNullable()
        {
            var query = new SQLQuery()
                .From<dbo.Orders>()
                .Where(o => o.OrderDate.IsNotNull())
                .GroupBy(
                    o => o.OrderDate.Value.Year,
                    o => o.OrderDate.Value.Month)
                .Having(o => Count.All() > 0)
                .OrderBy(
                    o => o.OrderDate.Value.Year.Desc(),
                    o => o.OrderDate.Value.Month.Asc())
                .Select<TestDatePartResult>(
                    (o, res) => o.OrderDate.Value.Year.As(res.Year),
                    (o, res) => o.OrderDate.Value.Month.As(res.Month),
                    (o, res) => Count.All().As(res.Count));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(0));

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());

        }

        [Test]
        public void TestDatePartWithDateNotNullable()
        {
            var query = new SQLQuery()
                .From<OrdersCompleted>()
                .GroupBy(
                    o => o.CompletedDate.Year,
                    o => o.CompletedDate.Month)
                .Having(o => Count.All() > 0)
                .OrderBy(
                    o => o.CompletedDate.Year.Desc(),
                    o => o.CompletedDate.Month.Asc())
                .Select<TestDatePartResult>(
                    (o, res) => o.CompletedDate.Year.As(res.Year),
                    (o, res) => o.CompletedDate.Month.As(res.Month),
                    (o, res) => Count.All().As(res.Count));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(0));

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));
        }

        [Test]
        public void TestDateDiffWithDateNullable()
        {
            var query = new SQLQuery()
                 .From<dbo.Orders>()
                 .Select(o => DateTime.Now.Subtract(o.OrderDate.Value).Days);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.TypeOf<DateTime>());
            
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT DATEDIFF(DAY, _t0.[OrderDate], @p0) FROM [dbo].[Orders] AS _t0"));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());

        }

        [Test]
        public void TestDateDiffFromDateNullable()
        {
            var query = new SQLQuery()
                 .From<dbo.Orders>()
                 .Select(o => o.OrderDate.Value.Subtract(DateTime.Now).Days);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.TypeOf<DateTime>());

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT DATEDIFF(DAY, @p0, _t0.[OrderDate]) FROM [dbo].[Orders] AS _t0"));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());

        }

        [Test]
        public void TestDateDiffWithDateNotNullable()
        {
            var query = new SQLQuery()
                .From<OrdersCompleted>()
                .Select(o => DateTime.Now.Subtract(o.CompletedDate).Days);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.TypeOf<DateTime>());

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT DATEDIFF(DAY, _t0.[CompletedDate], @p0) FROM [dbo].[Orders] AS _t0"));
        }

        [Test]
        public void TestDateDiffFromDateNotNullable()
        {
            var query = new SQLQuery()
                .From<OrdersCompleted>()
                .Select(o => o.CompletedDate.Subtract(DateTime.Now).Days);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.TypeOf<DateTime>());

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT DATEDIFF(DAY, @p0, _t0.[CompletedDate]) FROM [dbo].[Orders] AS _t0"));
        }

        [Test]
        public void TestDateDiffFromDates()
        {
            var query = new SQLQuery()
                .From<dbo.Orders>()
                .Where(o => o.OrderDate.IsNotNull() && o.ShippedDate.IsNotNull())
                .OrderBy(o => o.OrderDate)
                .Select(
                    o => o.OrderID,
                    o => o.OrderDate,
                    o => o.ShippedDate,
                    o => o.ShippedDate.Value.Subtract(o.OrderDate.Value).Days);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.[OrderID], _t0.[OrderDate], _t0.[ShippedDate], DATEDIFF(DAY, _t0.[OrderDate], _t0.[ShippedDate]) FROM [dbo].[Orders] AS _t0 WHERE ((_t0.[OrderDate] IS NOT NULL) AND (_t0.[ShippedDate] IS NOT NULL)) ORDER BY _t0.[OrderDate]"));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestDateSubqueryAvgFromDates()
        {
            var subQuery = new SQLQuery()
                .From<dbo.Orders>()
                .Where(o => o.OrderDate.IsNotNull() && o.ShippedDate.IsNotNull())
                .Select(o => o.ShippedDate.Value.Subtract(o.OrderDate.Value).Days.Avg());

            var query = new SQLQuery()
                .From<dbo.Orders>()
                .Where(o => o.OrderDate.IsNotNull() && o.ShippedDate.IsNotNull() && o.ShippedDate.Value.Subtract(o.OrderDate.Value).Days > subQuery)
                .OrderBy(o => o.OrderDate)
                .Select(
                    o => o.OrderID,
                    o => o.OrderDate,
                    o => o.ShippedDate,
                    o => o.ShippedDate.Value.Subtract(o.OrderDate.Value).Days);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestDateDiffWithDataPartFromDates()
        {
            var query = new SQLQuery()
                .From<dbo.Orders>()
                .Where(o => o.OrderDate.IsNotNull() && o.ShippedDate.IsNotNull())
                .Select(
                    o => o.OrderID,
                    o => o.OrderDate,
                    o => o.ShippedDate,
                    o => DateTime.Now.AddDays(-o.ShippedDate.Value.Subtract(o.OrderDate.Value).Days));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.TypeOf<DateTime>());

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        class OrdersCompleted : dbo.Orders
        {
            public DateTime CompletedDate { get; set; }
        }

        class TestDatePartResult
        {
            public int Year { get; set; }

            public int Month { get; set; }

            public int Count { get; set; }
        }

        protected string GetSQLStatement([CallerMemberName] string memeberName = null)
        {
            var statement = File.ReadAllText($@"UnitTests\DatePartStatements\{memeberName}.txt");

            return statement;
        }
    }
}
