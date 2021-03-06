# Nunit, Selenium and Allure (And Specflow)

This is a quick little setup for Nunit driving Selenium, with report generation in Allure.



# Setup

* Ensure that [.NET Core is installed](https://dotnet.microsoft.com/download).
* Ensure that [Allure commandline is installed](https://docs.qameta.io/allure/#_installing_a_commandline).
* Ensure that [Python is installed](https://www.python.org/downloads/).
	* This is no longer particularly necessary - all python does is run the tests and load allure, which can be done by hand if wanted.
* Ensure that the web drivers in `./drivers` are appropriate for your system (they'll probably need updating anyway).
  *  [Firefox Driver (geckodriver)](https://github.com/mozilla/geckodriver/releases)
  *  [Chrome Driver](https://sites.google.com/a/chromium.org/chromedriver/)
  *  [Internet Explorer](https://selenium-release.storage.googleapis.com/index.html)
  *  [Edge](https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/) (This is now a Windows Feature on Demand, so you won't need to download anything through your browser anymore, just follow the commands on the page)
* The drivers the tests run against are defined in /Helpers/TextFixtureDrivers, comment ones out you won't be using, or add them here

Specflow Specific:

* Allure currently does not support the version of Specflow that works with .NET Core, so no Allure reports for this. This may change when Specflow Core support comes out of beta.
* Install the specflow extension in Visual Studio.
* See https://specflow.org/2018/specflow-3-public-preview-now-available/ for setting up .NET Core for Specflow
* The table to make Gherkin tests run in each browser is tedious to setup for each feature and there is no workaround in the syntax. This either needs automating some other way or evaluation needed as to whether cross-browser testing in Specflow is at all worth it.



# Running Tests

* From project root, run `./runtests.py`. This will run all tests in Chrome, Firefox, Edge and Internet Explorer and serve the report.

* Alternatively, from project root run `dotnet test`, and when the tests are finished, then run `allure serve ./bin/Debug/netcoreapp2.0/allure-results` to serve the report.



# Known Issues

~~* Currently uses the Windows `start` command to start the Allure report, this won't work on other platforms.~~



# ToDo

~~* Starting 4 webservers to display reports is just awkward, look into finding a way to bundle them into one report or similar.~~
