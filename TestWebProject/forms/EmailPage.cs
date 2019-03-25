namespace TestWebProject.forms
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using TestWebProject.Entities.Email;
    using TestWebProject.Utils;

    public class EmailPage : BaseForm
    {
        private static readonly By EmailToField = By.XPath("//textarea[@data-original-name='To']");
        private readonly BaseElement addresseeField = new BaseElement(EmailToField);
        private readonly BaseElement subjectField = new BaseElement(By.XPath("//input[@name='Subject']"));
        private readonly BaseElement bodyTextFrame = new BaseElement(By.XPath("//iframe"));
        private readonly BaseElement bodyTextField = new BaseElement(By.XPath("//*[@id='tinymce']"));
        private readonly BaseElement saveBtn = new BaseElement(By.XPath("(//div[@data-name='saveDraft'])[1]"));
        private readonly BaseElement draftEmailSavedLbl = new BaseElement(By.XPath("(//div[text()='Сохранено в '])[1]"));
        private readonly BaseElement sendBtn = new BaseElement(By.XPath("(//div[@data-name='send'])[1]"));
        private readonly BaseElement sentEmailAdressLbl = new BaseElement(By.XPath("(//span[text()='elenasinevich91@gmail.com'])[2]"));
        private readonly BaseElement sentEmailBodyTextLbl = new BaseElement(By.XPath("//body[@id='tinymce']//div[text()[contains(.,'Test Text')]]"));
        private static readonly By SentEmailVerificationText = By.ClassName("message-sent__title");

        public EmailPage() : base(EmailToField, "Email Page")
        {
        }

        public void CreateDraftEmail(Email email)
        {
            addresseeField.SendKeys(email.address);
            subjectField.SendKeys(email.subject);
            Browser.GetDriver().SwitchTo().Frame(this.bodyTextFrame.GetElement());
            bodyTextField.SendKeys(email.expectedTestBody);
            Browser.GetDriver().SwitchTo().DefaultContent();
            saveBtn.Click();
            draftEmailSavedLbl.WaitForElementIsVisible();
        }

        public void WaitForEmailFields(string address, string subject, string expectedTestBody)
        {
            //Verify the draft addressee is still the same
            Assert.AreEqual(address, sentEmailAdressLbl.GetText(), "Email is not actual");

            //Verify the draft subject is still the same
            Assert.AreEqual(subject, subjectField.GetAttribute("value"), "Subject is not actual");

            //Verify the draft body text is still the same
            Browser.GetDriver().SwitchTo().Frame(this.bodyTextFrame.GetElement());
            Assert.IsTrue(sentEmailBodyTextLbl.GetText().Contains(expectedTestBody), "Body text is not as expected");
            Browser.GetDriver().SwitchTo().DefaultContent();
        }

        public void ClickSendEmailButton() => sendBtn.Click();

        public string GetAddress() => sentEmailAdressLbl.GetText();

        public string GetSubject() => subjectField.GetAttribute("value");

        public string GetMessage()
        {
            Browser.GetDriver().SwitchTo().Frame(this.bodyTextFrame.GetElement());
            string message = sentEmailBodyTextLbl.GetText();
            Browser.GetDriver().SwitchTo().DefaultContent();
            return message;
        }

        public string GetVerificationMessage()
        {
            this.sendBtn.WaitForNotVisible();
            return Browser.GetDriver().FindElement(SentEmailVerificationText).Text;
        }
    }
}
