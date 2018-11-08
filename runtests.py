from subprocess import call
import os
import json


def runtests(browser):
    with open('appsettings.json', 'r+') as f:
        data = json.load(f)
        data['Driver'] = browser
        f.seek(0)
        json.dump(data, f, indent=4)
        f.truncate()

    with open('allureConfig.json', 'r+') as f:
        data = json.load(f)
        data['allure']['directory'] = "allure-results"  # -" + browser
        f.seek(0)
        json.dump(data, f, indent=4)
        f.truncate()

    call(["dotnet", "test"])
    allurePath = "./bin/Debug/netcoreapp2.0/allure-results-" + browser
    if not os.path.exists(allurePath + '/environment.xml'):
        with open(allurePath + '/environment.xml', 'w') as env:
            env.write(
                "<environment><parameter><key>Browser</key><value>" + browser + "</value></parameter></environment>")
    # call("start allure serve" + allurePath + " -h localhost", shell=True)


runtests("Chrome")
runtests("Firefox")
runtests("Edge")
# runtests("InternetExplorer")
