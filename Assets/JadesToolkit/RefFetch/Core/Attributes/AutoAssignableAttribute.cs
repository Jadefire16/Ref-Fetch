using System;

namespace JadesToolkit
{
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
    public class AutoAssignableAttribute : System.Attribute
    {
        public Type AttributeType { get; private set; }
        public bool AddIfNotFound { get; private set; } = false;

        public AutoAssignableAttribute(Type type)
        {
            this.AttributeType = type;      
        }
        public AutoAssignableAttribute(Type attributeType, bool addIfNotFound) : this(attributeType)
        {
            AddIfNotFound = addIfNotFound;
        }
    }
}

