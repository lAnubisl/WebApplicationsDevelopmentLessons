Feature: Feature_7
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Being able to authenticate
    When I navigate to “Login” page
	Then I should not see text "Not Implemented"
	When I click "Forgot Password" button
	Then I should see text "Not Implemented"