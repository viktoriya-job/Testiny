using NLog;
using Testiny.Models;

namespace Testiny.Tests.API
{
    public class CaseTests : BaseApiTest
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private Case _case = null;

        [Test]
        public void GetCaseTest()
        {
            var actualCase = CaseService.GetCase(68);

            //Assert.Multiple(() =>
            //{
            //    Assert.That(actualCase.Result.Title, Is.EqualTo(_case.Title));
            //    Assert.That(actualCase.Result.PriorityId, Is.EqualTo(_case.PriorityId));
            //});

            _case = actualCase.Result;
            _logger.Info(_case.ToString());
        }
    }
}