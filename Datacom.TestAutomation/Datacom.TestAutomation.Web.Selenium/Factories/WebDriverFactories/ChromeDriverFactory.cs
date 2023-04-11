using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Datacom.TestAutomation.Web.Selenium
{
    public class ChromeDriverFactory : IWebDriverFactory
    {
        private readonly WebSettings settings;
        private readonly ILogger<ChromeDriverFactory> logger;
        public ChromeDriverFactory(ILogger<ChromeDriverFactory> logger, WebSettings settings)
        {
            this.settings = settings;
            this.logger = logger;   
        }

        public string Name => "chrome";

        public virtual IWebDriver Create()
        {
            SetupDriver();

            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService(settings.DriverPath);

            var driver = new ChromeDriver(driverService,
                                         (ChromeOptions)GetDriverOptions(),
                                          settings.CommandTimeoutSeconds.Seconds());

            if (settings.EnableEventFiringWebDriver)
            {
                return new EventFiringDriver(logger, driver);
            }
                
            return driver;
        }

        public virtual string SetupDriver()
        {
            return new DriverManager().SetUpDriver(new ChromeConfig());
        }

        public virtual DriverOptions GetDriverOptions()
        {
            var options = new ChromeOptions();
            if (settings.Headless)
            {
                options.AddArgument("headless");
            }
            options.AddArgument("--no-sandbox");
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-ssl-errors=yes");
            options.AddArgument("--ignore-certificate-errors");
            options.AddUserProfilePreference("download.default_directory", settings.DownloadDirectory);
            options.AddUserProfilePreference("profile.cookie_controls_mode", 0);
            options.SetLoggingPreference(LogType.Browser, OpenQA.Selenium.LogLevel.All);

            return options;
        }
    }
}

