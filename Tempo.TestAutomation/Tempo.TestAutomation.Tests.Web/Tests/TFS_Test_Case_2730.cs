using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web;

[TestFixture]
public class TFS_Test_Case_2730 : BaseTest
{
    /***************************************************************************************************
    * *************************  TFS Ticket number :  2730  ******************************************
    * Given: I'm logged on to the Ops Console
    * When: I create a job and choose to select an Action branch
    * Then: all the Action Branch's are displayed to allow for selection, and once selected the job can be saved
    *************************************************************************************************/

    [Test]
    public void TC_2730_CreateJobActionBranch()
    {
        //Initialization of Pages & Modal
        //========================================================================
        LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
        HomePage homePage = PageFactory!.GetComponent<HomePage>().Load();
        DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>().Load();
        AddJobModal addJobModal = PageFactory!.GetComponent<AddJobModal>().Load();

        //Step 1: Verify Tempo application is loaded
        //Expected Result: Tempo application should be loaded
        //========================================================================
        Logger!.LogInformation(Test!, "Verify Tempo application is loaded.");
        loginPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Tempo application Login page is loaded.");

        //Step 2-3: Login valid credentials to Login page
        //Expected Result: Home page should be displayed after login
        //========================================================================
        Logger!.LogInformation(Test!, "Login valid credentials to Login page");
        loginPage.LoginUser(AdminUser!);
        homePage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Home page is displayed after login.");

        //Step 4: Navigate to Dispatch page
        //Expected Result: Dispatch page should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Navigate to Dispatch page");
        homePage.ClickTempoLinkByText("dispatch");
        dispatchPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Dispatch page is displayed");

        //Step 5: Navigate to Add Job modal
        //Expected Result: Add Job modal should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Navigate to Add Job modal");
        dispatchPage.ClickAddJob();
        addJobModal.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Add Job modal is displayed");

        //Step 6-14: Create Job with Action Branch from Add Job modal
        //Expected Result: Job form with Action Branch from Add Job modal should be filled
        //========================================================================
        Logger!.LogInformation(Test!, "Create Job with Action Branch from Add Job modal");
        JobActionBranch jobActionBranch = TestDataService.Instance.Load<JobActionBranch>("TC_2730_CreateJobActionBranch");
        addJobModal.CreateJobActionBranch(jobActionBranch);
        Logger!.LogPass(Test!, "Job form with Action Branch from Add Job modal has been filled", ScreenCaptureService!.CaptureScreenImage());

        //Step 15: Save job from Add Job modal
        //Expected Result: Created job from Add Job modal should be saved
        //========================================================================
        Logger!.LogInformation(Test!, "Save job from Add Job modal");
        addJobModal.ClickSaveButton();
        Logger!.LogPass(Test!, "Created job from Add Job modal has been saved", ScreenCaptureService!.CaptureScreenImage());

        //Step 16-18: Logout User
        //Expected Result: Login page should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Logout User");
        homePage.RefreshPage();
        homePage.LogoutUser();
        loginPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Login page is displayed.");
    }
}