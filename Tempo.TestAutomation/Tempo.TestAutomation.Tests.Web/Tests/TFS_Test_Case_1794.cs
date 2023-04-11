using Datacom.TestAutomation.Common;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Dialogs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web.Tests
{
    public class TFS_Test_Case_1794 : BaseTest
    {
        /***************************************************************************************************
        * *************************  TFS Ticket number :  1794   ******************************************
        * Given: I'm on the dispatch page
        * When: I create a Order and assign it to a Route
        * Then: the Order is created and the following shows
        * And: the Job is allocated to the designated Route
        *************************************************************************************************/

        [Test]
        public void TC_1794_AddOrderAndDispatchToRoute()
        {
            //Loading and Initialization of Data
            DispatchJobDetails? DispatchJobData = TestDataService.Instance.Load<DispatchJobDetails>("TC_1794_AddOrderAndDispatchToRoute");
            OrderData? OrderDetails = TestDataService.Instance.Load<OrderData>("TC_1794_AddOrderAndDispatchToRoute");

            //Page Elements Initialization
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();
            AddJobModal dispatchNewJobModal = PageFactory!.GetComponent<AddJobModal>().Load();
            AddSurchargeModal addSurchargeModal = PageFactory!.GetComponent<AddSurchargeModal>().Load();
            ConfirmationDialog dispatchConfirmationDialog = PageFactory!.GetComponent<ConfirmationDialog>();

            //Step 1. Navigation to Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //Steps 2-3. Logging-in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            Logger!.LogInformation(Test!, "Logging In authorized user");
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //Step 4. Navigation to the dispatch Page
            //Expected Result: Dispatch page is loaded : Dispatch table and route list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Dispatch page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //Steps 5-19. Creation of a new job button and populating of add job form
            //Expected Result: Dispatch Add new job modal form Fields are filled
            //========================================================================
            Logger!.LogInformation(Test!, "Creating a new job order");
            dispatchPage.ClickAddJob();
            dispatchNewJobModal.CreateOrder(OrderDetails!);
            dispatchNewJobModal.FillJobForm(DispatchJobData!);
            Logger!.LogPass(Test!, "Dispatch Job Modal is Loaded and form is filled", ScreenCaptureService!.CaptureScreenImage());

            //Steps 20-23. Addition of surcharge and populating of surcharge form
            //Expected Result: Surcharge modal form Fields are filled
            //========================================================================
            Logger!.LogInformation(Test!, "Adding Surcharge to the Job Order");
            dispatchNewJobModal.ClickAddSurchargeButton();
            addSurchargeModal.FillSurchargeForm(OrderDetails!);
            addSurchargeModal.ClickSaveButton();
            Logger!.LogPass(Test!, "Surcharge Modal is Loaded and form is filled", ScreenCaptureService!.CaptureScreenImage());

            //Step 24. Saving the job to be added then locate and click on it in the dispatch job table
            //Expected Result: The created job is displayed on the dispatch job table on reference to the reference column (Data is unique due to added timestamp)
            //========================================================================
            Logger!.LogInformation(Test!, "Saving the new Job");
            dispatchNewJobModal.ClickSaveButton();
            Logger!.LogPass(Test!, "Job is saved and created");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Verify if the Job is added on the table", ScreenCaptureService.CaptureScreenImage());

            //Step 25. Selecting the recently created job
            //Expected Result: The recently created job was selected.
            //========================================================================
            Logger!.LogInformation(Test!, "Selecting the newly created Job");
            dispatchPage.ClickCreatedJobRow(DispatchJobData!);
            dispatchPage.IsJobExisting().Should().BeTrue();
            Logger!.LogPass(Test!, "Selected Newly created Job");

            //Steps 26-28. Cancellation of the created job
            //Expected Result: The created job does not exist on the dispatch job table
            //========================================================================
            Logger!.LogInformation(Test!, "Cancelling the Job Order");
            dispatchPage.CancelJob();
            dispatchConfirmationDialog.IsLoaded.Should().BeTrue();
            dispatchConfirmationDialog.ClickYesButton();
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Cancelled the created Job");
            dispatchPage.ClickCreatedJobRow(DispatchJobData!, (int)DispatchJobData!.JobDetails!.RowToCheck!);
            dispatchPage.IsJobExisting().Should().BeFalse();
            Logger!.LogPass(Test!, "Verify if the created Job is cancelled", ScreenCaptureService.CaptureScreenImage());

            //Step 29. Logout of user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Logging out the current user");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}