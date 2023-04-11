using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.Common;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web.Tests
{
    [TestFixture]
    public class TFS_Test_Case_1943 : BaseTest
    {
        /***************************************************************************************************
        * *************************TFS Ticket number :  1943    *******************************************
        *
        * Given: I'm on the @UpdatePassword page
        * When: I reset password
        * Then: I could Log in with the "new" password to the @UpdatePassword
        *
        *      //Expected Result//
        *
        * - OpsConsole
        * - HH
        *
        * ****Note****
        * This test case currently only changes the password, it does not validate the LoginPOM at this point.
        **************************************************************************************************/

        [Test]
        public void TC_1943_UserResetPassword()
        {
            //Initialization of Pages and Modal Objects
            //========================================================================
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            UsersPage usersPage = PageFactory!.GetComponent<UsersPage>();
            SetPasswordModal setPasswordModal = PageFactory!.GetComponent<SetPasswordModal>();

            //Initialization of Test Data
            //========================================================================
            UserDetails userDetails = TestDataService.Instance.Load<UserDetails>("TestUsers:PasswordResetUser");

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

            //3. Navigate to the Users Page
            //Expected Result: Users page should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigating to the Users Page");
            homePage.ClickTempoLinkByText("users");
            usersPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //4. Filter the Perf049 user
            //Expected Result: Search user should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Searching the Perf049 user");
            usersPage.SearchUser(userDetails.Username!);
            Logger!.LogInformation(Test!, "Search user is displayed");

            //5. Click the Perf049 User
            //Expected Result: Row with useraname Perf049 should be highlighted
            //========================================================================
            Logger!.LogInformation(Test!, "Clicking the Perf049 row");
            usersPage.ClickUserRow(userDetails.Username!);
            Logger!.LogInformation(Test!, "Row with username Perf049 is clicked");

            //5. Click Set Password
            //Expected Result: Set Password Modal should appeared
            //========================================================================
            Logger!.LogInformation(Test!, "Clicking the Set Password Button");
            usersPage.ClickSetPassword();
            setPasswordModal.IsLoaded.Should().BeTrue();
            Logger!.LogInformation(Test!, "Set Password Modal should appeared");

            //6. Fill Up the Reset Password Form
            //Expected Result: Reset Password Form should be filled
            //========================================================================
            Logger!.LogInformation(Test!, "Filling up the Reset Password Form");
            setPasswordModal.FillResetPasswordForm();
            Logger!.LogPass(Test!, "Reset Password Form is filled up", ScreenCaptureService.CaptureScreenImage());

            //7. Click Save Button
            //Expected Result: Set Password modal should disappeared
            //========================================================================
            Logger!.LogInformation(Test!, "Saving the changes");
            setPasswordModal.ClickSave();
            Logger!.LogPass(Test!, "Password Changes is saved", ScreenCaptureService.CaptureScreenImage());

            //8. Logout user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Logging out the current user");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}