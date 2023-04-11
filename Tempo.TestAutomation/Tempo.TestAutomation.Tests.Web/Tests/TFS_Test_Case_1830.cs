using AventStack.ExtentReports.Model;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1830 : BaseTest
    {
        /***************************************************************************************************
      * *************************TFS Ticket number :  1830    *******************************************
      *
      * Given: I'm on the Device page
      * When:  I choose to @DeviceAction
      * Then: i can see @DeviceActionResult
      *
      *      //Expected Result//
      *
      * - Disable Device - the disabled user will not b able to log back onto that device
      * - Device Logs - the logs for the selected User/Device and filtering options
      *************************************************************************************************/

        [Test]
        public void TC_1830_ValidateDevicesLogs()
        {
            //1. Load Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //2. Log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            loginPage.LoginUser(AdminUser!);
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //3. Proceed to the devices Page
            //Expected Result: Device page is loaded : Devices table is displayed
            //========================================================================
            homePage.ClickTempoLinkByText("devices");
            DevicesPage devicesPage = PageFactory!.GetComponent<DevicesPage>();
            devicesPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Devices Page is Loaded");

            //4. Select the first Device entry from the table
            //Expected Result: The selected entry was highlighted
            //========================================================================
            devicesPage.ClickDevicesRow(0);
            String HighlightedRowSerial = devicesPage.GetRowSerial(0);
            devicesPage.IsRowHighlighted().Should().BeTrue();
            Logger!.LogPass(Test!, "Selected entry was highlighted", ScreenCaptureService!.CaptureScreenImage());

            //5. Click disabled button
            //Expected Result: The selected device entry was disabled and a time stamp was displayed on the Disabled column
            //========================================================================
            devicesPage.ClickDisable();
            devicesPage.IsCellWithText(devicesPage.GetRowIndexByText(HighlightedRowSerial, "Serial Number"), "Disabled").Should().BeTrue();
            Logger!.LogPass(Test!, "Selected entry was disabled", ScreenCaptureService!.CaptureScreenImage());

            //6. Use Sidebar menu to navigate to Home
            //Expected Result: Home screen is displayed
            //========================================================================
            homePage.NavigateSidebarByText("Home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Home screen is displayed");

            //7. Proceed to the devices Page
            //Expected Result: Device page is loaded : Devices table is displayed
            //========================================================================
            homePage.ClickTempoLinkByText("devices");
            devicesPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Devices Page is Loaded");

            //8. Select the Device entry from the table
            //Expected Result: The selected entry was highlighted
            //========================================================================
            devicesPage.ClickDevicesRowByText(HighlightedRowSerial);
            devicesPage.IsRowWithTextHighlighted(HighlightedRowSerial, "Serial Number").Should().BeTrue();
            Logger!.LogPass(Test!, "Selected entry was highlighted", ScreenCaptureService!.CaptureScreenImage());

            //9. Click enabled button
            //Expected Result: The selected device entry was enabled and a time stamp was removed on the Disabled column
            //========================================================================
            devicesPage.ClickEnable();
            devicesPage.IsCellWithText(devicesPage.GetRowIndexByText(HighlightedRowSerial, "Serial Number"), "Disabled").Should().BeFalse();
            Logger!.LogPass(Test!, "Selected entry was enabled", ScreenCaptureService!.CaptureScreenImage());

            //10. Use Sidebar menu to navigate to Home
            //Expected Result: Home screen is displayed
            //========================================================================
            homePage.NavigateSidebarByText("Home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Home screen is displayed");

            //11. Proceed to the devices Page
            //Expected Result: Device page is loaded : Devices table is displayed
            //========================================================================
            homePage.ClickTempoLinkByText("devices");
            devicesPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Devices Page is Loaded");

            //12. Select the first Device entry from the table
            //Expected Result: The selected entry was highlighted
            //========================================================================
            devicesPage.ClickDevicesRowByText(HighlightedRowSerial);
            devicesPage.IsRowWithTextHighlighted(HighlightedRowSerial, "Serial Number").Should().BeTrue();
            Logger!.LogPass(Test!, "Selected entry was highlighted", ScreenCaptureService!.CaptureScreenImage());

            //13. Select Today on the Date Quick Select dropdown
            //Expected Result: Today item is selected
            //========================================================================
            devicesPage.SelectCurrentDate();
            devicesPage.IsDateQuickSelectItemSelected("Today").Should().BeTrue();
            Logger!.LogPass(Test!, "Dropdown Item is selected");

            //14. Click Apply button
            //Expected Result: Warning message is displayed
            //========================================================================
            devicesPage.ClickApply();
            devicesPage.IsNoLogsMessageDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Warning message is displayed", ScreenCaptureService!.CaptureScreenImage());

            //16.Logout user from tempo App
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}