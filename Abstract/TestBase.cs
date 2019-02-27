using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SeleniumAllure.Helpers;
using System;
using System.IO;

namespace Tests.Abstract
{
    public class TestBase
    {
        private readonly string _currentDirectory = TestContext.CurrentContext.TestDirectory;
        public IWebDriver driver;
        public Driver driverType;

        public TestBase(Driver driverType)
        {
            this.driverType = driverType;
        }

        [SetUp]
        public void Setup()
        {
            InitializeDriver(driverType);
            driver.Manage().Window.Maximize();
        }

        private void InitializeDriver(Driver driverType)
        {
            string driverDirectory = Path.Combine(_currentDirectory, "drivers");
            switch (driverType)
            {
                case Driver.Firefox:
                    AllureLifecycle.Instance.UpdateTestCase(e => e.labels.Add(Label.Suite("Firefox")));
                    driver = new FirefoxDriver(driverDirectory);
                    break;

                case Driver.Chrome:
                    AllureLifecycle.Instance.UpdateTestCase(e => e.labels.Add(Label.Suite("Chrome")));
                    driver = new ChromeDriver(driverDirectory);
                    break;

                case Driver.Edge:
                    AllureLifecycle.Instance.UpdateTestCase(e => e.labels.Add(Label.Suite("Microsoft Edge")));
                    var edgeOptions = new EdgeOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Eager
                    };
                    driver = new EdgeDriver(edgeOptions);
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                    break;

                case Driver.InternetExplorer:
                    AllureLifecycle.Instance.UpdateTestCase(e => e.labels.Add(Label.Suite("Internet Explorer")));
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
            }
            driver.Close();
            driver.Dispose();
        }
    }
}