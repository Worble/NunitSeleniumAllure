# Nunit, Selenium and Allure

This is a quick little setup for Nunit driving Selenium, with report generation in Allure.



# Setup

* Ensure that [.NET Core is installed](https://dotnet.microsoft.com/download).
* Ensure that [Allure commandline is installed](https://docs.qameta.io/allure/#_installing_a_commandline).
* Ensure that [Python is installed](https://www.python.org/downloads/).
	* This is no longer particularly necessary - all python does is run the tests and load allure, which can be done by hand if wanted.
* Ensure that the web drivers in `./drivers` are appropriate for your system (they'll probably need updating anyway).
* The drivers the tests run against are defined in /Helpers/TextFixtureDrivers, comment ones out you won't be using, or add them here

Specflow Specific:

* Install the specflow extension in Visual Studio.
* See https://specflow.org/2018/specflow-3-public-preview-now-available/ for setting up .NET Core for Specflow



# Running Tests

* From project root, run `./runtests.py`. This will run all tests in Chrome, Firefox, Edge and Internet Explorer and serve the report.

* Alternatively, from project root run `dotnet test`, and when the tests are finished, then run `allure serve ./bin/Debug/netcoreapp2.0/allure-results` to serve the report.



# Known Issues

~~* Currently uses the Windows `start` command to start the Allure report, this won't work on other platforms.~~



# ToDo

~~* Starting 4 webservers to display reports is just awkward, look into finding a way to bundle them into one report or similar.~~
