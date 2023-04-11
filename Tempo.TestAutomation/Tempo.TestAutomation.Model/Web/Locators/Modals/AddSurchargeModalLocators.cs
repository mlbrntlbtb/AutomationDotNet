using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modal
{
    public class AddSurchargeModalLocators
    {
        public static class Body
        {
            public static class Button
            {
                public static By Save => By.XPath("//*[@name='orderLineForm']//button[contains(text(),'Save')]");
            }

            public static class TextBox
            {
                public static By Description => By.CssSelector("textarea[name='descriptionInput']");
                public static By Surcharge => By.CssSelector("[name='surchargeLookup'] input");
            }
        }
    }
}