Feature: Login
Scenario: Successful login with valid credentials
	When I send a login request with username "<username>" and password "<password>"
	Then I should receive a successful response with a token

Examples:
	| username  | password  |
	| admin1    | admin123  |
	| parent1   | parent1   |
	| teacher1  | teacher1  |
	| moderator | moderator |

Scenario Outline: Unuccessful login with invalid credentials
	When I send a login request with username "<username>" and password "<password>"
	Then I should receive a response with status 'unauthorized' and an error message "Incorrect username or password"

Examples:
	| username | password |
	| admin999 | admin999 |
	| nimda1   | admin123 |
	| admin1   | admin321 |
	|          | admin123 |
	| admin1   |          |
