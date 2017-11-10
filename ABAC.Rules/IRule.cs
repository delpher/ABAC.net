using System.Collections.Generic;

namespace ABAC.Rules
{
    public interface IRule
    {
        Effect Effect { get; }
        bool Evaluate(IReadOnlyDictionary<string, object> attributes);
    }
}