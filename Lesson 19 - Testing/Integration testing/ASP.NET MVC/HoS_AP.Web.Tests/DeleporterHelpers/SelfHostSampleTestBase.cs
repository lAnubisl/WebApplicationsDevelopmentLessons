using System.Linq;
using DeleporterCore.Client;
using DeleporterCore.SelfHosting;
using DeleporterCore.SelfHosting.SeleniumServer.Servers;
using DeleporterCore.SelfHosting.Servers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace HoS_AP.Web.Tests.DeleporterHelpers
{
    [TestClass]
    public class SelfHostSampleTestBase
    {
        // We can only have one instance of AssemblyInitialize/Cleanup per assembly. 
        // If using this as a base class, uncomment these methods.
        //[AssemblyCleanup]
        //public static void AssemblyCleanup() {
        //    Cassini.Instance.Stop();
        //}

        //[AssemblyInitialize]
        //public static void AssemblyInit(TestContext testContext) {
        //    Cassini.Instance.Start();
        //}

        [TestCleanup]
        public void MyTestCleanup() {
            // Runs any tidy up tasks in both the local and remote appdomains
            TidyupUtils.PerformTidyup();
            Deleporter.Run(TidyupUtils.PerformTidyup);
        }

        [TestInitialize]
        public void TestInit() {
          
            // May want to run some setup that applies to each test - e.g. Membership
            Deleporter.Run(() =>
                               {
                                   //var membershipMock = new Mock<IMembershipService>();
                                   //membershipMock.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
                                   //membershipMock.Setup(x => x.GetUser(It.IsAny<string>())).Returns(new Person
                                   //{
                                   //    UserName = "SomePerson",
                                   //    EmailAddress = "some@email.com",
                                   //    MemberID = 1
                                   //});
                                   //NinjectControllerFactoryUtils.TemporarilyReplaceBinding(membershipMock.Object);

                                   //// Example of blowing up if we don't explicitly setup a mock on an interface (data access in this example)
                                   //var domainContextMock = new Mock<IDomainContext>(MockBehavior.Strict);
                                   //NinjectControllerFactoryUtils.TemporarilyReplaceBinding(domainContextMock.Object);
                               });
        }
    }
}