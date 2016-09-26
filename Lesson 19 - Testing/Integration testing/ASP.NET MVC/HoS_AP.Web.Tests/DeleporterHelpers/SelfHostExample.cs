//using System;
//using System.Linq;
//using System.Net;
//using DeleporterCore.Client;
//using DeleporterCore.SelfHosting;
//using DeleporterCore.SelfHosting.Configuration;
//using DeleporterCore.SelfHosting.Servers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Remote;

//namespace Sample.SeleniumSelfHost.IntegrationTest
//{
//    /// <summary>
//    /// Note: This code will only compile if you are referencing Selenium, MSTest, Ninject & MOQ.
//    /// It does however provide an example of how one may run tests with Deleporter.Client
//    /// </summary>
//    [TestClass]
//    public class SimpleTest
//    {
//        [AssemblyCleanup]
//        public static void AssemblyCleanup() {
//            Cassini.Instance.Stop();
//        }

//        [AssemblyInitialize]
//        public static void AssemblyInit(TestContext testContext) {
//            Cassini.Instance.Start();
//        }

//        [TestMethod]
//        public void DisplaysCurrentYear() {
//            Driver.Navigate().GoToUrl(DeleporterSelfHostConfiguration.SiteBaseUrl);
//            var dateElement = Driver.FindElement(By.Id("date"));
//            var displayedDate = DateTime.Parse(dateElement.Text);
//            Assert.AreEqual(DateTime.Now.Year, displayedDate.Year);

//            Console.WriteLine(new WebClient().DownloadString(DeleporterSelfHostConfiguration.SiteBaseUrl));
//        }

//        [TestMethod]
//        public void DisplaysSpecialMessageIfWebServerHasSomehowGoneBackInTime() {
//            // Inject a mock IDateProvider, setting the clock back to 1975
//            var dateToSimulate = new DateTime(1975, 1, 1);
//            Deleporter.Run(() =>
//                               {
//                                   var mockDateProvider = new Mock<IDateProvider>();
//                                   mockDateProvider.Setup(x => x.CurrentDate).Returns(dateToSimulate);
//                                   NinjectControllerFactoryUtils.TemporarilyReplaceBinding(mockDateProvider.Object);
//                               });

//            // Now see what it displays
//            Driver.Navigate().GoToUrl(DeleporterSelfHostConfiguration.SiteBaseUrl);
//            var dateElement = Driver.FindElement(By.Id("date"));
//            var displayedDate = DateTime.Parse(dateElement.Text);
//            Assert.AreEqual(1975, displayedDate.Year);

//            var extraInfo = Driver.FindElement(By.Id("extraInfo")).Text;
//            Assert.IsTrue(extraInfo.Contains("The world wide web hasn't been invented yet"));

//            Console.WriteLine(new WebClient().DownloadString(DeleporterSelfHostConfiguration.SiteBaseUrl));
//        }

//        [TestCleanup]
//        public void MyTestCleanup() {
//            Driver.Quit();

//            // Runs any tidy up tasks in both the local and remote appdomains
//            TidyupUtils.PerformTidyup();
//            Deleporter.Run(TidyupUtils.PerformTidyup);
//        }

//        [TestInitialize]
//        public void TestInit() {
//            // Use a new browser for each test.
//            Driver = new FirefoxDriver();
//        }
//    }
//}