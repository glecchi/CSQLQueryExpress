# CSQLQueryExpress.Scaffolding

**CSQLQueryExpress.Scaffolding** is a C# library designed to compile the data model for [CSQLQueryExpress](https://www.nuget.org/packages/CSQLQueryExpress) from the database schema.

### **Example**

Here's a simple example to demonstrate how to use CSQLQueryExpress.Scaffolding:

```csharp
using CSQLQueryExpress.Scaffolding;
using System.Configuration;

var connectionString = @"Data Source=...;Initial Catalog=...;Integrated Security=SSPI;";
var outputFolder = @"C:\Temp\...";
var overwriteExistingDataModelClasses = true;
var dataModelClassNamespace = ...;
var decorateWithDatabaseAttribute = false;

var codeGeneratorParameters = new SQLDataModelCodeGeneratorParameters(
    connectionString,
    outputFolder,
    dataModelClassNamespace,
    SQLDataModelCodeGeneratorEntityType.Table | SQLDataModelCodeGeneratorEntityType.View | SQLDataModelCodeGeneratorEntityType.StoredProcedure,
    overwriteExistingDataModelClasses, 
    decorateWithDatabaseAttribute);

var dataModelCodeGen = new SQLDataModelCodeGenerator(codeGeneratorParameters);

var result = dataModelCodeGen.GenerateDataModel();

Console.WriteLine($"Result: {(result.Successfully ? "Successfully" : "With errors")}");

if (result.Errors.Count > 0)
{
    Console.WriteLine("Errors:");
    
    foreach (var entity in result.Errors)
    {
        Console.WriteLine($"EntityType: {entity.Key}");

        foreach (var error in entity.Value)
        {
            Console.WriteLine($"    {error.EntityName} => {error.Error.Message}");
        }
    }
}

Console.ReadLine();
```
