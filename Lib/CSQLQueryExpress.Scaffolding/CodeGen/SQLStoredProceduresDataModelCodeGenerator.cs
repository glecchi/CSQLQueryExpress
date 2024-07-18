using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;

namespace CSQLQueryExpress.Scaffolding
{
    internal class SQLStoredProceduresDataModelCodeGenerator
    {
        private readonly SQLDataModelCodeGeneratorParameters _parameters;

        public SQLStoredProceduresDataModelCodeGenerator(SQLDataModelCodeGeneratorParameters parameters)
        {
            _parameters = parameters;
        }

        public void GenerateDataModel(SQLDataModelCodeGeneratorResult result)
        {
            var procedureScaffoldingScript = GetProcedureScaffoldingScript();
            var checkResultScript = GetCheckResultScript();
            var resultScaffoldingScript = GetResultScaffoldingScript();

            var connectionStringBuilder = new SqlConnectionStringBuilder(_parameters.DatabaseConnectionString);

            var workingFolder = new DirectoryInfo(GetFolderPath(connectionStringBuilder.InitialCatalog));

            if (_parameters.ClearFolder && workingFolder.Exists)
            {
                workingFolder.Delete(true);
            }

            if (!workingFolder.Exists)
            {
                workingFolder.Create();
            }

            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var procedures = new List<Procedure>();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = _parameters.ScaffoldingProceduresQuery;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            procedures.Add(new Procedure { Database = connectionStringBuilder.InitialCatalog, Schema = rd.GetString(0), Name = rd.GetString(1) });
                        }
                    }
                }

                foreach (var procedure in procedures)
                {
                    bool hasResult;

                    try
                    {
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = GetCheckResultCommand(procedure, checkResultScript);

                            var count = (int)cmd.ExecuteScalar();

                            hasResult = count > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        result.AddError(SQLDataModelCodeGeneratorEntityType.StoredProcedure, procedure.Name, ex);
                        continue;
                    }

                    var procedureFileCs = GetProcedureFileCs(workingFolder.FullName, procedure);
                    try
                    {                        
                        if (_parameters.OverwriteExistingDataModelClasses || !File.Exists(procedureFileCs))
                        {
                            if (File.Exists(procedureFileCs))
                            {
                                File.Delete(procedureFileCs);
                            }

                            using (var cmd = connection.CreateCommand())
                            {
                                cmd.CommandText = GetProcedureCommand(procedure, procedureScaffoldingScript, hasResult);

                                var cmdResult = cmd.ExecuteScalar();
                                if (cmdResult is string dto)
                                {
                                    if (dto.HasUnknownTypes(out IList<string> unknownTypes))
                                    {
                                        throw new Exception($"Unknown types: {string.Join(", ", unknownTypes)}");
                                    }

                                    File.WriteAllText(procedureFileCs, dto);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.AddError(SQLDataModelCodeGeneratorEntityType.StoredProcedure, procedure.Name, ex);
                    }

                    try
                    {
                        if (hasResult)
                        {
                            var procedureResultFileCs = GetProcedureResultFileCs(workingFolder.FullName, procedure);
                            if (_parameters.OverwriteExistingDataModelClasses || !File.Exists(procedureResultFileCs))
                            {
                                if (File.Exists(procedureResultFileCs))
                                {
                                    File.Delete(procedureResultFileCs);
                                }

                                using (var cmd = connection.CreateCommand())
                                {
                                    cmd.CommandText = GetProcedureResultCommand(procedure, resultScaffoldingScript);

                                    var cmdResult = cmd.ExecuteScalar();
                                    if (cmdResult is string dto)
                                    {
                                        if (dto.HasUnknownTypes(out IList<string> unknownTypes))
                                        {
                                            throw new Exception($"Unknown types: {string.Join(", ", unknownTypes)}");
                                        }

                                        File.WriteAllText(procedureResultFileCs, dto);
                                    }
                                    else
                                    {
                                        File.Delete(procedureFileCs);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.AddError(SQLDataModelCodeGeneratorEntityType.StoredProcedure, procedure.Name, ex);
                    }
                }
            }
        }

        string GetFolderPath(string databaseName)
        {
            return _parameters.GenerateDatabaseFolder
               ? !string.IsNullOrWhiteSpace(_parameters.StoredProcedureFolder)
                   ? Path.Combine(_parameters.OutputRootFolder, databaseName, _parameters.StoredProcedureFolder)
                   : Path.Combine(_parameters.OutputRootFolder, databaseName)
               : !string.IsNullOrWhiteSpace(_parameters.TablesFolder)
                   ? Path.Combine(_parameters.OutputRootFolder, _parameters.StoredProcedureFolder)
                   : Path.Combine(_parameters.OutputRootFolder);
        }

        string GetProcedureFileCs(string databaseFolderPath, Procedure procedure)
        {
            var schemaFolderPath = _parameters.GenerateSchemaFolder
                ? Path.Combine(databaseFolderPath, procedure.Schema)
                : databaseFolderPath;

            if (!Directory.Exists(schemaFolderPath))
            {
                Directory.CreateDirectory(schemaFolderPath);
            }

            var path = Path.Combine(schemaFolderPath, procedure.GetFileCs(_parameters));

            return path;
        }

        string GetProcedureResultFileCs(string databaseFolderPath, Procedure procedure)
        {
            var schemaFolderPath = Path.Combine(databaseFolderPath, procedure.Schema);
            if (!Directory.Exists(schemaFolderPath))
            {
                Directory.CreateDirectory(schemaFolderPath);
            }

            var path = Path.Combine(schemaFolderPath, procedure.GetResultFileCs(_parameters));

            return path;
        }

        string GetCheckResultCommand(Procedure procedure, string checkScript)
        {
            return checkScript
                .Replace(SQLDataModelCodeGeneratorConstants.ProcedureSchema, procedure.Schema)
                .Replace(SQLDataModelCodeGeneratorConstants.ProcedureName, procedure.Name);
        }

        string GetProcedureCommand(Procedure procedure, string scaffoldingScript, bool hasResult)
        {
            var className = procedure.GetClassName(_parameters);
            var nameSpace = procedure.GetNamespace(_parameters);
            var sqlStoredProcedureInterface = procedure.GetSqlStoreProcedureInterface(_parameters, hasResult);

            return scaffoldingScript
                .Replace(SQLDataModelCodeGeneratorConstants.DatabaseName, procedure.Database)
                .Replace(SQLDataModelCodeGeneratorConstants.ProcedureSchema, procedure.Schema)
                .Replace(SQLDataModelCodeGeneratorConstants.ProcedureName, procedure.Name)
                .Replace(SQLDataModelCodeGeneratorConstants.Namespace, nameSpace)
                .Replace(SQLDataModelCodeGeneratorConstants.ClassName, className)
                .Replace(SQLDataModelCodeGeneratorConstants.PartialClass, _parameters.GeneratePartialClasses ? "1" : "0")
                .Replace(SQLDataModelCodeGeneratorConstants.NotNullableParams, _parameters.StoreProceduresNotNullableParameters ? "1" : "0")
                .Replace(SQLDataModelCodeGeneratorConstants.SQLStoredProcedureInterface, sqlStoredProcedureInterface);
        }

        string GetProcedureResultCommand(Procedure procedure, string scaffoldingScript)
        {
            var className = procedure.GetResultClassName(_parameters);
            var nameSpace = procedure.GetNamespace(_parameters);

            return scaffoldingScript
                .Replace(SQLDataModelCodeGeneratorConstants.DatabaseName, procedure.Database)
                .Replace(SQLDataModelCodeGeneratorConstants.ProcedureSchema, procedure.Schema)
                .Replace(SQLDataModelCodeGeneratorConstants.ProcedureName, procedure.Name)
                .Replace(SQLDataModelCodeGeneratorConstants.Namespace, nameSpace)
                .Replace(SQLDataModelCodeGeneratorConstants.ClassName, className);
        }

        string GetProcedureScaffoldingScript()
        {
            var scaffoldingScript = !_parameters.DecorateWithDatabaseAttribute
                ? !_parameters.GenerateSchemaNestedClasses
                    ? SQLDataModelCodeGeneratorConstants.Script_Scaffolding_StoreProcedure
                    : SQLDataModelCodeGeneratorConstants.Script_Scaffolding_StoreProcedure_AsSchemaNestedClass
                : !_parameters.GenerateSchemaNestedClasses
                    ? SQLDataModelCodeGeneratorConstants.Script_Scaffolding_StoreProcedure_WithDbDecoration
                    : SQLDataModelCodeGeneratorConstants.Script_Scaffolding_StoreProcedure_WithDbDecoration_AsSchemaNestedClass;

            var info = Assembly.GetExecutingAssembly().GetName();
            var name = info.Name;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{name}.Scripts.{scaffoldingScript}"))
            {
                using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        string GetCheckResultScript()
        {
            var scaffoldingScript = SQLDataModelCodeGeneratorConstants.Script_Check_StoreProcedure_Result;

            var info = Assembly.GetExecutingAssembly().GetName();
            var name = info.Name;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{name}.Scripts.{scaffoldingScript}"))
            {
                using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        string GetResultScaffoldingScript()
        {
            var scaffoldingScript = SQLDataModelCodeGeneratorConstants.Script_Scaffolding_StoreProcedure_Result;

            var info = Assembly.GetExecutingAssembly().GetName();
            var name = info.Name;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{name}.Scripts.{scaffoldingScript}"))
            {
                using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        class Procedure
        {
            public string Database { get; set; }

            public string Schema { get; set; }

            public string Name { get; set; }

            public string GetFileCs(SQLDataModelCodeGeneratorParameters parameters)
            {
                return $"{GetClassName(parameters)}.cs";
            }

            public string GetNamespace(SQLDataModelCodeGeneratorParameters parameters)
            {
                return parameters.UseDatabaseNameAsNamespace 
                    ? !string.IsNullOrWhiteSpace(parameters.StoredProcedurNamespace)
                        ? $"{parameters.RootNamespace}.{Database}.{parameters.StoredProcedurNamespace}"
                        : $"{parameters.RootNamespace}.{Database}"
                    : !string.IsNullOrWhiteSpace(parameters.StoredProcedurNamespace)
                        ? $"{parameters.RootNamespace}.{parameters.StoredProcedurNamespace}"
                        : $"{parameters.RootNamespace}";
            }

            public string GetClassName(SQLDataModelCodeGeneratorParameters parameters)
            {
                return $"{parameters.StoredProcedurePrefix}{Name.Replace(" ", "_")}{parameters.StoredProcedureSuffix}";
            }

            public string GetResultFileCs(SQLDataModelCodeGeneratorParameters parameters)
            {
                return $"{GetResultClassName(parameters)}.cs";
            }

            public string GetResultClassName(SQLDataModelCodeGeneratorParameters parameters)
            {
                return $"{GetClassName(parameters)}{parameters.StoredProcedureResultSuffix}";
            }

            public string GetSqlStoreProcedureInterface(SQLDataModelCodeGeneratorParameters parameters, bool hasResult)
            {
                if (!hasResult)
                {
                    return SQLDataModelCodeGeneratorConstants.ISQLStoredProcedure;
                }

                return string.Format(SQLDataModelCodeGeneratorConstants.ISQLStoredProcedureFormat, GetResultClassName(parameters));
            }
        }
    }
}
