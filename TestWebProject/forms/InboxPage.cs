namespace TestWebProject.forms
{
    using OpenQA.Selenium;
    using TestWebProject.Utils;

    public class InboxPage : BaseForm
    {
        private static readonly By InboxCreateBtn = By.XPath("(//span[text()='Написать письмо'])[1]");
        private readonly BaseElement InboxMessagesFolderTree = new BaseElement(By.Id("b-nav_folders"));

        public InboxPage() : base(InboxCreateBtn, "Inbox Page")
        {
        }

        private readonly BaseElement createBtn = new BaseElement(InboxCreateBtn);

        public EmailPage ClickCreateNewMessageButton()
        {
            createBtn.Click();
            return new EmailPage();
        }

        public bool IsSucessfullyLoggedIn() => this.InboxMessagesFolderTree.Displayed;
    }
}
