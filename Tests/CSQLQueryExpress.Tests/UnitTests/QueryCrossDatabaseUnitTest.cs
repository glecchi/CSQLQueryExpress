using CSQLQueryExpress.Schema;
using QueryExecution.Dal.NorthwindPubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryCrossDatabaseUnitTest
    {
        /// <summary>
        /// NOTE: The databases involved in the query must be on the same SQLServer instance.
        /// </summary>
        [Test]
        public void TestCrossDatabaseQuery()
        {
            var query = new SQLQuery()
                .From<ProductsDbFirst>()
                .LeftOuterJoin<ProductsDbSecond>((p1, p2) => p1.ProductID == p2.ProductID)
                .Where((p1, p2) => p2.ProductID.IsNull())
                .Select((p1, p2) => p1.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(0));
            
            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* FROM [NorthwindPubsFirst].[dbo].[Products] AS _t0 LEFT OUTER JOIN [NorthwindPubsSecond].[dbo].[Products] AS _t1 ON (_t0.[ProductID] = _t1.[ProductID]) WHERE (_t1.[ProductID] IS NULL)"));
        }

        [Database("NorthwindPubsFirst")]
        class ProductsDbFirst : dbo.Products
        {

        }

        [Database("NorthwindPubsSecond")]
        class ProductsDbSecond : dbo.Products
        {

        }
    }
}
