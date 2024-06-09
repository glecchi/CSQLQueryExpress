using SQLQueryBuilder.Fragments;

namespace SQLQueryBuilder
{
    public interface ISQLQueryFragmentFromSelect
    {
        SQLQuerySelect FromSelect { get; }
    }
}
