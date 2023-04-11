using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modals
{
    public class EditProcessModalLocators
    {
        public static class EditProcessModalFrame
        {
            public static class Button
            {
            }

            public static class Tab
            {
                public static By EditCustomerTab => By.CssSelector("div[data-role='tabstrip']");

                public static By EditConfigurationTab => By.XPath("//*[text()='Configuration']");

                public static class categories

                {
                    public static By AppearanceCategory => By.XPath("//*[text()='Appearance']");
                    public static By AuthorisationAndSigningCategory => By.XPath("//*[text()='Authorisation and Signing']");
                    public static By ContainersAndManifestsCategory => By.XPath("//*[text()='Containers and Manifests']");
                    public static By EndorsementsAndReasonsCategory => By.XPath("//*[text()='Endorsements and Reasons']");
                    public static By EquipmentCategory => By.XPath("//*[text()='Equipment']");
                    public static By FreightAndScanningCategory => By.XPath("//*[text()='Freight and Scanning']");
                    public static By OtherCategory => By.XPath("//*[text()='Other']");

                    public static class AppearanceCategoryFields
                    {
                        public static By ProcessTitleHelpText => By.CssSelector("div[k-content=\"'Specifies the process title text.'\"]");
                    }
                }
            }

            public static class TextBox
            {
                public static By ShortName => By.CssSelector("#short-display-name-input");
            }
        }
    }
}