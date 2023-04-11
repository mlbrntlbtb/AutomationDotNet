using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs.Processes;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web.Tests;

[TestFixture]
public class TFS_Test_Case_1820 : BaseTest
{
    /***************************************************************************************************
    * **************************  TFS Ticket number :  1820  ******************************************
    * Given: I'm on the Events page
    * When: I press @Action
    * Then: I can @ActionResult
    *************************************************************************************************/

    [Test]
    public void TC_1820_Events_Functions()
    {
        //Initialization of Pages & Modal
        //========================================================================
        LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
        HomePage homePage = PageFactory!.GetComponent<HomePage>();
        EventsPage eventsPage = PageFactory!.GetComponent<EventsPage>();
        EventDetailsModal eventsDetailsModal = PageFactory!.GetComponent<EventDetailsModal>();

        EventDetails eventDetails = TestDataService.Instance.LoadFile<EventDetails>("TC_1820_Events_Functions");

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

        //Step 4: Navigate to Events page
        //Expected Result: Events page should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Navigate to Events page");
        homePage.ClickTempoLinkByText("Events");
        eventsPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Events page is displayed");

        //Step 5: Click Kebab menu in the Process column header Name field
        //Expected Result: Filter Dropdown displays
        //========================================================================
        Logger!.LogInformation(Test!, "Click Kebab menu in the Process column header Name field");
        eventsPage.ClickProcessFilterSettings("Process");
        Logger!.LogPass(Test!, "Filter Dropdown displays");

        //Step 6: Click/Hover on Column submenu
        //Expected Result: Column headings are displaying
        //========================================================================
        Logger!.LogInformation(Test!, "Click/Hover on Column submenu");
        eventsPage.HoverOnColumnSubmenu();
        Logger!.LogPass(Test!, "Column headings are displaying");

        //Step 7: Tick Events Id check box
        //Expected Result: Event ID column selected
        //========================================================================
        Logger!.LogInformation(Test!, "Tick Events Id check box");
        eventsPage.CheckColumnStatus().Should().BeFalse();
        eventsPage.AddEventIDColumn();
        Logger!.LogPass(Test!, "Event ID column selected");

        //Step 8: Click anywhere in the screen to close the modal.
        //Expected Result: Header details menu was closed.
        //========================================================================
        Logger!.LogInformation(Test!, "Click anywhere in the screen to close the modal.");
        eventsPage.ClickEventsTable();
        Logger!.LogPass(Test!, "Header details menu was closed.");

        //Step 9: Verify if "Event Id" column is added to the table.
        //Expected Result: Event Id column was added to the table.
        //========================================================================
        Logger!.LogInformation(Test!, "Verify if Event Id column is added to the table");
        eventsPage.IsColumnExist().Should().BeTrue();
        Logger!.LogPass(Test!, "Event Id column was added to the table.", ScreenCaptureService.CaptureScreenImage());

        //Step 10: Click "Save Layout" button.
        //Expected Result: Pop out "Your configurations has been saved" was displayed..
        //========================================================================
        Logger!.LogInformation(Test!, "Click Save Layout button");
        eventsPage.ClickOnSaveLayout();
        eventsPage.IsToastMessageDisplayed(eventDetails.SaveLayoutToastMessage);
        Logger!.LogPass(Test!, "Pop out Your configurations has been saved was displayed", ScreenCaptureService.CaptureScreenImage());

        //Step 11-12: Logout User
        //Expected Result: Login page should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Logout User");
        homePage.RefreshPage();
        homePage.LogoutUser();
        loginPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Login page is displayed.");

        //Step 13-14: Login valid credentials to Login page
        //Expected Result: Home page should be displayed after login
        //========================================================================
        Logger!.LogInformation(Test!, "Login valid credentials to Login page");
        loginPage.LoginUser(AdminUser!);
        homePage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Home page is displayed after login.");

        //Step 15: Navigate to Events page
        //Expected Result: Events page should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Navigate to Events page");
        homePage.ClickTempoLinkByText("Events");
        eventsPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Events page is displayed");

        //Step 16: Verify if layout was retained with "Event Id" column displayed.
        //Expected Result: Event ID column selected
        //========================================================================
        Logger!.LogInformation(Test!, "Verify if layout was retained with Event Id column displayed.");
        eventsPage.IsColumnExist().Should().BeTrue();
        Logger!.LogPass(Test!, "Layout was retained with Event Id column displayed.");

        //Step 17: Click "Clear Layout" button.
        //Expected Result: Pop out "Your configurations has been cleared" was displayed.
        //========================================================================
        Logger!.LogInformation(Test!, "Click Clear Layout button.");
        eventsPage.ClickOnClearLayout();
        eventsPage.IsToastMessageDisplayed(eventDetails.ClearLayoutToastMessage);
        Logger!.LogPass(Test!, "Pop up Your configurations has been cleared was displayed.", ScreenCaptureService.CaptureScreenImage());

        //Step 18-19: Logout User
        //Expected Result: Login page should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Logout User");
        homePage.RefreshPage();
        homePage.LogoutUser();
        loginPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Login page is displayed.");

        //Step 20-21: Login valid credentials to Login page
        //Expected Result: Home page should be displayed after login
        //========================================================================
        Logger!.LogInformation(Test!, "Login valid credentials to Login page");
        loginPage.LoginUser(AdminUser!);
        homePage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Home page is displayed after login.");

        //Step 22: Navigate to Events page
        //Expected Result: Events page should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Navigate to Events page");
        homePage.ClickTempoLinkByText("Events");
        eventsPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Events page is displayed");

        //Step 23: Verify if there are no filters applied to the table.
        //Expected Result: No filters were applied on the table.
        //========================================================================
        Logger!.LogInformation(Test!, "Verify if there are no filters applied to the table.");
        eventsPage.IsColumnExist().Should().BeFalse();
        Logger!.LogPass(Test!, "No filters were applied on the table.");

        //24-25. Use Sidebar menu to navigate to Home
        //Expected Result: Home screen is displayed
        //========================================================================
        homePage.NavigateSidebarByText("Home");
        homePage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Home screen is displayed");

        //Step 26: Navigate to Events page
        //Expected Result: Events page should be displayed
        //========================================================================
        Logger!.LogInformation(Test!, "Navigate to Events page");
        homePage.ClickTempoLinkByText("Events");
        eventsPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Events page is displayed");

        //Step 27: Select the first Event entry on the list.
        //Expected Result: The selected Event entry was highlighted.
        //========================================================================
        Logger!.LogInformation(Test!, "Select the first Event entry on the list.");
        eventsPage.ClickOnEventFirstRecord(0);
        Logger!.LogPass(Test!, "The selected Event entry was highlighted.");

        //Step 28: Click "View More" button.
        //Expected Result: "Event Details" modal was displayed.
        //========================================================================
        Logger!.LogInformation(Test!, "Click View More button.");
        eventsPage.ClickViewMore();
        eventsDetailsModal.IsLoaded.Should().BeTrue();
        eventsDetailsModal.IsEventModalBodyDisplayed();
        Logger!.LogPass(Test!, "Event Details modal was displayed.", ScreenCaptureService.CaptureScreenImage());

        //Step 29: Click "Close" button.
        //Expected Result: "Event Details" modal was closed.
        //========================================================================
        Logger!.LogInformation(Test!, "Click Close button.");
        eventsDetailsModal.ClickCloseButton();
        eventsPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Event Details modal was closed.");

        //Step 30: Click "View Map" button.
        //Expected Result: "View Map" modal was displayed.
        //========================================================================
        Logger!.LogInformation(Test!, "Click View Map button.");
        eventsPage.IsMapButtonDisplayed().Should().BeTrue();
        eventsPage.ClickOnMapButton();
        eventsDetailsModal.IsEventMapBodyDisplayed().Should().BeTrue();
        Logger!.LogPass(Test!, "View Map modal was displayed.", ScreenCaptureService.CaptureScreenImage());

        //Step 31: Click anywhere in the screen to close the modal.
        //Expected Result: "View Map" modal was closed
        //========================================================================
        Logger!.LogInformation(Test!, "Click anywhere in the screen to close the modal.");
        eventsPage.ClickOnEventHeader();
        Logger!.LogPass(Test!, "View Map modal was closed");

        //Step 32-33: Logout User
        //Expected Result: Login page should be
        //========================================================================
        Logger!.LogInformation(Test!, "Logout User");
        homePage.RefreshPage();
        homePage.LogoutUser();
        loginPage.IsLoaded.Should().BeTrue();
        Logger!.LogPass(Test!, "Login page is displayed.");
    }
}