using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Microsoft.Owin.Hosting;
using NUnit.Framework;
using Web.Api.JMeterTests.Properties;

namespace Web.Api.JMeterTests
{
    [TestFixture]
    public class JMeterIntegrationTests
    {
        private static readonly string BaseAddress = "http://localhost:8888/";
        private IDisposable webApp;

        [OneTimeSetUp]
        public void Up()
        {
            webApp = WebApp.Start<MyWebApi>(BaseAddress);
        }

        [OneTimeTearDown]
        public void Down()
        {
            webApp.Dispose();
        }

        [TestCase]
        public void RunJMeterSuite()
        {
            PatchJMeterInputFile();

            if (File.Exists(JMeterOutputFilePath()))
            {
                File.Delete(JMeterOutputFilePath());
            }

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Settings.Default.jMeterExecutableFile,
                    Arguments = "-n -t \"" + JMeterInputFilePath() + "\"",
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };

            proc.Start();
            proc.WaitForExit();

            if (!File.Exists(JMeterOutputFilePath()))
            {
                Assert.Fail("Cannot find " + JMeterOutputFilePath());
            }

            var jMeterOutputFileText = File.ReadAllText(JMeterOutputFilePath());
            var serializer = new XmlSerializer(typeof(testResults));
            testResults results = null;
            using (var reader = new StringReader(jMeterOutputFileText))
            {
                results = (testResults)serializer.Deserialize(reader);
            }

            var sb = new StringBuilder();
            foreach (var ri in results.httpSample.Where(s => s.s == false))
            {
                sb.AppendLine(string.Format("Test {0}{2}ResponseCode {3}{2}Response {1}", ri.lb, ri.responseData.Value,
                    Environment.NewLine, ri.rc));
            }

            if (sb.Length > 0)
            {
                Assert.Fail(sb.ToString());
            }
        }

        private static void PatchJMeterInputFile()
        {
            var jMeterInputFileText = File.ReadAllText(JMeterInputFilePath());
            jMeterInputFileText = PatchOutputFileLocation(jMeterInputFileText);
            File.WriteAllText(JMeterInputFilePath(), jMeterInputFileText);
        }

        private static string PatchOutputFileLocation(string jMeterInputFileText)
        {
            var jMeterOuputFileLocation =
                Regex.Match(jMeterInputFileText, "<stringProp name=\"filename\">[^<]+\\.jtl</stringProp>").Value;
            return jMeterInputFileText.Replace(jMeterOuputFileLocation,
                "<stringProp name=\"filename\">" + JMeterOutputFilePath() + "</stringProp>");
        }

        private static string JMeterInputFilePath()
        {
            return Path.Combine(CurrentPath(), "tests.jmx").Replace("\\", "/");
        }

        private static string JMeterOutputFilePath()
        {
            return Path.Combine(CurrentPath(), "tests.jtl").Replace("\\", "/");
        }

        private static string CurrentPath()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.CodeBase
                .Remove(assembly.CodeBase.LastIndexOf("/", StringComparison.Ordinal))
                .Replace("file:///", string.Empty);
        }
    }
}