using System.Linq.Expressions;

namespace CSQLQueryExpress.Fragments
{
    public class SQLQueryPage : ISQLQueryFragment
    {
        private readonly Expression _paging;

        internal SQLQueryPage(Expression paging)
        {
            _paging = paging;
        }

        public SQLQueryFragmentType FragmentType { get { return SQLQueryFragmentType.Page; } }

        public string Translate(ISQLQueryTranslator expressionTranslator)
        {
            return expressionTranslator.Translate(_paging, FragmentType);
        }
    }
}
