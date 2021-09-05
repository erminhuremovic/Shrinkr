# Shrinkr
Shrinkr is a service which takes urls, shortens them, adds the url mapping to a database and returns the shortened url to the client.

## SpecFlow tests
In order to run the tests you need the SpecFlow extension installed on Visual Studio. https://marketplace.visualstudio.com/items?itemName=TechTalkSpecFlowTeam.SpecFlowForVisualStudio

## Usage
Shrinkr can be run straight from Visual Studio using IIS Express and you will be presented with a Swagger UI.

### /Generate
Provide a url within "" and you will be given a shortened url.

### /{token}
This is the API endpoint responsible for handling all shortened urls.
e.g. With Shrinkr running, in a browser navigate to https://localhost:44324/49543a27 and you will be redirected to https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet

### /GetDatabase
Gets all mappings from the database.
> Note: A database is already present in the repository