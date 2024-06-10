namespace QueryExecution.Dapper.CommandFactory
{
    public class SQLQueryCommandAttribute : Attribute
    {
        public SQLQueryCommandAttribute(int group)
        {
            Group = group;
            Description = string.Empty;
        }

        public SQLQueryCommandAttribute(string description)
        {
            Group = 0;
            Description = description;
        }
        public SQLQueryCommandAttribute(int group, string description)
        {
            Group = group;
            Description = description;
        }

        public int Group { get; private set; }

        public string Description { get; private set; }
    }
}
