Feature: Parent views student grades

Scenario: Parent views grades of associated child

	Given I am logged in with username "LelyaSiika" and password "lelyasiika123"
	When I view grades for student with ID "1e155552-1833-4cad-be9d-acbbbad0b977"
	Then the grades should be displayed successfully

Scenario: Parent cannot view grades of a non-associated student

	Given I am logged in with username "LelyaSiika" and password "lelyasiika123"
	When I view grades for student with ID "43bac5dc-ecba-4826-8b8d-204cecd07b18"
	Then the grades should be not be displayed and an error message "You can't view this student's grades" is displayed

	Scenario: Parent cannot view grades of a non-existing student

	Given I am logged in with username "LelyaSiika" and password "lelyasiika123"
	When I view grades for student with ID "1234566789"
	Then the grades should be not be displayed and an error message "Student not found" is displayed