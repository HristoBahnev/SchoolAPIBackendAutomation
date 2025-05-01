Feature: Connect parent to student

Scenario: Admin connects a parent to a student
	Given I am logged in with username "admin1" and password "admin123"
	When I connect parent "LelyaSiika" to student with ID "1e155552-1833-4cad-be9d-acbbbad0b977"
	Then the parent should be connected successfully

Scenario: Teacher connects a parent to a student
	Given I am logged in with username "teacher1" and password "teacher1"
	When I connect parent "LelyaSiika" to student with ID "1e155552-1833-4cad-be9d-acbbbad0b977"
	Then I should receive response with status 'forbidden' and an error message "Only admin can link parent and student"
 
