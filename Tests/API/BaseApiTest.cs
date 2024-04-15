using Allure.Net.Commons;
using Allure.NUnit;
using NLog;
using System.Reflection;
using Testiny.Clients;
using Testiny.Services;

namespace Testiny.Tests.API
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [AllureNUnit]
    public class BaseApiTest
    {
        protected readonly string LocationResources = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources");
        protected readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected ProjectService? ProjectService;
        protected CaseService? CaseService;
        protected CaseService? CaseServiceNotAuth;
        protected Random Random = new Random();

        [OneTimeSetUp]
        public static void OneTimeSetup()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }

        [OneTimeSetUp]
        public void SetUpApi()
        {
            var restClient = new RestClientExtended();
            var restClientNotAyth = new RestClientExtended(false);

            ProjectService = new ProjectService(restClient);
            CaseService = new CaseService(restClient);
            CaseServiceNotAuth = new CaseService(restClientNotAyth);
        }

        [OneTimeTearDown]
        public void TearDownApi()
        {
            ProjectService?.Dispose();
            CaseService?.Dispose();
            CaseServiceNotAuth?.Dispose();
        }
    }
}