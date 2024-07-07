using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CSQLQueryExpress.Scaffolding
{
    internal static class SQLDataModelCodeGeneratorUtils
    {
        private static readonly Regex _unknownTypesRx = new Regex(@"public\sUNKNOWN_([A-Za-z0-9].*)\s[A-Za-z0-9].*\s\{", RegexOptions.Compiled);

        public static bool HasUnknownTypes(this string dto, out IList<string> unknownTypes)
        {
            unknownTypes = new List<string>();

            var matches = _unknownTypesRx.Matches(dto);

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    var unknType = match.Groups[1].Value;
                    if (!unknownTypes.Contains(unknType))
                    {
                        unknownTypes.Add(unknType);
                    }
                }

                return true;
            }

            return false;
        }

        public static string GetBaseClassAndInterfaces(this SQLDataModelCodeGeneratorParameters parameters)
        {
            var classAndInterfaces = new List<string>();
            
            if (parameters.TablesBaseClassFullName != null ||
                (parameters.TablesInterfacesFullNames != null && parameters.TablesInterfacesFullNames.Count > 0))
            {
                if (parameters.TablesBaseClassFullName != null)
                {
                    var cl = GetNameAndNamespaces(parameters.TablesBaseClassFullName);

                    classAndInterfaces.Add(cl.Key);
                }

                if (parameters.TablesInterfacesFullNames != null && parameters.TablesInterfacesFullNames.Count > 0)
                {
                    foreach (var interf in parameters.TablesInterfacesFullNames)
                    {
                        var ic = GetNameAndNamespaces(interf);

                        if (!classAndInterfaces.Contains(ic.Key))
                        {
                            classAndInterfaces.Add(ic.Key);
                        }
                    }
                }
            }

            classAndInterfaces.Add(SQLDataModelCodeGeneratorConstants.ISQLQueryEntity);

            var tabs = parameters.GenerateSchemaNestedClasses ? "\t\t\t" : "\t\t";

            return string.Join($",{Environment.NewLine}{tabs}", classAndInterfaces);
        }

        public static string GetOtherUsings(this SQLDataModelCodeGeneratorParameters parameters)
        {
            var usingBuilder = new StringBuilder();

            if (parameters.TablesBaseClassFullName != null ||
                (parameters.TablesInterfacesFullNames != null && parameters.TablesInterfacesFullNames.Count > 0))
            {
                var allNamespaces = new List<string>();

                if (parameters.TablesBaseClassFullName != null)
                {
                    var cl = GetNameAndNamespaces(parameters.TablesBaseClassFullName);

                    allNamespaces.Add(cl.Value);
                }

                if (parameters.TablesInterfacesFullNames != null && parameters.TablesInterfacesFullNames.Count > 0)
                {
                    foreach (var interf in parameters.TablesInterfacesFullNames)
                    {
                        var ic = GetNameAndNamespaces(interf);

                        if (!allNamespaces.Contains(ic.Value))
                        {
                            allNamespaces.Add(ic.Value);
                        }
                    }
                }

                foreach (var ns in allNamespaces)
                {
                    usingBuilder.Append($" {Environment.NewLine}using {ns};");
                }
            }

            return usingBuilder.ToString();
        }

        private static readonly Regex _fullNameRx = new Regex(@"(.*)\.([A-Za-z0-9<>_, ]*)");
        private static KeyValuePair<string, string> GetNameAndNamespaces(string fullName)
        {
            var match = _fullNameRx.Match(fullName);
            if (!match.Success)
            {
                throw new Exception($"Invalid base class name or interface: {fullName}");
            }

            var ns = match.Groups[1].Value;
            var name = match.Groups[2].Value;

            return new KeyValuePair<string, string>(name, ns);
        }
    }
}
