namespace SQLQueryBuilder.Dapper.CommandFactory
{
    public class SQLQueryDescriptionAttribute : Attribute
    {
        public SQLQueryDescriptionAttribute(string descrizione)
        {
            Descrizione = descrizione;
        }

        public string Descrizione { get; private set; }
    }
}
