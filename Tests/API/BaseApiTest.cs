using NLog;
using Testiny.Clients;
using Testiny.Services;

namespace Testiny.Tests.API
{
    public class BaseApiTest
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        protected ProjectService? ProjectService;
        protected CaseService? CaseService;
        protected Random Random = new Random();

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