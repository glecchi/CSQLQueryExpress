using System.Collections.Generic;

namespace SQLQueryBuilder
{
    public interface ISQLQuery : IEnumerable<ISQLQueryFragment> 
    { 
    }
}