Feature: Admin creates users

Scenario Outline: Admin creates a new user successfully
	Given I am logged in with username "admin1" and password "admin123"
	When I create user with unique username "<username>", password "<password>", and role "<role>"
	Then the user should be created successfully

Examples:
	| username  | password     | role      |
	| teacher   | teacher123   | teacher   |
	| moderator | moderator123 | moderator |
	| parent    | parent123    | parent    |


Scenario Outline: Admin fails to create user due to invalid data
	Given I am logged in with username "admin1" and password "admin123"
	When I create user with username "<username>", password "<password>", and role "<role>"
	Then the user creation should fail with message "<expectedMessage>"

Examples:
	| username | password | role    | expectedMessage  |
	|          | pass123  | teacher | Invalid username |
	| user123  |          | parent  | Invalid password |
	| user123  | pass123  |         | Invalid role     |
	| user123  | pass123  | adminnn | Invalid role     |
	|          |          |         | Invalid role     |

Scenario Outline: Role, other than admin, fails to create user
	Given I am logged in with username "<username>" and password "<password>"
	When I create user with username "padre", password "padre123", and role "parent"
	Then I should receive response with status 'forbidden' and an error message "Only admin can create users"

Examples:
	| username  | password  |
	| teacher1  | teacher1  |
	| moderator | moderator |
	| parent1   | parent1   |