using System;
using System.Collections.Generic;
using System.Text;

namespace SQLQueryBuilder
{
    public interface ISQLStoredProcedure
    {
    }

    public interface ISQLStoredProcedure<TR> : ISQLStoredProcedure
    {
    }
}
