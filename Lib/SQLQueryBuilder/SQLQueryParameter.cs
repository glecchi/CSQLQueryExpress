using System;

namespace SQLQueryBuilder
{
    public sealed class SQLQueryParameter
    {
        internal SQLQueryParameter(string name, object value)
            : this(name, value, SQLQueryParameterValueDirection.Input)
        {
         
        }

        internal SQLQueryParameter(string name, object value, SQLQueryParameterValueDirection direction)
        {
            Name = name;
            Value = value;
            Direction = direction;
        }

        public string Name { get; }
        public object Value { get; }
        public SQLQueryParameterValueDirection Direction { get; }

        public override string ToString()
        {
            if (Value != null)
            {
                if (Value is string || Value is DateTime || Value is DateTimeOffset)
                {
                    return $"{Name} = '{Value}'";
                }

                return $"{Name} = {Value}";
            }

            return $"{Name} = NULL";
        }
    }
}
