using OpenQA.Selenium;

namespace Datacom.TestAutomation.Web.Selenium
{
    public interface IWebDriverFactory : IFactory<IWebDriver>
    {
        string Name { get; }
        string SetupDriver();
        DriverOptions GetDriverOptions();
    }
}
