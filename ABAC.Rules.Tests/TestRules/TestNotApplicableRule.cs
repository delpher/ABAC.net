using System.Collections.Generic;

namespace ABAC.Rules.Tests.TestRules
{
    public class TestNotApplicableRule : IRule
    {
        public Effect Effect { get; }

        public bool Evaluate(IReadOnlyDictionary<string, object> attributes)
        {
            return false;
        }

        public override string ToString()
        {
            return "Not Applicable";
        }
    }
}