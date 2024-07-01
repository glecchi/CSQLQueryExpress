# CSQLQueryExpress

**CSQLQueryExpress** is a C# [NuGet Library](https://www.nuget.org/packages/CSQLQueryExpress) designed to compile TSQL code, providing developers with the utmost flexibility to write expressions in C# that closely resemble TSQL syntax.  

Note that while CSQLQueryExpress handles the compilation of TSQL code, the execution is delegated to any ORM such as Dapper.

In addition to compiling TSQL code, CSQLQueryExpress offers a [CSQLQueryExpress.Scaffolding](https://www.nuget.org/packages/CSQLQueryExpress.Scaffolding) tool and a test client for writing and executing queries.

## Key Features

- **TSQL Syntax Familiarity:** Write C# code that mirrors TSQL syntax, making it easier for developers familiar with TSQL.
- **Dynamic Code Compilation:** Compile TSQL expressions dynamically within your C# applications, enhancing flexibility and reducing the need for pre-defined static queries.
- **Comprehensive TSQL Support:** Support for a wide range of TSQL commands and expressions, ensuring compatibility with various database operations and scenarios.
- **Seamless Integration:** Easily integrate CSQLQueryExpress into your existing C# projects with minimal configuration, enabling rapid development and deployment.
- **Documentation:** Examples to help you get started quickly and make the most of CSQLQueryExpress's capabilities.
- **Database Scaffolding Tool:** Automatically generate database schema and compile the corresponding data model in C#, simplifying database integration and development.
- **Test Client:** A dedicated test client for writing and executing queries using Dapper, facilitating quick and easy query testing and validation.

## Getting Started

### Compile and execute your queries

1. **Write TSQL Expressions:** Use CSQLQueryExpress's intuitive syntax to write TSQL expressions directly in your C# code.
2. **Compile:** Compile your TSQL code dynamically.
3. **Execute with an ORM:** Use an ORM like Dapper to execute the compiled TSQL code.

#### Example

Here's a simple example to demonstrate how to use CSQLQueryExpress with Dapper:

```csharp
using CSQLQueryExpress;
using Dapper;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        SQLQuerySelect<dbo.Users> query = new SQLQuery()
          .From<dbo.Users>()
          .Where(u => u.Age > 30)
          .Select(u => u.All());

        var tSqlQuery = query.Compile();

        var statement = tSqlQuery.Statement;
        var parameters = tSqlQuery.Parameters.ToDictionary(p => p.Name, p => p.Value);

        using (var connection = new SqlConnection("YourConnectionString"))
        {
            connection.Open();

            var result = connection.Query<dbo.Users>(statement, parameters);
            foreach (var user in result)
            {
                Console.WriteLine(user);
            }
        }
    }
}
```

### Generate data model from your database schema

#### Example

Set your database connection string in app.config file of ScaffoldingDatabaseTool and execute program:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<connectionStrings>
		<add name="DBConnectionString" connectionString="YOUR DATABASE CONNECTION STRING" />
	</connectionStrings>
	<appSettings>
		<add key="OverwriteExistingDataModelClasses" value="true"/>
		<add key="DecorateWithDatabaseAttribute" value="false"/>
		<add key="DataModelClassNamespace" value="QueryExecution.Dal"/>
		<add key="OutputFolder" value="..\..\..\..\..\QueryExecution.Dal\Dal"/>
	</appSettings>
</configuration>
```
**You will find your database's data model in the Query Execution.Dal project in a folder named after your database.**
   
### Use TestClient to write and execute your queries

#### Example

Set your database connection string in app.config file of QueryExecution.TestClient.  

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<connectionStrings>
		<add name="NorthwindPubs" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=NorthwindPubs;Integrated Security=SSPI;" />
		<add name="Your_Database_Name" connectionString="..."/>
	</connectionStrings>
</configuration>
```
**The connection name must match the name of the folder in the project that will contain your queries.**

#### Example of a query written for the TestClient  

```csharp
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;
using QueryExecution.Dal.NorthwindPubs;
using CSQLQueryExpress.Statements;
using CSQLQueryExpress.Schema;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    internal class SelectMostShippedProductsByShipper : SQLSelectQueryCommand<ShippedProductsByShipper>
    {
        public SelectMostShippedProductsByShipper(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
        {
        }

        protected override SQLQuerySelect<ShippedProductsByShipper> GetQuerySelect()
        {
            SQLQuerySelect<ShippedProductsByShipper> cte = new SQLQuery()
                .From<dbo.Shippers>()
                .InnerJoin<dbo.Orders>((sh, ord) => sh.ShipperID == ord.ShipVia)
                .InnerJoin<dbo.Order_Details>((sh, ord, ordDet) => ord.OrderID == ordDet.OrderID)
                .InnerJoin<dbo.Products>((sh, ord, ordDet, prod) => ordDet.ProductID == prod.ProductID)
                .GroupBy(
                    (sh, ord, ordDet, prod) => sh.CompanyName,
                    (sh, ord, ordDet, prod) => prod.ProductName)
                .Select<ShippedProductsByShipper>(
                    (sh, ord, ordDet, prod, res) => sh.CompanyName,
                    (sh, ord, ordDet, prod, res) => prod.ProductName,
                    (sh, ord, ordDet, prod, res) => Count.All().As(res.ProductCount),
                    (sh, ord, ordDet, prod, res) => Row.Number()
                        .Over(n => n.PartitionBy(() => sh.CompanyName).OrderBy(() => Count.All().Desc()))
                        .As(res.RowNumber))
                .ToCteTable();

            SQLQuerySelect<ShippedProductsByShipper> query = new SQLQuery()
                .From(cte)
                .Where(c => c.RowNumber <= 10)
                .OrderBy(c => c.RowNumber.Asc(), c => c.CompanyName.Asc())
                .Select(c => c.All());

            return query;
        }
    }

    class ShippedProductsByShipper : ISQLQueryEntity
    {
        public string CompanyName { get; set; }

        public string ProductName { get; set; }

        public int ProductCount { get; set; }

        public int RowNumber { get; set; }
    }
}
```

## Setup

**Test Client:** The test client includes example queries for this data model and is configured to connect to the NorthwindPubs database on a localdb MSSQLServer instance. To execute the test queries, you need to create the NorthwindPubs database on your localdb MSSQLServer instance. 
  The scripts for creating the NorthwindPubs database can be found here:  
  https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs.

## Note

**Compile and verify generated statements from the library:** Compile and test the TSQL statements generated by the library. The library translates the query you write respecting the expressions used. Some expressions can be used in different contexts, the library is not able to discern the intention of the writer, therefore the control both in the writing and verification phases lies with the developer.
