using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages.CustomerPortal
{
    public static class HomePageLocators
    {
        public static class Tab
        {
            public static By Tabs => By.CssSelector("ul[class='nav list']");
        }
    }
}