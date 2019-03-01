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
    [TestFixtureSource(typeof(TestFixtureDrivers), "Drivers")]
    public class TestBase
    {
        public IWebDriver driver;
        public DriverEnum driverType;

        public TestBase(DriverEnum driverType)
        {
            this.driverType = driverType;
        }

        [SetUp]
        public void Setup()
        {
            driver = Driver.SetupDriver(driverType);
            driver.Manage().Window.Maximize();
        }

        private void SetupAllureLavel()
        {
            switch (driverType)
            {
                case DriverEnum.Firefox:
                    AllureLifecycle.Instance.UpdateTestCase(e => e.labels.Add(Label.Suite("Firefox")));
                    break;

                case DriverEnum.Chrome:
                    AllureLifecycle.Instance.UpdateTestCase(e => e.labels.Add(Label.Suite("Chrome")));
                    break;

                case DriverEnum.Edge:
                    AllureLifecycle.Instance.UpdateTestCase(e => e.labels.Add(Label.Suite("Microsoft Edge")));
                    break;

                case DriverEnum.InternetExplorer:
                    AllureLifecycle.Instance.UpdateTestCase(e => e.labels.Add(Label.Suite("Internet Explorer")));
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
            driver?.Quit();
        }
    }
}