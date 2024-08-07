# CSQLQueryExpress

**CSQLQueryExpress** is a C# library designed to compile TSQL code, providing developers with the utmost flexibility to write expressions in C# that closely resemble TSQL syntax.  

Note that while CSQLQueryExpress handles the compilation of TSQL code, the execution is delegated to any ORM such as Dapper.

### **Example**

Here's a simple example to demonstrate how to use CSQLQueryExpress with Dapper:

```csharp
using CSQLQueryExpress;
using Dapper;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

class Program
{
    static void Main()
    {
        SQLQuerySelect<Users> query = 
            new SQLQuery()
                .From<Users>()
                .Where(u => u.Age.IsNotNull() && u.Age > 30)
                .Select(u => u.All());

        var tSqlQuery = query.Compile();

        var statement = tSqlQuery.Statement; //"SELECT _t0.* FROM [dbo].[Users] AS _t0 WHERE ((_t0.[Age] IS NOT NULL) AND (_t0.[Age] > @p0))"

        var parameters = tSqlQuery.ParametersKeyValue; //@p0 = 30

        using (var connection = new SqlConnection("YourConnectionString"))
        {
            connection.Open();

            var result = connection.Query<Users>(statement, parameters);
            foreach (var user in result)
            {
                Console.WriteLine($"UserID:{user.UserID} - FirstName:{user.FirstName} - LastName:{user.LastName} - Age:{user.Age}");
            }
        }
    }
}

[Table("Users", Schema = "dbo")]
public class Users : ISQLQueryEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("EmployeeID")]
    public int UserID { get; set; }
    
    [Required]
    [Column("FirstName")]
    public string FirstName { get; set; }
    
    [Required]
    [Column("LastName")]
    public string LastName { get; set; }
    	
    [Column("Age")]
    public int? Age { get; set; }
}
```

## TSQL

Some functions and statements supported by **CSQLQueryExpress**:  

### Scalar Functions:

1. **Mathematical Functions:**
    - `ABS`: Returns the absolute value of a number.
    - `CEILING`: Returns the smallest integer greater than or equal to a number.
    - `FLOOR`: Returns the largest integer less than or equal to a number.
    - `ROUND`: Rounds a number to a specified number of decimal places.
    - `SQRT`: Returns the square root of a number.  
   **** 
	
2. **String Functions:**
    - `LEN`: Returns the length of a string.
    - `LOWER`: Converts a string to lowercase.
    - `UPPER`: Converts a string to uppercase.
    - `SUBSTRING`: Extracts a portion of a string.
    - `REPLACE`: Replaces all occurrences of a substring within a string.
    - `LTRIM` / `RTRIM`: Removes leading or trailing spaces from a string.
    - `LEFT` / `RIGHT`: Returns characters from the left or right part of a string.  
   **** 
	
3. **Date and Time Functions:**
    - `DATEADD`: Adds an interval to a date.
    - `DATEDIFF`: Returns the difference between two dates.
    - `DATEPART`: Returns a specified part of a date as an integer.
    - `DATEFROMPARTS`: Returns a date from year, month, and day values.
    - `SYSDATETIME`: Returns a system datetime.
    - `TIMEFROMPARTS`: Returns a time value for the specified time and with the specified precision.
    - `EOMONTH`: Returns the last day of the month containing a specified date, with an optional offset.
    - `CONVERT`: Converts a date to a specified format.  
   **** 
	
4. **Logical Functions:**
    - `ISNULL`: Replaces NULL with a specified replacement value.  
   **** 
	
5. **Aggregate Functions:**
    - `SUM`: Calculates the sum of a set of values.
    - `AVG`: Calculates the average of a set of values.
    - `MIN` / `MAX`: Returns the minimum or maximum value in a set of values.
    - `COUNT`: Returns the number of values in a set.  
   **** 
	
### Statements and Constructs:

1. **`CTE`:** Common Table Expression.  

2. **`SUBQUERY`**  

3. **`STORED PROCEDURE`**  

4. **`VIEW`**  

5. **`EXISTS`:** Checks if a subquery returns any rows.  

6. **`BETWEEN`:** Selects values within a given range.  

7. **`FOR XML`:** Returns the query result as XML.  

8. **`CASE`:** Returns different values based on conditional logic.  

9. **`BATCH`:** A group of two or more SQL statements executed before any results.  

10. **`MULTIPLE RESULT SETS`:**  A feature that works with SQL Server to allow the execution of multiple batches on a single connection.  

11. **`TABLE HINTS`:** Used to override the default behavior of the query optimizer during the data manipulation language (DML) statement.  
	
12. **`JOIN HINTS`:** Specify that the query optimizer enforce a join strategy between two tables.  
 
13. **`OUTPUT` with `INSERTED` and `DELETED`:** Returns the rows affected by an `INSERT`, `UPDATE`, or `DELETE` statement.
    - `INSERTED`: Holds the new values being inserted or updated.
    - `DELETED`: Holds the old values being deleted or updated.  

**** 

## Do you have a comprehensive list of examples?

CSQLQueryExpress has a comprehensive test suite in the [test project](https://github.com/glecchi/CSQLQueryExpress/tree/main/Tests/CSQLQueryExpress.Tests).

## CSQLQueryExpress.Scaffolding

**CSQLQueryExpress.Scaffolding** is a C# [NuGet library](https://www.nuget.org/packages/CSQLQueryExpress.Scaffolding) designed to compile the data model for CSQLQueryExpress from the database schema.

## Note

**Compile and verify generated statements from the library:** Compile and test the TSQL statements generated by the library. The library translates the query you write respecting the expressions used. Some expressions can be used in different contexts, the library is not able to discern the intention of the writer, therefore the control both in the writing and verification phases lies with the developer.
