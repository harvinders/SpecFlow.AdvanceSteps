Feature: All Steps are accessed through ScenarioContext in BeforeSceanrio hook
	In order to hook into interesting events
	As a SpecFlow statement writer
	I want to be have access to steps in BeforeScenario hook

@testof-sceanrio-context
@enable-peeking
Scenario: All Steps are accessed through ScenarioContext in BeforeSceanrio hook
	Given I have a simple given statement
	And I have a another simple given statement
	When I have a simple when statement
	Then I have a simple then statement
