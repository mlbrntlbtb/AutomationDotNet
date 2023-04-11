﻿using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modals
{
    public class AddNewCustomerModalLocators
    {
        public static class AddNewCustomerModalFrame
        {
            public static class Button
            {
                public static By Close => By.CssSelector("a[aria-label='Close']");
                public static By Save => By.CssSelector("button[ng-click*='saveEntity']");
                public static By Cancel => By.CssSelector("button[ng-click*='cancelEntity']");
            }

            public static class Tab
            {
                public static By AddCustomerTab => By.CssSelector("div[data-role='tabstrip']");
            }

            public static class TextBox
            {
                public static By ShortName => By.CssSelector("#shortNameInput");
            }
        }
    }
}