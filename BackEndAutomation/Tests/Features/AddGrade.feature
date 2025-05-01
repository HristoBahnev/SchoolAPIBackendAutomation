Feature: Add a grade

Scenario: Teacher adds a grade to a student
	Given I am logged in with username "teacher1" and password "teacher1"
	When I add grade 5 in subject "Math" to student with ID "1e155552-1833-4cad-be9d-acbbbad0b977"
	Then the grade should be added successfully


Scenario: Teacher adds a grade with invalid value
	Given I am logged in with username "teacher1" and password "teacher1"
	When I add grade 99 in subject "Math" to student with ID "1e155552-1833-4cad-be9d-acbbbad0b977"
	Then the request should fail with message "Invalid grade"
