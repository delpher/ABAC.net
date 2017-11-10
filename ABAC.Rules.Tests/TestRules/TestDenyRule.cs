using System.Collections.Generic;

namespace ABAC.Rules.Tests.TestRules
{
    public class TestDenyRule : IRule
    {
        public Effect Effect => Effect.Deny;

        public bool Evaluate(IReadOnlyDictionary<string, object> attributes)
        {
            return true;
        }

        public override string ToString()
        {
            return "Deny";
        }
    }
}