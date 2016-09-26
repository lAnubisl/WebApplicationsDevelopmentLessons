Feature: Edit existing character
    As a Heroes of Storm Administrator
    In order to feel control over system state
    I want to be able to edit existing character
Scenario: Beeign able to edit existing character
    Given there are the following characters in system
        | Name    | Price | Type     | Active | Deleted |
        | Zeratul | 10.00 | Assassin | True   | False   |
        And I am logged in as “Megan”
    When I navigate to “Edit Zeratul” page
        And I fill in controls as follows
            | Name   | Value   |
            | Name   | Zeratul |
            | Price  | 12.2    |
            | Type   | Warrior |
            | Active | False   |
        And I click "Save" button
    Then I should be on “Listing” page
        And I should see character in list
        | Name    | Price | Type     | Active | Deleted |
        | Zeratul | 12.20 | Warrior | False   | False   |