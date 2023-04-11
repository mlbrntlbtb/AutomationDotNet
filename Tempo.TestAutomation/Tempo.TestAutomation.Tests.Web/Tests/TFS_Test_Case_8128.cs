using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_8128 : BaseTest
    {
        /***************************************************************************************************
        * *************************TFS Ticket number :  8128    *******************************************
        * Test Case 8128: Add Equipment Type on the Ops Console
        *
        * Given: I'm an Ops Console user on the Equipment page
        * When:  I Choose to Add Equipment Types in the system
        * Then:  The equipment options are visible on the dropdown list when I am creating a consignment
        *
        * Precondition:
        * 1. TestingOnly is already added under the Equipment Types list and is active.
        * 2. TestDeactivated is added under the Equipment Types list and is INACTIVE.
        *
        * Expected:
        * When adding a consignment, the TestingOnly equipment type displays on the dropdown but not the TestDeactivated.
        *
        *************************************************************************************************/

        [Test]
        public void TC_8128_ValidateConsignmentEquipmentOptions()
        {
            //Initialization of Test Data and pages
            //========================================================================
            ConsignmentData consignmentData = TestDataService.Instance.LoadFile<ConsignmentData>("TC_8128_ValidateConsignmentEquipmentOptions");
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();
            ConsignmentPage consignmentPage = PageFactory!.GetComponent<ConsignmentPage>();

            //1. Load Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //2. Log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //3. Proceed to the dispatch Page
            //Expected Result: Dispatch page is loaded : Jobs table is displayed
            //========================================================================
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //4. Click Add Consignment option in Dispatch menu
            //Expected Result: New window Add Consignment should be displayed
            //========================================================================
            dispatchPage.AddConsignment();
            dispatchPage.IsAddConsignmentDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Add Consignment window is displayed");

            //5. Enter Consignment Details
            //Expected Result: Required consignment fields should be populated
            //========================================================================
            consignmentPage.AddConsignmentDetails(consignmentData);
            Logger!.LogPass(Test!, "Consignment fields have been populated");

            //6. Click Add Equipment button
            //Expected Result: Equipment table new row should be created
            //========================================================================
            consignmentPage.ClickEquipmentAddButton();
            consignmentPage.IsNewRowAdded().Should().BeTrue();
            Logger!.LogPass(Test!, "Equipment table new row has been created");

            //7-8. Open Equipment Type dropdown, and select Equipment Type dropdown item TestingOnly
            //Expected Result: Equipment options should be validated, and TestingOnly should be selected
            //========================================================================
            consignmentPage.ClickEquipmentTypeDropdown(0);
            consignmentPage.IsEquipmentTypePresent(consignmentData!.ConsignmentDetails!.EquipmentType!).Should().BeTrue();
            consignmentPage.IsEquipmentTypePresent("TestDeactivated").Should().BeFalse();
            Logger!.LogPass(Test!, "Equipment options have been validated", ScreenCaptureService!.CaptureScreenImage());
            consignmentPage.SelectEquipmentType(consignmentData!.ConsignmentDetails!.EquipmentType!);
            Logger!.LogPass(Test!, "Item " + consignmentData!.ConsignmentDetails!.EquipmentType! + " is selected");

            //9. Click Update button on new Equipment row
            //Expected Result: New row should be updated
            //========================================================================
            consignmentPage.ClickEquipmentRowButton("Update", 0);
            consignmentPage.IsRowUpdated("", "Edit", 0).Should().BeTrue();
            consignmentPage.IsRowUpdated("", "Delete", 0).Should().BeTrue();
            Logger!.LogPass(Test!, "New row has been updated");

            //10. Click Consignment Cancel button
            //Expected Result: All data entered should be cleared
            //========================================================================
            consignmentPage.ClickCancelButton();
            consignmentPage.IsPageCancelled().Should().BeTrue();
            Logger!.LogPass(Test!, "All data entered has been cleared after cancellation", ScreenCaptureService!.CaptureScreenImage());

            //11. Logout user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}