using System;

namespace SQLQueryBuilder.Scaffolding
{
    /// <summary>
    /// Class used to generate a data model from the database schema.
    /// </summary>
    public sealed class SQLDataModelCodeGenerator
    {
        private readonly SQLDataModelCodeGeneratorParameters _parameters;
        
        public SQLDataModelCodeGenerator(
            SQLDataModelCodeGeneratorParameters parameters
            )
        {
            _parameters = parameters;
        }

        /// <summary>
        /// Generate data model classes.
        /// </summary>
        /// <returns>The <see cref="SQLDataModelCodeGeneratorResult"/> of the data model classes generation procedure.</returns>
        public SQLDataModelCodeGeneratorResult GenerateDataModel()
        {
            var result = new SQLDataModelCodeGeneratorResult();

            if (_parameters.EntityTypes.HasFlag(SQLDataModelCodeGeneratorEntityType.Table))
            {
                try
                {
                    new SQLTablesDataModelCodeGenerator(_parameters).GenerateDataModel(result);
                }
                catch (Exception ex)
                {
                    result.AddError(SQLDataModelCodeGeneratorEntityType.Table, "CodeGenerator", ex);
                }
            }

            if (_parameters.EntityTypes.HasFlag(SQLDataModelCodeGeneratorEntityType.View))
            {
                try
                {
                    new SQLViewsDataModelCodeGenerator(_parameters).GenerateDataModel(result);
                }
                catch (Exception ex)
                {
                    result.AddError(SQLDataModelCodeGeneratorEntityType.View, "CodeGenerator", ex);
                }
                
            }

            if (_parameters.EntityTypes.HasFlag(SQLDataModelCodeGeneratorEntityType.StoredProcedure))
            {
                try
                {
                    new SQLStoredProceduresDataModelCodeGenerator(_parameters).GenerateDataModel(result);
                }
                catch (Exception ex)
                {
                    result.AddError(SQLDataModelCodeGeneratorEntityType.StoredProcedure, "CodeGenerator", ex);
                }
            }

            return result;
        }
    }

}
