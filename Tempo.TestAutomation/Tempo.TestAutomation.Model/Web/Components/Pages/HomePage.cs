using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Page;

namespace Tempo.TestAutomation.Model.Web
{
    public class HomePage : TempoBasePage<HomePage>, ILoadable<HomePage>
    {
        private readonly IWebDriver driver;
        private readonly LoadingWheel loadingWheel;

        public HomePage(ILogger<HomePage> logger, AppSettings settings, IWebDriver driver, WebDriverWait wait, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.loadingWheel = loadingWheel;
        }

        public void ClickTempoLinkByText(string referenceText)
        {
            var linkContainer = new TempoHomePageLinksContainer(driver.GetElement(HomePageLocators.Container.DashboardFlexContainer));
            try
            {
                linkContainer.GetElementByText(referenceText).Click();
            }
            catch (Exception)
            {
                linkContainer.GetElementByText(referenceText).JavaScriptClick();
            }
        }

        public string GetAuthorisedUser()
        {
            return driver.GetElement(HomePageLocators.NavBar.Text.AuthorisedUser).Text.GetStringBetween("Hi ", ",").FirstOrDefault() ?? string.Empty;
        }

        public void LogoutUser()
        {
            driver.GetElement(HomePageLocators.NavBar.List.UserMenu).Click();
            var UserMenuDropDown = new UserMenuDropDownList(driver.GetElement(HomePageLocators.NavBar.List.UserMenu));
            UserMenuDropDown.GetElementByText("Logout").Click();
        }

        public void NavigateSidebarByText(string referenceText)
        {
            driver.GetElement(HomePageLocators.NavBar.Button.SidebarButton).Click();
            var SidebarList = new SidebarMenuList(driver.GetElement(HomePageLocators.NavBar.List.SidebarMenu));
            SidebarList.GetElementByText(referenceText).Click();
        }

        public bool IsEnvironmentDisplayed(out string actualServerVersion)
        {
            actualServerVersion = string.Empty;
            if (driver.GetElement(HomePageLocators.Logo.Text.ServerVersion).Displayed)
            {
                string aServerVersion = driver.GetElement(HomePageLocators.Logo.Text.ServerVersion).Text;// "Server Version";
                actualServerVersion = aServerVersion.Split(':')[1];
            }
            return actualServerVersion != string.Empty;
        }

        public bool IsServerVersionNumberDisplayed(out string actualEnvironment)
        {
            actualEnvironment = string.Empty;
            if (driver.GetElement(HomePageLocators.Logo.Text.Environment).Displayed)
            {
                string aEnvironment = driver.GetElement(HomePageLocators.Logo.Text.Environment).Text;// "Environment" ;
                actualEnvironment = aEnvironment.Split(":")[1];
            }

            return actualEnvironment != string.Empty;
        }

        public void RefreshPage()
        {
            driver.Navigate().Refresh();
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsDashboardPresent = driver.IsElementPresent(HomePageLocators.Container.DashboardFlexContainer);
            return IsDashboardPresent;
        }
    }
}