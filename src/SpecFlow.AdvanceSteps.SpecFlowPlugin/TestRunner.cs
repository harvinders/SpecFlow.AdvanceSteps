using System;
using System.Collections.Generic;
using BoDi;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.Tracing;
using System.Linq;

namespace SpecFlow.AdvanceSteps
{
    public class TestRunner : ITestRunner
    {
        private readonly ITestExecutionEngine normalExecutionEngine;
        private readonly ITestExecutionEngine nullExecutionEngine;
        private bool delayedExecution = false;
        private ExecutionContext executionContext;

        //internal LinkedList<Action<ITestExecutionEngine>> StepsToReplay = new LinkedList<Action<ITestExecutionEngine>>();

        public int ThreadId { get; private set; }

        private class NullBindingInvoker : IBindingInvoker
        {
            public object InvokeBinding(IBinding binding, IContextManager contextManager, object[] arguments, ITestTracer testTracer,
                out TimeSpan duration)
            {
                duration = TimeSpan.Zero;
                return null;
            }
        }

        public TestRunner(ITestExecutionEngine executionEngine, IObjectContainer container)
        {
            this.normalExecutionEngine = executionEngine;
            var nullContainer = new ObjectContainer(container);

            nullContainer.RegisterInstanceAs((IBindingInvoker)new NullBindingInvoker());
            this.nullExecutionEngine = nullContainer.Resolve<ITestExecutionEngine>();

            //TODO: check with newer SpecFlow if this is still necessary
            container.RegisterTypeAs<ExecutionContext, ExecutionContext>();
            this.executionContext = container.Resolve<ExecutionContext>();
        }

        public void InitializeTestRunner(int threadId)
        {
            ThreadId = threadId;
        }

        public void OnTestRunStart()
        {
            ExecutionEngine.OnTestRunStart();
        }

        public void OnTestRunEnd()
        {
            ExecutionEngine.OnTestRunEnd();
        }

        public void OnFeatureStart(FeatureInfo featureInfo)
        {
            ExecutionEngine.OnFeatureStart(featureInfo);
        }

        public void OnFeatureEnd()
        {
            ExecutionEngine.OnFeatureEnd();
        }

        public void OnScenarioStart(ScenarioInfo scenarioInfo)
        {
            ExecutionContextContainer.Contexts[ThreadId] = this.executionContext;
            if (scenarioInfo.Tags.Contains("enable-peeking") || scenarioInfo.Tags.Contains("enable-regression"))
                this.delayedExecution = true;

            this.executionContext.Steps.Clear();

            if (this.delayedExecution)
            {
                executionContext.Steps.AddLast(
                    new StepDefinition(e => e.OnScenarioStart(scenarioInfo)));
            }
            else
            {
                ExecutionEngine.OnScenarioStart(scenarioInfo);
            }
            
        }

        public void CollectScenarioErrors()
        {
            if (this.delayedExecution)
            {
                executionContext.Steps.AddLast(
                    new StepDefinition(e => e.OnAfterLastStep()));
            }
            else
            {
                ExecutionEngine.OnAfterLastStep();
            }
        }

        public void OnScenarioEnd()
        {
            executionContext.Steps.AddLast(
                new StepDefinition(e => e.OnScenarioEnd()));

            try
            {
                if (this.delayedExecution)
                {

                    var node = this.executionContext.Steps.First;
                    do
                    {
                        this.executionContext.CurrentStep = node.Value;
                        this.executionContext.PreviousStep = null != node.Previous?.Value.Text? node.Previous?.Value: null;
                        this.executionContext.NextStep = null != node.Next?.Value.Text ? node.Next?.Value : null;

                        node.Value.Action(ExecutionEngine);

                        var endStep =
                            this.executionContext.RepeatContext.FirstOrDefault(
                                p => p.Value.EndStepDefinition == this.executionContext.CurrentStep);

                        if (null != endStep.Key && 0 != endStep.Value.Count)
                        {
                            node = this.executionContext.Steps.Find(endStep.Value.BeginStepDefinition);

                            if (null == node)
                                throw new Exception("Shit happened");
                        }
                    } while (null != (node = node.Next));
                }
            }
            finally
            {
                ExecutionContextContainer.Contexts.TryRemove(ThreadId, out this.executionContext);
            }
        }

        public void Given(string text, string multilineTextArg, Table tableArg, string keyword = null)
        {
            if (this.delayedExecution)
            {
                this.executionContext.Steps.AddLast(new StepDefinition(StepDefinitionType.Given, StepDefinitionKeyword.Given, text, tableArg, multilineTextArg, keyword));
            }
            else
            {
                ExecutionEngine.Step(StepDefinitionKeyword.Given, keyword, text, multilineTextArg, tableArg);
            }
        }

        public void When(string text, string multilineTextArg, Table tableArg, string keyword = null)
        {
            if (this.delayedExecution)
            {
                this.executionContext.Steps.AddLast(new StepDefinition(StepDefinitionType.When, StepDefinitionKeyword.When, text, tableArg, multilineTextArg, keyword));
            }
            else
            {
                ExecutionEngine.Step(StepDefinitionKeyword.When, keyword, text, multilineTextArg, tableArg);
            }
        }

        public void Then(string text, string multilineTextArg, Table tableArg, string keyword = null)
        {
            if (this.delayedExecution)
            {
                this.executionContext.Steps.AddLast(new StepDefinition(StepDefinitionType.Then, StepDefinitionKeyword.Then, text, tableArg, multilineTextArg, keyword));
            }
            else
            {
                ExecutionEngine.Step(StepDefinitionKeyword.Then, keyword, text, multilineTextArg, tableArg);
            }
        }

        public void And(string text, string multilineTextArg, Table tableArg, string keyword = null)
        {
            if (this.delayedExecution)
            {
                this.executionContext.Steps.AddLast(new StepDefinition(this.executionContext.CurrentDefinitionType, StepDefinitionKeyword.And, text, tableArg, multilineTextArg, keyword));
            }
            else
            {
                ExecutionEngine.Step(StepDefinitionKeyword.And, keyword, text, multilineTextArg, tableArg);
            }
        }

        public void But(string text, string multilineTextArg, Table tableArg, string keyword = null)
        {
            if (this.delayedExecution)
            {
                this.executionContext.Steps.AddLast(new StepDefinition(this.executionContext.CurrentDefinitionType, StepDefinitionKeyword.But, text, tableArg, multilineTextArg, keyword));
            }
            else
            {
                ExecutionEngine.Step(StepDefinitionKeyword.But, keyword, text, multilineTextArg, tableArg);
            }
        }

        public void Pending() => ExecutionEngine.Pending();

        public FeatureContext FeatureContext => ExecutionEngine.FeatureContext;

        public ScenarioContext ScenarioContext => ExecutionEngine.ScenarioContext;

        public ITestExecutionEngine ExecutionEngine => this.delayedExecution ? this.nullExecutionEngine : this.normalExecutionEngine;
    }
}