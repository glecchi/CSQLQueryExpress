using System;
using System.Collections.Generic;

namespace CSQLQueryExpress.Scaffolding
{
    /// <summary>
    /// The result of the data model class generation procedure.
    /// </summary>
    public sealed class SQLDataModelCodeGeneratorResult
    {
        internal SQLDataModelCodeGeneratorResult()
        {
            Errors = new Dictionary<SQLDataModelCodeGeneratorEntityType, IList<SQLDataModelCodeGeneratorError>>();
        }

        /// <summary>
        /// True if all data model classes were generated correctly. Otherwise False.
        /// </summary>
        public bool Successfully 
        { 
            get 
            { 
                return Errors.Count == 0; 
            } 
        }

        /// <summary>
        /// The errors collection of procedure.
        /// </summary>
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
