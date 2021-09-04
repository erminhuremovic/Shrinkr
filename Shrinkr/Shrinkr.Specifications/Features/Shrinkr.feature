Feature: Generate shorter urls
Web application to generate shorter urls

Background: 
	Given the Shrinkr service is running

Scenario: Generate a shorter Url
	When I provide a Url
	Then a shortened Url returned
	And a shortened Url is added to the database

Scenario: Redirect a shortened Url
	Given the follow Urls are present in the database
		| Long Url                                              | Short Url               |
		| https://docs.microsoft.com/en-us/dotnet/fundamentals/ | https://shrink.r/DoReMi |
	When I navigate to "https://shrink.r/DoReMi"
	Then I will be redirected to "https://docs.microsoft.com/en-us/dotnet/fundamentals/"