Feature: Generate shorter urls
Web application to generate shorter urls

Background:
	Given the Shrinkr service is running

Scenario: Generate a shorter Url
	When I provide the Url; "https://docs.microsoft.com/en-us/dotnet/fundamentals/"
	Then I will receive a shortened Url
	And the Url; "https://docs.microsoft.com/en-us/dotnet/fundamentals/" is added to the database

Scenario: Invalid Url provided
	When I provide the Url; "This Is not a valid Url"
	Then I will receive a bad request response

Scenario: Redirect a shortened Url
	Given the following records are present in the database
		| Token    | Long Url                                              |
		| 14436D9F | https://docs.microsoft.com/en-us/dotnet/fundamentals/ |
	When I navigate to "https://localhost/14436D9F"
	Then I will be redirected to "https://docs.microsoft.com/en-us/dotnet/fundamentals/"

Scenario: Invalid shortened Url provided
	Given the following records are present in the database
		| Token | Long Url |
		|       |          |
	When I navigate to "https://localhost/14436D9F"
	Then I will receive a bad request response