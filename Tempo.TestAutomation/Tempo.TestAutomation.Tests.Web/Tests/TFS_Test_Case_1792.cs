using Microsoft.Extensions.Logging;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1792 : BaseTest
    {
        /***************************************************************************************************
        * *************************  TFS Ticket number :  1792   ******************************************
        * Given: My 1st Logon to Tempo
        * When: I have permissions for selected menus
        * Then: the landing page is displayed
        *************************************************************************************************/

        [Test]
        public void TC_1792_ValidateLandingPageAndServerVersion()
        {
            //Initialization of Page Elements
            //========================================================================
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();

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

            //Step 4.Verification on Home Page
            //Expected Result: Home page is loaded : Home page server version and environment is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Home page is loaded");
            homePage.IsServerVersionNumberDisplayed(out string VersionNumber).Should().BeTrue();
            Logger!.LogInformation(Test!, "Server Version is " + VersionNumber);
            homePage.IsEnvironmentDisplayed(out string actualEnvironment).Should().BeTrue();
            Logger!.LogInformation(Test!, "Environment is " + actualEnvironment);
            Logger!.LogPass(Test!, "Server Version and Environment should be displayed and correct", ScreenCaptureService!.CaptureScreenImage());

            //Step-5. Logout user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}