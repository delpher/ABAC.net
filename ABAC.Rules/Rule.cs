using System;
using System.Collections.Generic;
using ABAC.Conditions;

namespace ABAC.Rules
{
    public class Rule : IRule
    {
        private readonly Condition _condition;
        private readonly Condition _target;

        public Effect Effect { get; }

        public Rule(Effect effect, Condition condition, Condition target = null)
        {
            Effect = effect;
            _condition = condition;
            _target = target ?? new Condition(()=>true);
        }

        public bool Evaluate(IReadOnlyDictionary<string, object> attributes)
        {
            try
            {
                return _target.Evaluate(attributes) && _condition.Evaluate(attributes);
            }
            catch (NotSupportedException ex)
            {
                throw new RuleEvaluationException(ex);
            }
            catch (AttributeMissingException ex)
            {
                throw new RuleEvaluationException(ex);
            }
        }

        public class RuleEvaluationException : Exception
        {
            public RuleEvaluationException(Exception innerException) :
                base ("Rule evaluation failed.", innerException)
            {
                
            }

            public RuleEvaluationException()
                : base("Rule evaluation failed.")
            {
            }
        }
    }
}
