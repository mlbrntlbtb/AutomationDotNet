using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Page
{
    public static class HomePageLocators
    {
        public static class Container
        {
            public static By DashboardFlexContainer => By.CssSelector(".dashboard-flex_container");
        }

        public static class Logo
        {
            public static class Text
            {
                public static By DisplayLogo => By.CssSelector("div.pad-top-logo h1, div.brand-container");

                public static By Environment => By.CssSelector("div.pad-top-logo h4:last-of-type, div.container h4:last-of-type");

                public static By ServerVersion => By.CssSelector("div.pad-top-logo h4:first-of-type, div.container h4:first-of-type");
            }
        }

        public static class NavBar
        {
            public static class List
            {
                public static By UserMenu => By.CssSelector(".navbar li.dropdown");
                public static By SidebarMenu => By.CssSelector("ul#side-menu");
            }

            public static class Button
            {
                public static By SidebarButton => By.CssSelector(".pure-toggle-icon");
            }

            public static class Text
            {
                public static By AuthorisedUser => By.CssSelector(".navbar .menuuser");
            }
        }
    }
}