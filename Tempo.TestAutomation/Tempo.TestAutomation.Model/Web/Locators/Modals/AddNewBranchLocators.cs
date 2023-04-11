using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modals
{
    internal class AddNewBranchLocators
    {
        public static class Footer
        {
            public static By Cancel => By.CssSelector("button.k-button.k-primary[ng-click='cancelEntity();$event.stopPropagation();']");
        }

        public static class Header
        {
            public static By AddNewBranch => By.XPath("//div[text()=' Add New Branch ']");
        }

        public static class TabMenu
        {
            public static class DetailsForm
            {
                public static By Code => By.Id("code");
                public static By Description => By.Id("description");
                public static By Key => By.Id("branchKey");
                public static By Name => By.Id("name");
                public static By ShortName => By.Id("shortName");
            }
        }
    }
}