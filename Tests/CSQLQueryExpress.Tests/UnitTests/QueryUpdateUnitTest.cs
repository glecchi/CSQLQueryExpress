using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryUpdateUnitTest : UnitTestBase
    {
        [Test]
        public void TestUpdateObject()
        {
            var whereCompanyName = "World Trust";

            var updateShipper = new dbo.Shippers();
            updateShipper.CompanyName = "World Trust";
            updateShipper.Phone = "999-999-000";

            var query = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == whereCompanyName)
                .Update(updateShipper)
                .Top(1);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(3));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(updateShipper.CompanyName));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(updateShipper.Phone));
            Assert.That(compiledQuery.Parameters[2].Value, Is.EqualTo(whereCompanyName));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"UPDATE TOP(1) [dbo].[Shippers] SET [CompanyName] = @p0, [Phone] = @p1 WHERE ([CompanyName] = @p2)"));
        }

        [Test]
        public void TestUpdateValues()
        {
            var updateShipper = new Dictionary<string, object>
            {
                { "CompanyName", "World Trust" },
                { "Phone", "999-999-000" }
            };

            var whereCompanyName = "World Trust";

            var query = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == whereCompanyName)
                .Update(updateShipper)
                .Top(1);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(3));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(updateShipper["CompanyName"]));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(updateShipper["Phone"]));
            Assert.That(compiledQuery.Parameters[2].Value, Is.EqualTo(whereCompanyName));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"UPDATE TOP(1) [dbo].[Shippers] SET [CompanyName] = @p0, [Phone] = @p1 WHERE ([CompanyName] = @p2)"));
        }

        [Test]
        public void TestUpdateSelecParameters()
        {
            var whereCompanyName = "World Trust";

            var subQuery = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == whereCompanyName)
                .Select(s => s.All())
                .Top(1);

            var updateShipper = new dbo.Shippers();
            updateShipper.CompanyName = "World Trust";
            updateShipper.Phone = "999-999-000";

            var query = new SQLQuery()
                .From(subQuery)
                .Update(s => s.CompanyName.Set(updateShipper.CompanyName), s => s.Phone.Set(updateShipper.Phone));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(3));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(updateShipper.CompanyName));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(updateShipper.Phone));
            Assert.That(compiledQuery.Parameters[2].Value, Is.EqualTo(whereCompanyName));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"UPDATE _t0 SET _t0.[CompanyName] = @p0, _t0.[Phone] = @p1 FROM (SELECT TOP(1) _t0.* FROM [dbo].[Shippers] AS _t0 WHERE (_t0.[CompanyName] = @p2)) AS _t0"));
        }

        [Test]
        public void TestUpdateParameters()
        {
            var whereCompanyName = "World Trust";

            var updateShipper = new dbo.Shippers();
            updateShipper.CompanyName = "World Trust";
            updateShipper.Phone = "999-999-000";

            var query = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == whereCompanyName)
                .Update(s => s.CompanyName.Set(updateShipper.CompanyName), s => s.Phone.Set(updateShipper.Phone))
                .Top(1);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(3));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(updateShipper.CompanyName));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(updateShipper.Phone));
            Assert.That(compiledQuery.Parameters[2].Value, Is.EqualTo(whereCompanyName));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"UPDATE TOP(1) [dbo].[Shippers] SET [CompanyName] = @p0, [Phone] = @p1 WHERE ([CompanyName] = @p2)"));
        }
    }
}
