using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Containers
{
    public class AuditHistoryListContainerLocator
    {
        public static By AuditHistoryCard => By.CssSelector("div[class*='audit_history_card']");

        public static class Label
        {
            public static By Activity => By.CssSelector("div[class*='audit_history_description']");
            public static By ConfigurationSetting => By.CssSelector("span[class*='audit_history_value']");
            public static By Date => By.CssSelector("div[class*='audit_history_date']");
        }
    }
}