using System.Collections.Generic;
using System.Linq;

namespace ABAC.Rules.Combining
{
    public abstract class RuleCombinationAlgorythm
    {
        public abstract Decision Evaluate(
            IEnumerable<IRule> rules, IReadOnlyDictionary<string, object> attributes);

        protected static IEnumerable<Decision> EvaluateRules(
            IEnumerable<IRule> rules, IReadOnlyDictionary<string, object> attributes)
        {
            return rules
                .Select(rule => EvaluateRule(attributes, rule))
                .Where(RuleIsApplicable);
        }

        private static Decision EvaluateRule(IReadOnlyDictionary<string, object> attributes, IRule rule)
        {
            try
            {
                if (rule.Evaluate(attributes))
                    return DecisionFromEffectOf(rule);

                return Decision.NotApplicable;
            }
            catch (Rule.RuleEvaluationException)
            {
                return Decision.Indeterminate;
            }
        }

        private static Decision DecisionFromEffectOf(IRule rule)
        {
            return rule.Effect == Effect.Deny ? Decision.Deny : Decision.Permit;
        }

        protected static bool Deny(Decision decision)
        {
            return decision == Decision.Deny;
        }

        protected static bool Permit(Decision decision)
        {
            return decision == Decision.Permit;
        }

        protected bool NotApplicable(Decision decision)
        {
            return decision == Decision.NotApplicable;
        }

        protected bool Indeterminate(Decision decision)
        {
            return decision == Decision.Indeterminate;
        }

        private static bool RuleIsApplicable(Decision decision)
        {
            return decision != Decision.NotApplicable;
        }
    }
}