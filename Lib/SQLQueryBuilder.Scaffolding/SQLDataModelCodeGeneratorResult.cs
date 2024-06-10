using System;
using System.Collections.Generic;

namespace SQLQueryBuilder.Scaffolding
{
    public sealed class SQLDataModelCodeGeneratorResult
    {
        internal SQLDataModelCodeGeneratorResult()
        {
            Errors = new Dictionary<SQLDataModelCodeGeneratorEntityType, IList<SQLDataModelCodeGeneratorError>>();
        }

        public bool Successfully 
        { 
            get 
            { 
                return Errors.Count == 0; 
            } 
        }

        public IDictionary<SQLDataModelCodeGeneratorEntityType, IList<SQLDataModelCodeGeneratorError>> Errors { get; }

        internal void AddError(SQLDataModelCodeGeneratorEntityType entityType, string entityName, Exception error)
        {
            if (!Errors.TryGetValue(entityType, out IList<SQLDataModelCodeGeneratorError> errorList))
            {
                errorList = new List<SQLDataModelCodeGeneratorError>();
                Errors.Add(entityType, errorList);
            }

            errorList.Add(new SQLDataModelCodeGeneratorError(entityName, error));
        }
    }

}
