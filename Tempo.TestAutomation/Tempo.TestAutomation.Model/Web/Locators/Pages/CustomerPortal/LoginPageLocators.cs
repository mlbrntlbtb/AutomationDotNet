using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages.CustomerPortal
{
    public static class LoginPageLocators
    {
        public static class Button
        {
            public static By Login => By.CssSelector("button[class*='primary']");
        }

        public static class Label
        {
            public static By Instructions => By.CssSelector("p[class='large heading']");
        }

        public static class TextBox
        {
            public static By Password => By.CssSelector("input[name='passwordEdit']");
            public static By Username => By.CssSelector("input[name='userNameEdit']");
        }
    }
}