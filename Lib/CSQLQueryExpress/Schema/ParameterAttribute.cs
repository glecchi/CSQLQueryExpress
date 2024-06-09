using System;

namespace CSQLQueryExpress.Schema
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ParameterAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the ParameterAttribute
        /// </summary>
        /// <param name="name">The name of the parameter the property is mapped to.</param>
        public ParameterAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of the parameter the property is mapped to.
        /// </summary>
        public string Name { get; }

        public bool Output { get; set; }
    }
}
