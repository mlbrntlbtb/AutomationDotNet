using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1800 : BaseTest
    {
        /***************************************************************************************************
        * *************************  TFS Ticket number :  1800   ******************************************
        * Given: I'm on the dispatch page
        * When: I choose to have AutoUpdate @AutoUpdateRules
        * Then: @AutoUpdateResult
        *************************************************************************************************/

        [Test]
        public void TC_1800_SuspendAutoUpdates()
        {
            //Initialization of Pages and Modal Objects
            //========================================================================
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();

            //1. Navigate to Tempo Login Page
            //Expected Result: Tempo application should be loaded on the Login Screen page
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //2-3. Enter Admin Username and Password and Click Login Button
            //Expected Result: Tempo Home/Dashboard page should be loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Logging In authorized user");
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //4. Navigate to the Dispatch Page
            //Expected Result: Dispatch page should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Dispatch Page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch page is displayed");

            //Steps 5-7. Suspending of Auto Updates
            //Expected Result: Menu options should be displayed, An alert message 'Warning Auto Updates Suspended!' and Alert message should disappeared
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Nav Bar MenuItems");
            Logger!.LogInformation(Test!, "Click on Suspend Auto Updates");
            dispatchPage.SuspendAutoUpdates(" Suspend Auto Updates ");
            Logger!.LogPass(Test!, "Menu options is displayed", ScreenCaptureService.CaptureScreenImage());
            Logger!.LogPass(Test!, "An alert message 'Warning, Auto Updates Suspended!' is displayed");
            Logger!.LogPass(Test!, "Alert message disappeared");

            //Step 8. Click on Random Space
            //Expected Result: Nothing should happen
            //========================================================================
            Logger!.LogInformation(Test!, "Click on Random Space");
            dispatchPage.ClickOnRandomElement().Should().BeTrue();
            Logger!.LogPass(Test!, "Nothing happened");

            //Steps 9-10. Restore Auto Updates
            //Expected Result: Dispatch page should be displayed Menu options
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Nav Bar MenuItems");
            Logger!.LogInformation(Test!, "Click on Restore Auto Updates");
            dispatchPage.RestoreAutoUpdates();
            Logger!.LogPass(Test!, "Menu options is displayed", ScreenCaptureService.CaptureScreenImage());
            Logger!.LogPass(Test!, "Menu options is disappeard");

            //Step 11. Click on Random Space
            //Expected Result: Nothing should happen
            //========================================================================
            Logger!.LogInformation(Test!, "Click on Random Space");
            dispatchPage.ClickOnRandomElement().Should().BeTrue();
            Logger!.LogPass(Test!, "Nothing happened");

            //Steos 12-13. Logout user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}