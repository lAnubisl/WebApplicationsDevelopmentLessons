using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;

namespace HoS_AP.Web.Tests
{
    [Binding]
    public class TestRunConfiguration
    {
        internal static InternetExplorerDriver BrowserDriver { get; private set; }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            StartIIS();
            BrowserDriver = new InternetExplorerDriver(GetCurrentDirectory());
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            if (Constants.iisProcess.HasExited == false)
            {
                Constants.iisProcess.Kill();
            }

            BrowserDriver.Quit();
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            BrowserDriver.Manage().Cookies.DeleteAllCookies();
        }

        private static void StartIIS()
        {
            var applicationPath = GetApplicationPath();
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            Constants.iisProcess = new Process();
            Constants.iisProcess.StartInfo.FileName = programFiles + @"\IIS Express\iisexpress.exe";
            Constants.iisProcess.StartInfo.Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, Constants.IisPort);
            Constants.iisProcess.Start();

            using (var client = new HttpClient())
            {
                client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"http://localhost:{Constants.IisPort}")).Wait();
            }
        }

        internal static string GetApplicationPath()
        {
            var path = Path.Combine(GetCurrentDirectory(), Constants.WebAppRelativePath);
            path = Path.GetFullPath(path);
            return path;
        }

        private static string GetCurrentDirectory()
        {
            var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}