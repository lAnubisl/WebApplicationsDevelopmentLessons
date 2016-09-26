Feature: Add new character
    As a Heroes of Storm Administrator
    In order to deliver new character for gamers
    I want to be able to add new character into the system.
Scenario: Beeign able to add new character
    Given there are the following characters in system
        | Name    | Price | Type     | Active | Deleted |
        And I am logged in as “Megan”
    When I navigate to “Add” page
        And I fill in controls as follows
            | Name  | Value    |
            | Name  | Zeratul  |
            | Price | 10.0     |
            | Type  | Assassin |
        And I click "Add" button
    Then I should be on “Listing” page
        And I should see character in list
        | Name    | Price | Type     | Active | Deleted |
        | Zeratul | 10.00 | Assassin | True   | False   |
