using CSQLQueryExpress.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CSQLQueryExpress
{
    public interface ISQLQueryExpressionTableNameResolver
    {
        IDictionary<Type, QueryExpressionTableName> Alias { get; }

        QueryExpressionTableName ResolveTableName(Type objType);

        string ResolveTableAlias(Type objType);

        string ResolveColumnName(Type objType, MemberInfo member);
    }

    internal class SQLQueryExpressionTableNameResolver : ISQLQueryExpressionTableNameResolver
    {
        public IDictionary<Type, QueryExpressionTableName> Alias { get; } = new Dictionary<Type, QueryExpressionTableName>();

        public IDictionary<Type, IDictionary<MemberInfo, string>> MembersTypes { get; } = new Dictionary<Type, IDictionary<MemberInfo, string>>();

        public string ResolveColumnName(Type objType, MemberInfo member)
        {
            if (!MembersTypes.TryGetValue(member.DeclaringType, out IDictionary<MemberInfo, string> members))
            {
                members = new Dictionary<MemberInfo, string>();
                MembersTypes.Add(member.DeclaringType, members);
            }

            string columnName;
            if (!members.TryGetValue(member, out columnName))
            {
                columnName = $"[{GetMemberName(member)}]";
                members.Add(member, columnName);
            }

            return columnName;
        }

        private string GetMemberName(MemberInfo member)
        {
            var columnAttribute = member.GetCustomAttribute<ColumnAttribute>();
            if (columnAttribute != null)
            {
                return columnAttribute.Name;
            }
            else
            {
                return member.Name;
            }
        }

        public QueryExpressionTableName ResolveTableName(Type objType)
        {
            if (!Alias.TryGetValue(objType, out QueryExpressionTableName alias))
            {
                alias = new QueryExpressionTableName();

                var tableAttribute = objType.GetCustomAttribute<TableAttribute>();
                if (tableAttribute != null)
                {
                    var databaseAttribute = objType.GetCustomAttribute<DatabaseAttribute>();
                    if (databaseAttribute != null)
                    {
                        alias.TableName = $"[{databaseAttribute.Name}].[{tableAttribute.Schema ?? "dbo"}].[{tableAttribute.Name}]";
                    }
                    else
                    {
                        alias.TableName = $"[{tableAttribute.Schema ?? "dbo"}].[{tableAttribute.Name}]";
                    }
                }
                else
                {
                    alias.TableName = $"[dbo].[{objType.Name}]";
                }

                alias.TableAlias = $"_t{Alias.Count}";
                Alias.Add(objType, alias);
                return alias;
            }

            return alias;
        }

        public string ResolveTableAlias(Type objType)
        {
            var tableAlias = ResolveTableName(objType);

            return $"{tableAlias.TableName} AS {tableAlias.TableAlias}";
        }
    }

    public class QueryExpressionTableName
    {
        public string TableName { get; set; }

        public string TableAlias { get; set; }
    }
}
