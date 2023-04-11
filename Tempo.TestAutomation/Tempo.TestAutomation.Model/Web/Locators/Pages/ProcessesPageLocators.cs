using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public static class ProcessesPageLocators
    {
        public static class ProcessFrame
        {
            public static class Table
            {
                public static By ColumnHeader => By.CssSelector(".panel_head");
                public static By DeliveryRecord => By.CssSelector("#eventTypeGrid");
            }

            public static class NavBar
            {
                public static class Button
                {
                    public static By EditProcess => By.CssSelector("button[data-tooltip='Edit']");
                }
            }
        }
    }
}