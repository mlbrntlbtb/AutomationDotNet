using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs.Processes;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web;

[TestFixture]
public class TFS_Test_Case_7727 : BaseTest
{
    /***************************************************************************************************
    * **************************  TFS Ticket number :  7727  ******************************************
    * Given: I am on the Processes screen of the Opsconsole and I select a process
    * When: I click on configurattions tab
    * Then: The configurations are grouped according to category using folding style AND tapping these category headers, opens up the List of Configurations
    *************************************************************************************************/

    [Test]
    public void TC_7727_ProcessesConfigurationLayout()
    {
        //Initialization of Pages & Modal
        //========================================================================
        LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
        HomePage homePage = PageFactory!.GetComponent<HomePage>().Load();
        ProcessesPage processesPage = PageFactory!.GetComponent<ProcessesPage>().Load();
        EditProcessModal editProcessModal = PageFactory!.GetComponent<EditProcessModal>().Load();
        ProcessDetails processDetails = TestDataService.Instance.LoadFile<ProcessDetails>("TC_7727_ProcessConfigurationsLayout");

        //Step 1: Verify Tempo application is loaded
        //Expected Result: Tempo application should be loaded
        //========================================================================
        Logger!.LogInformation(Test!, "Verify Tempo application is loaded.");
        loginPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Tempo application Login page is loaded.");

        //Step 2-3: Login valid credentials to Login page
        //Expected Result: Home page should be displayed after login
        //========================================================================
        Logger!.LogInformation(Test!, "Logging In authorized user");
        loginPage.LoginUser(AdminUser!);
        homePage.IsLoaded.Should().BeTrue();
        string authorisedUser = homePage.GetAuthorisedUser();
        authorisedUser.Should().Be(AdminUser!.Username);
        Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

        //Step 4: Navigate to Processes page
        //Expected Result: Processes page should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Navigate to Processes page");
        homePage.ClickTempoLinkByText("Processes");
        processesPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Processes page is displayed");

        //Step 5: Click filter in the Short Display Name field
        //Expected Result: Filter Dropdown displays
        //========================================================================
        Logger!.LogInformation(Test!, "Click On filter settings");
        processesPage.ClickColumnFilterSettings(processDetails.TableColumnHeadingSettings);
        Logger!.LogPass(Test!, "Filter Dropdown displays");

        //Step 6: Click/Hover on Filter and Enter Text
        //Expected Result: Filter Text is displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Enter Filter Text");
        processesPage.HoverOnFilter();
        processesPage.EnterFilterText(processDetails.ProcessFilterTableBy);
        Logger!.LogPass(Test!, " Delivery entered into the Filter Text field");

        //Step 7: Click on Filter Button
        //Expected Result: Filtered Delivery record is displaying
        //========================================================================
        Logger!.LogInformation(Test!, "Click on Filter Button");
        processesPage.FilterButtonDisplayed().Should().BeTrue();
        processesPage.ClickOnFilterButton();
        Assert.AreEqual(processesPage.VerifyTableRecordDelivery(), processDetails.ProcessFilterTableBy);
        Logger!.LogPass(Test!, "Filtered Delivery record is displaying", ScreenCaptureService.CaptureScreenImage());

        //Step 8: Click on the Delivery record
        //Expected Result: Delivery Record is displayed and selected
        //========================================================================
        Logger!.LogInformation(Test!, "Click on the Delivery record");
        processesPage.ClickOnDeliveryRecord(0);
        Logger!.LogPass(Test!, "Delivery Record is displayed and selected");

        //Step 9: Navigate to Edit Delivery record by clicking on the Edit button
        //Expected Result: Navigated to the Edit Delivery record
        //========================================================================
        Logger!.LogInformation(Test!, "Navigate to Edit Delivery record by clicking on the Edit button");
        processesPage.ClickEditProcessButton();
        Logger!.LogPass(Test!, "Navigated to the Edit Delivery record");

        //Step 10: Click on the Configuration button and Verify the Configuration Tabs
        //Expected Result: Configuration Tabs are verified
        //========================================================================
        Logger!.LogInformation(Test!, "Click on the Configuration button and Verify the Configuration Tabs");
        editProcessModal.IsLoaded.Should().BeTrue();
        editProcessModal.ClickConfigurationTab();
        editProcessModal.IsConfigurationCategoriesPresent().Should().BeTrue();
        Logger!.LogPass(Test!, "Configuration Tabs are verified");

        //Step 11: Click on Appearance and Verifying help text
        //Expected Result: Help Text verified
        //========================================================================
        Logger!.LogInformation(Test!, "Click on Appearance and Verifying help text");
        editProcessModal.ClickOnAppearanceCategory();
        Assert.AreEqual(editProcessModal.ClickOnAppearacnceCategoryTitleHelpText(), processDetails.ConfigurationTabdetails.ProcessTitleHelpText);
        Logger!.LogPass(Test!, "Help Text verified", ScreenCaptureService.CaptureScreenImage());

        //Step 12: Logout User
        //Expected Result: Login page should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Logout User");
        homePage.RefreshPage();
        homePage.LogoutUser();
        loginPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Login page is displayed.");
    }
}