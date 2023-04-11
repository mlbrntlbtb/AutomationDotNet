using OpenQA.Selenium;

namespace BNZ.TestAutomation.Sample.Model
{
    public static class PayeesPageLocators
    {
        public static class Texts
        {
            public static By PayeesHeader => By.CssSelector(".CustomPage-heading > .Language__container");
            public static By Notification => By.CssSelector("#notification .message");
        }

        public static class Buttons
        {
            public static By Add => By.CssSelector(".CustomSection button.js-add-payee");
        }

        public static class Tables
        {
            public static class PayeeTable
            {
                public static By NameColumn => By.CssSelector(".js-payee-name-column");
                public static By Items => By.CssSelector(".js-payee-item");
                public static By Names => By.CssSelector(".js-payee-name");
                public static By Accounts => By.CssSelector(".js-payee-account");
            }

        }

    }
}
