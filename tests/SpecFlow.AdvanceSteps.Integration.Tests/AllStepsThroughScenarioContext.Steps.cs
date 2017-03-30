using System;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace SpecFlow.AdvanceSteps.Integration.Tests
{
    [Binding]
    public class AllStepsThroughScenarioContextSteps
    {
        private readonly ScenarioContext scenarioContext;

        public AllStepsThroughScenarioContextSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [BeforeScenario("testof-sceanrio-context")]
        public void OnBeforeScenario()
        {
            Assert.Equal(4, this.scenarioContext.GetAllSteps().Count());
        }

        [Given(@"I have a simple given statement")]
        public void GivenIHaveASimpleGivenStatement()
        {
        }

        [Given(@"I have a another simple given statement")]
        public void GivenIHaveAAnotherSimpleGivenStatement()
        {
        }

        [When(@"I have a simple when statement")]
        public void WhenIHaveASimpleWhenStatement()
        {
        }

        [Then(@"I have a simple then statement")]
        public void ThenIHaveASimpleThenStatement()
        {
        }
    }
}