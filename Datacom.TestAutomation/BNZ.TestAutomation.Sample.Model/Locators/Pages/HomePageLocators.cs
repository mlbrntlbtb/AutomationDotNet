using OpenQA.Selenium;

namespace BNZ.TestAutomation.Sample.Model
{
    public static class HomePageLocators
    {
        public static class Links
        {
            public static By Menu => By.CssSelector(".js-main-menu-btn.MenuButton");
            public static By MenuItems => By.CssSelector(".MainMenu-nav li");
        }
    }
}
