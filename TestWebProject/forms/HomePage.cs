namespace TestWebProject.forms
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using TestWebProject.Entities.User;
    using TestWebProject.Utils;

    public class HomePage : BaseForm
    {
        private static readonly By HomeBtn = By.XPath("//*[@id='mailbox:submit']");
        private readonly BaseElement submitBtn = new BaseElement(HomeBtn);
        private readonly BaseElement loginField = new BaseElement(By.Id("mailbox:login"));
        private readonly BaseElement passwordField = new BaseElement(By.Id("mailbox:password"));
        private readonly BaseElement validationMessage = new BaseElement(By.Id("mailbox:error"));

        public HomePage() : base(HomeBtn, JsReturnTitle()) //JSReturnTitle method was added
        {
        }
        
        public InboxPage Login(User user)
        {
            loginField.Clear();
            loginField.SendKeys(user.login);
            passwordField.Clear();
            passwordField.SendKeys(user.password);
            submitBtn.Click();
            return new InboxPage();
        }

        public void WaitForValidationMessage(string expectedValidationMessage) => Assert.IsTrue(validationMessage.GetText().Contains(expectedValidationMessage), "Validation text is not as expected");

    }
}

