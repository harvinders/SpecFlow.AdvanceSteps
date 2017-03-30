using System;
using TechTalk.SpecFlow;
using Xunit;

namespace SpecFlow.AdvanceSteps.Integration.Tests
{
    [Binding]
    public class PreviousNextThroughScenarioStepContextSteps
    {
        private readonly ScenarioContext scenarioContext;

        public PreviousNextThroughScenarioStepContextSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [When(@"I successfully enquire about the current statement")]
        public void WhenISuccessfullyEnquireAboutTheCurrentStatement()
        {
            Assert.Equal("I successfully enquire about the current statement",
                this.scenarioContext.StepContext.CurrentStep().Text);
        }

        [When(@"I successfully enquire about the next statement")]
        public void WhenISuccessfullyEnquireAboutTheNextStatement()
        {
            Assert.Equal("I successfully enquire about the previous statement",
                this.scenarioContext.StepContext.NextStep().Text);
        }

        [When(@"I successfully enquire about the previous statement")]
        public void WhenISuccessfullyEnquireAboutThePreviousStatement()
        {
            Assert.Equal("I successfully enquire about the next statement",
                this.scenarioContext.StepContext.PreviousStep().Text);
        }

        [Given(@"I have a simple given statement with previous step as null")]
        public void GivenIHaveASimpleGivenStatementWithPreviousStepAsNull()
        {
            Assert.Equal(null, this.scenarioContext.StepContext.PreviousStep());
        }

        [Then(@"I have a simple then statement with next step as null")]
        public void ThenIHaveASimpleThenStatementWithNextStepAsNull()
        {
            Assert.Equal(null, this.scenarioContext.StepContext.NextStep());
        }
    }
}