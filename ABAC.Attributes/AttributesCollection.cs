using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ABAC.Attributes
{
    public class AttributesCollection : IReadOnlyDictionary<string, object>
    {
        private readonly Dictionary<string, object> _storage;

        public object this[Expression<Func<object>> expression]
        {
            get { return GetAttributeValue(expression); }
            set { SetAttributeValue(expression, value); }
        }

        private void SetAttributeValue(Expression<Func<object>> expression, object value)
        {
            _storage[GetAttributeName(expression)] = value;
        }

        public AttributesCollection()
        {
            _storage = new Dictionary<string, object>();
        }

        private object GetAttributeValue(Expression<Func<object>> attributeExpression)
        {
            return _storage[GetAttributeName(attributeExpression)];
        }

        private static string GetAttributeName(Expression<Func<object>> expression)
        {
            MemberExpression memberAccess;

            switch (expression.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    memberAccess = (MemberExpression)expression.Body;
                    break;
                case ExpressionType.Convert:
                    var convert = (UnaryExpression) expression.Body;
                    memberAccess = (MemberExpression)convert.Operand;
                    break;
                default: throw new NotSupportedException();
            }

            if (!AttributesHelper.IsAttributesClass(memberAccess.Member.DeclaringType))
                    throw new NotSupportedException();

            return memberAccess.ToString();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _storage.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _storage.Count;

        public bool ContainsKey(string key)
        {
            return _storage.ContainsKey(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _storage.TryGetValue(key, out value);
        }

        object IReadOnlyDictionary<string, object>.this[string key] => _storage[key];

        public IEnumerable<string> Keys => _storage.Keys;

        public IEnumerable<object> Values => _storage.Values;
    }
}
