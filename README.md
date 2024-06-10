# SQLQueryBuilder

**SQLQueryBuilder** is a C# library designed to compile TSQL code, providing developers with the utmost flexibility to write expressions in C# that closely resemble TSQL syntax.  
This library bridges the gap between C# and TSQL, enabling integration of TSQL commands directly within a C# environment.  
Note that while SQLQueryBuilder handles the compilation of TSQL code, the execution is delegated to any ORM such as Dapper.

In addition to compiling TSQL code, SQLQueryBuilder offers a scaffolding tool and a test client for writing and executing queries.

## Please note that this library is in Beta and intended exclusively for use in test environments.

## Key Features

- **TSQL Syntax Familiarity:** Write C# code that mirrors TSQL syntax, making it easier for developers familiar with TSQL.
- **Dynamic Code Compilation:** Compile TSQL expressions dynamically within your C# applications, enhancing flexibility and reducing the need for pre-defined static queries.
- **Comprehensive TSQL Support:** Support for a wide range of TSQL commands and expressions, ensuring compatibility with various database operations and scenarios.
- **Seamless Integration:** Easily integrate SQLQueryBuilder into your existing C# projects with minimal configuration, enabling rapid development and deployment.
- **Documentation:** Examples to help you get started quickly and make the most of SQLQueryBuilder's capabilities.
- **Database Scaffolding Tool:** Automatically generate database schema and compile the corresponding data model in C#, simplifying database integration and development.
- **Test Client:** A dedicated test client for writing and executing queries using Dapper, facilitating quick and easy query testing and validation.

## Usage Scenarios

- **Database Query Generation:** Dynamically generate complex TSQL queries from within your C# applications, and execute them using an ORM such as Dapper.
- **Data Migration and Transformation:** Simplify data migration and transformation tasks by leveraging familiar TSQL commands in your C# code.
- **Report Generation:** Create detailed reports by combining the power of TSQL and C# to extract, manipulate, and present data.
- **Database Scaffolding:** Automatically generate and compile database schemas into C# models, speeding up development workflows.
- **Query Testing:** Write and execute TSQL queries using the provided test client to ensure correctness and performance.

## Getting Started

### Usage

1. **Write TSQL Expressions:** Use SQLQueryBuilder's intuitive syntax to write TSQL expressions directly in your C# code.
2. **Compile:** Compile your TSQL code dynamically.
3. **Execute with an ORM:** Use an ORM like Dapper to execute the compiled TSQL code.
4. **Use Scaffolding Tool:** Automatically generate database schema and compile the data model.
5. **Test Queries:** Use the test client to write and execute your queries.

### Example

Here's a simple example to demonstrate how to use SQLQueryBuilder with Dapper:

```csharp
using SQLQueryBuilder;
using Dapper;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        var query = new SQLQuery()
          .From<dbo.Users>()
          .Where(u => u.Age > 30)
          .Select(u => u.All());

        var tSqlQuery = query.Compile();

        using (var connection = new SqlConnection("YourConnectionString"))
        {
            var result = connection.Query(tSqlQuery.Statement, tSqlQuery.Parameters);
            foreach (var user in result)
            {
                Console.WriteLine(user);
            }
        }
    }
}
```
## Setup

Within the solution, you will find:

- **ScaffoldingDatabaseTool:** This tool is designed for database scaffolding and data model compilation in C#.
  You need to configure the connection string in the "App.config" file of the project. Once the connection string is configured, simply run the tool, and the data model will be set up in the "QueryExecution.Dal" project, which is also      present in the solution.
- **QueryExecution.Dal:** This project already contains a precompiled data model based on Microsoft's NorthwindPubs sample database. 
- **Test Client:** The test client includes example queries for this data model and is configured to connect to the NorthwindPubs database on a localdb MSSQLServer instance. To execute the test queries, you need to create the NorthwindPubs database on your localdb MSSQLServer instance.  
  The scripts for creating the NorthwindPubs database can be found here:  
  https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs.
