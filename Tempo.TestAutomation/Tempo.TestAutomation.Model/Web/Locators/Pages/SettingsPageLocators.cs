using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public class SettingsPageLocators
    {
        public static class Button
        {
            public static By Edit => By.CssSelector("button[data-tooltip='Edit']");
            public static By MenuList => By.CssSelector("ul[data-role='menu']");
            public static By PasswordComplexity => By.CssSelector("button[data-tooltip='Password Complexity']");
        }

        public static class Label
        {
            public static By Header => By.CssSelector("div[class='head_inner']");
        }

        public static class Table
        {
            public static By Settings => By.CssSelector("div[id='configurationSettingsGrid']");
        }
    }
}