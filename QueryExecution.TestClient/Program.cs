using System.Configuration;
using System.Reflection;
using QueryExecution.Dapper.CommandFactory;

var queries = typeof(Program).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ISQLQueryCommand)));

var dbList = new Dictionary<string, string>();
var allQueryList = new Dictionary<string, Dictionary<string, ExecutionQuery>>();

foreach (var query in queries.OrderBy(q => q.GetCustomAttribute<SQLQueryCommandAttribute>()?.Group ?? 0).ThenBy(q => q.Name))
{
    var queryIdAttr = query.GetCustomAttribute<SQLQueryCommandAttribute>();

    var dbName = query.Namespace.Split(".").Last();

    var executionQuery = new ExecutionQuery
    {
        Database = dbName,
        Name = query.Name,
        Description = queryIdAttr?.Description,
        QueryType = query,
        ConnectionString = ConfigurationManager.ConnectionStrings[dbName].ConnectionString
    };

    if (!allQueryList.TryGetValue(dbName, out Dictionary<string, ExecutionQuery> queryList))
    {
        queryList = new Dictionary<string, ExecutionQuery>();
        allQueryList.Add(dbName, queryList);
        dbList.Add((dbList.Count + 1).ToString(), dbName);
    }

    queryList.Add((queryList.Count + 1).ToString(), executionQuery);
}

do
{
    Console.Clear();
    Console.WriteLine("Database list:");

    var idx = 0;
    foreach (var db in dbList)
    {
        Console.WriteLine($"  {db.Key} => {db.Value}");
    }

    Console.WriteLine();
    Console.WriteLine("Enter the database identifier and press ENTER, to finish type END.");
    Console.WriteLine("Database identifier:");
    var idDb = Console.ReadLine();

    if (string.Equals(idDb, "end", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    if (dbList.TryGetValue(idDb, out string dbName))
    {
        var queryList = allQueryList[dbName];

        do
        {
            Console.Clear();
            Console.WriteLine("Query list:");

            const int rowMax = 25;
            idx = 0;
            int step = 0;
            foreach (var item in queryList)
            {
                idx++;

                Console.WriteLine($"  {item.Key} => {item.Value.Name}");
                if (!string.IsNullOrWhiteSpace(item.Value.Description))
                {
                    idx++;
                    Console.WriteLine(item.Value.Description);
                }

                if (idx == rowMax && (queryList.Count - (rowMax * step)) > rowMax)
                {
                    Console.WriteLine("Press Enter to list other queries...");
                    Console.ReadLine();

                    idx = 0;
                    step++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Enter the identifier of the query you want to execute and press ENTER, finally write END.");
            Console.WriteLine("Query identifier:");
            var idQuery = Console.ReadLine();
            if (string.Equals(idQuery, "end", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (queryList.TryGetValue(idQuery, out ExecutionQuery query))
            {
                Console.WriteLine();
                Console.WriteLine($"Query execution: {query.Name} ({query.Description})");
                Console.WriteLine();

                try
                {
                    var queryInstance = (IEnumerable<string>)Activator.CreateInstance(query.QueryType, new SQLQueryCommandFactory(query.ConnectionString));

                    foreach (var item in queryInstance)
                    {
                        if (item == "BREAK")
                        {
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERRORE: {ex.Message}");
                }

                Console.WriteLine();
                Console.WriteLine("Query completed. Press ENTER to continue.");
            }
            else
            {
                Console.WriteLine("Query identifier not valid. Press ENTER to continue.");
            }

            Console.ReadLine();

        } while (true);
    }
    else
    {
        Console.WriteLine("Database identifier not valid. Press ENTER to continue.");
        Console.ReadLine();
    }

} while (true);

class ExecutionQuery
{
    public string Database { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Type QueryType { get; set; }

    public string ConnectionString { get; set; }
}