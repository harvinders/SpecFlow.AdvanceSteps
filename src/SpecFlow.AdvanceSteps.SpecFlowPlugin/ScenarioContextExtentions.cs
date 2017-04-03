using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SpecFlow.AdvanceSteps;

namespace TechTalk.SpecFlow
{
    public static class ScenarioContextExtentions
    {
        public static IEnumerable<StepDefinition> GetAllSteps(this ScenarioContext context)
        {
            if (!ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].PeekingEnabled)
            {
                throw new Exception($"Please set enable-peeking tag on the scenario before attempting to call the method {nameof(GetAllSteps)}");
            }

            return ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].Steps.Where( def => !string.IsNullOrEmpty(def.Text));
        }
    }
}