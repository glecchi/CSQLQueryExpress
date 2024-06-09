using System;

namespace CSQLQueryExpress.Scaffolding
{
    [Flags]
    public enum SQLDataModelCodeGeneratorEntityType
    {
        Table = 1,

        StoredProcedure = 2,

        View = 4
    }
}
