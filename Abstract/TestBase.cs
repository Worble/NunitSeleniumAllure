using System;
using System.IO;
using Allure.Commons;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SeleniumAllure.Helpers;

namespace Tests.Abstract
{
    public class TestBase
    {
        private readonly string _currentDirectory = TestContext.CurrentContext.TestDirectory;
        public IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var configuration = ConfigurationHelper.GetApplicationConfiguration(_currentDirectory);
            InitializeDriver(configuration.Driver);
            driver.Manage().Window.Maximize();
        }

        private void InitializeDriver(Driver driverType)
        {
            string driverDirectory = Path.Combine(_currentDirectory, "drivers");
            switch (driverType)
            {
                case Driver.Firefox:
                    driver = new FirefoxDriver(driverDirectory);
                    break;

                case Driver.Chrome:
                    driver = new ChromeDriver(driverDirectory);
                    break;

                case Driver.Edge:
                    var edgeOptions = new EdgeOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Eager
                    };
                    driver = new EdgeDriver(driverDirectory, edgeOptions);
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                    break;

                case Driver.InternetExplorer:
                    var ieOptions = new InternetExplorerOptions
                    {
                        EnsureCleanSession = true
                    };
                    driver = new InternetExplorerDriver(driverDirectory, ieOptions);
                    break;
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
                AllureLifecycle.Instance.AddAttachment("ScreenShot", "image/png", screenshot, ".png");
            };
            driver.Close();
            driver.Dispose();
        }
    }
}