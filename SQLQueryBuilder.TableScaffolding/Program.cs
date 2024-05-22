using System.Configuration;
using System.Data.SqlClient;

DirectoryInfo di = new DirectoryInfo(GetFolderPath());

if (ClearFiles())
{
    foreach (FileInfo file in di.GetFiles())
    {
        file.Delete();
    }
    foreach (DirectoryInfo dir in di.GetDirectories())
    {
        dir.Delete(true);
    }
}

if (!di.Exists)
{
    di.Create();
}

using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
{
    connection.Open();

    var tables = new List<Table>();

    using (var cmd = connection.CreateCommand())
    {
        cmd.CommandText = ConfigurationManager.AppSettings["Command"];

        using (var rd = cmd.ExecuteReader())
        {
            while (rd.Read())
            {
                tables.Add(new Table { Schema = rd.GetString(0), Name = rd.GetString(1) });
            }
        }
    }

    foreach (var table in tables)
    {
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = GetCommand(table.Schema, table.Name);

            var dto = (string)cmd.ExecuteScalar();

            File.WriteAllText(GetFileCs(table.Schema, table.Name), dto);
        }
    }
}

bool ClearFiles()
{
    return bool.Parse(ConfigurationManager.AppSettings["ClearFiles"]);
}

string GetFolderPath()
{
    return Path.GetFullPath(ConfigurationManager.AppSettings["OutputFolder"]);
}

string GetFileCs(string schemaName, string tableName)
{
    var schemaFolderPath  = Path.Combine(Path.GetFullPath(ConfigurationManager.AppSettings["OutputFolder"]), schemaName);
    if (!Directory.Exists(schemaFolderPath))
    {
        Directory.CreateDirectory(schemaFolderPath);
    }

    var path = Path.Combine(schemaFolderPath, $"{tableName}.cs");

    return path;
}

string GetCommand(string schemaName, string tableName)
{
    return File.ReadAllText("Script_Scaffolding.sql").Replace("{TableSchema}", schemaName).Replace("{TableName}", tableName).Replace("{Namespace}", ConfigurationManager.AppSettings["Namespace"]);
}

class Table
{
    public string Schema { get; set; }

    public string Name { get; set; }
}

