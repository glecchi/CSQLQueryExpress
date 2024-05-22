namespace SQLQueryBuilder
{
    public enum SQLQueryFragmentType
    {
        Insert,
        InsertValues,
        Delete,
        Update,
        Truncate,
        Drop,
        Output,
        Select,
        Into,
        From,
        FromBySelect,
        Join,
        JoinBySelect,
        Where,
        Group,
        GroupHaving,
        Union,
        Page, 
        Order,
        ForXml
    }
}