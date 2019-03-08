using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using SeleniumAllure.Helpers;
using System;

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
            SetupAllureLabel(driverType);
            driver = Driver.SetupDriver(driverType);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                {
                    byte[] screenshot = Driver.GetScreenshot(driverType, driver);
                    AllureLifecycle.Instance.AddAttachment("ScreenShot", "image/png", screenshot, ".png");
                }
            }
            catch (Exception e)
            {
                TestContext.Out.WriteLine($"Getting screenshot for failed test failed, reason: {e.Message}");
            }
            finally
            {
                driver?.Quit();
            }
        }

        private static void SetupAllureLabel(DriverEnum driverType)
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
    }
}