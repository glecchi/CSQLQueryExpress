using System;

namespace CSQLQueryExpress
{
    [Flags]
    public enum WithOptions
    {
        UPDLOCK = 1,
        
        READPAST = 2
    }
}