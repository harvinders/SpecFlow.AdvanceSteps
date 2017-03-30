Feature: Previous and next steps are accessed through ScenarioStepContext 
	In order to hook into interesting events
	As a SpecFlow statement writer
	I want to be have access to steps in BeforeStep hook


@enable-peeking
Scenario: Previous and next steps are accessed through ScenarioStepContext 
	Given I have a simple given statement
	And I have a another simple given statement
	When I successfully enquire about the current statement
	And I successfully enquire about the next statement
	And I successfully enquire about the previous statement
	Then I have a simple then statement

@enable-peeking
Scenario: Previous and next steps are null in boundary case when accessed through ScenarioStepContext 
	Given I have a simple given statement with previous step as null
	Then I have a simple then statement with next step as null
