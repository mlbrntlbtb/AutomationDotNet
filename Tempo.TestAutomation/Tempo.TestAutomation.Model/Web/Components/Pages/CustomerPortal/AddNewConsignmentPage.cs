using Datacom.TestAutomation.Common.Extensions;
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.DTOs.CustomerPortal.AddNewConsignment;
using Tempo.TestAutomation.Model.Utilities;
using Tempo.TestAutomation.Model.Web.Components.Elements;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Components.PageContainers.CustomerPortal.AddNewConsignment;
using Tempo.TestAutomation.Model.Web.Locators.Containers.CustomerPortal.AddNewConsignment;
using Tempo.TestAutomation.Model.Web.Locators.Pages.CustomerPortal;

namespace Tempo.TestAutomation.Model.Web.Components.Pages.CustomerPortal
{
    public class AddNewConsignmentPage : TempoBasePage<AddNewConsignmentPage>, ILoadable<AddNewConsignmentPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingProgressBar loadingProgressBar;
        public AddNewConsignmentPage(IWebDriver driver, LoadingProgressBar loadingProgressBar, DropDownListContainer dropDownList)
            : base(driver)
        {
            this.driver = driver;
            this.loadingProgressBar = loadingProgressBar;
            this.dropDownList = dropDownList;
        }

        public void ClickConsignmentNote()
        {
            driver.GetElement(AddNewConsignmentPageLocators.ConsignmentConfirmation.Link.ConsignmentNote).Click();
        }

        public void ClickSubmit()
        {
            driver.GetElement(AddNewConsignmentPageLocators.Button.Submit).Click();
        }
        public void FillAddItemDetails(AddItemDetails addItemDetails, int index)
        {
            //Get target Add Item Form by index
            AddItemContainer addItemContainer = new AddItemContainer(driver.GetElement(AddItemContainerLocators.Container));
            IWebElement addItemForm = addItemContainer.GetAddItemFormByIndex(index);

            IWebElement packageType = addItemForm.GetElement(AddNewConsignmentPageLocators.AddItemDetails.DropDownList.PackageType);
            packageType.Click();
            dropDownList.SelectByText(addItemDetails.PackageType!);

            IWebElement quantity = addItemForm.GetElement(AddNewConsignmentPageLocators.AddItemDetails.TextBox.Quantity);
            quantity.SendKeys(addItemDetails.Quantity!, true);

            IWebElement weight = addItemForm.GetElement(AddNewConsignmentPageLocators.AddItemDetails.TextBox.Weight);
            weight.SendKeys(addItemDetails.Weight!, true);

            IWebElement length = addItemForm.GetElement(AddNewConsignmentPageLocators.AddItemDetails.TextBox.Length);
            length.SendKeys(addItemDetails.Length!, true);

            IWebElement width = addItemForm.GetElement(AddNewConsignmentPageLocators.AddItemDetails.TextBox.Width);
            width.SendKeys(addItemDetails.Width!, true);

            IWebElement height = addItemForm.GetElement(AddNewConsignmentPageLocators.AddItemDetails.TextBox.Height);
            height.SendKeys(addItemDetails.Height!, true);

            IWebElement volume = addItemForm.GetElement(AddNewConsignmentPageLocators.AddItemDetails.TextBox.Volume);
            volume.SendKeys(addItemDetails.Volume!, true);
        }

        public void FillDeliveryDetails(DeliveryDetails deliveryDetails)
        {
            IWebElement contactName = driver.GetElement(AddNewConsignmentPageLocators.DeliveryDetails.TextBox.ContactName);
            contactName.SendKeys(deliveryDetails.ContactName!, true);

            IWebElement company = driver.GetElement(AddNewConsignmentPageLocators.DeliveryDetails.TextBox.Company);
            company.SendKeys(deliveryDetails.Company!, true);

            IWebElement address = driver.GetElement(AddNewConsignmentPageLocators.DeliveryDetails.TextBox.Address);
            address.SendKeys(deliveryDetails.Address!, true);

            IWebElement email = driver.GetElement(AddNewConsignmentPageLocators.DeliveryDetails.TextBox.Email);
            email.SendKeys(deliveryDetails.Email!, true);

            CheckBox saveNewContact = new CheckBox(driver.GetElement(AddNewConsignmentPageLocators.DeliveryDetails.CheckBox.SaveNewContact), driver);
            saveNewContact.SetCheckBoxState(deliveryDetails.SaveNewContact!);
        }

        public void FillServiceDetails(ServiceDetails serviceDetails)
        {
            IWebElement service = driver.GetElement(AddNewConsignmentPageLocators.ServicePane.DropDownList.Service);
            service.Click();
            dropDownList.SelectByText(serviceDetails.Service!);

            IWebElement reference = driver.GetElement(AddNewConsignmentPageLocators.ServicePane.TextBox.Reference);
            reference.SendKeys(serviceDetails.Reference!);

            IWebElement specialInstructions = driver.GetElement(AddNewConsignmentPageLocators.ServicePane.TextBox.SpecialInstructions);
            specialInstructions.SendKeys(serviceDetails.SpecialInstructions!);
        }

        public bool IsConsignmentNoteFileMatchData(string path, AddNewConsignmentDetails addNewConsignmentDetails)
        {
            string PDFContent = PDFUtilities.GetPDFText(path);
            return PDFContent.Contains(addNewConsignmentDetails.FileContentDetails!.Quantity!)
                & PDFContent.Contains(addNewConsignmentDetails.FileContentDetails!.Dimensions!)
                & PDFContent.Contains(addNewConsignmentDetails.FileContentDetails!.Values!);
        }

        public bool IsConsignmentNoteFileValidComplete(string path, AddNewConsignmentDetails addNewConsignmentDetails)
        {
            int countWait = 1;
            int maxWait = 10;
            loadingProgressBar.WaitToDisappear();
            while (countWait <= maxWait)
            {
                if (!FileExtensions.GetLatestFileName(path).Contains(addNewConsignmentDetails.FileName!)
                & !FileExtensions.GetLatestFileExtension(path).Contains(addNewConsignmentDetails.FileExtension!))
                {
                    countWait++;
                    Thread.Sleep(1000);
                }
                else
                    return true;
            }
            return false;
        }

        public bool IsConsignmentSubmitted()
        {
            loadingProgressBar.WaitToDisappear();
            return driver.IsElementPresent(AddNewConsignmentPageLocators.ConsignmentConfirmation.Label.Message) &
                 driver.GetElement(AddNewConsignmentPageLocators.ConsignmentConfirmation.Label.Message)
                 .IsElementTextContains("SUBMITTED", true);
        }
        protected override bool EvaluateLoadedStatus()
        {
            loadingProgressBar.WaitToDisappear();
            bool isHeaderPresent = driver.GetElement(AddNewConsignmentPageLocators.Label.Header)
                .IsElementTextContains("ADD NEW CONSIGNMENT");
            return isHeaderPresent;
        }
    }
}