using System.Collections;

namespace SQLQueryBuilder
{
    public interface ISQLQueryFragment
    {
        SQLQueryFragmentType FragmentType { get; }

        string Translate(ISQLQueryExpressionTranslator expressionTranslator);
    }
}