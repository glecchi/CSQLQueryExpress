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
    [SQLQueryCommand(6)]
    internal class OuputUpdateNewShipper : SQLOutputQueryCommand<dbo.Shippers>
    {
        public OuputUpdateNewShipper(ISQLQueryCommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected override SQLQueryOutput<dbo.Shippers> GetQueryOutput()
        {
            var newShipper = new dbo.Shippers();
            newShipper.CompanyName = "World Trust";
            newShipper.Phone = "999-999-000";

            var query = new SQLQuery()
                .From<dbo.Shippers>()
                .Where(s => s.CompanyName == "World Trust")
                .Update(s => s.Phone.Set(newShipper.Phone))
                .Top(1)
                .Output(s => s.All().Deleted());

            return query;
        }
    }
}
