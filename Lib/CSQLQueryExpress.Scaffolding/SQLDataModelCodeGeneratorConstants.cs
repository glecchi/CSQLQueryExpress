using System;

namespace CSQLQueryExpress.Scaffolding
{
    internal class SQLDataModelCodeGeneratorConstants
    {
        public const String ISQLQueryEntity = "ISQLQueryEntity";
        public const String ISQLStoredProcedure = "ISQLStoredProcedure";
        public const String ISQLStoredProcedureFormat = "ISQLStoredProcedure<{0}>";

        public const String DatabaseName = "{DatabaseName}";
        public const String TableSchema = "{TableSchema}";
        public const String TableName = "{TableName}";
        public const String Namespace = "{Namespace}";
        public const String ClassName = "{ClassName}";
        public const String PartialClass = "{PartialClass}";
        public const String OtherUsing = "{OtherUsing}";
        public const String BaseClassAndInterfaces = "{BaseClassAndInterfaces}";
        public const String ProcedureSchema = "{ProcedureSchema}";
        public const String ProcedureName = "{ProcedureName}";
        public const String NotNullableParams = "{NotNullableParams}";
        public const String SQLStoredProcedureInterface = "{SQLStoredProcedureInterface}";

        public const String Script_Scaffolding_Table = "Script_Scaffolding_Table.sql";
        public const String Script_Scaffolding_Table_AsSchemaNestedClass = "Script_Scaffolding_Table_AsSchemaNestedClass.sql";
        public const String Script_Scaffolding_Table_WithDbDecoration = "Script_Scaffolding_Table_WithDbDecoration.sql";
        public const String Script_Scaffolding_Table_WithDbDecoration_AsSchemaNestedClass = "Script_Scaffolding_Table_WithDbDecoration_AsSchemaNestedClass.sql";

        public const String Script_Scaffolding_StoreProcedure = "Script_Scaffolding_StoreProcedure.sql";
        public const String Script_Scaffolding_StoreProcedure_AsSchemaNestedClass = "Script_Scaffolding_StoreProcedure_AsSchemaNestedClass.sql";
        public const String Script_Scaffolding_StoreProcedure_WithDbDecoration = "Script_Scaffolding_StoreProcedure_WithDbDecoration.sql";
        public const String Script_Scaffolding_StoreProcedure_WithDbDecoration_AsSchemaNestedClass = "Script_Scaffolding_StoreProcedure_WithDbDecoration_AsSchemaNestedClass.sql";
        public const String Script_Check_StoreProcedure_Result = "Script_Check_StoreProcedure_Result.sql";
        public const String Script_Scaffolding_StoreProcedure_Result = "Script_Scaffolding_StoreProcedure_Result.sql";
    }
}
