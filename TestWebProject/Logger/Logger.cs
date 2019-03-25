namespace TestWebProject.Logger
{
    using log4net;
    using log4net.Config;
    using TestWebProject.Utils;

    public static class Logger
    {
        public static ILog Log { get; } = LogManager.GetLogger("LOGGER");

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }

        public static void LogPageInfo(string pageTitle)
        {
            Log.Debug($"Page {pageTitle} is opened");
        }

        public static void LogStep(string stepInfo)
        {
            Log.Info($"Step {BaseTest.StepNumber}: {stepInfo}");
            BaseTest.StepNumber++;
        }
    }
}
