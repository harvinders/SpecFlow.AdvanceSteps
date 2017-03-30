using System.Collections.Generic;
using System.Threading;
using SpecFlow.AdvanceSteps;

namespace TechTalk.SpecFlow
{
    public static class ScenarioContextExtentions
    {
        public static IEnumerable<StepDefinition> GetAllSteps(this ScenarioContext context)
        {
            return ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].Steps;
        }
    }
}