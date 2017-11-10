using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ABAC.Conditions
{
    public class Condition
    {
        private readonly Expression<Func<bool>> _expression;

        public Condition(Expression<Func<bool>> expression)
        {
            _expression = expression;
        }

        public bool Evaluate(IReadOnlyDictionary<string, object> values)
        {
            var visitor = new SubstituteAttributesWithConstantsVisitor(values);
            var toEval = (LambdaExpression)visitor.Visit(_expression);
            return (bool) toEval.Compile().DynamicInvoke();
        }
    }
}
