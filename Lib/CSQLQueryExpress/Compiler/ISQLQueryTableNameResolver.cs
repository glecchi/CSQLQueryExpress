using System;
using System.Reflection;

namespace CSQLQueryExpress
{
    public interface ISQLQueryTableNameResolver
    {
        void Initialize();

        SQLQueryTableName ResolveTableName(Type objType);

        string ResolveTableNameAsAlias(Type objType);

        string ResolveColumnName(Type objType, MemberInfo member);        
    }
}
