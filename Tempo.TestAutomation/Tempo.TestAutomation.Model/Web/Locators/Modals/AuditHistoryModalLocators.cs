using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modals
{
    public class AuditHistoryModalLocators
    {
        public static class Button
        {
            public static By Close => By.CssSelector("div[class='k-widget k-window']:last-of-type span[class='k-icon k-i-close']");
            public static By Sort => By.CssSelector("div[class='panel_body audit_history_options']:last-of-type span[role='listbox']");
        }

        public static class Label
        {
            public static By Header => By.CssSelector("span#auditHistoryDialogWindow_wnd_title:last-of-type");
        }

        public static class List
        {
            public static By AuditHistory = By.CssSelector("div[class='audit_history']");
        }
    }
}