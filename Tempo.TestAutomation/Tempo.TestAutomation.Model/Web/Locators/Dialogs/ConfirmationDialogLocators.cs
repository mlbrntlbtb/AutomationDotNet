using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Dialogs
{
    public class ConfirmationDialogLocators
    {
        public static class Button
        {
            public static By No => By.CssSelector("div[style*='display: block'] #confirmationWindow button[ng-show='noCaption']");
            public static By Yes => By.CssSelector("div[style*='display: block'] #confirmationWindow button[ng-show='yesCaption']");
        }
    }
}