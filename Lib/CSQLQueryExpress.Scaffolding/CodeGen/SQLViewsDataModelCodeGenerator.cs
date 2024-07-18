using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;

namespace CSQLQueryExpress.Scaffolding
{
    internal class SQLViewsDataModelCodeGenerator
    {
        private readonly SQLDataModelCodeGeneratorParameters _parameters;

        public SQLViewsDataModelCodeGenerator(SQLDataModelCodeGeneratorParameters parameters)
        {
            _parameters = parameters;
        }

        public void GenerateDataModel(SQLDataModelCodeGeneratorResult result)
        {
            var scaffoldingScript = GetScaffoldingScript();

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

                var views = new List<View>();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = _parameters.ScaffoldingViewsQuery;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            views.Add(new View { Database = connectionStringBuilder.InitialCatalog, Schema = rd.GetString(0), Name = rd.GetString(1) });
                        }
                    }
                }

                foreach (var view in views)
                {
                    try
                    {
                        var fileCs = GetFileCs(workingFolder.FullName, view);
                        if (!_parameters.OverwriteExistingDataModelClasses && File.Exists(fileCs))
                        {
                            continue;
                        }

                        if (File.Exists(fileCs))
                        {
                            File.Delete(fileCs);
                        }

                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = GetCommand(view, scaffoldingScript);

                            var dto = (string)cmd.ExecuteScalar();

                            if (dto.HasUnknownTypes(out IList<string> unknownTypes))
                            {
                                throw new Exception($"Unknown types: {string.Join(", ", unknownTypes)}");
                            }

                            File.WriteAllText(fileCs, dto);
                        }
                    }
                    catch (Exception ex)
                    {
                        result.AddError(SQLDataModelCodeGeneratorEntityType.View, view.Name, ex);
                    }
                }
            }
        }

        string GetFolderPath(string databaseName)
        {
            return _parameters.GenerateDatabaseFolder
               ? !string.IsNullOrWhiteSpace(_parameters.ViewsFolder)
                   ? Path.Combine(_parameters.OutputRootFolder, databaseName, _parameters.ViewsFolder)
                   : Path.Combine(_parameters.OutputRootFolder, databaseName)
               : !string.IsNullOrWhiteSpace(_parameters.TablesFolder)
                   ? Path.Combine(_parameters.OutputRootFolder, _parameters.ViewsFolder)
                   : Path.Combine(_parameters.OutputRootFolder);
        }

        string GetFileCs(string databaseFolderPath, View view)
        {
            var schemaFolderPath = _parameters.GenerateSchemaFolder 
                ? Path.Combine(databaseFolderPath, view.Schema)
                : databaseFolderPath;

            if (!Directory.Exists(schemaFolderPath))
            {
                Directory.CreateDirectory(schemaFolderPath);
            }

            var path = Path.Combine(schemaFolderPath, view.GetFileCs(_parameters));

            return path;
        }

        string GetCommand(View view, string scaffoldingScript)
        {
            var className = view.GetClassName(_parameters);
            var nameSpace = view.GetNamespace(_parameters);

            return scaffoldingScript
                .Replace(SQLDataModelCodeGeneratorConstants.DatabaseName, view.Database)
                .Replace(SQLDataModelCodeGeneratorConstants.TableSchema, view.Schema)
                .Replace(SQLDataModelCodeGeneratorConstants.TableName, view.Name)
                .Replace(SQLDataModelCodeGeneratorConstants.Namespace, nameSpace)
                .Replace(SQLDataModelCodeGeneratorConstants.ClassName, className)
                .Replace(SQLDataModelCodeGeneratorConstants.PartialClass, _parameters.GeneratePartialClasses ? "1" : "0")
                .Replace(SQLDataModelCodeGeneratorConstants.OtherUsing, string.Empty)
                .Replace(SQLDataModelCodeGeneratorConstants.BaseClassAndInterfaces, SQLDataModelCodeGeneratorConstants.ISQLQueryEntity);
        }

        string GetScaffoldingScript()
        {
            var scaffoldingScript = !_parameters.DecorateWithDatabaseAttribute
                ? !_parameters.GenerateSchemaNestedClasses
                    ? SQLDataModelCodeGeneratorConstants.Script_Scaffolding_Table
                    : SQLDataModelCodeGeneratorConstants.Script_Scaffolding_Table_AsSchemaNestedClass
                : !_parameters.GenerateSchemaNestedClasses
                    ? SQLDataModelCodeGeneratorConstants.Script_Scaffolding_Table_WithDbDecoration
                    : SQLDataModelCodeGeneratorConstants.Script_Scaffolding_Table_WithDbDecoration_AsSchemaNestedClass;

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

        class View
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
                   ? !string.IsNullOrWhiteSpace(parameters.ViewsNamespace)
                       ? $"{parameters.RootNamespace}.{Database}.{parameters.ViewsNamespace}"
                       : $"{parameters.RootNamespace}.{Database}"
                   : !string.IsNullOrWhiteSpace(parameters.ViewsNamespace)
                       ? $"{parameters.RootNamespace}.{parameters.ViewsNamespace}"
                       : $"{parameters.RootNamespace}";
            }

            public string GetClassName(SQLDataModelCodeGeneratorParameters parameters)
            {
                return $"{parameters.ViewPrefix}{Name.Replace(" ", "_")}{parameters.ViewSuffix}";
            }
        }
    }
}
