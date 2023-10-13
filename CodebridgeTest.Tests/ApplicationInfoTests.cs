using CodebridgeTest.BL.Settings;

namespace CodebridgeTest.Tests;

public class ApplicationInfoTests
{
    [Fact]
    public void Class_ReturnsExpectedToStringResult()
    {
        var applicationInfo = new ApplicationInfo
        {
            ApplicationName = "Test",
            ApplicationVersion = "1.0.0",
        };

        var result = applicationInfo.ToString();

        Assert.Equal("Test.Version1.0.0", result);
    }
}