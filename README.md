# SpecFlow.AdvanceSteps
This plugin provides a facility to 
- to peek into step definitions
- to do regression on scenarios


# Installation 
[TODO]
Get it from nuget at [SpecFlow.AdvanceSteps](https://www.nuget.org/packages/SpecFlow.AdvanceSteps/)

# Peek into steps

## Usage
The plugin works by adding extention methods to [ScenarioContext](https://github.com/techtalk/SpecFlow/wiki/ScenarioContext) and `ScenarioStepContext`

To enable peeking add `@enable-peeking` tag to to the scenario definition
```gherkin
@enable-peeking
Scenario: Scenario demonstrating step peeking  
	Given I have a simple given statement
	And I have a another simple given statement
	When I successfully enquire about the current statement
	And I successfully enquire about the next statement
	And I successfully enquire about the previous statement
	Then I have a simple then statement

```

Context | Extension method | Details
--- | --- | ---
[ScenarioContext](https://github.com/techtalk/SpecFlow/wiki/ScenarioContext) | GetAllSteps() | Get all steps information  
[ScenarioStepContext](https://github.com/techtalk/SpecFlow/wiki/ScenarioContext) | GetCurrentStep() | Get details about the currently executing step  
. | GetPreviousStep() | Get details about the previously executed step  
. | GetNextStep() | Get details about the next step to be executed
 
## StepDefinition
The step information is presented as `StepDefinition` instead of `StepInfo` object. However, the information present in `StepDefinition` is same as provided by `StepInfo`. 

## Limitation
- The steps that are invoked from within step bindings are not visible. These invoked steps would have access to Previous and Next steps information, but most likely it would be incorrect. 
- Debugging by adding breakpoint to gherkin file will not allow to jump into the step definition code. This is because of the fact that the steps are executed at the end of the scenario. However it is possible to debug into the steps by putting the breakpoint within the step definition code.

## Example 

```cs
TODO
```

