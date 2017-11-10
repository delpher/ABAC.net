using ABAC.Attributes;
using ABAC.Conditions;
using FluentAssertions;
using Xunit;

namespace ABAC.Rules.Tests
{
    public class TestRuleEvaluation
    {
        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(false, false, false)]
        public void Should_Return_True_When_Condition_And_Target_Match(bool targetMatch, bool conditionMatch, bool expectedResult)
        {
            var target = new Condition(()=>Values.TargetMatch);
            var condition = new Condition(()=>Values.ConditionMatch);
            var rule = new Rule(Effect.Permit, target, condition);
            var values = new AttributesCollection
            {
                [() => Values.ConditionMatch] = conditionMatch,
                [() => Values.TargetMatch] = targetMatch
            };
            rule.Evaluate(values).Should().Be(expectedResult);
        }

        [Fact]
        public void Should_Throw_Evaluation_Exception_If_Condition_Contains_Not_Supported_Expression()
        {
            var rule = new Rule(Effect.Deny, new Condition(()=>true), new Condition(()=>NotSupported.Attr));
            Assert.Throws<Rule.RuleEvaluationException>(() => rule.Evaluate(new AttributesCollection()));
        }

        [Fact]
        public void Should_Throw_Evaluation_Exception_If_Target_Contains_Not_Supported_Expression()
        {
            var rule = new Rule(Effect.Deny, new Condition(() => NotSupported.Attr));
            Assert.Throws<Rule.RuleEvaluationException>(() => rule.Evaluate(new AttributesCollection()));
        }

        [Fact]
        public void Shoult_Throw_Evaluation_Exception_If_Missing_Attributes()
        {
            var rule = new Rule(Effect.Deny, new Condition(()=>Values.ConditionMatch));
            Assert.Throws<Rule.RuleEvaluationException>(() => rule.Evaluate(new AttributesCollection()));
        }

        [Fact]
        public void Should_Return_Effect_As_Provided_In_Constructor()
        {
            var rule = new Rule(Effect.Permit, new Condition(() => true));
            rule.Effect.Should().Be(Effect.Permit);
        }

        [Attributes]
        private static class Values
        {
            public static bool ConditionMatch;
            public static bool TargetMatch;
        }

        private static class NotSupported
        {
            public static bool Attr;
        }
    }
}
