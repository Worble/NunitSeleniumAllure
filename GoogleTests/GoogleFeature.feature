Feature: GoogleFeature
	In order to search the web
	As a user of the internet
	I want to be able to type into google

@mytag
Scenario: Search the web
	Given I have opened by browser
	And I have navigated to Google 
	When I type "Test"
	And I click the "Google Search" button
	Then I should see the results page
