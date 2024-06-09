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
    }
}
