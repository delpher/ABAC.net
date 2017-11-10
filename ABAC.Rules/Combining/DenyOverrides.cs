using System.Collections.Generic;
using System.Linq;

namespace ABAC.Rules.Combining
{
    public class DenyOverrides : RuleCombinationAlgorythm
    {
        public override Decision Evaluate(IEnumerable<IRule> rules, IReadOnlyDictionary<string, object> attributes)
        {
            var decisions = EvaluateRules(rules, attributes).ToArray();

            if (!decisions.Any())
                return Decision.NotApplicable;

            if (decisions.Any(Deny))
                return Decision.Deny;

            if (decisions.All(Permit))
                return Decision.Permit;

            return Decision.Indeterminate;
        }
    }
}
