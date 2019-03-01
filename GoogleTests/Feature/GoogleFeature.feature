Feature: GoogleFeature
	In order to search the web
	As a user of the internet
	I want to be able to type into google

	@mytag
	Scenario Outline: Search the web
		Given I am using <browser>
		And I have navigated to Google
		When I type "Test"
		And I submit the form
		Then I should see the results page

			Scenarios:
			| browser           |
			| Chrome            |
			| Firefox           |
			| Internet Explorer |
			| Edge              |