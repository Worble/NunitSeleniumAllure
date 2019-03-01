using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SeleniumAllure.Helpers
{
    public static class Driver
    {
        private static readonly string driverDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "drivers");

        public static IWebDriver SetupDriver(DriverEnum driverType)
        {
            IWebDriver driver;
            switch (driverType)
            {
                case DriverEnum.Firefox:
                    driver = new FirefoxDriver(driverDirectory);
                    break;

                case DriverEnum.Chrome:
                    driver = new ChromeDriver(driverDirectory);
                    break;

                case DriverEnum.Edge:
                    var edgeOptions = new EdgeOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Eager
                    };
                    driver = new EdgeDriver(edgeOptions);
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                    break;

                case DriverEnum.InternetExplorer:
                    var ieOptions = new InternetExplorerOptions
                    {
                        EnsureCleanSession = true
                    };
                    driver = new InternetExplorerDriver(driverDirectory, ieOptions);
                    break;

                default:
                    throw new IndexOutOfRangeException("Driver type not recognised");
            }
            return driver;
        }
    }
}