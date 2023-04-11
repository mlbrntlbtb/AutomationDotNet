using Datacom.TestAutomation.Common;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Datacom.TestAutomation.Web.Selenium
{
    public class RemoteDriverFactory : IWebDriverFactory
    {
        private readonly IServiceProvider serviceProvider;
        private readonly WebSettings settings;
        public RemoteDriverFactory(IServiceProvider serviceProvider, WebSettings settings)
        {
            this.settings = settings;
            this.serviceProvider = serviceProvider;
        }

        public string Name => "remote";

        public virtual IWebDriver Create()
        {
            SetupDriver();

            return new RemoteWebDriver(new Uri(settings.RemoteHubServer!), GetDriverOptions());
        }

        public virtual string SetupDriver()
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

        public virtual DriverOptions GetDriverOptions()
        {
            IWebDriverFactory? factory = serviceProvider!.GetServices<IWebDriverFactory>()
                                                         .FirstOrDefault(f => f.Name.Equals(settings.Browser, 
                                                                                            StringComparison.InvariantCultureIgnoreCase));
            if (factory is null)
            {
                throw new ServiceNotRegisteredException($"No factory registered for {settings.Browser} browser.");
            }

            return factory.GetDriverOptions();
        }
    }
}

