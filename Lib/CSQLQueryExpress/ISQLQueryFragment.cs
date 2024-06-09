using System.Collections;

namespace CSQLQueryExpress
{
    public interface ISQLQueryFragment
    {
        SQLQueryFragmentType FragmentType { get; }

        string Translate(ISQLQueryExpressionTranslator expressionTranslator);
    }
}