using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1819 : BaseTest
    {
        /***************************************************************************************************
        * *************************  TFS Ticket number :  1819   ******************************************
        * Given: I'm on the Events page
        * When: I view an Event to see the Event Details
        * Then: I can see all the details for that event
        *************************************************************************************************/

        [Test]
        public void TC_1819_EventsViewMore()
        {
            //00. Instantiations
            //Expected Result: Initialized All Page/Modal Components
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>().Load();
            EventsPage eventsPage = PageFactory!.GetComponent<EventsPage>().Load();
            EventDetailsModal eventDetailsModal = PageFactory!.GetComponent<EventDetailsModal>().Load();

            //10. Load Tempo Login Page
            //Expected Result: Tempo Login page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is displayed");

            //20. Log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            Logger!.LogInformation(Test!, "Login Valid Credentials to Events Page");
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //30. Proceed to the Events Page
            //Expected Result: Events page is displayed : Events table list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Events Page");
            homePage.ClickTempoLinkByText("events");
            eventsPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Events page is displayed");

            //40. Select an event from the Table & Click View More
            //Expected Result: Event details modal should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigate to Events Details Modal");
            eventsPage.ClickEventsTable();
            eventsPage.ClickViewMore();
            eventDetailsModal.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Event Details Modal is displayed", ScreenCaptureService!.CaptureScreenImage());

            //50. If available, verify if each tabs is active & clickable
            //Expected Result: Contents of each tab should be displayed if avaialble
            //========================================================================
            Logger!.LogInformation(Test!, "Click Each Events Tab Contents");
            eventDetailsModal.ValidateEventsTabContents("Event Details");
            eventDetailsModal.ValidateEventsTabContents("Items");
            eventDetailsModal.ValidateEventsTabContents("Signatures");
            eventDetailsModal.ValidateEventsTabContents("Jobs");
            eventDetailsModal.ValidateEventsTabContents("Endorsements");
            eventDetailsModal.ValidateEventsTabContents("Attachments");
            eventDetailsModal.ValidateEventsTabContents("Location");
            eventDetailsModal.ValidateEventsTabContents("Audit History");
            eventDetailsModal.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "All events tab are validated", ScreenCaptureService!.CaptureScreenImage());

            //60. Close the event details modal
            //Expected Result: "Event Details" modal should be closed & events page should be displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Close Event Details Modal");
            eventDetailsModal.ClickCloseButton();
            eventsPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully closed Event Details Modal", ScreenCaptureService!.CaptureScreenImage());

            ////70. Logout user from tempo App
            /////Expected Result: Tempo Login page is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Logout User to Automation Dev Page");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}