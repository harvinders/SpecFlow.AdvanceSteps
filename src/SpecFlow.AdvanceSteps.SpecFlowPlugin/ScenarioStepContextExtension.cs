using System;
using System.Threading;
using SpecFlow.AdvanceSteps;
using ExecutionContext = SpecFlow.AdvanceSteps.ExecutionContext;

namespace TechTalk.SpecFlow
{
    public static class ScenarioStepContextExtension
    {
        public static StepDefinition CurrentStep(this ScenarioStepContext context)
        {
            if (!(ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].PeekingEnabled 
                || ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].RegressionEnabled))
            {
                throw new Exception($"Please set enable-peeking tag on the scenario before attempting to call the method {nameof(CurrentStep)}");
            }
            return ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].CurrentStep;
        }

        public static StepDefinition NextStep(this ScenarioStepContext context)
        {
            if (!(ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].PeekingEnabled
                  || ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].RegressionEnabled))
            {
                throw new Exception($"Please set enable-peeking tag on the scenario before attempting to call the method {nameof(NextStep)}");
            }
            return ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].NextStep;
        }

        public static StepDefinition PreviousStep(this ScenarioStepContext context)
        {
            if (!(ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].PeekingEnabled
                  || ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].RegressionEnabled))
            {
                throw new Exception($"Please set enable-peeking tag on the scenario before attempting to call the method {nameof(PreviousStep)}");
            }
            return ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].PreviousStep;
        }

        public static void RegisterRepeatCount(this ScenarioStepContext context, string repeatContextName, int count)
        {
            if (!ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].RegressionEnabled)
            {
                throw new Exception($"Please set enable-regression tag on the scenario before attempting to call the method {nameof(RegisterRepeatCount)}");
            }

            var contextDictionary = ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].RepeatContext;

            if (contextDictionary.ContainsKey(repeatContextName))
            {
                throw new ArgumentException($"Repeat context with the name {repeatContextName} is already present, please use a different name");
            }
            contextDictionary[repeatContextName] = new RepeatContext()
            {
                Count = count,
                BeginStepDefinition = context.CurrentStep()
            };
        }

        public static void DecrementRepeatCount(this ScenarioStepContext context, string repeatContextName)
        {
            if (!ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].RegressionEnabled)
            {
                throw new Exception($"Please set enable-regression tag on the scenario before attempting to call the method {nameof(DecrementRepeatCount)}");
            }

            var contextDictionary = ExecutionContextContainer.Contexts[Thread.CurrentThread.ManagedThreadId].RepeatContext;

            if (contextDictionary.ContainsKey(repeatContextName))
            {
                contextDictionary[repeatContextName] = new RepeatContext()
                {
                    Count = contextDictionary[repeatContextName].Count - 1,
                    BeginStepDefinition = contextDictionary[repeatContextName].BeginStepDefinition,
                    EndStepDefinition = context.CurrentStep()
                };
            }
            else
            {
                throw new Exception($"Repeat context with the name {repeatContextName} is not yet set, please call 'RegisterRepeatCount' to set it");
            }
        }
    }
}