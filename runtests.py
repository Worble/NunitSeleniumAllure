from subprocess import call
import os

allurePath = os.path.join(os.getcwd(), 'bin', 'Debug', 'netcoreapp2.0', 'allure-results')
call(["allure", "serve", allurePath], shell=True)