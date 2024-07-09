using CSQLQueryExpress.Fragments;
using QueryExecution.Dal.NorthwindPubs;
using System.Runtime.CompilerServices;

namespace CSQLQueryExpress.Tests.UnitTests
{
    [TestFixture]
    public class QueryBatchUnitTest : UnitTestBase
    {
        [Test]
        public void TestBatchInsert()
        {
            var lastIDQuery = new SQLQuery()
                .From<dbo.Region>()
                .Select(s => s.RegionID.Max());

            var insertQueries = new List<SQLQueryInsert<dbo.Region>>();

            for (int i = 0; i < 5; i++)
            {
                var updateRegionDescription = $"World Region {i + 1}";

                var query = new SQLQuery()
                    .From<dbo.Region>()
                    .Insert(s => s.RegionID.Set(lastIDQuery + 1), s => s.RegionDescription.Set(updateRegionDescription));

                var compiledQuery = query.Compile();

                Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
                Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(1));
                Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(updateRegionDescription));

                insertQueries.Add(query);
            }

            var batchQuery = new SQLQuery()
                .Batch(insertQueries.ToArray());

            var compiledBatchQuery = batchQuery.Compile();

            Assert.That(compiledBatchQuery.Parameters.Count, Is.EqualTo(10));
            for (int i = 0; i < 5; i++)
            {
                Assert.That(compiledBatchQuery.Parameters[2 * i].Value, Is.EqualTo(1));
                Assert.That(compiledBatchQuery.Parameters[(2 * i) + 1].Value, Does.StartWith("World Region"));
            }

            var statement = GetSQLStatement();

            Assert.That(compiledBatchQuery.Statement, Is.EqualTo(statement));
        }

        [Test]
        public void TestBatchUpdate()
        {
            var updateQueries = new List<SQLQueryUpdate<dbo.Region>>();

            for (int i = 0; i < 5; i++)
            {
                var regionID = i+1;

                var updateRegionDescription = $"World Region {regionID + 1}";

                var query = new SQLQuery()
                    .From<dbo.Region>()
                    .Where(r => r.RegionID == regionID)
                    .Update(s => s.RegionDescription.Set(updateRegionDescription));

                var compiledQuery = query.Compile();

                Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(2));
                Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(updateRegionDescription));
                Assert.That(compiledQuery.Parameters[1].Value, Is.EqualTo(regionID));

                updateQueries.Add(query);
            }

            var batchQuery = new SQLQuery()
                .Batch(updateQueries.ToArray());

            var compiledBatchQuery = batchQuery.Compile();

            Assert.That(compiledBatchQuery.Parameters.Count, Is.EqualTo(10));
            for (int i = 0; i < 5; i++)
            {
                var regionID = i+1;

                Assert.That(compiledBatchQuery.Parameters[2 * i].Value, Does.StartWith("World Region"));
                Assert.That(compiledBatchQuery.Parameters[(2 * i) + 1].Value, Is.EqualTo(regionID));
            }

            var statement = GetSQLStatement();

            Assert.That(compiledBatchQuery.Statement, Is.EqualTo(statement));
        }

        [Test]
        public void TestBatchDelete()
        {
            var deleteQueries = new List<SQLQueryDelete<dbo.Region>>();

            for (int i = 0; i < 5; i++)
            {
                var regionID = i + 1;

                var updateRegionDescription = $"World Region {regionID + 1}";

                var query = new SQLQuery()
                    .From<dbo.Region>()
                    .Where(r => r.RegionID == regionID)
                    .Delete();

                var compiledQuery = query.Compile();

                Assert.That(compiledQuery.Parameters.Count, Is.EqualTo(1));
                Assert.That(compiledQuery.Parameters[0].Value, Is.EqualTo(regionID));

                deleteQueries.Add(query);
            }

            var batchQuery = new SQLQuery()
                .Batch(deleteQueries.ToArray());

            var compiledBatchQuery = batchQuery.Compile();

            Assert.That(compiledBatchQuery.Parameters.Count, Is.EqualTo(5));
            for (int i = 0; i < 5; i++)
            {
                var regionID = i + 1;

                Assert.That(compiledBatchQuery.Parameters[i].Value, Is.EqualTo(regionID));
            }

            var statement = GetSQLStatement();

            Assert.That(compiledBatchQuery.Statement, Is.EqualTo(statement));
        }

        protected string GetSQLStatement([CallerMemberName] string memeberName = null)
        {
            var statement = File.ReadAllText($@"UnitTests\BatchStatements\{memeberName}.txt");

            return statement;
        }
    }
}
