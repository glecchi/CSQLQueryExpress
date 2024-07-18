using CSQLQueryExpress.Fragments;

namespace CSQLQueryExpress
{
    public interface ISQLQueryFragmentFromSelect
    {
        SQLQuerySelect FromSelect { get; }
    }
}
