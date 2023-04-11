using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public class EditOrdersPageLocators
    {
        public static class Body
        {
            public static class Header
            {
                public static By EditOrder => By.CssSelector(".job_edit_panel_row");
            }

            public static class Button
            {
                public static By Edit => By.CssSelector("[data-tooltip='Edit Job']");
                public static By Map => By.CssSelector("[data-tooltip='Map']");
            }

            public static class Table
            {
                public static By Job => By.CssSelector("#OrderJobsGrid");
            }

            public static class DropDownList
            {
                public static By Print => By.CssSelector("ul[ng-show*='consignmentSummary']");
            }
        }
    }
}