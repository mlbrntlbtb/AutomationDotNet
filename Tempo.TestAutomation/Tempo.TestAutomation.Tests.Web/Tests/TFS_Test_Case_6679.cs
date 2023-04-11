using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.Common;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_6679 : BaseTest
    {
        /***************************************************************************************************
     * *************************TFS Ticket number :  6679    *******************************************
     * Test Case 6679: Routes
     *
     * Given: User associated to branches (67Datacom (active), City Centre (active), Christchurch (inactive)
     * When:  The user logs in and goes to the Dispatch page
     * Then:  The branch dropdown options only display the active branches (67Datacom, City Centre)
     *
     *************************************************************************************************/

        [Test]
        public void TC_6679_VerifyRouteBranches()
        {
            //Initialization of Test Data
            //========================================================================
            UserDetails RouteUser = TestDataService.Instance.Load<UserDetails>("TestUsers:RouteUser");

            //Initialization of Pages & Modal
            //========================================================================
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>().Load();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>().Load();

            //1. Load Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //2. Log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            loginPage.LoginUser(RouteUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(RouteUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' is equal to '{RouteUser.Username}'");

            //3. Proceed to the dispatch Page
            //Expected Result: Dispatch page is loaded : Dispatch table and route list is displayed
            //========================================================================
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //4. Verify Branches dropdown menu
            //Expected Result: Branch items list should be displayed, items should be verified
            //========================================================================
            Logger!.LogInformation(Test!, "Verify if Branches are present");
            dispatchPage.ClickBranchDropdownField();
            dispatchPage.IsBranchesPresent("67Datacom", "City Centre");
            Logger!.LogPass(Test!, "Branches are present", ScreenCaptureService!.CaptureScreenImage());

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