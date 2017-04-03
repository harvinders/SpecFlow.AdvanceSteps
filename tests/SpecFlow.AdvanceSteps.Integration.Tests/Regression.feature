Feature: Regression
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@enable-regression
Scenario: Add two numbers
	Given I have a calculator
		And I would like to perform the following steps 3 time
	
	Given I have entered 50 into the calculator
		And I have entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen
		And I repeat the steps

	Then I close the calculator and verify steps executed 3 times