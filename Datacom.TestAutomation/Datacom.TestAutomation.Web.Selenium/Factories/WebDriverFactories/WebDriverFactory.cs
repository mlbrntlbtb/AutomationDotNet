using Datacom.TestAutomation.Common;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

namespace Datacom.TestAutomation.Web.Selenium
{
    public class WebDriverFactory
    {
        private readonly WebSettings settings;
        private readonly IServiceProvider serviceProvider;

        public WebDriverFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            settings = serviceProvider.GetRequiredService<WebSettings>();
        }

        public WebDriverFactory(IServiceProvider serviceProvider, WebSettings settings)
        {
            this.serviceProvider = serviceProvider;
            this.settings = settings;
        }

        public virtual IWebDriver Create()
        {
            IWebDriverFactory? factory;

            string name = settings.Browser;

            if (!string.IsNullOrEmpty(settings.RemoteHubServer))
            {
                name = "remote";
            }

            if (settings.ReuseWebDriver)
            {
                name = "reuse";
            }

            factory = serviceProvider!.GetServices<IWebDriverFactory>()
                                      .FirstOrDefault(f => f.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (factory is null)
            {
                throw new ServiceNotRegisteredException($"No factory registered for {settings.Browser} browser.");
            }

            return factory.Create();
        }
    }
}

