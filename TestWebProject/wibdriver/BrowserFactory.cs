namespace TestWebProject.Utils
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Remote;
    public class BrowserFactory
	{
		public enum BrowserType
		{
			Chrome,
			Firefox,
			IEedge,
			phantomJs,
            remoteFirefox,
            remoteChrome
		}

		public static IWebDriver GetDriver(BrowserType type, int timeOutSec)
		{
			IWebDriver driver = null;

			switch (type)
			{
				case BrowserType.Chrome:
				{
					var service = ChromeDriverService.CreateDefaultService();
					var option = new ChromeOptions();
					option.AddArgument("disable-infobars");
					driver = new ChromeDriver(service, option, TimeSpan.FromSeconds(timeOutSec));
					break;
				}
					case BrowserType.Firefox:
				{
					var service = FirefoxDriverService.CreateDefaultService();
					var options = new FirefoxOptions();
					driver = new FirefoxDriver(service, options, TimeSpan.FromSeconds(timeOutSec));
					break;
				}

                case BrowserType.remoteFirefox:
                    {
                        var capability = DesiredCapabilities.Firefox();
                        capability.SetCapability(CapabilityType.BrowserName, "firefox");
                        capability.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Vista));
                        driver = new RemoteWebDriver(new Uri("http://localhost:5567/wd/hub"), capability);
                        break;
                    }

                case BrowserType.remoteChrome:
                    {
                        var capability = DesiredCapabilities.Chrome();
                        capability.SetCapability(CapabilityType.BrowserName, "chrome");
                        capability.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Vista));
                        driver = new RemoteWebDriver(new Uri("http://localhost:5566/wd/hub"), capability);
                        break;
                    }
            }
			return driver;
		}
	}
}