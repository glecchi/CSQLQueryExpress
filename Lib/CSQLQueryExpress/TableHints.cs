using System;

namespace CSQLQueryExpress
{
    [Flags]
    public enum TableHints
    {
        FORCESCAN = 1,
        HOLDLOCK = 2,
        NOLOCK = 4,
        NOWAIT = 8,
        PAGLOCK = 16,
        READCOMMITTED = 32,
        READCOMMITTEDLOCK = 64,
        READPAST = 128,
        READUNCOMMITTED = 256,
        REPEATABLEREAD = 512,
        ROWLOCK = 1024,
        SERIALIZABLE = 2048,
        SNAPSHOT = 4096,
        TABLOCK = 8192,
        TABLOCKX = 16384,
        UPDLOCK = 32768,
        XLOCK = 65536,
    }
}