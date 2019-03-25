namespace TestWebProject.Utils
{
    using System;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Threading;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;

    public class BaseElement:IWebElement
	{
		protected string Name;
		protected By Locator;
		protected IWebElement Element;

		public BaseElement(By locator, string name)
		{
			Locator = locator;
			Name = name == "" ? GetText() : name;
		}

        public BaseElement(By locator) => Locator = locator;

        public string GetText()
		{
			WaitForElementIsVisible();
			return Browser.GetDriver().FindElement(Locator).Text;
		}

		public IWebElement GetElement()
		{
			try
			{
				Element = Browser.GetDriver().FindElement(Locator);
			}
			catch (Exception)
			{
				
				throw;
			}
			return Element;
		}

		public void WaitForElementIsVisible()
		{
            try
            {
                new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Browser.TimeoutForElement)).Until(ExpectedConditions.ElementIsVisible(Locator));

                if (Configuration.RunWithHighlightForDebug.Equals("yes"))
                {
                    JSHighlight();
                }
            }
            catch
            {
                TestUtils.TakeScreenshot();
                Logger.Logger.Log.Error($"Element {Locator} has not been displayed");
            }
        }

        public void WaitForNotVisible() => new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Browser.TimeoutForElement)).Until(ExpectedConditions.InvisibilityOfElementLocated(Locator));

        public IWebElement FindElement(By @by) => throw new System.NotImplementedException();

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
		{
			throw new System.NotImplementedException();
		}

		public void Clear()
		{
            WaitForElementIsVisible();
            Browser.GetDriver().FindElement(Locator).Clear();
        }

		public void SendKeys(string text)
		{
            WaitForElementIsVisible();
            Browser.GetDriver().FindElement(Locator).SendKeys(text);
        }

        public void Submit() => throw new System.NotImplementedException();

        public void Click()
		{
			WaitForElementIsVisible();
            TestUtils.WaitElementAvailable(Locator);
			Browser.GetDriver().FindElement(Locator).Click();
		}

		public void JsClick()
		{
			WaitForElementIsVisible();
			IJavaScriptExecutor executor = (IJavaScriptExecutor)Browser.GetDriver();
			executor.ExecuteScript("arguments[0].click();", GetElement());
		}

        //Added JSHighlight method for web elements
        public void JSHighlight()
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Browser.GetDriver();
            string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: red"";";
            try
            {
                executor.ExecuteScript(highlightJavascript, GetElement());
                Thread.Sleep(400);
                executor.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", GetElement(), "");
            }
            catch
            {
                Console.Out.WriteLine("Element {0} wasn't able to be highlight", Locator);
            }
        }

        public string GetAttribute(string attributeName)
		{
            return Browser.GetDriver().FindElement(Locator).GetAttribute(attributeName);
        }

        public void DragAndDrop(IWebElement target)
        {
            Actions actions = new Actions(Browser.GetDriver());
            actions.DragAndDrop(Browser.GetDriver().FindElement(Locator), target);
            actions.Build().Perform();
        }

        public void ContextClick()
        {
            Actions actions = new Actions(Browser.GetDriver());
            actions.ContextClick(Browser.GetDriver().FindElement(Locator));
            actions.Build().Perform();
        }

        public string GetCssValue(string propertyName) => throw new System.NotImplementedException();

        public string GetProperty(string propertyName) => throw new NotImplementedException();

        public bool Displayed
        {
            get
            {
                try
                {
                    return Browser.GetDriver().FindElement(Locator).Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public string TagName { get; }
		public string Text { get; }
		public bool Enabled { get; }
		public bool Selected { get; }
		public Point Location { get; }
		public Size Size { get; }
	}
}
