using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Common.Extensions;
using Tempo.TestAutomation.Model.Common;
using Tempo.TestAutomation.Model.DTOs.CustomerPortal.AddNewConsignment;
using Tempo.TestAutomation.Model.Web.Components.Pages;
using Tempo.TestAutomation.Model.Web.Components.Pages.CustomerPortal;
using CustomerPortalHomePage = Tempo.TestAutomation.Model.Web.Components.Pages.CustomerPortal.HomePage;
using CustomerPortalLoginPage = Tempo.TestAutomation.Model.Web.Components.Pages.CustomerPortal.LoginPage;
using TempoHomePage = Tempo.TestAutomation.Model.Web.HomePage;
using TempoLoginPage = Tempo.TestAutomation.Model.Web.LoginPage;

namespace Tempo.TestAutomation.Tests.Web.Tests
{
    [TestFixture]
    public class TFS_Test_Case_3596 : BaseTest
    {
        /***************************************************************************************************
        **************************  TFS Ticket number :  3596  *********************************************
        * Given: I have a user/role permission to log in to OpsConsole & Customer Portal
        * When: I go to the @TempoPage
        * Then: I can see the dimensions label, Length, Width, Height with “(m)”
        *
        * TempoPage:
        * Customer Portal - Add Consignment Page
        * Ops Console - Consignment Report – Consignment with tracked item and print as “consignment note”
        ****************************************************************************************************/

        [Test]
        public void TC_3596_AddVerifyNewConsignment()
        {
            //Initialization of Pages
            //========================================================================
            //Tempo - Customer Portal Pages:
            CustomerPortalLoginPage customerPortalLoginPage = PageFactory!.GetComponent<CustomerPortalLoginPage>().Load();
            CustomerPortalHomePage customerPortalHomePage = PageFactory!.GetComponent<CustomerPortalHomePage>();
            AddNewConsignmentPage addNewConsignmentPage = PageFactory!.GetComponent<AddNewConsignmentPage>();
            //Tempo - Main Pages:
            TempoLoginPage tempoLoginPage = PageFactory!.GetComponent<TempoLoginPage>();
            TempoHomePage tempoHomePage = PageFactory!.GetComponent<TempoHomePage>();
            OrdersPage ordersPage = PageFactory!.GetComponent<OrdersPage>();
            EditOrdersPage editOrdersPage = PageFactory!.GetComponent<EditOrdersPage>();

            //Initialization of Test Data
            UserDetails customerPortalUser = TestDataService.Instance.Load<UserDetails>("TestUsers:CustomerPortalUser");
            AddNewConsignmentDetails addNewConsignmentDetails = TestDataService.Instance.LoadFile<AddNewConsignmentDetails>("TC_3596_TestData");

            //Step 1: Verify Tempo - Customer Portal application is loaded
            //Expected Result: Tempo - Customer Portal login page should be loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Verify Tempo - Customer Portal application is loaded");
            customerPortalLoginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Tempo - Customer Portal login page is loaded");

            //Step 2-3: Login valid credentials to Customer Portal login page
            //Expected Result: Customer Portal home page should be displayed after login
            //========================================================================
            Logger!.LogInformation(Test!, "Login valid credentials to Customer Portal login page");
            customerPortalLoginPage.LoginUserCustomer(customerPortalUser);
            customerPortalHomePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Customer Portal home page is displayed");

            //Step 4: Navigate to Add New Consignment page
            //Expected Result: Add New Consignment page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Add New Consignment page");
            customerPortalHomePage.IsTabItemPresent(addNewConsignmentDetails.CustomerPortal_TargetPage!).Should().BeTrue();
            customerPortalHomePage.SelectTabItem(addNewConsignmentDetails.CustomerPortal_TargetPage!);
            addNewConsignmentPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Add New Consignment page is displayed");

            //Step 5-8: Fill up required fields from Delivery Details pane
            //Expected Result: Fields from Delivery Details pane should be filled
            //========================================================================
            Logger!.LogInformation(Test!, "Fill up required fields from Delivery Details pane");
            addNewConsignmentPage.FillDeliveryDetails(addNewConsignmentDetails.DeliveryDetails!);
            Logger!.LogPass(Test!, "Fields from Delivery Details pane has been filled", ScreenCaptureService!.CaptureScreenImage());

            //Step 9: Fill up required fields from Service pane
            //Expected Result: Fields from Service pane should be filled
            //========================================================================
            Logger!.LogInformation(Test!, "Fill up required fields from Service pane");
            addNewConsignmentPage.FillServiceDetails(addNewConsignmentDetails.ServiceDetails!);
            Logger!.LogPass(Test!, "Fields from Service pane has been filled", ScreenCaptureService!.CaptureScreenImage());

            //Step 10-14: Fill up required fields from first Add Item pane
            //Expected Result: Fields from first Add Item pane should be filled
            //========================================================================
            Logger!.LogInformation(Test!, "Fill up required fields from Add Item pane");
            addNewConsignmentPage.FillAddItemDetails(addNewConsignmentDetails.AddItemDetails!, 1);
            Logger!.LogPass(Test!, "Fields from Add Item pane has been filled", ScreenCaptureService!.CaptureScreenImage());

            //Step 15: Submit new consignment entry
            //Expected Result: New consignment entry should be confirmed submitted successfully
            //========================================================================
            Logger!.LogInformation(Test!, "Submit new consignment entry");
            addNewConsignmentPage.ClickSubmit();
            addNewConsignmentPage.IsConsignmentSubmitted().Should().BeTrue();
            Logger!.LogPass(Test!, "New consignment entry has been confirmed submitted successfully", ScreenCaptureService!.CaptureScreenImage());

            //Step 16-19: Verify data from downloaded PDF file from 'Consignment Note' link source
            //Expected Result: Data from the downloaded PDF file should contain dimensions Quantity, Weight, Length, Width, Height with '(m)', Volume
            //========================================================================
            Logger!.LogInformation(Test!, "Verify data from downloaded PDF file from 'Consignment Note' link source");
            addNewConsignmentPage.ClickConsignmentNote();
            addNewConsignmentPage.IsConsignmentNoteFileValidComplete(WebSettings!.DownloadDirectory, addNewConsignmentDetails).Should().BeTrue();
            string latestFile = FileExtensions.GetLatestFilePath(WebSettings!.DownloadDirectory);
            //Test Discrepancy - Step Modified: Included verifying entered values (Quantity, Weight, Length, Width, Height, Volume) aside from Dimensions
            addNewConsignmentPage.IsConsignmentNoteFileMatchData(latestFile, addNewConsignmentDetails).Should().BeTrue();
            Logger!.LogPass(Test!, "Data from the downloaded PDF file contains dimensions Quantity, Weight, Length, Width, Height with '(m)', Volume'");

            //Step 20: Delete downloaded PDF file 'Consignment Note'
            //Expected Result: Downloaded PDF file should be deleted
            //========================================================================
            Logger!.LogInformation(Test!, "Delete downloaded PDF file 'Consignment Note'");
            FileExtensions.DeleteFile(latestFile);
            FileExtensions.IsFileExists(latestFile).Should().BeFalse();
            Logger!.LogPass(Test!, "Downloaded PDF file has been deleted");

            //Step 21: Navigate to Tempo - Main application
            //Expected Result: Tempo - Main login page should be loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Tempo - Main application");
            tempoLoginPage.Load();
            tempoLoginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Tempo - Main login page is loaded");

            //Step 22-23: Login valid credentials to Tempo - Main login page
            //Expected Result: Tempo - Main home page should be displayed after login
            //========================================================================
            Logger!.LogInformation(Test!, "Login valid credentials to Tempo - Main login page");
            tempoLoginPage.LoginUser(AdminUser!);
            tempoHomePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Tempo - Main home page is displayed");

            //Step 24: Navigate to Orders page
            //Expected Result: Orders page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Orders page");
            tempoHomePage.ClickTempoLinkByText(addNewConsignmentDetails.Tempo_TargetPage!);
            ordersPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Orders page is displayed");

            //Step 25: Select first table row from Orders page
            //Expected Result: First table row from Orders page should be selected
            //========================================================================
            Logger!.LogInformation(Test!, "Select first table row from Orders page");
            ordersPage.ClickRowByIndex(addNewConsignmentDetails.OrderRowIndex!);
            ordersPage.IsRowSelected(addNewConsignmentDetails.OrderRowIndex!).Should().BeTrue();
            Logger!.LogPass(Test!, "First table row from Orders page has been selected");

            //Step 26: Navigate to Edit Order page
            //Expected Result: Edit Order page should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Edit Order page");
            ordersPage.ClickEditOrderButton();
            editOrdersPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Edit Order page should be displayed");

            //Step 27-30:  Verify data from downloaded PDF file from 'Consignment Note' item on print menu
            //Expected Result: Data from the downloaded PDF file should contain dimensions Quantity, Weight, Length, Width, Height with '(m)'
            //========================================================================
            Logger!.LogInformation(Test!, "Verify data from downloaded PDF file from 'Consignment Note' item on print menu");
            editOrdersPage.SelectItemPrintMenu(addNewConsignmentDetails.PrintMenu!);
            editOrdersPage.IsConsignmentNoteFileValidComplete(WebSettings!.DownloadDirectory, addNewConsignmentDetails).Should().BeTrue();
            latestFile = FileExtensions.GetLatestFilePath(WebSettings!.DownloadDirectory);
            //Test Discrepancy - Step Modified: Included verifying entered values (Quantity, Weight, Length, Width, Height, Volume) aside from Dimensions
            editOrdersPage.IsConsignmentNoteFileMatchData(latestFile, addNewConsignmentDetails).Should().BeTrue();
            Logger!.LogPass(Test!, "Data from the downloaded PDF file contains dimensions Quantity, Weight, Length, Width, Height with '(m)', Volume");

            //Step 31: Delete downloaded PDF file 'Consignment Note'
            //Expected Result: Downloaded PDF file should be deleted
            //========================================================================
            Logger!.LogInformation(Test!, "Delete downloaded PDF file 'Consignment Note'");
            FileExtensions.DeleteFile(latestFile);
            FileExtensions.IsFileExists(latestFile).Should().BeFalse();
            Logger!.LogPass(Test!, "Downloaded PDF file has been deleted");

            //Step 32: Logout User
            //Expected Result: Login page should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Logout User");
            //Test Discrepancy - Step Added: Included logout step
            tempoHomePage.LogoutUser();
            tempoLoginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is displayed.");
        }
    }
}