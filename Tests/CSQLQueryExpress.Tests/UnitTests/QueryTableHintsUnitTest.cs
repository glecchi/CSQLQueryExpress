using QueryExecution.Dal.NorthwindPubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryTableHintsUnitTest
    {
        [Test]
        public void TestTableHints()
        {
            var cte = new SQLQuery()
                .From<OrdersToProcess>().With(TableHints.NOLOCK | TableHints.READPAST)
                .Where(o => o.Processing == false)
                .Select(o => o.All())
                .ToCteTable();

            var query = new SQLQuery()
                .From(cte)
                .Update(o => o.Processing.Set(true))
                .Output(o => o.All());

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(false));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(true));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"WITH _t0 AS (SELECT _t0.* FROM [dbo].[Orders] AS _t0 WITH (NOLOCK, READPAST) WHERE (_t0.[Processing] = @p0)) UPDATE _t0 SET _t0.[Processing] = @p1 OUTPUT _t0.* FROM _t0"));
        }

        class OrdersToProcess : dbo.Orders
        {
            public bool Processing { get; set; }
        }
    }
}
