namespace SeleniumAllure.Helpers
{
    public static class TestFixtureDrivers
    {
        private static readonly object[] Drivers = {
            new object[] { DriverEnum.Chrome },
            new object[] { DriverEnum.Firefox },
            new object[] { DriverEnum.Edge },
            new object[] { DriverEnum.InternetExplorer }
        };
    }
}