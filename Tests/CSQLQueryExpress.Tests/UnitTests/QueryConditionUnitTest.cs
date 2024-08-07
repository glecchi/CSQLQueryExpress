﻿using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryConditionUnitTest : UnitTestBase
    {
        [Test]
        public void TestIsNullExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => p.ProductID.IsNull())
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductID] IS NULL)"));
        }

        [Test]
        public void TestIsNullNegationExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => !p.ProductID.IsNull())
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductID] IS NOT NULL)"));
        }

        [Test]
        public void TestIsNotNullExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => p.ProductID.IsNotNull())
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductID] IS NOT NULL)"));
        }

        [Test]
        public void TestIsNotNullNegationExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => !p.ProductID.IsNotNull())
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductID] IS NULL)"));
        }

        [Test]
        public void TestImplicitBooleanExpression()
        {
            var query = new SQLQuery()
                 .From<ShippedOrders>()
                 .Where(o => (!o.Shipped.HasValue || (o.Shipped.HasValue && (o.Shipped.Value || !o.Shipped.Value))) && (o.Prepared || !o.Prepared))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(4));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(true));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(false));
            Assert.That(compiledQuery.Parameters[2].Value, Is.EqualTo(true));
            Assert.That(compiledQuery.Parameters[3].Value, Is.EqualTo(false));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Orders] AS _t0 WHERE (((_t0.[Shipped] IS NULL) OR ((_t0.[Shipped] IS NOT NULL) AND ((_t0.[Shipped] = @p0) OR (_t0.[Shipped] = @p1)))) AND ((_t0.[Prepared] = @p2) OR (_t0.[Prepared] = @p3)))"));
        }

        class ShippedOrders : dbo.Orders
        {
            public bool Prepared { get; set; }

            public bool? Shipped { get; set; }
        }

        [Test]
        public void TestInExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => p.ProductID.In(new List<int> { 1, 2, 3 }))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(3));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[2].Value, Is.EqualTo(3));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductID] IN (@p0, @p1, @p2))"));
        }

        [Test]
        public void TestInNegationExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => !p.ProductID.In(new List<int> { 1, 2, 3 }))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(3));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[2].Value, Is.EqualTo(3));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductID] NOT IN (@p0, @p1, @p2))"));
        }

        [Test]
        public void TestNotInExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => p.ProductID.NotIn(new List<int> { 1, 2, 3 }))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(3));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[2].Value, Is.EqualTo(3));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductID] NOT IN (@p0, @p1, @p2))"));
        }

        [Test]
        public void TestNotInNegationExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => !p.ProductID.NotIn(new List<int> { 1, 2, 3 }))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(3));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[2].Value, Is.EqualTo(3));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductID] IN (@p0, @p1, @p2))"));
        }

        [Test]
        public void TestIsNullWithReplacementExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(p => p.ProductID.IsNull(0));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT ISNULL(_t0.[ProductID], @p0) FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestCaseWhenExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(p => Case.When(p.ProductID.IsNotNull()).Then(p.ProductID).Else(0));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT CASE WHEN (_t0.[ProductID] IS NOT NULL) THEN _t0.[ProductID] ELSE @p0 END FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestCaseMultipleWhenExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Select(p => Case.When(p.CategoryID == 1).Then("CATEGORIA 1").When(p.CategoryID == 2).Then("CATEGORIA 2").Else("CATEGORIA N"));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(5));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo("CATEGORIA 1"));
            Assert.That(compiledQuery.Parameters[2].Value, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[3].Value, Is.EqualTo("CATEGORIA 2"));
            Assert.That(compiledQuery.Parameters[4].Value, Is.EqualTo("CATEGORIA N"));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT CASE WHEN (_t0.[CategoryID] = @p0) THEN @p1 WHEN (_t0.[CategoryID] = @p2) THEN @p3 ELSE @p4 END FROM [dbo].[Products] AS _t0"));
        }

        [Test]
        public void TestStartsWithExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => p.ProductName.StartsWith("A"))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo("A%"));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductName] LIKE @p0)"));
        }

        [Test]
        public void TestNotStartsWithExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => !p.ProductName.StartsWith("A"))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo("A%"));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductName] NOT LIKE @p0)"));
        }

        [Test]
        public void TestEndsWithExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => p.ProductName.EndsWith("A"))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo("%A"));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductName] LIKE @p0)"));
        }

        [Test]
        public void TestNotEndsWithExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => !p.ProductName.EndsWith("A"))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo("%A"));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductName] NOT LIKE @p0)"));
        }

        [Test]
        public void TestContainsExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => p.ProductName.Contains("A"))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo("%A%"));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductName] LIKE @p0)"));
        }

        [Test]
        public void TestNotContainsExpression()
        {
            var query = new SQLQuery()
                 .From<dbo.Products>()
                 .Where(p => !p.ProductName.Contains("A"))
                 .Select(p => p.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo("%A%"));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [dbo].[Products] AS _t0 WHERE (_t0.[ProductName] NOT LIKE @p0)"));
        }

        [Test]
        public void TestExistsExpression()
        {
            var query1 = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.ProductID);

            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.Exists(query1))
                .Select(p => p.ProductName, p => p.UnitPrice);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
               Is.EqualTo(@"SELECT _t0.[ProductName], _t0.[UnitPrice] FROM [dbo].[Products] AS _t0 WHERE EXISTS (SELECT _t0.[ProductID] FROM [dbo].[Products] AS _t0)"));
        }

        [Test]
        public void TestExistsNegationExpression()
        {
            var query1 = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.ProductID);

            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => !p.Exists(query1))
                .Select(p => p.ProductName, p => p.UnitPrice);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
               Is.EqualTo(@"SELECT _t0.[ProductName], _t0.[UnitPrice] FROM [dbo].[Products] AS _t0 WHERE NOT EXISTS (SELECT _t0.[ProductID] FROM [dbo].[Products] AS _t0)"));
        }

        [Test]
        public void TestNotExistsExpression()
        {
            var query1 = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.ProductID);

            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.NotExists(query1))
                .Select(p => p.ProductName, p => p.UnitPrice);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
               Is.EqualTo(@"SELECT _t0.[ProductName], _t0.[UnitPrice] FROM [dbo].[Products] AS _t0 WHERE NOT EXISTS (SELECT _t0.[ProductID] FROM [dbo].[Products] AS _t0)"));
        }

        [Test]
        public void TestNotExistsNegationExpression()
        {
            var query1 = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.ProductID);

            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => !p.NotExists(query1))
                .Select(p => p.ProductName, p => p.UnitPrice);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
               Is.EqualTo(@"SELECT _t0.[ProductName], _t0.[UnitPrice] FROM [dbo].[Products] AS _t0 WHERE EXISTS (SELECT _t0.[ProductID] FROM [dbo].[Products] AS _t0)"));
        }

        [Test]
        public void TestBetweenExpression()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.CategoryID.Between(1, 10))
                .Select(p => p.ProductName, p => p.UnitPrice);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(10));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
               Is.EqualTo(@"SELECT _t0.[ProductName], _t0.[UnitPrice] FROM [dbo].[Products] AS _t0 WHERE _t0.[CategoryID] BETWEEN @p0 AND @p1"));
        }

        [Test]
        public void TestBetweenNegationExpression()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => !p.CategoryID.Between(1, 10))
                .Select(p => p.ProductName, p => p.UnitPrice);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(10));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
               Is.EqualTo(@"SELECT _t0.[ProductName], _t0.[UnitPrice] FROM [dbo].[Products] AS _t0 WHERE _t0.[CategoryID] NOT BETWEEN @p0 AND @p1"));
        }

        [Test]
        public void TestNotBetweenExpression()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.CategoryID.NotBetween(1, 10))
                .Select(p => p.ProductName, p => p.UnitPrice);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(10));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
               Is.EqualTo(@"SELECT _t0.[ProductName], _t0.[UnitPrice] FROM [dbo].[Products] AS _t0 WHERE _t0.[CategoryID] NOT BETWEEN @p0 AND @p1"));
        }

        [Test]
        public void TestNotBetweenNegationExpression()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => !p.CategoryID.NotBetween(1, 10))
                .Select(p => p.ProductName, p => p.UnitPrice);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(10));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
               Is.EqualTo(@"SELECT _t0.[ProductName], _t0.[UnitPrice] FROM [dbo].[Products] AS _t0 WHERE _t0.[CategoryID] BETWEEN @p0 AND @p1"));
        }

        [Test]
        public void TestAppLockExpression()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => AppLock.Test("public", string.Concat("Prod", p.ProductID.Cast<string>(System.Data.SqlDbType.VarChar.Size(50))), AppLockMode.Exclusive, AppLockOwner.Transaction) == 1)
                .Select(p => p.ProductName, p => p.UnitPrice)
                .Top(10);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(3));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo("public"));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo("Prod"));
            Assert.That(compiledQuery.Parameters[2].Value, Is.EqualTo(1));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
               Is.EqualTo(@"SELECT TOP(10) _t0.[ProductName], _t0.[UnitPrice] FROM [dbo].[Products] AS _t0 WHERE (APPLOCK_TEST(@p0, CONCAT(@p1, CAST(_t0.[ProductID] AS VARCHAR(50))), 'Exclusive', 'Transaction') = @p2)"));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }
    }
}