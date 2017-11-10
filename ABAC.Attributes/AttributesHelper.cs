using System.Linq;
using System.Reflection;

namespace ABAC.Attributes
{
    public static class AttributesHelper
    {
        public static bool IsAttributesClass(MemberInfo memberAccess)
        {
            return memberAccess.CustomAttributes
                .Any(a => a.AttributeType == typeof(AttributesAttribute));
        }
    }
}
