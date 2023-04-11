using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modals
{
    public class EditLocationModalLocators
    {
        public static class EditLocationModalFrame
        {
            public static class Button
            {
                public static By Close => By.CssSelector("a[aria-label='Close']");
            }

            public static class TextBox
            {
                public static By Key => By.CssSelector("#keyInput");
                public static By Address => By.CssSelector("input[name='addressInput']");
            }
        }
    }
}