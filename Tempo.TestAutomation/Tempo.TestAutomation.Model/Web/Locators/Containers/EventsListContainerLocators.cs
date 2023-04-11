using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web
{
    public static class EventsListContainerLocators
    {
        public static class Text
        {
            public static By EventsTable => By.CssSelector("div[id='eventGrid'] div[class='k-grid-content k-auto-scrollable']");
        }
    }
}