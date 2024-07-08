using QueryExecution.Dal.NorthwindPubs;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryInsertUnitTest : UnitTestBase
    {
        [Test]
        public void TestInsertObject()
        {
            var newShipper = new dbo.Shippers();
            newShipper.CompanyName = "World Trust";
            newShipper.Phone = "999-999-000";

            var query = new SQLQuery()
                .Insert(newShipper);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(newShipper.CompanyName));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(newShipper.Phone));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"INSERT INTO [dbo].[Shippers] ([CompanyName], [Phone]) VALUES (@p0, @p1)"));
        }

        [Test]
        public void TestInsertValues()
        {
            var newShipper = new Dictionary<string, object>
            {
                { "CompanyName", "World Trust" },
                { "Phone", "999-999-000" }
            };

            var query = new SQLQuery()
                .From<dbo.Shippers>()
                .Insert(newShipper);

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(newShipper["CompanyName"]));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(newShipper["Phone"]));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"INSERT INTO [dbo].[Shippers] ([CompanyName], [Phone]) VALUES (@p0, @p1)"));
        }

        [Test]
        public void TestInsertSelect()
        {
            var whereCompanyName = "World Trust";

            var subQuery = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == whereCompanyName)
                .Select(s => s.CompanyName, s => s.Phone);

            var query = new SQLQuery()
                .From(subQuery)
                .Insert();

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(whereCompanyName));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"INSERT INTO [dbo].[Shippers] ([CompanyName], [Phone]) SELECT _t0.[CompanyName], _t0.[Phone] FROM [dbo].[Shippers] AS _t0 WHERE (_t0.[CompanyName] = @p0)"));
        }

        [Test]
        public void TestInsertParameters()
        {
            var lastIDQuery = new SQLQuery()
                .From<dbo.Region>()
                .Select(s => s.RegionID.Max());

            var updateRegionDescription = "World Region";

            var query = new SQLQuery()
                .From<dbo.Region>()
                .Insert(s => s.RegionID.Set(lastIDQuery + 1), s => s.RegionDescription.Set(updateRegionDescription));

            var compiledQuery = query.Compile();

            Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
            Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));
            Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(updateRegionDescription));

            Assert.That(compiledQuery.Statement.Replace(Environment.NewLine, string.Empty),
                Is.EqualTo(@"INSERT INTO [dbo].[Region] ([RegionID], [RegionDescription]) VALUES (((SELECT MAX(_t0.[RegionID]) FROM [dbo].[Region] AS _t0) + @p0), @p1)"));
        }
    }
}
