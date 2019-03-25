namespace TestWebProject
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestWebProject.Entities.Email;
    using TestWebProject.Entities.User;
    using TestWebProject.forms;
    using TestWebProject.Utils;

    [TestClass]
    public class HappyPathTest : BaseTest
    {
        //Test data
        const string login = "testmail.2020";
        const string password = "Asas432111";
        const string address = "elenasinevich91@gmail.com";
        static readonly string subject = $"Test Mail {TestUtils.GetRandomSubjectNumber()}";
        const string expectedTestBody = "Test Text";

        UserCreator usercreator = new ValidUserCreator();
        Email email = new EmptyEmail(address, subject, expectedTestBody);

        [TestMethod]
        public void TestSmokeEmail()
        {
            User user = usercreator.Create(login, password);

            Logger.Logger.LogStep("Login to the mail.ru");
            HomePage homePage = new HomePage();
            InboxPage inboxPage = homePage.Login(user);

            Logger.Logger.LogStep("Assert a user is logged in");
            Assert.IsTrue(inboxPage.IsSucessfullyLoggedIn(), "User is not logged in");

            Logger.Logger.LogStep("Create a new mail");
            EmailPage emailPage = inboxPage.ClickCreateNewMessageButton();
            email = new DraftEmail(email);

            Logger.Logger.LogStep("Navigate to DraftsPage");
            NavigationMenu navigationMenu = new NavigationMenu();
            DraftsPage draftsPage = navigationMenu.NavigateToDrafts();

            Logger.Logger.LogStep("Open Draft Email on DraftsPage");
            emailPage = draftsPage.ClickDraftEmail(email);

            Logger.Logger.LogStep("Verify the draft content (addressee, subject and body – should be the same) ");
            Assert.IsTrue(emailPage.GetAddress().Equals(address), "Address is wrong.");
            Assert.IsTrue(emailPage.GetSubject().Equals(email.subject), "Message subject doesn't match");
            Assert.IsTrue(emailPage.GetMessage().Contains(expectedTestBody), "Message is incorrect.");

            Logger.Logger.LogStep("Send the mail");
            emailPage.ClickSendEmailButton();

            // Verify the email is sent message
            //Assert.IsTrue(emailPage.GetVerificationMessage().Contains(ExpectedMessage));

            Logger.Logger.LogStep("Navigate to DraftsPage and verify, that the mail disappeared from ‘Drafts’ folder");
            draftsPage = navigationMenu.NavigateToDrafts();
            draftsPage.WaitForEmailDisappearedBySubject(email.subject);
            Assert.IsFalse(draftsPage.IsEmailPresentBySubject(email.subject));

            Logger.Logger.LogStep("Navigate to SentPage");
            SentPage sentPage = navigationMenu.NavigateToSent();

            Logger.Logger.LogStep("Verify, that the mail presents in ‘Sent’ folder. ");
            sentPage.WaitForEmailinSentFolder(subject);
            Assert.IsTrue(sentPage.IsEmailPresentBySubject(email.subject));

            Logger.Logger.LogStep("Log out");
            navigationMenu.LogOut();
        }

        [TestMethod]
        public void TestDeleteEmail()
        {
            User user = usercreator.Create(login, password);

            //Login to the mail.ru
            HomePage homePage = new HomePage();
            InboxPage inboxPage = homePage.Login(user);

            //Assert a user is logged in
            Assert.IsTrue(inboxPage.IsSucessfullyLoggedIn(), "User is not logged in");

            //Create a new mail 
            EmailPage emailPage = inboxPage.ClickCreateNewMessageButton();
            email = new DraftEmail(email);

            //Send the mail
            emailPage.ClickSendEmailButton();

            //Navigate to SentPage
            NavigationMenu navigationMenu = new NavigationMenu();
            SentPage sentPage = navigationMenu.NavigateToSent();

            //Verify, that the mail presents in ‘Sent’ folder. 
            sentPage.WaitForEmailinSentFolder(subject);

            //Delete the mail from Sent folder
            //sentPage.DeleteEmail(subject);

            //Delete email dragging to the trash bin
            sentPage.DragEmailToTrashBin(subject);

            //Navigate to recycle bin
            RecycleBinPage recyclePage = navigationMenu.NavigateToRecycle();

            //Verify, that the mail presents in ‘Recycle bin’ folder. 
            recyclePage.WaitForDeletedEmail(subject);
        }

        [TestMethod]
        public void TestInvalidLogin()
        {
            User user = usercreator.Create(login, password);
            usercreator = new InvalidUserCreator();
            User invalidUser = usercreator.Create(login, password);

            string expectedValidationMessage = "Неверное имя или пароль";

            //Login to the mail.ru with invalid password
            HomePage homePage = new HomePage();
            homePage.Login(invalidUser);

            //Verify, that red text message appears
            homePage.WaitForValidationMessage(expectedValidationMessage);

            //Login to the mail.ru 
            homePage.Login(user);
        }

        [TestMethod]
        public void Test()
        {
            Log.Info("Test!");
           
        }
    }
}
