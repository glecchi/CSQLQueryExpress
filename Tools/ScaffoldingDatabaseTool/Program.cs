using SQLQueryBuilder.Scaffolding;
using System.Configuration;

var connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
var outputFolder = ConfigurationManager.AppSettings["OutputFolder"];
var overwriteExistingDataModelClasses = bool.Parse(ConfigurationManager.AppSettings["OverwriteExistingDataModelClasses"]);
var dataModelClassNamespace = ConfigurationManager.AppSettings["DataModelClassNamespace"];
var decorateWithDatabaseAttribute = bool.Parse(ConfigurationManager.AppSettings["DecorateWithDatabaseAttribute"]);

var scaffoldingParameters = new SQLDataModelCodeGeneratorParameters(
        connectionString,
        outputFolder,
        dataModelClassNamespace,
        SQLDataModelCodeGeneratorEntityType.Table | SQLDataModelCodeGeneratorEntityType.View | SQLDataModelCodeGeneratorEntityType.StoredProcedure,
        overwriteExistingDataModelClasses, 
        decorateWithDatabaseAttribute);

var dataModelCodeGen = new SQLDataModelCodeGenerator(scaffoldingParameters);

dataModelCodeGen.GenerateDataModel();

