using QueryExecution.Dal.NorthwindPubs;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QuerySelectIntoUnitTest : UnitTestBase
    {
        [Test]
        public void TestTruncate()
        {
            var whereCompanyName = "World Trust";

            var query = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == whereCompanyName)
                .Select(s => s.All())
                .Into<ShippersBackup>();

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(whereCompanyName));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"SELECT _t0.* INTO [dbo].[ShippersBackup] FROM [dbo].[Shippers] AS _t0 WHERE (_t0.[CompanyName] = @p0)"));
        }

        [Table("ShippersBackup", Schema = "dbo")]
        class ShippersBackup : dbo.Shippers
        {

        }
    }
}
