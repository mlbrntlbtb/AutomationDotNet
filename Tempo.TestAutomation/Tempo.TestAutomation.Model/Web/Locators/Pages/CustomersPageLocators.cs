using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public static class CustomersPageLocators
    {
        public static class CustomersFrame
        {
            public static class NavBar
            {
                public static class Button
                {
                    public static By AddCustomer => By.CssSelector("button[data-tooltip='Add']");
                    public static By EditCustomer => By.CssSelector("button[data-tooltip='Edit']");
                    public static By ClearLayout => By.CssSelector("button[data-tooltip='Clear Layout']");
                    public static By SaveLayout => By.CssSelector("button[data-tooltip='Save Layout']");
                }
            }

            public static class Label
            {
                public static By NotificationMessage => By.CssSelector("span[ng-bind-html='message.content']");
            }

            public static class Table
            {
                public static By Customer => By.CssSelector("#customerGrid");

                public static class Button
                {
                    public static By DetailsColumnSettings => By.CssSelector("a[title='Column Settings']");
                    public static By ColumnFilter => By.CssSelector("button[type='Submit']");
                }

                public static class TextBox
                {
                    public static By ColumnSettingsFilter => By.CssSelector("input[title='Value']");
                }
            }
        }
    }
}