using System.Collections.Generic;
using ABAC.Rules.Combining;
using Xunit;

namespace ABAC.Rules.Tests
{
    public class PermitOverridesTests : RuleCombinationAlgorythmTest
    {
        protected override RuleCombinationAlgorythm GetAlgorythm()
        {
            return new PermitOverrides();
        }

        public static IEnumerable<object[]> TestCases = new[]
        {
            new object[] {Permit, Permit, Decision.Permit},
            new object[] {Permit, Deny, Decision.Permit},
            new object[] {Permit, NotApplicable, Decision.Permit},
            new object[] {Permit, Indeterminate, Decision.Permit},

            new object[] {Deny, Permit, Decision.Permit},
            new object[] {Deny, Deny, Decision.Deny},
            new object[] {Deny, NotApplicable, Decision.Deny},
            new object[] {Deny, Indeterminate, Decision.Deny},

            new object[] {NotApplicable, Permit, Decision.Permit},
            new object[] {NotApplicable, Deny, Decision.Deny},
            new object[] {NotApplicable, NotApplicable, Decision.NotApplicable},
            new object[] {NotApplicable, Indeterminate, Decision.Indeterminate},

            new object[] {Indeterminate, Permit, Decision.Permit},
            new object[] {Indeterminate, Deny, Decision.Deny},
            new object[] {Indeterminate, NotApplicable, Decision.Indeterminate},
            new object[] {Indeterminate, Indeterminate, Decision.Indeterminate},
        };

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Test(IRule rule1, IRule rule2, Decision expectedDecision)
        {
            RunTestCase(rule1, rule2, expectedDecision);
        }
    }
}