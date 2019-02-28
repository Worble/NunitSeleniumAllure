using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumAllure.Helpers;
using TechTalk.SpecFlow;
using Tests.Abstract;

namespace SeleniumAllure.GoogleTests
{
    [TestFixtureSource(typeof(TestFixtureDrivers), "Drivers")]
    [Binding]
    public class GoogleFeatureSteps : TestBase
    {
        public GoogleFeatureSteps(Driver driverType) : base(driverType)
        {
        }

        [Given(@"I have opened by browser")]
        public void GivenIHaveOpenedByBrowser()
        {
        }

        [Given(@"I have navigated to Google")]
        public void GivenIHaveNavigatedToGoogle()
        {
            driver.Url = "http://www.google.com";
        }

        [When(@"I type ""(.*)""")]
        public void WhenIType(string searchTerm)
        {
            var input = driver.FindElement(By.XPath("//input[@title='Search']"));
            input.SendKeys(searchTerm);
        }

        [When(@"I click the ""(.*)"" button")]
        public void WhenIClickTheButton(string buttonContent)
        {
            var button = driver.FindElement(By.XPath("/html/body/div/div[3]/form/div[2]/div/div[3]/center/input[1]"));
            button.Click();
        }

        [Then(@"I should see the results page")]
        public void ThenIShouldSeeTheResultsPage()
        {
            driver.Url.Contains("https://www.google.com/search?");
        }
    }
}