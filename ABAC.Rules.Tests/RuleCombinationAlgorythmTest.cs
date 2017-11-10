using ABAC.Attributes;
using ABAC.Rules.Combining;
using ABAC.Rules.Tests.TestRules;
using FluentAssertions;

namespace ABAC.Rules.Tests
{
    public abstract class RuleCombinationAlgorythmTest
    {
        protected static readonly IRule Deny;
        protected static readonly IRule Permit;
        protected static readonly IRule Indeterminate;
        protected static readonly IRule NotApplicable;

        static RuleCombinationAlgorythmTest()
        {
            Deny = new TestDenyRule();
            Permit = new TestPermitRule();
            NotApplicable = new TestNotApplicableRule();
            Indeterminate = new TestIndeterminateRule();
        }

        protected void RunTestCase(IRule rule1, IRule rule2, Decision expectedDecision)
        {
            GetAlgorythm()
                .Evaluate(new[] {rule1, rule2}, new AttributesCollection())
                .Should()
                .Be(expectedDecision,
                    because: rule1 + " with " + rule2 + " should give " + expectedDecision);
        }

        protected abstract RuleCombinationAlgorythm GetAlgorythm();
    }
}