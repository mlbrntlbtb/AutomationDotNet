using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Dialogs
{
    public class UpdateSettingsDialogLocators
    {
        public static class Button
        {
            public static By Cancel = By.CssSelector("div#configurationSettingsEditWindow:last-of-type button[class='k-button']");
            public static By Close = By.CssSelector("div#configurationSettingsEditWindow:last-of-type span[class='k-icon k-i-close']");
            public static By Save = By.CssSelector("div#configurationSettingsEditWindow:last-of-type button[class='k-button k-primary']");
        }

        public static class Label
        {
            public static By Header => By.CssSelector("span#configurationSettingsEditWindow_wnd_title:last-of-type");
        }

        public static class List
        {
            public static By CreateConsignmentOrder = By.CssSelector("div#configurationSettingsEditWindow:last-of-type span[role='listbox']");
        }
    }
}