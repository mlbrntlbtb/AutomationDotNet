using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public class FreightPageLocators
    {
        public static class Message
        {
            public static By NotificationMessage => By.CssSelector("span[ng-bind-html='message.content']");
        }

        public static class Table
        {
            public static By AddConsignment => By.CssSelector("div[class='flex_panel'] div[class='head_inner ng-binding']");
            public static By Freight => By.CssSelector(".column_100.side_shadow.ng-isolate-scope");
            public static By Results => By.CssSelector("tbody tr:nth-child(1)");

            public static class Button
            {
                public static By ItemIDRadio => By.CssSelector("input[type='checkbox'][data-field='itemId']");
                public static By MenuKebab => By.CssSelector(".fas.fa-ellipsis-v.fa-fw");
                public static By Search => By.CssSelector(".fa.fa-search.fa-fw");
            }

            public static class Header
            {
                public static By BarcodeListItem => By.CssSelector("li[role]");
                public static By BarcodeSideMenu => By.CssSelector(".k-column-menu.k-popup.k-group.k-reset.k-state-border-up");
                public static By BarcodeSubMenu => By.CssSelector(".k-group.k-menu-group.k-popup.k-reset.k-state-border-left");
                public static By MenuList => By.CssSelector(".k-group.k-menu-group.k-popup.k-reset.k-state-border-up");
            }

            public static class TextBox
            {
                public static By SearchBox => By.XPath("//form[@ng-submit='doSearch()']//input[1]");
            }
        }
    }
}