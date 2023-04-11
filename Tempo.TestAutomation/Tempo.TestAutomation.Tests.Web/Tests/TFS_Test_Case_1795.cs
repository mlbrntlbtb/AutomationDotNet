using Datacom.TestAutomation.Common;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web.Tests
{
    public class TFS_Test_Case_1795 : BaseTest
    {
         /**************************************************************************************************
         * ********************************   TFS Ticket number :  1795    *********************************
         * Given: I'm on the dispatch page
         * When:  I create a Consignment (auto-generated con) and assign it to a Customer 
         *        (Note: The Consignment Note is left blank)
         * Then:  the Consignment is created and the following shows:
         *        - Consignment is created
         *        - Option to Create a Job
         *        - Option to assign to Route
         *        - Click on the Order Details link from the Edit consignment will take user to the Edit Order page
         *        - The Order is the latest order on the Orders page with the Consignment number
         *        - The consignment can be search upon on the Freight page
         * ***********************************************************************************************/

        [Test]
        public void TFS_1795_AddConsignment()
        {
            //Loading and Initialization of Data
            //========================================================================
            ConsignmentData? ConsignmentData = TestDataService.Instance.LoadFile<ConsignmentData>("TC_1795_AddConsignment");

            //Page Elements Initialization
            //========================================================================
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();
            ConsignmentPage consignmentPage = PageFactory!.GetComponent<ConsignmentPage>().Load();
            ConsignmentModal consignmentModal = PageFactory!.GetComponent<ConsignmentModal>().Load();

            //Step 1. Navigation to Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page was loaded");

            //Steps 2-3. Logging-in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            Logger!.LogInformation(Test!, "Logging In authorized user");
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //Step 4. Navigation to the Dispatch Page
            //Expected Result: Dispatch page is loaded : Dispatch table and route list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Dispatch page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page was Loaded");

            // Step 5.Navigation to the Add Consignment Page
            //Expected Result: Add Consignment page is loaded from a different tab
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Add Consignment page");
            dispatchPage.SelectDispatchMenuByText("Add Consignment");
            dispatchPage.SwitchToAddConsigneeWindow();
            Logger!.LogPass(Test!, "Add Consignment Page should be displayed", ScreenCaptureService!.CaptureScreenImage());

            //Step 6. Enter Consignment Details
            //Expected Result: Required consignment fields should be populated
            //========================================================================
            Logger!.LogInformation(Test!, "Fill in Consignment Details");
            consignmentPage.AddConsignmentDetails(ConsignmentData!);
            Logger!.LogPass(Test!, "Consignment details should be populated", ScreenCaptureService!.CaptureScreenImage());

            // Step 7. Save and Select a Job Type in Consignment Modal
            //Expected Result: Required consignment fields should be populated
            //========================================================================
            Logger!.LogInformation(Test!, "Save and Select a Job Type in Consignment Modal");
            consignmentPage.ClickSaveButton();
            consignmentPage.IsAddConsignmentMessageDisplayed().Should().BeTrue();
            consignmentModal.SelectCreateJob(ConsignmentData!);
            consignmentModal.AssignToRoute(ConsignmentData!);
            consignmentModal.IsSavedConsignmentMessageDisplayed().Should().BeTrue();
            consignmentModal.ClickSaveButton();
            Logger!.LogPass(Test!, "Consignment details should be saved", ScreenCaptureService!.CaptureScreenImage());

            //Step 8. Go Back to Dispatch Window
            //Expected Result: Dispatch page should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Go Back to Dispatch Page");
            consignmentPage.GoBackToFreightPage();
            Logger!.LogPass(Test!, "Dispatch Page should be displayed");

            //Steps Step 9. Logout of user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Logging out the current user");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}