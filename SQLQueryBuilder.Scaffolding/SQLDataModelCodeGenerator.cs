namespace SQLQueryBuilder.Scaffolding
{

    public sealed class SQLDataModelCodeGenerator
    {
        private readonly SQLDataModelCodeGeneratorParameters _parameters;
        
        public SQLDataModelCodeGenerator(
            SQLDataModelCodeGeneratorParameters parameters
            )
        {
            _parameters = parameters;
        }

        public void GenerateDataModel()
        {
            if (_parameters.EntityTypes.HasFlag(SQLDataModelCodeGeneratorEntityType.Table))
            {
                new SQLTablesDataModelCodeGenerator(_parameters).GenerateDataModel();
            }

            if (_parameters.EntityTypes.HasFlag(SQLDataModelCodeGeneratorEntityType.View))
            {
                new SQLViewsDataModelCodeGenerator(_parameters).GenerateDataModel();
            }

            if (_parameters.EntityTypes.HasFlag(SQLDataModelCodeGeneratorEntityType.StoredProcedure))
            {
                new SQLStoredProceduresDataModelCodeGenerator(_parameters).GenerateDataModel();
            }
        }
    }
}
