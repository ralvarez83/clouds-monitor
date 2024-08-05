Feature: CreateBackup
	In order to have new Backups of the Cloud that I adminitrate
	As a System Administrator
	I want to ask to Cloud and save only the new Backups

Scenario: New Backups return
	Given I have no backups
  When I ask for new Backups to the Cloud
	Then all Backups are saved
