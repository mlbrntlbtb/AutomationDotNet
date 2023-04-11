using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Dialogs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1822 : BaseTest
    {
        /***************************************************************************************************
        * *************************  TFS Ticket number :  1822   ******************************************
        * Given: I'm on the Freight page
        * When: I press @Action
        * Then: I can @ActionResult
        *************************************************************************************************/

        [Test]
        public void TC_1822_FreightFunctions()
        {
            //Initialize Page and Modal Components
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            FreightPage freightPage = PageFactory!.GetComponent<FreightPage>().Load();
            ConsignmentPage consignmentPage = PageFactory!.GetComponent<ConsignmentPage>().Load();
            ConsignmentModal consignmentModal = PageFactory!.GetComponent<ConsignmentModal>().Load();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>().Load();
            ConfirmationDialog dispatchConfirmationDialog = PageFactory!.GetComponent<ConfirmationDialog>();

            //Initialize Test Data
            FreightData FreightData = TestDataService.Instance.Load<FreightData>("TC_1822_FreightFunctions");
            ConsignmentData ConsignmentData = TestDataService.Instance.Load<ConsignmentData>("TC_1822_FreightFunctions");
            DispatchJobDetails DispatchJobData = TestDataService.Instance.Load<DispatchJobDetails>("TC_1793_CreateAndCancelDispatchJob");

            //1. Load Tempo Login Page
            //Expected Result: Tempo Login page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is displayed");

            //2-3. Log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            Logger!.LogInformation(Test!, "Login Valid Credentials");
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //4. Proceed to the Freight Page
            //Expected Result: Freight Page is loaded : Freight table is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Freight Page");
            homePage.ClickTempoLinkByText("freight");
            freightPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Freight Page is Loaded");

            //5-6. Enter a Freight Detail in Search Box
            //Expected Result: Search results is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Search for Freight Details");
            freightPage.SearchFreightDetails(FreightData!);
            freightPage.ClickSearchButton();
            freightPage.IsSearchedResultDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Freight Details is displayed", ScreenCaptureService!.CaptureScreenImage());

            //6-8. Proceed to the Home Page
            //Expected Result: Home Page is loaded : Home Page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Home Page");
            homePage.NavigateSidebarByText("home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Home Page is displayed");

            //9. Proceed to the Freight Page
            //Expected Result: Freight Page is loaded : Freight table is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Freight Page");
            homePage.ClickTempoLinkByText("freight");
            freightPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Freight Page is Loaded");

            //10-14. Select Column Item Id & Save Layout
            //Expected Result: Item ID tick box was ticked and Item ID column was added to the table.
            //========================================================================
            Logger!.LogInformation(Test!, "Select Item ID and Save Layout");
            freightPage.SelectBardcodeItem(FreightData!);
            freightPage.SaveLayout(FreightData!);
            freightPage.IsFreightDetailsSaved().Should().BeTrue();
            Logger!.LogPass(Test!, "Layout saved notification is displayed", ScreenCaptureService!.CaptureScreenImage());

            //15. Proceed to the Home Page
            //Expected Result: Home Page is loaded : Home Page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Home Page");
            homePage.NavigateSidebarByText("home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Home Page is displayed");

            //16. Proceed to the Freight Page
            //Expected Result: Freight Page is loaded : Freight table is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Freight Page");
            homePage.ClickTempoLinkByText("freight");
            freightPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Freight Page is Loaded");

            //17-19. Click Clear Saved Layout
            //Expected Result: Saved Layout should be removed
            //========================================================================
            Logger!.LogInformation(Test!, "clear all existing layout");
            freightPage.ClearLayout(FreightData!);
            freightPage.IsFreightDetailsCleared().Should().BeTrue();
            Logger!.LogPass(Test!, "layout cleared notification is displayed", ScreenCaptureService!.CaptureScreenImage());

            //20-22.  Proceed to Add  Consignment Page
            //Expected Result: Add  Consignment Page should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Add Consignment Page");
            freightPage.AddConsignment(FreightData!);
            freightPage.IsAddConsignmentDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Add Consignment Page should be displayed");

            //23-36. Enter Consignment Details
            //Expected Result: Required consignment fields should be populated
            //========================================================================
            Logger!.LogInformation(Test!, "Fill in Consignment Details");
            consignmentPage.AddConsignmentDetails(ConsignmentData!);
            Logger!.LogPass(Test!, "Consignment details should be populated", ScreenCaptureService!.CaptureScreenImage());

            //37-42. Save and Select a Job Type in Consignment Modal
            //Expected Result: Required consignment fields should be populated
            //========================================================================
            Logger!.LogInformation(Test!, "Save and Select a Job Type in Consignment Modal");
            consignmentPage.ClickSaveButton();
            consignmentPage.IsAddConsignmentMessageDisplayed().Should().BeTrue();
            consignmentModal.SelectCreateJob(ConsignmentData!);
            consignmentModal.AssignToRoute(ConsignmentData!);
            consignmentModal.ClickSaveButton();
            consignmentModal.IsSavedConsignmentMessageDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Consignment details should be saved", ScreenCaptureService!.CaptureScreenImage());

            //43.Go Back to Freight Page
            //Expected Result: Freight Page should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Go Back to Freight Page");
            consignmentPage.GoBackToFreightPage();
            Logger!.LogPass(Test!, "Freight Page should be displayed");

            //44-45. Proceed to the Home Page
            //Expected Result: Home Page is loaded : Home Page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Home Page");
            homePage.NavigateSidebarByText("home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Home Page is displayed");

            //46. Proceed to the Dispatch Page
            //Expected Result: Dispatch Page is loaded : Dispatch table is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Dispatch Page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //47-50. Cancel Jobs that are scheduled on the list
            //Expected Result: Dispatch Page is loaded : Dispatch table is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Cancel Jobs that are scheduled on the list");
            dispatchPage.ClickCreatedJobRow(DispatchJobData!);
            dispatchPage.CancelJob();
            dispatchConfirmationDialog.IsLoaded.Should().BeTrue();
            dispatchConfirmationDialog.ClickYesButton();
            dispatchPage.IsLoaded.Should().BeTrue();
            dispatchPage.WaitToDisappearCancelledMessage();
            Logger!.LogPass(Test!, "Job with reference: '" + DispatchJobData.JobDetails!.Reference + "' is successfully cancelled/deleted on the dispatch job table", ScreenCaptureService.CaptureScreenImage());

            //51-52. Logout user from tempo App
            //Expected Result: Tempo Login page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Logout User to Automation Dev Page");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}