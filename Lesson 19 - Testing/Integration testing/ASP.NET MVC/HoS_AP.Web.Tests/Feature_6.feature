Feature: Authentication
    As a Heroes of Storm Administrator
    In order to feel that character control is in secure
    I want to have administration tool secured by username and password

Scenario: Being able to authenticate
    Given Given there is are following users in the system
        | Name  | Password |
        | Megan | 123456   |
    When I navigate to “Listing” page
    Then I should be on “Login” page
    When I fill in controls as follows
            | Name     | Value  |
            | UserName | Megan  |
            | Password | 123456 |
        And I click "Sign In" button
    Then I should be on “Listing” page