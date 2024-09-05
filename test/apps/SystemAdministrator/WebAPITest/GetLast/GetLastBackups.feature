Feature: GetLastBackups
	In order to know all backups status
	As a System Administrator
	I want to see the last backups of each server

  Scenario: Access to a backups list
    Given I send a GET request to '/Backups/last'
    Then the response status code should be 200
    And the result should return a not empty list
