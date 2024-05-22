using System.Configuration;
using System.Reflection;
using SQLQueryBuilder.Dapper.CommandFactory;
using SqlQueryClient;
using SqlQueryClient.Queries;

SQLQueryCommandFactory.SetConnectionString(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString);

var queries = typeof(Program).Assembly.GetTypes().Where(t => t.GetCustomAttribute<SQLQueryDescriptionAttribute>() != null);

var queryList = new Dictionary<string, Tuple<SQLQueryDescriptionAttribute, Type>>();

int idx = 1;
foreach (var query in queries.OrderBy(q => q.Name))
{
    var queryIdAttr = query.GetCustomAttribute<SQLQueryDescriptionAttribute>();

    queryList.Add(idx++.ToString(), new Tuple<SQLQueryDescriptionAttribute, Type>(queryIdAttr, query));
}

do
{
    Console.Clear();
    Console.WriteLine("Elenco queries:");

    foreach (var item in queryList)
    {
        Console.WriteLine($"  {item.Key} => {item.Value.Item1.Descrizione} [{item.Value.Item2.Name}]");
    }
    Console.WriteLine();
    Console.WriteLine("Inserisci l'identificativo della query che vuoi eseguire e premi ENTER, per terminare scrivi END.");
    Console.WriteLine("Identificativo Query:");
    var idQuery = Console.ReadLine();
    if (string.Equals(idQuery, "end", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    if (queryList.TryGetValue(idQuery, out Tuple<SQLQueryDescriptionAttribute, Type> query))
    {
        Console.WriteLine();
        Console.WriteLine($"Esecuzione query: {query.Item2.Name} ({query.Item1.Descrizione})");
        Console.WriteLine();

        try
        {
            var queryInstance = (IEnumerable<string>)Activator.CreateInstance(query.Item2);

            foreach (var item in queryInstance)
            {
                Console.WriteLine(item);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRORE: {ex.Message}");
        }
        
        Console.WriteLine();
        Console.WriteLine("Query completata. Premi ENTER per proseguire.");
    }
    else
    {
        Console.WriteLine("Identificativo non valido. Premi ENTER per proseguire.");
    }

    Console.ReadLine();

} while (true);