using CSQLQueryExpress.Fragments;
using System;

namespace CSQLQueryExpress
{
    public static class Query
    {
        public static QueryInstanceStruct<T> Instance<T>(this SQLQuerySelect<T> instance) where T : struct
        {
            return default;
        }

        public static QueryInstanceStruct<T> Instance<T>(this SQLQuerySelect<T?> instance) where T : struct
        {
            return default;
        }

        public static QueryInstanceString Instance(this SQLQuerySelect<string> instance)
        {
            return default;
        }

        private static void CheckSQLQuerySelect(SQLQuerySelect select)
        {
            if (select.FragmentType == SQLQueryFragmentType.SelectCte || select.IsHierarchicalSelectFromCte())
            {
                throw new NotSupportedException($"Queries with CTE TABLEs is not supported in {nameof(QueryInstance)}");
            }
        }
    }

    public abstract class QueryInstance
    {

    }

    public sealed class QueryInstanceStruct<T> : QueryInstance where T : struct
    {
        public static bool operator ==(QueryInstanceStruct<T> i, T value)
        {
            return default;
        }

        public static bool operator !=(QueryInstanceStruct<T> i, T value)
        {
            return default;
        }

        public static bool operator ==(QueryInstanceStruct<T> i, T? value)
        {
            return default;
        }

        public static bool operator !=(QueryInstanceStruct<T> i, T? value)
        {
            return default;
        }

        public static bool operator <(QueryInstanceStruct<T> i, T value)
        {
            return default;
        }

        public static bool operator >(QueryInstanceStruct<T> i, T value)
        {
            return default;
        }

        public static bool operator <(QueryInstanceStruct<T> i, T? value)
        {
            return default;
        }

        public static bool operator >(QueryInstanceStruct<T> i, T? value)
        {
            return default;
        }

        public static bool operator <=(QueryInstanceStruct<T> i, T value)
        {
            return default;
        }

        public static bool operator >=(QueryInstanceStruct<T> i, T value)
        {
            return default;
        }

        public static bool operator <=(QueryInstanceStruct<T> i, T? value)
        {
            return default;
        }

        public static bool operator >=(QueryInstanceStruct<T> i, T? value)
        {
            return default;
        }

        public static bool operator ==(T value, QueryInstanceStruct<T> i)
        {
            return default;
        }

        public static bool operator !=(T value, QueryInstanceStruct<T> i)
        {
            return default;
        }

        public static bool operator ==(T? value, QueryInstanceStruct<T> i)
        {
            return default;
        }

        public static bool operator !=(T? value, QueryInstanceStruct<T> i)
        {
            return default;
        }

        public static bool operator <(T value, QueryInstanceStruct<T> i)
        {
            return default;
        }

        public static bool operator >(T value, QueryInstanceStruct<T> i)
        {
            return default;
        }

        public static bool operator <(T? value, QueryInstanceStruct<T> i)
        {
            return default;
        }

        public static bool operator >(T? value, QueryInstanceStruct<T> i)
        {
            return default;
        }

        public static bool operator <=(T value, QueryInstanceStruct<T> i)
        {
            return default;
        }

        public static bool operator >=(T value, QueryInstanceStruct<T> i)
        {
            return default;
        }

        public static bool operator <=(T? value, QueryInstanceStruct<T> i)
        {
            return default;
        }

        public static bool operator >=(T? value, QueryInstanceStruct<T> i)
        {
            return default;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public sealed class QueryInstanceString : QueryInstance
    {
        public static bool operator ==(QueryInstanceString i, string value)
        {
            return default;
        }

        public static bool operator !=(QueryInstanceString i, string value)
        {
            return default;
        }

        public static bool operator ==(string value, QueryInstanceString i)
        {
            return default;
        }

        public static bool operator !=(string value, QueryInstanceString i)
        {
            return default;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
