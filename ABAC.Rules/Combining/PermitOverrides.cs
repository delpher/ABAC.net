using System.Collections.Generic;
using System.Linq;

namespace ABAC.Rules.Combining
{
    public class PermitOverrides : RuleCombinationAlgorythm
    {
        public override Decision Evaluate(IEnumerable<IRule> rules, IReadOnlyDictionary<string, object> attributes)
        {
            var decisions = EvaluateRules(rules, attributes).ToArray();

            if (!decisions.Any())
                return Decision.NotApplicable;

            if (decisions.Any(decision => decision == Decision.Permit))
                return Decision.Permit;

            if (decisions.Any(decision => decision == Decision.Deny))
                return Decision.Deny;

            return Decision.Indeterminate;
        }
    }
}
