using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Dialogs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1941 : BaseTest
    {
        /***************************************************************************************************
        * *************************  TFS Ticket number :  1941   ******************************************
        * Given: a Service is created and is applied to a Customer
        * When: the customer creates an Order
        * Then: the Service is available to select obeying the configurations it has been set to
        *
        * Expected Results
        * No errors are given
        * Event is sent to the server (viewed on the Events page - View more show details of the event)
        * All options configured on Service platform as expected (check the specific Service are correct)
        *************************************************************************************************/

        [Test]
        public void TC_1941_CreateService()
        {
            //Initialization of Pages and Modal Objects
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();
            AddJobModal dispatchNewJobModal = PageFactory!.GetComponent<AddJobModal>();
            ConfirmationDialog dispatchConfirmationDialog = PageFactory!.GetComponent<ConfirmationDialog>();

            //Initialization of Test Data
            OrderData OrderDetails = TestDataService.Instance.Load<OrderData>("TC_1941_CreateService");
            DispatchJobDetails DispatchJobData = TestDataService.Instance.Load<DispatchJobDetails>("TC_1941_CreateService");

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

            //5. Populate the Order Details form
            //Expected Result: Order details form should be populated
            //========================================================================
            Logger!.LogInformation(Test!, "Filling Up Order Details Form");
            dispatchNewJobModal.CreateOrder(OrderDetails!);
            Logger!.LogPass(Test!, "Order details form is filled up", ScreenCaptureService.CaptureScreenImage());

            //6. Populate the Job Details Form
            //Expected Result: Job details form should be populated
            //========================================================================
            Logger!.LogInformation(Test!, "Filling Up Job Details Form");
            dispatchNewJobModal.FillJobForm(DispatchJobData!);
            Logger!.LogPass(Test!, "Job details form is filled up", ScreenCaptureService.CaptureScreenImage());

            //7. Save the Job
            //Expected Result: Job should be created and added on the list
            //========================================================================
            Logger!.LogInformation(Test!, "Saving the Job");
            dispatchNewJobModal.ClickSaveButton();
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Job is saved and created");
            Logger!.LogPass(Test!, "Verify if the Job is added on the table", ScreenCaptureService.CaptureScreenImage());

            //8. Select the newly created Job
            //Expected Result: Row should be highlighted
            //========================================================================
            Logger!.LogInformation(Test!, "Selecting the newly created Job");
            dispatchPage.ClickCreatedJobRow(DispatchJobData!);
            dispatchPage.IsJobExisting().Should().BeTrue();
            Logger!.LogPass(Test!, "Selected Newly created Job");

            //9. Cancel the created Job
            //Expected Result: Created Job should be deleted and remove on the table
            Logger!.LogInformation(Test!, "Cancelling the created Job");
            dispatchPage.CancelJob();
            dispatchConfirmationDialog.IsLoaded.Should().BeTrue();
            dispatchConfirmationDialog.ClickYesButton();
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Cancelled the created Job");
            dispatchPage.ClickCreatedJobRow(DispatchJobData!, (int)DispatchJobData!.JobDetails!.RowToCheck!);
            dispatchPage.IsJobExisting().Should().BeFalse();
            Logger!.LogPass(Test!, "Verify if the created Job is cancelled", ScreenCaptureService.CaptureScreenImage());

            //10. Logout user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Logging out the current user");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}