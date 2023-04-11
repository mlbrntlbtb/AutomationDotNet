using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Dialogs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web;

[TestFixture]
public class TFS_Test_Case_6747 : BaseTest
{
    /***************************************************************************************************
   * *************************  TFS Ticket number :  6747  ******************************************
   * Given: I am on the Setting page (Assumes Permissions are granted to the Settings Page)
   * When: I update a configuration
   * Then: the configuration i updated is persisted and an Audit is created of the change
   *************************************************************************************************/

    [Test]
    public void TC_6747_UpdateSettingsConfiguration()
    {
        //Initialization of Pages, Modals & Dialogs
        //========================================================================
        LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
        HomePage homePage = PageFactory!.GetComponent<HomePage>().Load();
        SettingsPage settingsPage = PageFactory!.GetComponent<SettingsPage>().Load();
        UpdateSettingsDialog updateSettingsDialog = PageFactory!.GetComponent<UpdateSettingsDialog>();
        AuditHistoryModal auditHistoryModal = PageFactory!.GetComponent<AuditHistoryModal>();

        //Initialization of Test Data
        SettingsConfiguration settingsConfiguration = TestDataService.Instance.LoadFile<SettingsConfiguration>("TC_6747_TestData");

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

        //Step 4: Navigate to Settings page
        //Expected Result: Dispatch page should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Navigate to Settings page");
        homePage.ClickTempoLinkByText("settings");
        settingsPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Settings page is displayed");

        //Step 5: Select table row with 'Create consignment with order' label from Settings page
        //Expected Result: Table row with 'Create consignment with order' label should be selected
        //========================================================================
        Logger!.LogInformation(Test!, "Select table row with 'Create consignment with order' label from Settings page");
        int rowIndex = (int)settingsPage.GetRowIndex(settingsConfiguration.ColumnName!, settingsConfiguration.RowText!)!;
        settingsPage.ClickTableRow(rowIndex);
        settingsPage.IsRowSelected(rowIndex).Should().BeTrue();
        Logger!.LogPass(Test!, "Table row with 'Create consignment with order' label is selected");

        //Step 6: Navigate to Update Settings dialog
        //Expected Result: Update Settings dialog should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Navigate to Update Settings dialog");
        settingsPage.ClickEditButton();
        updateSettingsDialog.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Update Settings dialog is displayed");

        //Step 7: Select 'Optional' from the Create consignment order dropdown list
        //Expected Result: 'Optional' item should be selected from the dropdown list
        //========================================================================
        Logger!.LogInformation(Test!, "Select 'Optional' from the Create consignment order dropdown list");
        updateSettingsDialog.SelectCreateConsignmentOrder(settingsConfiguration.CreateConsignmentOrderValue1!);
        string createConsignmentOrder = updateSettingsDialog.GetCreateConsignmentOrderValue();
        createConsignmentOrder.Should().Be(settingsConfiguration.CreateConsignmentOrderValue1!);
        Logger!.LogPass(Test!, $"'{createConsignmentOrder}' item is selected from the dropdown list", ScreenCaptureService!.CaptureScreenImage());

        //Step 8: Save settings configuration
        //Expected Result: Settings configuration should be saved successfully
        //========================================================================
        Logger!.LogInformation(Test!, "Save settings configuration");
        updateSettingsDialog.ClickSaveButton();
        string toastMessage = settingsPage.GetToastMessage();
        toastMessage.Should().Be(settingsConfiguration.ToastMessage);
        Logger!.LogPass(Test!, "Settings configuration has been saved successfully", ScreenCaptureService!.CaptureScreenImage());
        settingsPage.CloseToastMessage();

        //Step 9.0: Navigate to Audit history modal
        //Expected Result: Audit history modal should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Navigate to Audit history modal");
        settingsPage.SelectMenuItem(settingsConfiguration.MenuItem!);
        auditHistoryModal.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Audit history modal has been displayed");

        //Step 9.1: Verify 'Create consignment with order' activity on audit history modal
        //Expected Result: Audit history modal should display 'Create consignment with order' activity
        //========================================================================
        Logger!.LogInformation(Test!, "Verify 'Create consignment with order' activity on audit history modal");
        string latestAuditHistoryActivity = auditHistoryModal.GetAuditHistoryCardActivity(0);
        string latestAuditHistoryConfigurationSetting = auditHistoryModal.GetAuditHistoryCardConfigurationSetting(0);
        latestAuditHistoryActivity.Should().Contain(settingsConfiguration.auditHistory!.Activity!);
        latestAuditHistoryConfigurationSetting.Should().Be(settingsConfiguration.auditHistory.ConfigurationSetting);
        Logger!.LogPass(Test!, "Audit history modal contains 'Create consignment with order' activity", ScreenCaptureService!.CaptureScreenImage());

        //Step 10-11: Refresh then select table row with 'Create consignment with order' label again from Settings page
        //Expected Result: Table row with 'Create consignment with order' label should be selected
        //========================================================================
        Logger!.LogInformation(Test!, "Refresh then select table row with 'Create consignment with order' label again from Settings page");
        homePage.RefreshPage();
        settingsPage.ClickTableRow(rowIndex);
        settingsPage.IsRowSelected(rowIndex).Should().BeTrue();
        Logger!.LogPass(Test!, "Table row with 'Create consignment with order' label is selected");

        //Step 12: Navigate to Update Settings again
        //Expected Result: Update Settings dialog should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Navigate to Update Settings dialog again");
        settingsPage.ClickEditButton();
        updateSettingsDialog.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Update Settings dialog is displayed");

        //Step 13: Select 'Yes' from the Create consignment order dropdown list
        //Expected Result: 'Yes' item should be selected from the dropdown list
        //========================================================================
        Logger!.LogInformation(Test!, "Select 'Yes' from the Create consignment order dropdown list");
        updateSettingsDialog.SelectCreateConsignmentOrder(settingsConfiguration.CreateConsignmentOrderValue2!);
        createConsignmentOrder = updateSettingsDialog.GetCreateConsignmentOrderValue();
        createConsignmentOrder.Should().Be(settingsConfiguration.CreateConsignmentOrderValue2!);
        Logger!.LogPass(Test!, $"'{createConsignmentOrder}' item is selected from the dropdown list", ScreenCaptureService!.CaptureScreenImage());

        //Step 14: Save settings configuration again
        //Expected Result: Settings configuration should be saved
        //========================================================================
        Logger!.LogInformation(Test!, "Save settings configuration again");
        updateSettingsDialog.ClickSaveButton();
        toastMessage = settingsPage.GetToastMessage();
        toastMessage.Should().Be(settingsConfiguration.ToastMessage);
        Logger!.LogPass(Test!, "Settings configuration has been saved", ScreenCaptureService!.CaptureScreenImage());
        settingsPage.CloseToastMessage();

        //Step 15: Logout User
        //Expected Result: Login page should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Logout User");
        homePage.RefreshPage();
        homePage.LogoutUser();
        loginPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Login page is displayed.");
    }
}