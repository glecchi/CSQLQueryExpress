using System;

namespace CSQLQueryExpress.Schema
{
    /// <summary>
    /// Specifies the database store procedure that a class is mapped to.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class StoredProcedureAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the ProcedureAttribute
        /// class using the specified name of the store procedure.
        /// </summary>
        /// <param name="name">The name of the store procedure the class is mapped to.</param>
        public StoredProcedureAttribute(string name)
        {
            Name = name;
        }


        /// <summary>
        /// Gets the name of the store procedure the class is mapped to.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the schema of the store procedure the class is mapped to.
        /// </summary>
        public string Schema { get; set; }
    }
}
