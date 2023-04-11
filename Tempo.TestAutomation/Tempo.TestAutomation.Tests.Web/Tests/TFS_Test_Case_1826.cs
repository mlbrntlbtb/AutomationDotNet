using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1826 : BaseTest
    {
        /***************************************************************************************************
        * *************************TFS Ticket number :  1826    *******************************************
        *
        * Given: I'm on the Branches page
        * When:  I press @Action
        *
        * Note: If you are not seeing the icons on the map, check the "Routes with vehicles selected" check
        * box is not ticked (unless you are specifically testing that scenario)
        *
        * Then: I can @ActionResult
        *
        * //Expected Result//
        *
        * - Edit - see the Update Existing Branch dialog where Details, Address & Geographic Area can be edited
        * - Add - see the Add New Branch dialog where  and new branch can be added
        *************************************************************************************************/

        [Test]
        public void TC_1826_UpdateAndAddBranch()
        {
            //Initialization of Test Data
            //========================================================================
            BranchDetails BranchData = TestDataService.Instance.Load<BranchDetails>("TC_1826_UpdateAndAddBranch:Details");

            //Initialization of Pages & Modal
            //========================================================================
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            BranchPage branchPage = PageFactory!.GetComponent<BranchPage>();
            UpdateExistingBranchModal updateExistingBranchModal = PageFactory!.GetComponent<UpdateExistingBranchModal>();
            AddNewBranchModal addNewBranchModal = PageFactory!.GetComponent<AddNewBranchModal>();

            //1. Load Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //2. Log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            Logger!.LogInformation(Test!, "Login Valid Credentials");
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //3. Proceed to the Branches Page
            //Expected Result: Branches page is loaded : Branches table is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Branches Page");
            homePage.ClickTempoLinkByText("branches");
            branchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Branches Page is Loaded");

            //Edit Branch Scenario
            Logger!.LogInformation(Test!, "Execute Update Branch Scenario");
            //4. Select the first branch(first row) from the table
            //Expected Result: The selected Branch should be highlighted.
            //========================================================================
            Logger!.LogInformation(Test!, "Select the first branch(first row) to Update");
            branchPage.ClickBranchRowLink(0);
            Logger!.LogPass(Test!, "Branch is Selected");

            //5. Click "Edit"/PencilIcon button.
            //Expected Result: "Update Existing Branch" modal should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Update Existing Branch modal");
            branchPage.ClickPencilIcon();
            updateExistingBranchModal.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Update Existing Branch modal is displayed", ScreenCaptureService!.CaptureScreenImage());

            //6. Enter <branch_code>  on the "Code" field.
            //Expected Result: Entered <branch_code>  on the "Code" field.
            //========================================================================
            Logger!.LogInformation(Test!, "Populate Code field");
            updateExistingBranchModal.UpdateBranchDetails(BranchData!);
            Logger!.LogPass(Test!, "Field Code is Populated", ScreenCaptureService!.CaptureScreenImage());

            //7. Click "Cancel" button..
            //Expected Result: Changes were not saved and field returned to it's orignal value.
            //========================================================================
            Logger!.LogInformation(Test!, "Cancel Update");
            updateExistingBranchModal.ClickCancel();
            Logger!.LogPass(Test!, "Changes are not saved", ScreenCaptureService!.CaptureScreenImage());
            Logger!.LogPass(Test!, "Executed Update Branch Scenario");

            //8. Click "Logout" button.
            //Expected Result: Page should be redirected on the login screen page.
            //========================================================================
            Logger!.LogInformation(Test!, "Log Out User");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Redirected to Login page");

            //9. Re log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            Logger!.LogInformation(Test!, "Re login Valid Credentials");
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //10. Proceed to the Branches Page
            //Expected Result: Branches page is loaded : Branches table is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Branches Page");
            homePage.ClickTempoLinkByText("branches");
            branchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Branches Page is Loaded");

            //Add New Branch Scenario
            Logger!.LogInformation(Test!, "Execute Add New Branch Scenario");
            //11. Click Add New Branch button.
            //Expected Result: "Add New Branch" modal should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Add New Branch Modal");
            branchPage.ClickAddNewBranchIcon();
            addNewBranchModal.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Add New Branch modal is displayed", ScreenCaptureService!.CaptureScreenImage());

            //12. Populate required fields.
            //Expected Result: Required fields should be populated
            //========================================================================
            Logger!.LogInformation(Test!, "Populate required fields");
            addNewBranchModal.AddNewBranchDetails(BranchData!);
            Logger!.LogPass(Test!, "Required fields are Populated", ScreenCaptureService!.CaptureScreenImage());

            //13. Click "Cancel" button..
            //Expected Result: Changes were not saved and field returned to it's orignal value.
            //========================================================================
            Logger!.LogInformation(Test!, "Cancel Creation");
            addNewBranchModal.ClickCancel();
            Logger!.LogPass(Test!, "Changes are not saved", ScreenCaptureService!.CaptureScreenImage());
            Logger!.LogPass(Test!, "Executed Add New Branch Scenario");

            //14. Click "Logout" button.
            //Expected Result: Page should be redirected on the login screen page.
            //========================================================================
            Logger!.LogInformation(Test!, "Log Out User");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Redirected to Login page");
        }
    }
}