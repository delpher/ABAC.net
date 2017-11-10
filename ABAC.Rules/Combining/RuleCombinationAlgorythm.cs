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
                .Where(decision => decision != Decision.NotApplicable);
        }

        private static Decision EvaluateRule(IReadOnlyDictionary<string, object> attributes, IRule rule)
        {
            try
            {
                if (rule.Evaluate(attributes))
                    return RuleEffect(rule);

                return Decision.NotApplicable;
            }
            catch (Rule.RuleEvaluationException)
            {
                return Decision.Indeterminate;
            }
        }

        private static Decision RuleEffect(IRule rule)
        {
            return rule.Effect == Effect.Deny ? Decision.Deny : Decision.Permit;
        }
    }
}