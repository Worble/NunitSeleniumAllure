using System;
using System.IO;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumAllure.Helpers;
using Tests.Abstract;

namespace Tests.GoogleTests
{
    [AllureNUnit]
    [AllureSuite("GoogleTests")]
    [TestFixture]
    public class GoogleTest : TestBase
    {

        [Test]
        public void CanNavigateToGoogle()
        {
            driver.Url = "http://www.google.com";
        }

        [Test]
        public void CanNavigateToGoogle_AndSearchForTest()
        {
            CanNavigateToGoogle();
            var input = driver.FindElement(By.XPath(@"//input[@title='Search']"));
            input.SendKeys("Test");
            input.Submit();
        }

        [Test]
        public void CanNavigateToGoogle_AndSearchForTest_AndClickOnSpeedTestLink()
        {
            CanNavigateToGoogle_AndSearchForTest();
            var link = driver.FindElement(By.XPath(@"//a[@href='http://www.speedtest.net/']"));
            link.Click();
        }
    }
}