using System.Collections.Generic;

namespace ABAC.Rules.Tests.TestRules
{
    public class TestIndeterminateRule : IRule
    {
        public Effect Effect { get; }

        public bool Evaluate(IReadOnlyDictionary<string, object> attributes)
        {
            throw new Rule.RuleEvaluationException();
        }

        public override string ToString()
        {
            return "Indeterminate";
        }
    }
}