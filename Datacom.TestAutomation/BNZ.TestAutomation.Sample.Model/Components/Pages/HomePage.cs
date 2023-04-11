using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace BNZ.TestAutomation.Sample.Model
{
    public class HomePage : BasePage<HomePage>, ILoadable<HomePage>
    {
        private readonly ILogger<HomePage> logger;
        private readonly AppSettings settings;
        private readonly IWebDriver driver;

        public HomePage(ILogger<HomePage> logger, AppSettings settings, IWebDriver driver) : base(driver) 
        {
            this.logger = logger;
            this.settings = settings;
            this.driver = driver;
        }

        public HomePage NavigateTo(string text)
        {
            logger.LogInformation("Navigate to {text}", text);
            driver.GetElement(HomePageLocators.Links.Menu).Click();
            driver.GetElements(HomePageLocators.Links.MenuItems, item => item.Text.IsValueEqualsTo(text)).First().Click();
            return this;
        }

        protected override void ExecuteLoad()
        {
            driver.NavigateTo(settings.BaseUrl!);
        }

        protected override bool EvaluateLoadedStatus()
        {
            return driver.IsElementPresent(HomePageLocators.Links.Menu);
        }
    }
}
