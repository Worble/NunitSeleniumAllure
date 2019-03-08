using OpenQA.Selenium;
using SeleniumAllure.Abstract;
using TechTalk.SpecFlow;

namespace SeleniumAllure
{
    [Binding]
    public class GoogleFeatureSteps : SpecflowTestBase
    {
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

        [When(@"I submit the form")]
        public void WhenIClickTheButton()
        {
            var input = driver.FindElement(By.XPath("//input[@title='Search']"));
            input.Submit();
        }

        [Then(@"I should see the results page")]
        public void ThenIShouldSeeTheResultsPage()
        {
            driver.Url.Contains("https://www.google.com/search?");
        }
    }
}