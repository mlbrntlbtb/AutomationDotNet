using Datacom.TestAutomation.Common;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Datacom.TestAutomation.Web.Selenium
{
    public class ReuseWebDriverFactory : IWebDriverFactory
    {
        private readonly IServiceProvider serviceProvider;
        private readonly WebSettings settings;

        public ReuseWebDriverFactory(IServiceProvider serviceProvider, WebSettings settings)
        {
            this.serviceProvider = serviceProvider;
            this.settings = settings;
        }

        public string Name => "reuse";

        public IWebDriver Create()
        {
            SetupDriver();

            var driverOptions = GetDriverOptions();
            var commandExecutor = new HttpCommandExecutor(settings.ExecutorURL, settings.CommandTimeoutSeconds.Seconds());

            return new ReuseWebDriver(settings.SessionID!, commandExecutor, driverOptions);
        }

        public string SetupDriver()
        {
            IWebDriverFactory? factory = serviceProvider!.GetServices<IWebDriverFactory>()
                                                         .FirstOrDefault(f => f.Name.Equals(settings.Browser, 
                                                                                            StringComparison.InvariantCultureIgnoreCase));
            if (factory is null)
            {
                throw new ServiceNotRegisteredException($"No factory registered for {settings.Browser} browser.");
            }

            return factory.SetupDriver();
        }

        public DriverOptions GetDriverOptions()
        {
            IWebDriverFactory? factory = serviceProvider!.GetServices<IWebDriverFactory>()
                                                         .FirstOrDefault(f => f.Name.Equals(settings.Browser, 
                                                                                            StringComparison.InvariantCultureIgnoreCase));
            if (factory is null)
            {
                throw new ServiceNotRegisteredException($"No factory registered for {settings.Browser} browser.");
            }

            return factory.GetDriverOptions(); ;
        }
    }
}
