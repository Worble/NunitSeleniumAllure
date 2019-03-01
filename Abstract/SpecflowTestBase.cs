using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumAllure.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace SeleniumAllure.Abstract
{
    public abstract class SpecflowTestBase
    {
        public IWebDriver driver;

        [Given(@"I am using (.*)")]
        public void GivenIAmUsing(string browser)
        {
            DriverEnum driverType;
            if (!Enum.TryParse(browser, out driverType)) throw new InvalidOperationException("Invalid driver");

            driver = Driver.SetupDriver(driverType);
        }

        [AfterScenario]
        public void Cleanup()
        {
            driver?.Quit();
        }
    }
}