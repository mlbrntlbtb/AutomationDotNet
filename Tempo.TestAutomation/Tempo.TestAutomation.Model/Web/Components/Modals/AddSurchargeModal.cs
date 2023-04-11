using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Modal;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class AddSurchargeModal : TempoBasePage<AddSurchargeModal>, ILoadable<AddSurchargeModal>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;

        public AddSurchargeModal(IWebDriver driver, DropDownListContainer dropDownList)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
        }

        public void ClickSaveButton()
        {
            bool isSaveSurchargeEnabled = driver.GetElement(AddSurchargeModalLocators.Body.Button.Save).Enabled;
            if (isSaveSurchargeEnabled == true)
            {
                driver.GetElement(AddSurchargeModalLocators.Body.Button.Save).Click();
            }
            else
                throw new ElementNotInteractableException("Save button is not enabled");
        }

        public void FillSurchargeForm(OrderData orderDetails)
        {
            if (orderDetails.surcharge!.SurchargeName.IsNotNullOrEmpty())
            {
                driver.GetElement(AddSurchargeModalLocators.Body.TextBox.Surcharge).Click();
                driver.GetElement(AddSurchargeModalLocators.Body.TextBox.Surcharge).SendKeys(orderDetails.surcharge!.SurchargeName!);
                dropDownList.SelectByText(orderDetails.surcharge!.SurchargeName!);
            }
            if (orderDetails.surcharge!.Description.IsNotNullOrEmpty())
                driver.GetElement(AddSurchargeModalLocators.Body.TextBox.Description).SendKeys(orderDetails.surcharge!.Description!);
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool IsSurchargePresent = driver.IsElementPresent(AddSurchargeModalLocators.Body.TextBox.Surcharge);
            return IsSurchargePresent;
        }
    }
}