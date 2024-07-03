using CSQLQueryExpress.Fragments;
using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using System.Runtime.CompilerServices;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QuerySQLStatementsUnitTest : UnitTestBase
    {
        [Test]
        public void TestStatement1()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .InnerJoin<dbo.Categories>((p, c) => p.CategoryID == c.CategoryID)
                .OrderBy((p, c) => c.CategoryName, (p, c) => p.ProductName)
                .Select((p, c) => c.CategoryID,
                        (p, c) => c.CategoryName,
                        (p, c) => c.Description,
                        (p, c) => p.ProductID,
                        (p, c) => p.ProductName);

            var compiledQuery = query.Compile();

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestStatement2()
        {
            var cte = new SQLQuery()
                .From<dbo.Shippers>()
                .InnerJoin<dbo.Orders>((sh, ord) => sh.ShipperID == ord.ShipVia)
                .InnerJoin<dbo.Order_Details>((sh, ord, ordDet) => ord.OrderID == ordDet.OrderID)
                .InnerJoin<dbo.Products>((sh, ord, ordDet, prod) => ordDet.ProductID == prod.ProductID)
                .GroupBy(
                    (sh, ord, ordDet, prod) => sh.CompanyName,
                    (sh, ord, ordDet, prod) => prod.ProductName)
                .Select<TestStatement2Result>(
                    (sh, ord, ordDet, prod, res) => sh.CompanyName,
                    (sh, ord, ordDet, prod, res) => prod.ProductName.IsNull("UNKNOWN").As(res.ProductName),
                    (sh, ord, ordDet, prod, res) => Count.All().As(res.ProductCount),
                    (sh, ord, ordDet, prod, res) => Row.Number().Over(n => n.PartitionBy(() => sh.CompanyName).OrderBy(() => Count.All().Desc())).As(res.RowNumber))
                .ToCteTable();

            var query = new SQLQuery()
                .From(cte)
                .Where(c => c.RowNumber <= 10)
                .OrderBy(c => c.RowNumber.Asc(), c => c.CompanyName.Asc())
                .Select(c => c.All());

            var compiledQuery = query.Compile();

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        class TestStatement2Result : ISQLQueryEntity
        {
            public string CompanyName { get; set; }

            public string ProductName { get; set; }

            public int ProductCount { get; set; }

            public int RowNumber { get; set; }
        }

        [Test]
        public void TestStatement3()
        {
            var cte = new SQLQuery()
                .From<dbo.Customers>()
                .InnerJoin<dbo.Orders>((cus, ord) => cus.CustomerID == ord.CustomerID)
                .InnerJoin<dbo.Order_Details>((cus, ord, ordDet) => ord.OrderID == ordDet.OrderID)
                .InnerJoin<dbo.Products>((cus, ord, ordDet, prod) => prod.ProductID == ordDet.ProductID)
                .GroupBy(
                    (cus, ord, ordDet, prod) => cus.Country,
                    (cus, ord, ordDet, prod) => cus.ContactName,
                    (cus, ord, ordDet, prod) => prod.ProductName)
                .Having((cus, ord, ordDet, prod) => Count.All() > 3)
                .Select<TestStatement3Result>(
                    (cus, ord, ordDet, prod, res) => cus.Country,
                    (cus, ord, ordDet, prod, res) => cus.ContactName,
                    (cus, ord, ordDet, prod, res) => prod.ProductName,
                    (cus, ord, ordDet, prod, res) => Count.All().As(res.Orders),
                    (cus, ord, ordDet, prod, res) => Row.Number().Over(o => o.OrderBy(() => Count.All().Desc())).As(res.Row))
                .ToCteTable();

            var query = new SQLQuery()
                .From(cte)
                .OrderBy(c => c.Country, c => c.Row, c => c.ProductName)
                .Select(c => c.All());

            var compiledQuery = query.Compile();

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        class TestStatement3Result : ISQLQueryEntity
        {
            public string Country { get; set; }

            public string ProductName { get; set; }

            public string ContactName { get; set; }

            public int Orders { get; set; }

            public int Row { get; set; }
        }

        [Test]
        public void TestStatement4()
        {
            var categories = new SQLQuery()
                .From<dbo.Categories>()
                .Where(c => c.CategoryID.In(new List<int> { 1, 2, 3 }))
                .Select(c => c.CategoryID, c => c.CategoryName);

            var products1 = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.CategoryID == 1)
                .OrderBy(p => p.UnitPrice.IsNull(0).Asc())
                .Select(p => p.ProductID, p => p.ProductName);

            var products2 = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.CategoryID == 2)
                .OrderBy(p => p.UnitPrice.IsNull(0).Asc())
                .Select(p => p.ProductID, p => p.ProductName);

            var products3 = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.CategoryID == 3)
                .OrderBy(p => p.UnitPrice.IsNull(0).Asc())
                .Select(p => p.ProductID, p => p.ProductName);

            var suppliers = new SQLQuery()
                .From<dbo.Suppliers>()
                .Select(p => p.SupplierID, p => p.CompanyName, p => p.ContactName);

            var query = new SQLQuery()
                .MultipleResultSets(categories, products1, products2, products3, suppliers);

            var compiledQuery = query.Compile();

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryMultipleResultSetsCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestStatement5()
        {
            var query = new SQLQuery()
               .StoredProcedure(new dbo.Proc_CustOrderHist { CustomerID = "ALFKI" });

            var compiledQuery = query.Compile();

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestStatement6()
        {            
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.CategoryID, p => p.UnitPrice.Avg().Over(o => o.PartitionBy(() => p.CategoryID)))
                .Distinct();

            var compiledQuery = query.Compile();

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestStatement7()
        {
            var query1 = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.UnitPrice.Avg());

            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.UnitPrice > query1)
                .Select(p => p.ProductName, p => p.UnitPrice);

            var compiledQuery = query.Compile();

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestStatement8()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.ProductID, p => p.ProductName)
                .ForXml(x => x.Path("ProductData").Root("Root"));

            var compiledQuery = query.Compile();

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestStatement9()
        {
            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.CategoryID.Between(1, 100))
                .OrderBy(p => p.CategoryID.Asc())
                .Select(p => p.ProductName, p => p.UnitPrice);

            var compiledQuery = query.Compile();

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestStatement10()
        {
            var query1 = new SQLQuery()
                .From<dbo.Products>()
                .Select(p => p.ProductID);

            var query = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.Exists(query1))
                .Select(p => p.ProductName, p => p.UnitPrice);

            var compiledQuery = query.Compile();

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestStatement11()
        {
            var query = new SQLQuery()
                .From<dbo.Products>().With(WithOptions.READPAST)
                .InnerJoin<dbo.Categories>((p, c) => p.CategoryID == c.CategoryID).With(WithOptions.READPAST)
                .Select((p, c) => c.CategoryName, (p, c) => p.ProductName);

            var compiledQuery = query.Compile();

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestStatement12()
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

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        [Test]
        public void TestStatement13()
        {
            var queryCrApply = new SQLQuery()
                .From<dbo.Products>()
                .Where(p => p.UnitsInStock > 100)
                .OrderBy(p => p.ProductName.Desc())
                .Select(c => c.All())
                .Top(1);

            var query = new SQLQuery()
                .From<dbo.Categories>()
                .OuterApply(queryCrApply, (c, p) => p.CategoryID == c.CategoryID)
                .Select((c, p) => c.CategoryName, (c, p) => p.ProductName);

            var compiledQuery = query.Compile();

            var statement = GetSQLStatement();

            Assert.That(compiledQuery.Statement, Is.EqualTo(statement));

            var queryCommand = new SQLQueryCommandReader(ConnectionString, compiledQuery);

            Assert.DoesNotThrow(() => queryCommand.ToList());
        }

        private string GetSQLStatement([CallerMemberName] string memeberName = null)
        {
            var statement = File.ReadAllText($@"UnitTests\SQLStatements\{memeberName}.txt");

            return statement;
        }
    }
}
