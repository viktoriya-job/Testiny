using Allure.Net.Commons;
using Allure.NUnit;
using OpenQA.Selenium;
using System.Text;
using Testiny.Core;
using Testiny.Helpers;
using Testiny.Helpers.Configuration;
using Testiny.Steps;

namespace Testiny.Tests
{
    [Parallelizable(scope: ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [AllureNUnit]
    public class BaseTest
    {
        protected IWebDriver Driver { get; private set; }
        protected WaitsHelper WaitsHelper { get; private set; }

        protected Random Random = new Random();

        protected NavigationSteps NavigationSteps;
        protected ProjectsSteps ProjectSteps;

        [OneTimeSetUp]
        public static void OneTimeSetup()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }

        [SetUp]
        public void FactoryDriverTest()
        {
            Driver = new Browser().Driver;

            NavigationSteps = new NavigationSteps(Driver);
            ProjectSteps = new ProjectsSteps(Driver);

            Driver.Navigate().GoToUrl(Configurator.AppSettings.URL);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                    byte[] screenshotBytes = screenshot.AsByteArray;

                    AllureApi.AddAttachment("Screenshot", "image/png", screenshotBytes);
                    AllureApi.AddAttachment("error.txt", "text/plain", Encoding.UTF8.GetBytes(TestContext.CurrentContext.Result.Message));
                }
            }

            finally
            {
                Driver.Quit();
            }
        }
    }
}