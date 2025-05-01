Feature: Create class

Scenario: Teacher creates a new class

	Given I am logged in with username "teacher1" and password "teacher1"
	When I create a class with name "Math101" and subjects "Math, Algebra, Biology"
	Then the class should be created successfully

Scenario: Admin creates a new class

	Given I am logged in with username "admin1" and password "admin123"
	When I create a class with name "Math101" and subjects "Math, Algebra, Biology"
	Then the request should fail with message "Only teachers can create classes"




