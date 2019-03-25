namespace TestWebProject.Utils
{
    using OpenQA.Selenium;

    public class BaseForm
	{
		protected By TitleLocator;
		protected string title;
		public static string titleForm;

		protected BaseForm(By TitleLocator, string title)
		{
			this.TitleLocator = TitleLocator;
			this.title = titleForm = title;
			WaitUntilFormIsOpen();
		}

		public void WaitUntilFormIsOpen()
		{
			var label = new BaseElement(this.TitleLocator, this.title);
			label.WaitForElementIsVisible();
            Logger.Logger.LogPageInfo(title);
		}

        public static string JsReturnTitle()
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Browser.GetDriver();
            return (string)executor.ExecuteScript("return document.title");
        }
    }
}