using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public class JobWorkflowPageLocators
    {
        public static class JobWorkflowFrame
        {
            public static class Table

            {
                public static By Body => By.TagName("body");
                public static By JobWorkflow => By.CssSelector("#jobWorkflowGrid");
                public static By ColumnSettingsDetails => By.XPath("//th[@data-field='shortDisplayName']//a");
            }

            public static class Button

            {
                public static By ColumnFilter => By.CssSelector("button[type='Submit']");
                public static By Edit => By.CssSelector("button[data-tooltip='Edit']");
                public static By Workflow => By.CssSelector("ul > li.k-item.k-state-default.k-last");
                public static By EditWorkflow => By.CssSelector("button[class='pull-right btn btn-default btn-xs']");
                public static By CanCreate => By.CssSelector("#step-CanCreateManualEvent_94");
                public static By Save => By.XPath("//button[text()='Save']");
            }

            public static class FirstTextBox
            {
                public static By ColumnSettingsFilter => By.CssSelector("input[title='Value']");
            }

            public static class Label
            {
                public static By PopUp => By.XPath("//div[@class='head_inner ng-binding'][contains(.,' Update Existing Job Workflow ')]");
                public static By Delivery => By.CssSelector("label[class='ng-binding']");
                public static By Process => By.XPath("//label[@class='fieldlist-item-label column_15 form_label pad_left required'][contains(.,'Process')]");
            }
        }
    }
}