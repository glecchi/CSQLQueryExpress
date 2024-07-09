using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSQLQueryExpress.Tests
{
    [TestFixture]
    public class UnitTestBase
    {
        protected string ConnectionString;

        [SetUp]
        public void Initialize()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["NorthwindPubs"].ConnectionString;
        }
    }
}
