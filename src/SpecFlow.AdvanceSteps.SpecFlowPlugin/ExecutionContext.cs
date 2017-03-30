using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace SpecFlow.AdvanceSteps
{
    internal class ExecutionContext
    {
        internal ExecutionContext()
        {
            Steps = new List<StepDefinition>();
        }

        internal IList<StepDefinition> Steps { get; set; }
        internal StepDefinition CurrentStep { get; set; }
        internal StepDefinition NextStep { get; set; }
        internal StepDefinition PreviousStep { get; set; }

        internal StepDefinitionType CurrentDefinitionType => Steps.Last().Type;
    }
}