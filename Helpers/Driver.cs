using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeleniumAllure.Helpers
{
    public static class Driver
    {
        private static readonly string driverDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "drivers");

        private static readonly HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri($"http://localhost:2222"),
        };

        private const int port = 4444;

        public static IWebDriver SetupDriver(DriverEnum driverType)
        {
            IWebDriver driver;
            switch (driverType)
            {
                case DriverEnum.Firefox:
                    var firefoxService = FirefoxDriverService.CreateDefaultService(driverDirectory);
                    firefoxService.Port = port;
                    driver = new FirefoxDriver(firefoxService);
                    break;

                case DriverEnum.Chrome:
                    var chromeService = ChromeDriverService.CreateDefaultService(driverDirectory);
                    chromeService.Port = port;
                    driver = new ChromeDriver(chromeService);
                    break;

                case DriverEnum.Edge:
                    var edgeService = EdgeDriverService.CreateDefaultService();
                    edgeService.Port = port;
                    driver = new EdgeDriver(edgeService);
                    break;

                case DriverEnum.InternetExplorer:
                    var ieService = InternetExplorerDriverService.CreateDefaultService(driverDirectory);
                    ieService.Port = port;
                    var ieOptions = new InternetExplorerOptions
                    {
                        EnsureCleanSession = true
                    };
                    driver = new InternetExplorerDriver(ieService, ieOptions);
                    break;

                default:
                    throw new IndexOutOfRangeException("Driver type not recognised");
            }
            driver.Manage().Window.Maximize();
            return driver;
        }

        public static byte[] GetScreenshot(DriverEnum driverType, IWebDriver driver)
        {
            switch (driverType)
            {
                case DriverEnum.Firefox:
                    var sessionId = ((FirefoxDriver)driver).SessionId.ToString();
                    var base64 = GetFirefoxFullscreenScreenshot(sessionId).Result;
                    return Convert.FromBase64String(base64);

                default:
                    return ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            }
        }

        private async static Task<string> GetFirefoxFullscreenScreenshot(string sessionId)
        {
            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = "http",
                Host = $"localhost",
                Port = port,
                Path = $"/session/{sessionId}/moz/screenshot/full"
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(uriBuilder.ToString()).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var cont = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var dobj = JsonConvert.DeserializeObject<ImageData>(cont);
                return dobj.Value;
            }
            else
            {
                throw new HttpRequestException($"{response.StatusCode.ToString()}: {response.ReasonPhrase}");
            }
        }

        private class ImageData
        {
            public string Value { get; set; }
        }
    }
}