using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web.Tests
{
    /***************************************************************************************************
     * *************************TFS Ticket number :  5281    *******************************************
     *
     * Given: I have multiple jobs on a single Route
     * When: I select the elipses on the Route and choose to 'Cancel all allocations'
     * Then: all jobs from that truck are removed
     *
     **************************************************************************************************/

    [TestFixture]
    public class TFS_Test_Case_5281 : BaseTest
    {
        [Test]
        public void TC_5281_CancelAllocationToARoute()
        {
            //Initialization of Pages and Modal Objects
            //========================================================================
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();
            AddJobModal dispatchNewJobModal = PageFactory!.GetComponent<AddJobModal>();

            //Initialization of Test Data
            //========================================================================
            JobActionBranch DispatchJobActionDataCustomerOne = TestDataService.Instance.LoadFile<JobActionBranch>("TC_5281_CancelAllocationToARoute", "TC_5281_DispatchJob_Customer1");
            JobActionBranch DispatchJobActionDataCustomerTwo = TestDataService.Instance.LoadFile<JobActionBranch>("TC_5281_CancelAllocationToARoute", "TC_5281_DispatchJob_Customer2");

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
            Logger!.LogInformation(Test!, "Navigating to the Dispatch Page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //4. Cancel existing Route Allocations from the selected route
            //Expected Result: Allocations should be cancelled in a specific route
            //========================================================================
            Logger!.LogInformation(Test!, "Cancelling Allocations if existing");
            dispatchPage.FilterRoute(DispatchJobActionDataCustomerOne.Route!);
            dispatchPage.CancelRouteAllocations();
            Logger!.LogPass(Test!, "Allocations Cancelled");

            //5. Open the Add Job Modal
            //Expected Result: A modal with header title of "Add Job" should appeared
            //========================================================================
            Logger!.LogInformation(Test!, "Opening the Add Job Modal");
            dispatchPage.ClickAddJob();
            dispatchNewJobModal.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Add Job Modal is Loaded");

            //6. Populate the Job Details Form
            //Expected Result: Job details form should be populated
            //========================================================================
            Logger!.LogInformation(Test!, "Filling Up Job Details Form");
            dispatchNewJobModal.CreateJobActionBranch(DispatchJobActionDataCustomerOne!);
            Logger!.LogPass(Test!, "Job details form is filled up", ScreenCaptureService.CaptureScreenImage());

            //7. Save the Job
            //Expected Result: Job should be created and added on the list
            //========================================================================
            Logger!.LogInformation(Test!, "Saving the Job");
            dispatchNewJobModal.ClickSaveButton();
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Job is saved and created", ScreenCaptureService.CaptureScreenImage());

            //8. Assign the created job to the Route
            //Expected Result: Job should be allocated with the route
            //========================================================================
            Logger!.LogInformation(Test!, "Assigning Job to Route");
            dispatchPage.AssignJobToRoute("1", DispatchJobActionDataCustomerOne.Route!);
            Logger!.LogInformation(Test!, "Job is dragged and dropped to the route", ScreenCaptureService.CaptureScreenImage());

            //9. Validate the toast message if the job is allocated
            //Expected Result: Toast message should be displayed that the job is allocated
            //========================================================================
            dispatchPage.IsAllocatedToastMessageDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Job should be allocated on the route", ScreenCaptureService.CaptureScreenImage());
            dispatchPage.CloseToastMessage();

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

            //13. Assign the created job to the Route
            //Expected Result: Job should be allocated with the route
            //========================================================================
            Logger!.LogInformation(Test!, "Assigning Job to Route");
            dispatchPage.AssignJobToRoute("1", DispatchJobActionDataCustomerTwo.Route!);
            Logger!.LogInformation(Test!, "Job is dragged and dropped to the route", ScreenCaptureService.CaptureScreenImage());

            //14 Cancel existing Route Allocations from the selected route
            //Expected Result: Allocations should be cancelled in a specific route
            //========================================================================
            dispatchPage.CancelRouteAllocations();
            dispatchPage.CloseToastMessage();
            Logger!.LogPass(Test!, "Allocations Cancelled", ScreenCaptureService.CaptureScreenImage());

            //15. Logout user from tempo App
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Logging out the current user");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}