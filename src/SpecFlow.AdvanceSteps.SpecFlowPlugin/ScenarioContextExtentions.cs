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
                throw new TagNotSetException("enable-peeking",nameof(GetAllSteps));
            }

            return ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].Steps.Where( def => !string.IsNullOrEmpty(def.Text));
        }
    }
}