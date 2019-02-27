namespace SeleniumAllure.Helpers
{
    public static class TestFixtureSource
    {
        private static readonly object[] Drivers = {
            new object[] { Driver.Chrome },
            new object[] { Driver.Firefox },
            new object[] {Driver.Edge},
            new object[] {Driver.InternetExplorer}
        };
    }
}