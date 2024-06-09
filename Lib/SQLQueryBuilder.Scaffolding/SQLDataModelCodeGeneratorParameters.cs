using System;

namespace SQLQueryBuilder.Scaffolding
{
    public sealed class SQLDataModelCodeGeneratorParameters
    {
        public const string DefaultScaffoldingTablesQuery = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";
        public const string DefaultScaffoldingProceduresQuery = "SELECT ROUTINE_SCHEMA, ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE'";
        public const string DefaultScaffoldingViewsQuery = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS";

        /// <summary>
        /// Class of parameters used to generate a data model from the database schema.
        /// </summary>
        /// <param name="databaseConnectionString">The connectionString to database.</param>
        /// <param name="outputRootFolder">The working folder where the .cs files are generated.</param>
        /// <param name="rootNamespace">The root namespace of the generated data model classes.</param>
        /// <param name="entityTypes">The types of entities to generate. (Default Table|View|StoredProcedure)</param>
        /// <param name="overwriteExistingDataModelClasses">True if the data model class exists, it is overridden. Otherwise False. (Default True)</param>
        /// <param name="decorateWithDatabaseAttribute">True data model classes will be decorated with DatabaseAttribute. Otherwise False. (Default False)</param>
        public SQLDataModelCodeGeneratorParameters(
            string databaseConnectionString,
            string outputRootFolder,
            string rootNamespace,
            SQLDataModelCodeGeneratorEntityType entityTypes = SQLDataModelCodeGeneratorEntityType.Table,
            bool overwriteExistingDataModelClasses = true,
            bool decorateWithDatabaseAttribute = false)
        {
            DatabaseConnectionString = databaseConnectionString;
            OutputRootFolder = outputRootFolder;
            RootNamespace = rootNamespace;
            EntityTypes = entityTypes;
            OverwriteExistingDataModelClasses = overwriteExistingDataModelClasses;
            DecorateWithDatabaseAttribute = decorateWithDatabaseAttribute;
        }

        /// <summary>
        /// The connectionString to database.
        /// </summary>
        public string DatabaseConnectionString { get; }

        /// <summary>
        /// The working folder where the .cs files are generated.
        /// </summary>
        public string OutputRootFolder { get; }

        /// <summary>
        /// True if the data model class exists, it is overridden. Otherwise False. (Default True)
        /// </summary>
        public bool OverwriteExistingDataModelClasses { get; }

        /// <summary>
        /// The root namespace of the generated data model classes.
        /// </summary>
        public string RootNamespace { get; }

        /// <summary>
        /// True data model classes will be decorated with DatabaseAttribute. Otherwise False. (Default False)
        /// </summary>
        public bool DecorateWithDatabaseAttribute { get; }

        /// <summary>
        /// The types of entities to generate. (Default Table|View|StoredProcedure)
        /// </summary>
        public SQLDataModelCodeGeneratorEntityType EntityTypes { get; }

        /// <summary>
        /// The query used to scaffold database tables. (Default <see cref="DefaultScaffoldingTablesQuery"/>)
        /// </summary>
        public string ScaffoldingTablesQuery { get; set; } = DefaultScaffoldingTablesQuery;

        /// <summary>
        /// The query used to scaffold database stored procedures. (Default <see cref="DefaultScaffoldingProceduresQuery"/>)
        /// </summary>
        public string ScaffoldingProceduresQuery { get; set; } = DefaultScaffoldingProceduresQuery;

        /// <summary>
        /// The query used to scaffold database views. (Default <see cref="DefaultScaffoldingViewsQuery"/>)
        /// </summary>
        public string ScaffoldingViewsQuery { get; set; } = DefaultScaffoldingViewsQuery;

        /// <summary>
        /// Clear recursive che <see cref="OutputRootFolder"/>. (Default False)
        /// </summary>
        public bool ClearFolder { get; set; }

        /// <summary>
        /// Generates data model classes as nested classes of their own schema subclass. (Default True)
        /// </summary>
        public bool GenerateSchemaNestedClasses { get; set; } = true;

        /// <summary>
        /// Generate subfolders for database schemas. (Default True)
        /// </summary>
        public bool GenerateSchemaFolder { get; set; } = true;

        /// <summary>
        /// Tables sub-folder. (Default NULL)
        /// </summary>
        public string TablesFolder { get; set; }

        /// <summary>
        /// Tables data model classes Prefix. (Default NULL)
        /// </summary>
        public string TablePrefix { get; set; }

        /// <summary>
        /// Tables data model classes Suffix. (Default NULL)
        /// </summary>
        public string TableSuffix { get; set; }

        /// <summary>
        /// Views data model classes sub-folder. (Default "Views")
        /// </summary>
        public string ViewsFolder { get; set; } = "Views";

        /// <summary>
        /// Views data model classes sub-namespace. (Default NULL)
        /// </summary>
        public string ViewsNamespace { get; set; }

        /// <summary>
        /// Views data model classes Prefix. (Default "View_")
        /// </summary>
        public string ViewPrefix { get; set; } = "View_";

        /// <summary>
        /// Views data model classes Suffix. (Default NULL)
        /// </summary>
        public string ViewSuffix { get; set; }

        /// <summary>
        /// Stored procedures sub-folder. (Default "StoredProcedures")
        /// </summary>
        public string StoredProcedureFolder { get; set; } = "StoredProcedure";

        /// <summary>
        /// Stored procedures data model classes sub-namespace. (Default NULL)
        /// </summary>
        public string StoredProcedurNamespace { get; set; }

        /// <summary>
        /// Stored procedures data model classes Prefix. (Default "Proc_")
        /// </summary>
        public string StoredProcedurePrefix { get; set; } = "Proc_";

        /// <summary>
        /// Stored procedures data model classes Suffix. (Default NULL)
        /// </summary>
        public string StoredProcedureSuffix { get; set; }

        private string _storedProcedureResultSuffix = "_Result";
        /// <summary>
        /// Stored procedures Result Data Model classes Suffix. (Default "_Result"). ATTENTION: Cannot be NULL or Empty.
        /// </summary>
        public string StoredProcedureResultSuffix 
        {
            get { return _storedProcedureResultSuffix; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(StoredProcedureResultSuffix));
                }

                _storedProcedureResultSuffix = value;
            }
        }
    }
}
