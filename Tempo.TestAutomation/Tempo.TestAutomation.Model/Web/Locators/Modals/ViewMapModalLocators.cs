using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modals
{
    public class ViewMapModalLocators
    {
        public static class Header
        {
            public static By Map => By.CssSelector("#jobMapWindow_wnd_title");
        }

        public static class Button
        {
            public static By Close => By.XPath("//div[@class='k-widget k-window'][2]//a[@aria-label='Close']");
        }

        
    }
}