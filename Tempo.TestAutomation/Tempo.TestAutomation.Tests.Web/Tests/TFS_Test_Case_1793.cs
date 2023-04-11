using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Dialogs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1793 : BaseTest
    {
        /***************************************************************************************************
        * *************************  TFS Ticket number :  1793   ******************************************
        * Given: I'm on the dispatch page
        * When: I create a job and assign it to a Route
        * Then: the job is created and
        * And: the dispatch shows the newest Job displays at the top of the grid
        * And: the Job is allocated to the designated Route
        *************************************************************************************************/

        [Test]
        public void TC_1793_CreateAndCancelDispatchJob()
        {
            //Initialization of Test Data
            DispatchJobDetails DispatchJobData = TestDataService.Instance.Load<DispatchJobDetails>("TC_1793_CreateAndCancelDispatchJob");

            //10. Load Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //2. Log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            loginPage.LoginUser(AdminUser!);
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //3. Proceed to the dispatch Page
            //Expected Result: Dispatch page is loaded : Dispatch table and route list is displayed
            //========================================================================
            homePage.ClickTempoLinkByText("dispatch");
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //4. Click new job button and fill add job form
            //Expected Result: Dispatch Add new job modal form Fields are filled
            //========================================================================
            dispatchPage.ClickAddJob();
            AddJobModal dispatchNewJobModal = PageFactory!.GetComponent<AddJobModal>().Load();
            dispatchNewJobModal.FillJobForm(DispatchJobData!, true);
            Logger!.LogPass(Test!, "Dispatch Job Modal is Loaded and form is filled", ScreenCaptureService!.CaptureScreenImage());

            //5. Click the save button for the job creation and locate and click it on the dispatch job table
            //Expected Result: The created job is displayed on the dispatch job table on reference to the reference column (Data is unique due to added timestamp)
            //========================================================================
            dispatchNewJobModal.ClickSaveButton();
            dispatchPage.IsLoaded.Should().BeTrue();
            dispatchPage.ClickCreatedJobRow(DispatchJobData!);
            dispatchPage.IsJobExisting().Should().BeTrue();
            Logger!.LogPass(Test!, "Job with reference: '" + DispatchJobData!.JobDetails!.Reference + "' is successfully created and selected on the dispatch job table", ScreenCaptureService.CaptureScreenImage());

            //6. Cancel the created job
            //Expected Result: The created job does not exist on the dispatch job table
            //========================================================================
            dispatchPage.CancelJob();
            ConfirmationDialog dispatchConfirmationDialog = PageFactory!.GetComponent<ConfirmationDialog>();
            dispatchConfirmationDialog.IsLoaded.Should().BeTrue();
            dispatchConfirmationDialog.ClickYesButton();
            dispatchPage.IsLoaded.Should().BeTrue();
            dispatchPage.ClickCreatedJobRow(DispatchJobData!, (int)DispatchJobData!.JobDetails!.RowToCheck!);
            dispatchPage.IsJobExisting().Should().BeFalse();
            Logger!.LogPass(Test!, "Job with reference: '" + DispatchJobData.JobDetails!.Reference + "' is successfully cancelled/deleted on the dispatch job table", ScreenCaptureService.CaptureScreenImage());

            //7. Logout user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}