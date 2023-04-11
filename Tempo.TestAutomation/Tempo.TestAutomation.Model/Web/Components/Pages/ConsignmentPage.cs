using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class ConsignmentPage : TempoBasePage<ConsignmentPage>, ILoadable<ConsignmentPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;

        public ConsignmentPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
        }

        public void AddConsignmentDetails(ConsignmentData consignmentData)
        {
            if (consignmentData.ConsignmentDetails!.Customer.IsNotNullOrEmpty())
            {
                driver.GetElement(ConsignmentPageLocators.TextBox.Customer).SendKeys(consignmentData.ConsignmentDetails!.Customer!);
                dropDownList.SelectByText(consignmentData.ConsignmentDetails!.Customer!);
            }
            if (consignmentData.ConsignmentDetails!.Service.IsNotNullOrEmpty())
            {
                driver.GetElement(ConsignmentPageLocators.TextBox.Service).SendKeys(consignmentData.ConsignmentDetails!.Service!);
                dropDownList.SelectByText(consignmentData.ConsignmentDetails!.Service!);
            }

            if (consignmentData.ConsignmentDetails!.Reference.IsNotNullOrEmpty())
                driver.GetElement(ConsignmentPageLocators.TextBox.Reference).SendKeys(consignmentData.ConsignmentDetails!.Reference!);

            if (consignmentData.ConsignmentDetails!.ConsignmentNotes.IsNotNullOrEmpty())
                driver.GetElement(ConsignmentPageLocators.TextBox.ConsignmentNotes).SendKeys(consignmentData.ConsignmentDetails!.ConsignmentNotes!);

            if (consignmentData.ConsignmentDetails!.PaperWorkRequired.IsNotNullOrEmpty())
                driver.GetElement(ConsignmentPageLocators.TextBox.PaperworkRequired).SendKeys(consignmentData.ConsignmentDetails!.PaperWorkRequired!);

            if (consignmentData.ConsignmentDetails!.ScannedItems.IsNotNullOrEmpty())
                driver.GetElement(ConsignmentPageLocators.TextBox.ScannedItems).SendKeys(consignmentData.ConsignmentDetails!.ScannedItems!);

            if (consignmentData.ConsignmentDetails!.DeliveryAddress.IsNotNullOrEmpty())
            {
                driver.GetElement(ConsignmentPageLocators.TextBox.DeliveryAddress).SendKeys(consignmentData.ConsignmentDetails!.DeliveryAddress!);
                dropDownList.SelectByText(consignmentData.ConsignmentDetails!.DeliveryAddress!);
            }

            if (consignmentData.ConsignmentDetails!.DeliveryAddressLookup.IsNotNullOrEmpty())
            {
                driver.GetElement(ConsignmentPageLocators.TextBox.DeliveryAddressLookup).SendKeys(consignmentData.ConsignmentDetails!.DeliveryAddressLookup!);
                dropDownList.SelectByText(consignmentData.ConsignmentDetails!.DeliveryAddressLookup!);
            }

            if (consignmentData.ConsignmentDetails!.Description.IsNotNullOrEmpty())
                driver.GetElement(ConsignmentPageLocators.TextBox.Description).SendKeys(consignmentData.ConsignmentDetails!.Description!);

            if (consignmentData.ConsignmentDetails!.Quantity.IsNotNullOrEmpty())
                driver.GetElement(ConsignmentPageLocators.TextBox.Quantity).SendKeys(consignmentData.ConsignmentDetails!.Quantity!);

            if (consignmentData.ConsignmentDetails!.Volume.IsNotNullOrEmpty())
                driver.GetElement(ConsignmentPageLocators.TextBox.Volume).SendKeys(consignmentData.ConsignmentDetails!.Volume!);

            if (consignmentData.ConsignmentDetails!.Weight.IsNotNullOrEmpty())
                driver.GetElement(ConsignmentPageLocators.TextBox.Weight).SendKeys(consignmentData.ConsignmentDetails!.Weight!);
        }

        public void ClickCancelButton()
        {
            driver.GetElement(ConsignmentPageLocators.Button.Cancel).Click();
        }

        public void ClickEquipmentAddButton()
        {
            driver.GetElement(ConsignmentPageLocators.Button.EquipmentAdd).Click();
        }

        public void ClickEquipmentRowButton(string referenceText, int rowIndex = 0)
        {
            Table ConsignmentEquipmentTable = new Table(driver.GetElement(ConsignmentPageLocators.Table.ConsignmentEquipment), driver);
            ConsignmentEquipmentTable.ClickCellLinkByName("", rowIndex, referenceText);
        }

        public void ClickEquipmentTypeDropdown(int rowIndex = 0)
        {
            Table ConsignmentEquipmentTable = new Table(driver.GetElement(ConsignmentPageLocators.Table.ConsignmentEquipment), driver);
            ConsignmentEquipmentTable.ClickCell("Equipment Type", rowIndex);
        }

        public void ClickSaveButton()
        {
            if (driver.IsElementPresent(ConsignmentPageLocators.Table.Body, TimeSpan.FromMilliseconds(3000)))
            {
                var ConsignmentBody = driver.GetElement(ConsignmentPageLocators.Table.Body);
                ConsignmentBody.Click();
                ConsignmentBody.SendKeys(Keys.PageDown);
            }

            if (driver.IsElementPresent(ConsignmentPageLocators.Button.Save, TimeSpan.FromMilliseconds(3000)))
                driver.GetElement(ConsignmentPageLocators.Button.Save).Click();
        }

        public void GoBackToFreightPage()
        {
            if (driver.IsElementPresent(ConsignmentPageLocators.Table.Consignment, TimeSpan.FromMilliseconds(3000)))
                driver.SwitchToFirstWindow();
        }

        public bool IsAddConsignmentMessageDisplayed()
        {
            loadingWheel.WaitToDisappear();
            return driver.GetElement(ConsignmentPageLocators.Message.NotificationMessage).IsElementTextContains("has been saved");
        }

        public bool IsEquipmentTypePresent(string referenceText)
        {
            return dropDownList.IsDropDownItemPresent(referenceText);
        }

        public bool IsNewRowAdded()
        {
            Table ConsignmentEquipmentTable = new Table(driver.GetElement(ConsignmentPageLocators.Table.ConsignmentEquipment), driver);
            bool isEquipmentTypeEmpty = ConsignmentEquipmentTable.GetCellValue("Equipment Type", 0) == String.Empty;
            bool isUpdatePresent = ConsignmentEquipmentTable.IsCellLinkExisting("", 0, "Update");
            bool isCancelPresent = ConsignmentEquipmentTable.IsCellLinkExisting("", 0, "Cancel");
            return isEquipmentTypeEmpty && isUpdatePresent && isCancelPresent;
        }

        public bool IsPageCancelled()
        {
            bool isCustomerEmpty = driver.GetElement(ConsignmentPageLocators.TextBox.Customer).Text == String.Empty;
            bool isServiceEmpty = driver.GetElement(ConsignmentPageLocators.TextBox.Service).Text == String.Empty;
            bool isAddressEmpty = driver.GetElement(ConsignmentPageLocators.TextBox.DeliveryAddress).Text == String.Empty;
            return IsEquipmentTableEmpty() && isCustomerEmpty && isServiceEmpty && isAddressEmpty;
        }

        public bool IsRowUpdated(string columnName, string referenceText, int rowIndex = 0)
        {
            Table ConsignmentEquipmentTable = new Table(driver.GetElement(ConsignmentPageLocators.Table.ConsignmentEquipment), driver);
            return ConsignmentEquipmentTable.IsCellLinkExisting(columnName, rowIndex, referenceText);
        }

        public void SelectEquipmentType(string referenceText)
        {
            dropDownList.SelectByText(referenceText);
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsAddConsignmentTablePresent = driver.IsElementPresent(ConsignmentPageLocators.Table.Consignment);

            return IsAddConsignmentTablePresent;
        }

        private bool IsEquipmentTableEmpty()
        {
            return !driver.GetElement(ConsignmentPageLocators.Table.ConsignmentEquipment).FindElements(By.CssSelector("tbody tr")).Any();
        }
    }
}