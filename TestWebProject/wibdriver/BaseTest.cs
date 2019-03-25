namespace TestWebProject.Utils
{
    using log4net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestWebProject.Logger;

    [TestClass]
    public class BaseTest
	{
        public static TestContext TestContext { get; set; }
        protected static Browser Browser = Browser.GetInstance();
        protected static ILog Log = Logger.Log;
        public static int StepNumber;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context){
            TestContext = context;
        }


        [TestInitialize]
        public virtual void InitTest()
        {
            StepNumber = 1;
            Logger.InitLogger();
            Browser = Browser.GetInstance();
            Browser.WindowMaximise();
            Browser.NavigateTo(Configuration.StartUrl);
        }

		[TestCleanup]
		public virtual void CleanTest()
		{
			Browser.Quit();
		}
	}
}