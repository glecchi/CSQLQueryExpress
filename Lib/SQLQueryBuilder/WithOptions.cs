using System;

namespace SQLQueryBuilder
{
    [Flags]
    public enum WithOptions
    {
        UPDLOCK = 1,
        
        READPAST = 2
    }
}