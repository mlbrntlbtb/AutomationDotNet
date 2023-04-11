using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.Web.Components.Dialogs;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1799 : BaseTest
    {
        /***************************************************************************************************
        * *************************TFS Ticket number :  1799    *******************************************
        *
        * Given: I'm on the dispatch page
        * When: I press 'Cancel Allocation' on a Job that is dispatched
        * Then: the job is set as 'unallocated', ready for a new allocation to be made and removed from the first driver.
        *
        *      //Expected Result//
        *
        * - Route allocation canceled
        * - Job is set to unallocated
        *************************************************************************************************/

        [Test]
        public void TC_1799_CancelJob()
        {
            //Initialization of Pages & Modal
            //========================================================================
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();
            ConfirmationDialog dispatchConfirmationDialog = PageFactory!.GetComponent<ConfirmationDialog>();

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
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' is equal to '{AdminUser.Username}'");

            //3. Proceed to the dispatch Page
            //Expected Result: Dispatch page is loaded : Dispatch table and route list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Load Dispatch Page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //4. Cancel job from the Jobs table
            //Expected Result: Job should be cancelled
            //========================================================================
            Logger!.LogInformation(Test!, "Cancel job from the Jobs table");
            dispatchPage.ClickRow(0);
            dispatchPage.CancelJob();
            dispatchConfirmationDialog.IsLoaded.Should().BeTrue();
            dispatchConfirmationDialog.ClickYesButton();
            Logger!.LogPass(Test!, "Job is cancelled", ScreenCaptureService!.CaptureScreenImage());
            dispatchPage.CloseToastMessage();

            //5. Logout user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Log Out User");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}