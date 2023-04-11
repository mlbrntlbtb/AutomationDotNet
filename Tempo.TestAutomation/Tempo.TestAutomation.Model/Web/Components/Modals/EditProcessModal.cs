using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Modals;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class EditProcessModal : TempoBaseModal<EditProcessModal>, ILoadable<EditProcessModal>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;

        public EditProcessModal(IWebDriver driver, DropDownListContainer dropDownList)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
        }

        public void WaitForElementAppearanceCongurationTab()
        {
            driver.WaitUntilElementIsPresent(EditProcessModalLocators.EditProcessModalFrame.Tab.categories.AppearanceCategory);
        }

        public void ClickConfigurationTab()
        {
            WaitForElementAppearanceCongurationTab();
            driver.GetElement(EditProcessModalLocators.EditProcessModalFrame.Tab.EditConfigurationTab).Click();
        }

        public void ClickOnAppearanceCategory()
        {
            driver.GetElement(EditProcessModalLocators.EditProcessModalFrame.Tab.categories.AppearanceCategory)
                .Click();
        }

        public string ClickOnAppearacnceCategoryTitleHelpText()
        {
            driver.GetElement(EditProcessModalLocators.EditProcessModalFrame.Tab.categories.AppearanceCategoryFields
                .ProcessTitleHelpText).Click();
            var helptext = driver.GetElement(EditProcessModalLocators.EditProcessModalFrame.Tab.categories.AppearanceCategoryFields
               .ProcessTitleHelpText).GetAttribute("k-content");
            return helptext;
        }

        public bool IsConfigurationCategoriesPresent()
        {
            bool IsAppearance = driver.IsElementPresent(EditProcessModalLocators.EditProcessModalFrame.Tab.categories.AppearanceCategory);
            bool IsAuthorisationAndSigning = driver.IsElementPresent(EditProcessModalLocators.EditProcessModalFrame.Tab.categories.AuthorisationAndSigningCategory);
            bool IsContainersAndManifests = driver.IsElementPresent(EditProcessModalLocators.EditProcessModalFrame.Tab.categories.ContainersAndManifestsCategory);
            bool IsEndorsementsAndReasons = driver.IsElementPresent(EditProcessModalLocators.EditProcessModalFrame.Tab.categories.EndorsementsAndReasonsCategory);
            bool IsEquipment = driver.IsElementPresent(EditProcessModalLocators.EditProcessModalFrame.Tab.categories.EquipmentCategory);
            bool IsFreightAndScanning = driver.IsElementPresent(EditProcessModalLocators.EditProcessModalFrame.Tab.categories.FreightAndScanningCategory);
            bool IsOther = driver.IsElementPresent(EditProcessModalLocators.EditProcessModalFrame.Tab.categories.OtherCategory);

            return IsAppearance && IsAuthorisationAndSigning && IsContainersAndManifests
                && IsEndorsementsAndReasons && IsEquipment && IsFreightAndScanning && IsOther;
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool IsEditCustomerTabPresent = driver.IsElementPresent(EditProcessModalLocators.EditProcessModalFrame.Tab.EditCustomerTab);
            bool IsShortNamePresent = driver.IsElementPresent(EditProcessModalLocators.EditProcessModalFrame.TextBox.ShortName);
            bool IsConfigurationDisplayed = driver.IsElementPresent(EditProcessModalLocators.EditProcessModalFrame.Tab.EditConfigurationTab);
            return IsEditCustomerTabPresent && IsShortNamePresent && IsConfigurationDisplayed;
        }
    }
}