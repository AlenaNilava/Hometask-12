namespace TestWebProject.forms
{
    using OpenQA.Selenium;
    using System;
    using TestWebProject.Entities.Email;
    using TestWebProject.Utils;

    public class DraftsPage : BaseForm
    {
        private static readonly By DraftsLbl = By.XPath("//a[@data-mnemo='drafts']");
        string draftEmailLblTemplate = "(//div[text()='{0}'])[1]";
        string sentEmailLblTemplate = "(//a[@data-subject='{0}'])[1]";

        public DraftsPage() : base(DraftsLbl, "Drafts Page")
        {
        }

        public EmailPage ClickDraftEmail(Email email)
        {
            new BaseElement(By.XPath(String.Format(draftEmailLblTemplate, email.subject))).Click();
            return new EmailPage();
        }

        public void WaitForEmailDisappearedBySubject(string subject) => new BaseElement(By.XPath(String.Format(sentEmailLblTemplate, subject))).WaitForNotVisible();

        public bool IsEmailPresentBySubject(string subject) =>
            new BaseElement(By.XPath(String.Format(sentEmailLblTemplate, subject))).Displayed;

    }
}