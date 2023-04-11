using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Dialogs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Tests.Web.Tests
{
    /***************************************************************************************************
    * *************************TFS Ticket number :  1814    *******************************************
    * Given: I'm on the DispatchPOM page
    * When: I choose assign @JobNumber to a Route
    * Then: I expect to see
    *
    *      //Expected Result//
    *
    * - A singular job --- That job allocated to the assigned Route
    * - Multiple jobs --- Multiple jobs are moved to Route in one movement
    *************************************************************************************************/

    [TestFixture]
    public class TFS_Test_Case_1814 : BaseTest
    {
        protected JobActionBranch? DispatchJobActionDataCustomerOne, DispatchJobActionDataCustomerTwo;

        [Test]
        public void TC_1814_DispatchJob()
        {
            //Initialization of Pages and Modal Objects
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();
            AddJobModal dispatchNewJobModal = PageFactory!.GetComponent<AddJobModal>();
            ConfirmationDialog dispatchConfirmationDialog = PageFactory!.GetComponent<ConfirmationDialog>();

            //Initialization of Test Data
            DispatchJobActionDataCustomerOne = TestDataService.Instance.Load<JobActionBranch>("TC_1814_DispatchJob_Customer1");
            DispatchJobActionDataCustomerTwo = TestDataService.Instance.Load<JobActionBranch>("TC_1814_DispatchJob_Customer2");

            //1. Navigate to Tempo Login Page
            //Expected Result: Tempo application should be loaded on the Login Screen page
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //2. Enter Admin Username and Password and Click Login Button
            //Expected Result: Tempo Home/Dashboard page should be loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Logging In authorized user");
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //3. Navigate to the Dispatch Page
            //Expected Result: Dispatch page should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigating to the Dispath Page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //4. Open the Add Job Modal
            //Expected Result: A modal with header title of "Add Job" should appeared
            //========================================================================
            Logger!.LogInformation(Test!, "Opening the Add Job Modal");
            dispatchPage.ClickAddJob();
            dispatchNewJobModal.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Add Job Modal is Loaded");

            //5. Populate the Job Details Form
            //Expected Result: Job details form should be populated
            //========================================================================
            Logger!.LogInformation(Test!, "Filling Up Job Details Form");
            dispatchNewJobModal.CreateJobActionBranch(DispatchJobActionDataCustomerOne!);
            Logger!.LogPass(Test!, "Job details form is filled up", ScreenCaptureService.CaptureScreenImage());

            //6. Save the Job
            //Expected Result: Job should be created and added on the list
            //========================================================================
            Logger!.LogInformation(Test!, "Saving the Job");
            dispatchNewJobModal.ClickSaveButton();
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Job is saved and created", ScreenCaptureService.CaptureScreenImage());

            //7. Assign the created job to the Route
            //Expected Result: Job should be allocated with the route
            //========================================================================
            Logger!.LogInformation(Test!, "Assigning Job to Route");
            dispatchPage.FilterRoute(DispatchJobActionDataCustomerOne.Route!);
            dispatchPage.AssignJobToRoute("1", DispatchJobActionDataCustomerOne.Route!);
            Logger!.LogInformation(Test!, "Job is dragged and dropped to the route");

            //8. Validate the toast message if the job is allocated
            //Expected Result: Toast message should be displayed that the job is allocated
            //========================================================================
            dispatchPage.IsAllocatedToastMessageDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Job should be allocated on the route", ScreenCaptureService.CaptureScreenImage());
            dispatchPage.CloseToastMessage();

            //9. Cancel the Allocation on the job
            //Expected Result: Job should now be unallocated
            //========================================================================
            Logger!.LogInformation(Test!, "Cancelling the Job allocation");
            dispatchPage.ClickRow(0);
            dispatchPage.CancelAllocation();
            dispatchPage.CloseToastMessage();
            Logger!.LogInformation(Test!, "Job allocation is cancelled");

            //10.Open the Add Job Modal
            //Expected Result: A modal with header title of "Add Job" should appeared
            //========================================================================
            Logger!.LogInformation(Test!, "Opening the Add Job Modal");
            dispatchPage.ClickAddJob();
            dispatchNewJobModal.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Add Job Modal is Loaded");

            //11. Populate the Job Details Form
            //Expected Result: Job details form should be populated
            //========================================================================
            Logger!.LogInformation(Test!, "Filling Up Job Details Form");
            dispatchNewJobModal.CreateJobActionBranch(DispatchJobActionDataCustomerTwo!);
            Logger!.LogPass(Test!, "Job details form is filled up", ScreenCaptureService.CaptureScreenImage());

            //12. Save the Job
            //Expected Result: Job should be created and added on the list
            //========================================================================
            Logger!.LogInformation(Test!, "Saving the Job");
            dispatchNewJobModal.ClickSaveButton();
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Job is saved and created", ScreenCaptureService.CaptureScreenImage());

            //12. Suspend Auto Updates
            //Expected Result: Auto updates should be temporarily suspended.
            //========================================================================
            Logger!.LogInformation(Test!, "Suspending Auto Updates");
            dispatchPage.SuspendAutoUpdates(DispatchJobActionDataCustomerOne.SuspendAutoUpdates!);
            Logger!.LogPass(Test!, "Temporarily suspended auto updates", ScreenCaptureService.CaptureScreenImage());

            //13. Assign the two created jobs to the Route
            //Expected Result: Jobs should be allocated with the route
            //========================================================================
            Logger!.LogInformation(Test!, "Assigning Jobs to Route");
            dispatchPage.AssignMultipleJobToRoute(2, DispatchJobActionDataCustomerOne.Route!);
            Logger!.LogInformation(Test!, "Jobs are dragged and dropped to the route");

            //14. Validate the toast message if the job is allocated
            //Expected Result: Toast message should be displayed that the job is allocated
            //========================================================================
            dispatchPage.IsAllocatedToastMessageDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Job should be allocated on the route", ScreenCaptureService.CaptureScreenImage());
            dispatchPage.CloseToastMessage();

            //15. Cancel the two created jobs
            //Expected Result: Jobs should be cancelled
            //========================================================================
            Logger!.LogInformation(Test!, "Cancelling the two created Jobs");
            dispatchPage.ClickRow(0);
            dispatchPage.CancelJob();
            dispatchConfirmationDialog.IsLoaded.Should().BeTrue();
            dispatchConfirmationDialog.ClickYesButton();
            dispatchPage.CloseToastMessage();
            dispatchPage.IsLoaded.Should().BeTrue();
            dispatchPage.ClickRow(0);
            dispatchPage.CancelJob();
            dispatchConfirmationDialog.IsLoaded.Should().BeTrue();
            dispatchConfirmationDialog.ClickYesButton();
            dispatchPage.IsLoaded.Should().BeTrue();
            dispatchPage.CloseToastMessage();
            Logger!.LogPass(Test!, "Cancelled the two created Jobs");

            //16. Logout user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Logging out the current user");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}