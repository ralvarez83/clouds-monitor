Feature: GetAllMachines
	In order to know the machines last backups status
	As a System Administrator
	I want to get all machines with it's last backup status

  Scenario: Access to a backups list
    Given I send a GET request to '/Machines/'
    Then the response status code should be 200
    And the result should return a not empty list
