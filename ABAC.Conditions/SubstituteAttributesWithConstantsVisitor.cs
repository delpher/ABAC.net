using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ABAC.Attributes;

namespace ABAC.Conditions
{
    public class SubstituteAttributesWithConstantsVisitor : ExpressionVisitor
    {
        private readonly IReadOnlyDictionary<string, object> _attributes;

        public SubstituteAttributesWithConstantsVisitor(IReadOnlyDictionary<string, object> attributes)
        {
            _attributes = attributes;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == null)
                throw new NotSupportedException();

            if (!AttributesHelper.IsAttributesClass(node.Member.DeclaringType))
                throw new NotSupportedException();

            var attributeName = node.ToString();

            if (!_attributes.ContainsKey(attributeName))
                throw new AttributeMissingException(attributeName);

            return Expression.Constant(_attributes[attributeName]);
        }
    }
}