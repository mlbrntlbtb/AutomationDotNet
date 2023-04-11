using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Page
{
    public static class LoginPageLocators
    {
        public static class Button
        {
            public static By Login => By.CssSelector("button[ng-click='doLogin()']");
        }

        public static class Field
        {
            public static By Password => By.Name("password");
            public static By Username => By.Name("username");
        }
    }
}