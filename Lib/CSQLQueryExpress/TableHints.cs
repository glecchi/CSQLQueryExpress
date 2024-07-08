using System;

namespace CSQLQueryExpress
{
    [Flags]
    public enum TableHints
    {
        FORCESCAN = 1,
        HOLDLOCK = 2,
        NOLOCK = 3,
        NOWAIT = 4,
        PAGLOCK = 5,
        READCOMMITTED = 6,
        READCOMMITTEDLOCK = 7,
        READPAST = 8,
        READUNCOMMITTED = 9,
        REPEATABLEREAD = 10,
        ROWLOCK = 11,
        SERIALIZABLE = 12,
        SNAPSHOT = 13,
        TABLOCK = 14,
        TABLOCKX = 15,
        UPDLOCK = 16,
        XLOCK = 17,
    }
}