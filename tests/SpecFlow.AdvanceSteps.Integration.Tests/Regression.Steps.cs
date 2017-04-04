using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace SpecFlow.PeekSteps.Integration.Tests
{
    [Binding]
    public class RegressionSteps
    {
        private readonly ScenarioContext scenarioContext;
        private readonly List<int> numbers = new List<int>();
        private int result = 0;
        private int count;
        private int addCount;
        public RegressionSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given(@"I have a calculator")]
        public void GivenIHaveACalculator()
        {
        }
        
        [Given(@"I would like to perform the following steps (.*) time")]
        public void GivenIWouldLikeToPerformTheFollowingStepsTime(int count)
        {
            this.scenarioContext.StepContext.RegisterRepeatCount("steps", count);
        }
        
        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int number)
        {
            this.numbers.Add(number);
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            result = this.numbers.Sum();
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            Assert.Equal(p0, result);
        }
        
        [Then(@"I repeat the steps")]
        public void ThenIRepeatTheSteps()
        {
            numbers.Clear();
            this.count++;
            this.scenarioContext.StepContext.DecrementRepeatCount("steps");
        }

        [Then(@"I close the calculator and verify steps executed (.*) times")]
        public void ThenICloseTheCalculatorAndVerifyStepsExecutedTimes(int p0)
        {
            Assert.Equal(this.count, p0);
        }


        [Given(@"I would like to add a number (.*) times")]
        public void GivenIWouldLikeToAddANumberTimes(int p0)
        {
            this.addCount++;
            this.scenarioContext.StepContext.RegisterRepeatCount("addSteps", p0);
        }

        [Then(@"I repeat the add steps")]
        public void ThenIRepeatTheAddSteps()
        {
            this.scenarioContext.StepContext.DecrementRepeatCount("addSteps");
        }

    }
}
