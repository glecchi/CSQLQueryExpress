using System;
using System.Collections.Generic;
using System.Text;

namespace SQLQueryBuilder.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<Enum> GetFlags(this Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
            {
                if (input.HasFlag(value))
                {
                    yield return value;
                }
            }
        }
    }
}
