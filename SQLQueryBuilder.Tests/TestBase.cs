using SQLQueryBuilder.Dapper.CommandFactory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQueryBuilder.Tests
{
    [TestFixture]
    public class TestBase
    {
        [SetUp]
        public void Initialize()
        {
            //SQLQueryCommandFactory.SetConnectionString("...");
        }
    }
}
