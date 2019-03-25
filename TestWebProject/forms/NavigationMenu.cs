namespace TestWebProject.forms
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using TestWebProject.Utils;

    class NavigationMenu : BaseForm
    {
        private static readonly By NavigationLbl = By.XPath("//a[@data-mnemo='drafts']");
        private readonly BaseElement recycleBinMenuItem = new BaseElement(By.XPath("//span[text()='Корзина']"));

        [FindsBy(How = How.XPath, Using = "//span[text()='Отправленные']")]
        private IWebElement sentMenuItem;

        [FindsBy(How = How.XPath, Using = "//*[@data-mnemo='drafts']")]
        private IWebElement draftsMenuItem;

        [FindsBy(How = How.XPath, Using = "//a[@id='PH_logoutLink']")]
        private IWebElement LogOffBtn;

        public NavigationMenu() : base(NavigationLbl, "Navigation Page") => PageFactory.InitElements(Browser.GetDriver(), this);

        public DraftsPage NavigateToDrafts()
        {
            draftsMenuItem.Click();
            return new DraftsPage();
        }
        public SentPage NavigateToSent()
        {
            sentMenuItem.Click();
            return new SentPage();
        }
        public HomePage LogOut()
        {
            LogOffBtn.Click();
            return new HomePage();
        }

        public RecycleBinPage NavigateToRecycle()
        {
            recycleBinMenuItem.Click();
            return new RecycleBinPage();
        }
    }
}
