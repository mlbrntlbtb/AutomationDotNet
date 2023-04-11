using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Elements;
using Tempo.TestAutomation.Model.Web.Locators.Pages.CustomerPortal;

namespace Tempo.TestAutomation.Model.Web.Components.Pages.CustomerPortal
{
    public class HomePage : TempoBasePage<HomePage>, ILoadable<HomePage>
    {
        private readonly IWebDriver driver;
        private readonly LoadingProgressBar loadingProgressBar;

        public HomePage(IWebDriver driver, LoadingProgressBar loadingProgressBar)
            : base(driver)
        {
            this.driver = driver;
            this.loadingProgressBar = loadingProgressBar;
        }

        public bool IsTabItemPresent(string referenceText)
        {
            Tab tab = new Tab(driver.GetElement(HomePageLocators.Tab.Tabs), driver);
            return tab.IsTabItemPresent(referenceText);
        }

        public void SelectTabItem(string referenceText)
        {
            Tab tab = new Tab(driver.GetElement(HomePageLocators.Tab.Tabs), driver);
            tab.SelectByText(referenceText);
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingProgressBar.WaitToDisappear();
            bool isTabsPresent = driver.IsElementPresent(HomePageLocators.Tab.Tabs);
            return isTabsPresent;
        }
    }
}