using CSQLQueryExpress.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CSQLQueryExpress
{
    public sealed class SQLQueryTableNameResolver : ISQLQueryTableNameResolver
    {
        private readonly SQLQueryCompilerSettings _settings;

        private IDictionary<Type, SQLQueryTableName> Alias { get; } = new Dictionary<Type, SQLQueryTableName>();

        private IDictionary<Type, IDictionary<MemberInfo, string>> MembersTypes { get; } = new Dictionary<Type, IDictionary<MemberInfo, string>>();

        public SQLQueryTableNameResolver(SQLQueryCompilerSettings settings)
        {
            _settings = settings;
        }

        void ISQLQueryTableNameResolver.Initialize()
        {
            Alias.Clear();
            MembersTypes.Clear();
        }

        SQLQueryTableName ISQLQueryTableNameResolver.ResolveTableName(Type objType)
        {
            if (!Alias.TryGetValue(objType, out SQLQueryTableName alias))
            {
                string tableName;
                
                var tableAttribute = objType.GetCustomAttribute<TableAttribute>();
                if (tableAttribute != null)
                {
                    var databaseAttribute = objType.GetCustomAttribute<DatabaseAttribute>();
                    if (databaseAttribute != null)
                    {
                        tableName = $"[{databaseAttribute.Name}].[{tableAttribute.Schema ?? "dbo"}].[{tableAttribute.Name}]";
                    }
                    else
                    {
                        tableName = $"[{tableAttribute.Schema ?? "dbo"}].[{tableAttribute.Name}]";
                    }
                }
                else
                {
                    tableName = $"[dbo].[{objType.Name}]";
                }

                var tableAlias = $"{_settings.TableAliasPrefix}{Alias.Count}";

                alias = new SQLQueryTableName(tableName, tableAlias);
                Alias.Add(objType, alias);
                return alias;
            }

            return alias;
        }

        string ISQLQueryTableNameResolver.ResolveTableNameAsAlias(Type objType)
        {
            var tableAlias = ((ISQLQueryTableNameResolver)this).ResolveTableName(objType);

            return $"{tableAlias.TableName} AS {tableAlias.TableAlias}";
        }

        string ISQLQueryTableNameResolver.ResolveColumnName(Type objType, MemberInfo member)
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
    }
}
