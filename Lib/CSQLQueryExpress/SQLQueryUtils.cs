using CSQLQueryExpress.Fragments;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSQLQueryExpress
{
    public static class SQLQueryUtils
    {
        public static bool IsHierachicalSelectFromCte(this SQLQuerySelect select)
        {
            foreach (var fragment in select)
            {
                if (fragment.FragmentType == SQLQueryFragmentType.FromBySelect ||
                    fragment.FragmentType == SQLQueryFragmentType.JoinBySelect)
                {
                    var hSelect = ((ISQLQueryFragmentFromSelect)fragment).FromSelect;
                    if (hSelect.FragmentType == SQLQueryFragmentType.Select)
                    {
                        return IsHierachicalSelectFromCte(hSelect);
                    }
                    else if (hSelect.FragmentType == SQLQueryFragmentType.SelectCte) 
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static T[] Merge<T>(this T first, T second, params T[] others)
        {
            var list = new List<T>
            {
                first,
                second
            };

            if (others != null && others.Length > 0)
            {
                list.AddRange(others);
            }

            return list.ToArray();
        }

        public static T[] Merge<T>(this T first, params T[] others)
        {
            var list = new List<T>
            {
                first
            };

            if (others != null && others.Length > 0)
            {
                list.AddRange(others);
            }

            return list.ToArray();
        }
    }
}
