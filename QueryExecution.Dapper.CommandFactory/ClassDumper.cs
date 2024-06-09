using Newtonsoft.Json;

namespace QueryExecution.Dapper.CommandFactory
{
    public static class ClassDumper
    {
        public static string DumpJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string Dump<T>(this T obj) where T : class
        {
            var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetProperty);
            var dic = new Dictionary<string, object>();
            foreach (var item in properties)
            {
                dic.Add(item.Name, item.GetValue(obj));
            }

            return string.Join(" - ", dic.Select(k => $"[{k.Key}] => [{k.Value}]"));
        }
    }
}