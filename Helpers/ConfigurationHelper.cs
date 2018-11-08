using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;
using Microsoft.Extensions.Configuration.Json;

namespace SeleniumAllure.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional : true)
                .Build();
        }

        public static TestConfiguration GetApplicationConfiguration(string outputPath)
        {
            var configuration = new TestConfiguration();

            var iConfig = GetIConfigurationRoot(outputPath);

            iConfig.Bind(configuration);

            return configuration;
        }
    }
}