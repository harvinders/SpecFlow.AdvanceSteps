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
            Steps = new LinkedList<StepDefinition>();
        }

        internal LinkedList<StepDefinition> Steps { get; set; }
        internal StepDefinition CurrentStep { get; set; }
        internal StepDefinition NextStep { get; set; }
        internal StepDefinition PreviousStep { get; set; }

        internal StepDefinitionType CurrentDefinitionType => Steps.Last().Type;

        internal Dictionary<string, RepeatContext> RepeatContext { get; set; } = new Dictionary<string, RepeatContext>();
    }

    internal class RepeatContext
    {
        public uint Count { get; set; }
        public StepDefinition BeginStepDefinition { get; set; }
        public StepDefinition EndStepDefinition { get; set; }
    }
}