using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public class DevicesPageLocators
    {
        public static class DevicesFrame
        {
            public static class Table

            {
                public static By Devices => By.CssSelector("#deviceGrid");

                public static class Button
                {
                    public static By Disable => By.CssSelector("button[data-tooltip='Disable']");
                    public static By Enable => By.CssSelector("button[data-tooltip='Enable']");
                }
            }
        }
        public static class DeviceLogsFrame
        {
            public static class Button
            {
                public static By Apply => By.CssSelector("button[ng-click='performSearch()']");
                public static By DateQuickSelectMenu => By.XPath("//label[contains(.,'Date Quick Select')]/following-sibling::span[@role='listbox']");
            }

            public static class List
            {
                public static By DateQuickSelectMenu => By.CssSelector(".k-animation-container ul[role='listbox']");
            }

            public static class Label
            {
                public static By LogMessage => By.CssSelector(".log_message");
            }
        }

    }    
}
    
