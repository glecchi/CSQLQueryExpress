using System;

namespace CSQLQueryExpress.Schema
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DatabaseAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the DatabaseAttribute class using the specified name of the database.
        /// </summary>
        /// <param name="name">The name of the database referenced by the table to which the class is mapped.</param>
        public DatabaseAttribute(string name)
        {
            Name = name;
        }


        /// <summary>
        /// Gets the name of the database referenced by the table to which the class is mapped.
        /// </summary>
        public string Name { get; }
    }
}
