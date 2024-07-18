namespace CSQLQueryExpress
{
    public interface ISQLQueryFragment
    {
        SQLQueryFragmentType FragmentType { get; }

        string Translate(ISQLQueryTranslator expressionTranslator);
    }
}