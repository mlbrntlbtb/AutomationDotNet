using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Common.Extensions;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Pages;
using HomePage = Tempo.TestAutomation.Model.Web.HomePage;
using LoginPage = Tempo.TestAutomation.Model.Web.LoginPage;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1797 : BaseTest
    {
        /***************************************************************************************************
      * *************************TFS Ticket number :  1797    *******************************************
      *
      * Given: I'm on the Jobs Details page located from @JobDetailsFrom page
      * When: I press Print Job
      * Then: The Jobs Details report opens for that Job
      *      //Expected Result//
      *
      * - Dispatch - Jobs grid
      *************************************************************************************************/

        [Test]
        public void TC_1797_PrintJob()
        {
            //Initialize Page and Modal
            //========================================================================
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();

            //Initialization of Test Data
            //========================================================================
            DispatchJobDetails DispatchJobData = TestDataService.Instance.LoadFile<DispatchJobDetails>("TC_1797_TestData");

            //1. Load Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //2. Log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //3. Proceed to the dispatch Page
            //Expected Result: Dispatch page is loaded : Dispatch table and route list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Click Dispatch menu");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //4. Select the first entry from the table
            //Expected Result: The selected entry was highlighted
            //========================================================================
            Logger!.LogInformation(Test!, "Click first entry in the dispatch table");
            dispatchPage.ClickDispatchTableFirstRow(0);
            dispatchPage.IsFirstRowHighlighted().Should().BeTrue();
            Logger!.LogPass(Test!, "Selected entry was highlighted", ScreenCaptureService!.CaptureScreenImage());

            //5. Select Print Job in the Hamburger Menu Button
            //Expected Result: A toast message will displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Select Print Job");
            dispatchPage.ClickHamburgerButton();
            dispatchPage.ClickPrintJob();
            string toastMessage = dispatchPage.GetToastMessage();
            toastMessage.Should().Be(DispatchJobData.ToastMessage);
            Logger!.LogPass(Test!, "Toast message is displayed", ScreenCaptureService!.CaptureScreenImage());
            dispatchPage.CloseToastMessage();

            //6. Verify that Job details pdf file should be displayed and generated on your local computer
            //Expected Result: Job details pdf file is displayed and generated file should be successfully downloaded on your local computer
            Logger!.LogInformation(Test!, "Verify that Job details pdf file should be displayed and generated file should be successfully downloaded on your local computer");
            dispatchPage.IsJobDetailsFileValidComplete(WebSettings!.DownloadDirectory, DispatchJobData).Should().BeTrue();
            Logger!.LogPass(Test!, "File has been downloaded");

            //7.Delete downloaded Job Details PDF file
            //Expected Result: Downloaded PDF file should be deleted
            //========================================================================
            Logger!.LogInformation(Test!, "Delete Job Details downloaded PDF file ");
            string latestFile = FileExtensions.GetLatestFilePath(WebSettings!.DownloadDirectory);
            FileExtensions.DeleteFile(latestFile);
            FileExtensions.IsFileExists(latestFile).Should().BeFalse();
            Logger!.LogPass(Test!, "Downloaded PDF file has been deleted");

            //8. Logout user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}