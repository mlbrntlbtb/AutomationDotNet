using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public class UsersPageLocators
    {
        public static class UsersFrame
        {
            public static class NavBar
            {
                public static class TextBox
                {
                    public static By Search = By.CssSelector("input[ng-model='searchCriteria']");
                }

                public static class Button
                {
                    public static By Search = By.CssSelector("button[data-tooltip='Search']");
                    public static By SetPassword = By.CssSelector("button[data-tooltip='Set Password']");
                }
            }

            public static class Table
            {
                public static By Users = By.CssSelector("#userGrid");
            }
        }
    }
}