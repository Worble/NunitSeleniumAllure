# Nunit, Selenium and Allure

This is a quick little setup for Nunit driving Selenium, with report generation in Allure.



# Setup

* Ensure that [Allure commandline is installed](https://docs.qameta.io/allure/#_installing_a_commandline).
* Ensure that [Python is installed](https://www.python.org/downloads/).
* Ensure that the web drivers in ./drivers are appropriate for your system.



# Running Tests

* Run `./runtests.py`. This will run all tests in Chrome, Firefox, Edge and Internet Explorer, and serve Allure reports for each individually.



# Known Issues

* Currently uses the Windows `start` command to start the Allure report, this won't work on other platforms.



# ToDo

* Starting 4 webservers to display reports is just awkward, look into finding a way to bundle them into one report or similar.