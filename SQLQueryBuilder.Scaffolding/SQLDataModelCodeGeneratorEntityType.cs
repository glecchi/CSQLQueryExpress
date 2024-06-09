using System;

namespace SQLQueryBuilder.Scaffolding
{
    [Flags]
    public enum SQLDataModelCodeGeneratorEntityType
    {
        Table = 1,

        StoredProcedure = 2,

        View = 4
    }
}
