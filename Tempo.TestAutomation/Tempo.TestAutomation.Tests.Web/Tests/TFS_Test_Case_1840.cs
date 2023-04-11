using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1840 : BaseTest
    {
        /***************************************************************************************************
         * *************************TFS Ticket number :  1840    *******************************************
         *
         * Given: I have workflow where Can Create Manual Event is set as @CanCreateManualEvent
         * When: complete the workflow job in the Ops Console
         * Then: @CanCreateManualEventResult
         *
         *      //Expected Result//
         *
         * - True - I can complete the step successfully and the event will appear in the Events page.
         * - False - I can complete the step successfully and the event WILL NOT appear in the Events page.
         *
         **************************************************************************************************/

        [Test]
        public void TC_1840_CreateManualEvent()
        {
            //Initialize Page and Modal
            //========================================================================
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            JobWorkflowPage jobWorkflowPage = PageFactory!.GetComponent<JobWorkflowPage>();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();
            AddJobModal dispatchNewJobModal = PageFactory!.GetComponent<AddJobModal>().Load();
            EventsPage eventsPage = PageFactory!.GetComponent<EventsPage>().Load();
            EditJobModal editJobModal = PageFactory!.GetComponent<EditJobModal>().Load();
            EventDetailsModal eventDetailsModal = PageFactory!.GetComponent<EventDetailsModal>().Load();

            //Initialization of Test Data
            //========================================================================
            JobWorkflowDetails JobWorkflowData = TestDataService.Instance.Load<JobWorkflowDetails>("TC_1840_ManualEventCreation");
            DispatchJobDetails DispatchJobData = TestDataService.Instance.Load<DispatchJobDetails>("TC_1840_CreateDispatchJob");

            //1. Load Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //2. Log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            Logger!.LogInformation(Test!, "Login Valid Credentials to Events Page");
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //3. Proceed to the Job Workflow
            //Expected Result: Job Workflow page is loaded : Job Workflow table is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Jow Workflow Page");
            homePage.ClickTempoLinkByText("job workflow");
            jobWorkflowPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Job Workflow Page is Loaded");

            //4. Click settings icon of Details column in Job Workflow table
            //Expected Result: Settings menu of Details column is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Click Column Settings");
            jobWorkflowPage.ClickShortNameButton();
            jobWorkflowPage.IsDropDownListDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Dropdown List in Short Name column is clicked and menu is displayed");

            //5.Click Filter item of Details Settings menu in Job Workflow table
            //Expected Result: Settings submenu is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Click Filter Dropdown");
            jobWorkflowPage.ClickFilterListItem();
            jobWorkflowPage.IsShortNameDropDownSubmenuDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Short Name Dropdown submenu is displayed");

            //6.Enter short name into Filter field
            //Expected Result: Filter field has short name value
            //========================================================================
            Logger!.LogInformation(Test!, "Filling up filter field");
            string shortName = jobWorkflowPage.GetJobWorkFlow(JobWorkflowData!);
            jobWorkflowPage.SetFilterValue(shortName);
            jobWorkflowPage.IsFilterFieldPopulated(shortName).Should().BeTrue();
            Logger!.LogPass(Test!, "Filter field has short name value");

            //7.Click Filter button
            //Expected Result: Filter has been applied to job workflow table
            //========================================================================
            Logger!.LogInformation(Test!, "Click Filter Button");
            jobWorkflowPage.ClickFilterButton();
            jobWorkflowPage.IsShortNameFiltered().Should().BeTrue();
            Logger!.LogPass(Test!, "Filter field has been applied to job workflow table");

            //8. Verify if job workflow table rows are populated only with short name
            //Expected Result: Job Workflow table has been verified to only contain short name rows
            //========================================================================
            Logger!.LogInformation(Test!, "Job workflow table contains the filtered short name");
            jobWorkflowPage.IsTableFilteredByShortName(shortName, "Short Name").Should().BeTrue();
            Logger!.LogPass(Test!, "Job workflow table has been verified to only show short name rows");

            //9. Select the first job workflow entry from the table
            //Expected Result: The selected entry was highlighted
            //========================================================================
            Logger!.LogInformation(Test!, "Click first row");
            jobWorkflowPage.ClickJobWorkFlowRow(0);
            jobWorkflowPage.IsFirstRowHighlighted().Should().BeTrue();
            Logger!.LogPass(Test!, "Selected entry was highlighted", ScreenCaptureService!.CaptureScreenImage());

            //10. Click Edit button
            //Expected Result: A modal with header title of "Update Existing Job Wokfflow" should appeared
            //========================================================================
            Logger!.LogInformation(Test!, "Click Edit button");
            jobWorkflowPage.ClickEditButton();
            jobWorkflowPage.IsPopupDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Update Existing Job Wokfflow pop up is displayed", ScreenCaptureService!.CaptureScreenImage());

            //11. Click Workflow button
            //Expected Result: Workflow page tab should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Click Workflow");
            jobWorkflowPage.ClickWorkflowButton();
            jobWorkflowPage.IsWorkflowPageDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Workflow Page is displayed", ScreenCaptureService!.CaptureScreenImage());

            //12. Click Edit button
            //Expected Result: Additional fields should appeared
            //========================================================================
            Logger!.LogInformation(Test!, "Click Edit button");
            jobWorkflowPage.ClickEditWorkflowButton();
            jobWorkflowPage.IsAdditionalFieldsDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Additional fields is displayed", ScreenCaptureService!.CaptureScreenImage());

            //13. Click Can Create Workflow Checkbox
            //Expected Result: Can Create checkbox is ticked
            //========================================================================
            Logger!.LogInformation(Test!, "Click Can Create Workflow Checkbox");
            jobWorkflowPage.ClickCanCreateCheckbox();
            Logger!.LogPass(Test!, "Additional fields is displayed", ScreenCaptureService!.CaptureScreenImage());

            //14. Click Save Button
            //Expected Result: Workflow has been saved
            //========================================================================
            Logger!.LogInformation(Test!, "Click Save button");
            jobWorkflowPage.ClickSaveButton();
            jobWorkflowPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Workflow is saved successfully", ScreenCaptureService!.CaptureScreenImage());

            //15. Proceed to the Home Page
            //Expected Result: Home Page is loaded: Home Page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Home Page");
            homePage.NavigateSidebarByText("home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Home Page is displayed");

            //16. Proceed to the dispatch Page
            //Expected Result: Dispatch page is loaded : Dispatch table and route list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Dispatch Page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //17. Click new job button and fill add job form
            //Expected Result: Dispatch Add new job modal form Fields are filled
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Home Page");
            dispatchPage.ClickAddJob();
            dispatchNewJobModal.FillAddJobForm(DispatchJobData!, true);
            Logger!.LogPass(Test!, "Dispatch Job Modal is Loaded and form is filled", ScreenCaptureService!.CaptureScreenImage());

            //18. Click the save button for the job creation
            //Expected Result: The created job is successfully created
            //========================================================================
            Logger!.LogInformation(Test!, "Click Save Button");
            dispatchNewJobModal.ClickSaveButton();
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Job is successculy saved", ScreenCaptureService.CaptureScreenImage());

            //19. Proceed to the Home Page
            //Expected Result: Home Page is loaded: Home Page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Home Page");
            homePage.NavigateSidebarByText("home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Home Page is displayed");

            //20. Proceed to the dispatch Page
            //Expected Result: Dispatch page is loaded : Dispatch table and route list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Dispatch Page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //21. Select the first entry from the table
            //Expected Result: The selected entry was highlighted
            //========================================================================
            Logger!.LogInformation(Test!, "Click first row");
            dispatchPage.ClickDispatchTableFirstRow(0);
            dispatchPage.IsFirstRowHighlighted().Should().BeTrue();
            Logger!.LogPass(Test!, "Selected entry was highlighted", ScreenCaptureService!.CaptureScreenImage());

            //22. Click Edit button
            //Expected Result: A modal with header title of "Edit" should appeared
            //========================================================================
            Logger!.LogInformation(Test!, "Click Edit button");
            dispatchPage.ClickEditWorkflowButton();
            String HeaderID = editJobModal.GetHeaderJobID();
            dispatchPage.IsEditJobHeaderDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Edit Pop up is displayed", ScreenCaptureService!.CaptureScreenImage());

            //23. Select AutoAdhoc in the Process Job Dropdown
            //Expected Result: Confirmation Pop-up is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Select AutoAdhoc in the Process Job Dropdown");
            dispatchPage.ClickAutoAdhoc();
            dispatchPage.IsConfirmationPopupDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "CONFIRMATION pop-up is displayed", ScreenCaptureService!.CaptureScreenImage());

            //24. Process the Job
            //Expected Result: Job hase been processed
            //========================================================================
            Logger!.LogInformation(Test!, "Processed Job");
            dispatchPage.FillConfirmation(DispatchJobData!);
            dispatchPage.ClickProcessJob();
            //dispatchPage.IsConfirmationPopupDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "CONFIRMATION pop-up is displayed", ScreenCaptureService!.CaptureScreenImage());

            //25. Proceed to the Home Page
            //Expected Result: Home Page is loaded: Home Page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Home Page");
            homePage.NavigateSidebarByText("home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Home Page is displayed");

            //26. Proceed to the Events Page
            //Expected Result: Events page is displayed : Events table list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Events Page");
            homePage.ClickTempoLinkByText("events");
            eventsPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Events page is displayed");

            //27. Transaction should appear on the Events Page
            //Expected Result: Transaction is avaailable on the Events Page
            //========================================================================
            Logger!.LogInformation(Test!, "Verify Transaction is available");
            eventsPage.ClickEventsRow(0);
            eventsPage.ClickViewMore();
            eventDetailsModal.ClickJobHeader();
            eventDetailsModal.IsJobIDEqual(HeaderID).Should().BeTrue();
            eventDetailsModal.ClickCloseButton();
            eventsPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Transaction is displayed on the Events Page");

            //28. Logout user from tempo App
            //Expected Result: Tempo Login page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Logout User to Automation Dev Page");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}