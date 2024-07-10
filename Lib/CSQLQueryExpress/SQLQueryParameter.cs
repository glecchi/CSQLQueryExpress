using System;

namespace CSQLQueryExpress
{
    public sealed class SQLQueryParameter
    {
        internal SQLQueryParameter(string name, object value)
            : this(name, value, SQLQueryParameterDirection.Input)
        {
         
        }

        internal SQLQueryParameter(string name, object value, SQLQueryParameterDirection direction)
        {
            Name = name;
            Value = value;
            Direction = direction;
        }

        public string Name { get; }
        public object Value { get; set; }
        public SQLQueryParameterDirection Direction { get; }

        public override string ToString()
        {
            if (Value != null)
            {
                if (Value is string || Value is DateTime || Value is DateTimeOffset || Value is Guid)
                {
                    return $"{Name} = '{Value}'";
                }

                return $"{Name} = {Value}";
            }

            return $"{Name} = NULL";
        }
    }
}
