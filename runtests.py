from subprocess import call

allurePath = "./bin/Debug/netcoreapp2.0/allure-results"

call(["dotnet", "test"])
call("start allure serve" + allurePath + " -h localhost", shell=True)