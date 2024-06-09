namespace SQLQueryBuilder.Scaffolding
{
    public sealed class SQLDataModelCodeGeneratorParameters
    {
        public const string DefaultScaffoldingTablesQuery = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";
        public const string DefaultScaffoldingProceduresQuery = "SELECT ROUTINE_SCHEMA, ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE'";
        public const string DefaultScaffoldingViewsQuery = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS";

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

        public string DatabaseConnectionString { get; }

        public string OutputRootFolder { get; }

        public bool OverwriteExistingDataModelClasses { get; }

        public string RootNamespace { get; }

        public bool DecorateWithDatabaseAttribute { get; } 

        public SQLDataModelCodeGeneratorEntityType EntityTypes { get; }

        public string ScaffoldingTablesQuery { get; set; } = DefaultScaffoldingTablesQuery;

        public string ScaffoldingProceduresQuery { get; set; } = DefaultScaffoldingProceduresQuery;

        public string ScaffoldingViewsQuery { get; set; } = DefaultScaffoldingViewsQuery;

        public bool ClearFolder { get; set; }

        public string TablesFolder { get; set; }

        public string TablePrefix { get; set; }

        public string TableSuffix { get; set; }

        public string ViewsFolder { get; set; } = "Views";

        public string ViewsNamespace { get; set; }

        public string ViewPrefix { get; set; } = "View_";

        public string ViewSuffix { get; set; }

        public string StoredProcedureFolder { get; set; } = "StoredProcedure";

        public string StoredProcedurNamespace { get; set; }

        public string StoredProcedurePrefix { get; set; } = "Proc_";

        public string StoredProcedureSuffix { get; set; }

        public string StoredProcedureResultSuffix { get; set; } = "_Result";
    }
}
