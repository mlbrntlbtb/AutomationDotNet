using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modals
{
    public class ConsignmentModalLocators
    {
        public static class Body
        {
            public static By ParentRoute = By.CssSelector("div[style*='display: block'] ul.k-reset");
            public static By Consignment => By.CssSelector("#consignmentJobWindow");
            public static By CreateJob => By.CssSelector("span[class='k-widget k-dropdown k-header column_70 left'] span[aria-label='select']");
            public static By CreateJobList => By.CssSelector("li[tabindex]");
            public static By JobInputList => By.CssSelector("body > div:nth-child(41)");
        }

        public static class Button
        {
            public static By Save => By.CssSelector(".k-button.k-primary[ng-click='doOk();$event.stopPropagation();']");
        }

        public static class Message
        {
            public static By Notification => By.CssSelector("span[ng-bind-html='message.content']");
        }

        public static class Text
        {
            public static By AssignToRoute => By.CssSelector(".autocomplete-field.column_70.left.k-input");
        }
    }
}