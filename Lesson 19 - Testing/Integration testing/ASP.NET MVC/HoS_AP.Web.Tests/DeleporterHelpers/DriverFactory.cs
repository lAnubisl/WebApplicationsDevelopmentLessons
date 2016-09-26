using System;
using System.Linq;
using DeleporterCore.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace HoS_AP.Web.Tests.DeleporterHelpers
{
    /// <summary>
    ///   Allows a WebDriver to be shared without having
    /// </summary>
    public static class DriverFactory
    {
        private static IWebDriver _currentDriver;
        private static Func<IWebDriver> _driverGenerationMethod;

        /// <summary>
        ///   Consume this method to get a Selenium WebDriver
        /// </summary>
        /// <returns> An instance of an IWebDriver implementation as set by DriverGenerationMethod </returns>
        public static IWebDriver Driver {
            get {
                if (_currentDriver != null) return _currentDriver;
                return _currentDriver = DriverGenerationMethod.Invoke();
            }
        }

        /// <summary>
        ///   Set this method to determine which Selenium WebDriver to instantiate Default is to use RemoteWebDriver
        /// </summary>
        public static Func<IWebDriver> DriverGenerationMethod {
            get {
                return _driverGenerationMethod
                       ??
                       (() =>
                        new RemoteWebDriver(
                                new Uri(string.Format("http://127.0.0.1:{0}/wd/hub",
                                                      DeleporterConfiguration.SeleniumServerPort)),
                                DesiredCapabilities.HtmlUnitWithJavaScript()));
            }
            set { _driverGenerationMethod = value; }
        }

        /// <summary>
        ///   Causes a new WebDriver to be instantiated when Driver is next called.
        /// </summary>
        public static void GetNewDriverInstance() {
            if (_currentDriver != null) _currentDriver.Quit();
            _currentDriver = null;
        }
    }
}