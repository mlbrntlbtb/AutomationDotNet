using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public class OrdersPageLocators
    {
        public static class Body
        {
            public static class TextBox
            {
                public static By Search => By.CssSelector("input[ng-model='searchCriteria']");
            }

            public static class Button
            {
                public static By Search => By.CssSelector("li[data-tooltip='Search']");
                public static By Edit => By.CssSelector("button[ng-click='editOrder()']");
            }
        }

        
        public static class OrdersFrame
        {
            public static class Footer
            {
                public static class Button
                {
                    public static By ItemsPerPageMenu => By.CssSelector("span[class*='k-pager-sizes'] span[class*='k-i-arrow-60-down']");
                }
            }

            public static class Table
            {
                public static By Orders => By.CssSelector("#orderGrid");
            }
        }
    }
}