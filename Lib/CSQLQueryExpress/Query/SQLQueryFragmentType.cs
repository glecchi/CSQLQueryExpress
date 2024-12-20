﻿namespace CSQLQueryExpress
{
    public enum SQLQueryFragmentType
    {
        Insert,
        InsertValues,
        InsertBySelect,
        Delete,
        Update,
        Truncate,
        Drop,
        Output,
        Select,
        SelectCte,
        Into,
        From,
        FromBySelect,
        Join,
        JoinBySelect,
        Where,
        Group,
        GroupHaving,
        Union,
        FromUnion,
        Page, 
        Order,
        ForXml,
        StoredProcedure,
        Batch,
        MultipleResultSets
    }
}