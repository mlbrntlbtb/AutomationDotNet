using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public class EventsPageLocators
    {
        public static class EventsFrame
        {
            public static class NavBar
            {
                public static class Button
                {
                    public static By ViewMore => By.CssSelector(".fa.fa-eye.fa-fw");
                    public static By SaveLayout => By.CssSelector("div[class='header-bar-center'] button[data-tooltip='Save Layout']");
                    public static By ClearLayout => By.CssSelector("div[class='header-bar-center'] button[data-tooltip='Clear Layout']");
                    public static By Map => By.CssSelector("div[class='header-bar-center'] button[data-tooltip='Map']");
                }

                public static class Header
                {
                    public static By EventsList => By.CssSelector(".header-bar");
                    public static By EventsHeading => By.CssSelector("div[class='header-bar-center'] i[class='fa fa-truck fa-fw']");
                }
            }

            public static class Table
            {
                public static By Events => By.CssSelector(".main_box");

                public static By EventsTable => By.CssSelector("#eventGrid");

                public static By EventsRow => By.CssSelector("tbody tr");

                public static By EventDetails => By.CssSelector("div[class='column_50 first pad_right'] div[class='panel side_shadow']");

                public static By EventItems => By.CssSelector("div[class='column_50 last pad_left'] div[class='panel side_shadow']");
            }

        }
    }
}