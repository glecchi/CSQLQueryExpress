using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using SQLQueryBuilder;
using SQLQueryBuilder.Extensions;
using SQLQueryBuilder.Fragments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(2)]
    internal class OutputInsertNewShipper : SQLOutputQueryCommand<dbo.Shippers>
    {
        public OutputInsertNewShipper(ISQLQueryCommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected override SQLQueryOutput<dbo.Shippers> GetQueryOutput()
        {
            var newShipper = new dbo.Shippers();
            newShipper.CompanyName = "World Trust";
            newShipper.Phone = "999-999-000";

            var query = new SQLQuery()
                .Insert(newShipper)
                .Output(s => s.All().Inserted());

            return query;
        }
    }
}
