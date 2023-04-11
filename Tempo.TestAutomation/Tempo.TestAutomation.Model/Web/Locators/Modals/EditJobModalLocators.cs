using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modals
{
    public class EditJobModalLocators
    {
        public static class EditJobModalFrame
        {
            public static class Label
            {
                public static By EditJobHeader => By.CssSelector(".modalContent .head_inner span:first-of-type[class*='column_20']");
            }
        }
        public static class Header
        {
            public static By EditJob => By.XPath("//span[contains(text(),'Edit Job')]");
        }

        public static class Button
        {
            public static By Close => By.XPath("//div[@class='head_inner']//button[@ng-click='cancel()']");
        }
    }
}