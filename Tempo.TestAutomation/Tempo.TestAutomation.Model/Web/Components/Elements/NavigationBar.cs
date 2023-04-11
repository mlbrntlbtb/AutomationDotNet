using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Components.Elements
{
    public class NavigationBar
    {
        protected IWebDriver driver;

        public NavigationBar(IWebDriver driver)
        {
            this.driver = driver;
        }

        /* Buttons:
         * Save Layout
         * Clear Layout
         * Refresh
         * Map
         * Edit
         * Add
         */

        private By NavBarButtons(string datatooltip) => By.CssSelector($"div[class='panel_head'] button[data-tooltip='{datatooltip}']");

        private By NavMenuItem => By.CssSelector(".no_arrow[role='menubar'],.no_arrow[role='menubar']:nth-of-type(2)");

        private By NavSearchField => By.CssSelector(".ng-pristine input[type='text'][ng-model='controller.searchCriteria'],input[type='text'][ng-model='searchCriteria'],[placeholder='Find a Job']");

        public void NavButton(string datatooltip)
        {
            driver.GetElement(NavBarButtons(datatooltip)).Click();
        }

        public bool IsNavButtonStatus(string datatooltip)
        {
            bool buttonStatus = false;

            if (driver.IsElementEnabled(NavBarButtons(datatooltip)))

            {
                buttonStatus = true;
            }

            return buttonStatus;
        }

        public void NavMenuButton()
        {
            driver.GetElement(NavMenuItem).Click();
        }

        public void NavMenuSearchField(string searchtext)
        {
            driver.GetElement(NavSearchField).Click();
            driver.GetElement(NavSearchField).SendKeys(searchtext);
        }
    }
}