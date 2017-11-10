using System;

namespace ABAC.Conditions
{
    public class AttributeMissingException : Exception
    {
        public AttributeMissingException(string attributeName)
            : base("Missing attribute " + attributeName)
        {
        }
    }
}