using Allure.Net.Commons;
using Allure.NUnit;
using NLog;
using Testiny.Clients;
using Testiny.Services;

namespace Testiny.Tests.API
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [AllureNUnit]
    public class BaseApiTest
    {
        protected readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected ProjectService? ProjectService;
        protected CaseService? CaseService;
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
            ProjectService = new ProjectService(restClient);
            CaseService = new CaseService(restClient);
        }

        [OneTimeTearDown]
        public void TearDownApi()
        {
            ProjectService?.Dispose();
            CaseService?.Dispose();
        }
    }
}