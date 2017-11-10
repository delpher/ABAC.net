using System.Collections.Generic;

namespace ABAC.Rules.Tests.TestRules
{
    public class TestPermitRule : IRule
    {
        public Effect Effect => Effect.Permit;

        public bool Evaluate(IReadOnlyDictionary<string, object> attributes)
        {
            return true;
        }

        public override string ToString()
        {
            return "Permit";
        }

    }
}