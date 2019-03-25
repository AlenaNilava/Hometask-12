namespace TestWebProject.Utils
{
    using System;
    using OpenQA.Selenium;
    public class Browser
	{
		private static Browser _currentInstance;
		private static IWebDriver _driver;
		public static BrowserFactory.BrowserType CurrentBrowser;
		public static int ImplWait;
		public static double TimeoutForElement;
		private static string _browser;

		private Browser()
		{
			InitParamas();
			_driver = BrowserFactory.GetDriver(CurrentBrowser, ImplWait);
		}

		private static void InitParamas()
		{
			ImplWait = Convert.ToInt32(Configuration.ElementTimeout);
			TimeoutForElement = Convert.ToDouble(Configuration.ElementTimeout);
			_browser = Configuration.Browser;
			Enum.TryParse(_browser, out CurrentBrowser);
		}
        
        public static Browser GetInstance()
        {
            if (_currentInstance == null)
                _currentInstance = new Browser();
            return _currentInstance;
        }

        public static void WindowMaximise()
		{
			_driver.Manage().Window.Maximize();
		}

		public static void NavigateTo(string url)
		{
			_driver.Navigate().GoToUrl(url);
		}

		public static IWebDriver GetDriver()
		{
			return _driver;
		}

		public static void Quit()
		{
			_driver.Quit();
			_currentInstance = null;
			_driver = null;
			_browser = null;
		}
	}
}