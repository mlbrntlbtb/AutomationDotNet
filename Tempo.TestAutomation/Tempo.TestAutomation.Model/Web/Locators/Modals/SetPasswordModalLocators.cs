using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modals
{
    public class SetPasswordModalLocators
    {
        public static class ResetPasswordForm
        {
            public static class TextBox
            {
                public static By NewPassword = By.CssSelector("#newPasswordInput");
                public static By RepeatNewPassword = By.CssSelector("#newPasswordRepeatInput");
            }
            
            public static class Button
            {
                public static By Save = By.CssSelector("button[ng-click='savePasswordChange()']");
                public static By Cancel = By.CssSelector("button[ng-click='cancelPasswordChange()']");
            }
        }
    }
}