using Allure.NUnit.Attributes;
using NLog;
using System.Reflection;
using Testiny.Helpers;
using Testiny.Models;
using Allure.Net.Commons;

namespace Testiny.Tests.API
{
    [TestFixture]
    public class CaseTests : BaseApiTest
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private Case _case = null;
        private Project _project = null;

        [OneTimeSetUp]
        public void AddData()
        {
            string projectFileName = "projectTestdata.json";
            string caseFileName = "caseTestdata.json";

            string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //Adding Project from Json
            string path = Path.Combine(location, "Resources", projectFileName);

            Project project = JsonHelper<Project>.FromJson(path, FileMode.Open);

            _project = ProjectService.AddProject(project).Result;
            _logger.Info(_project.ToString());


            //Adding TestCase from Json
            path = Path.Combine(location, "Resources", caseFileName);

            Case tCase = JsonHelper<Case>.FromJson(path, FileMode.Open);
            tCase.ProjectId = _project.Id;

            _case = CaseService.AddCase(tCase).Result;
            _logger.Info(_case.ToString());
        }

        [Test]
        [Order(1)]
        public void GetCaseTest()
        {
            var actualCase = CaseService.GetCase(_case.Id);

            Assert.Multiple(() =>
            {
                Assert.That(actualCase.Result.Title, Is.EqualTo(_case.Title));
                Assert.That(actualCase.Result.ProjectId, Is.EqualTo(_case.ProjectId));
            });

            _logger.Info(actualCase.Result.ToString());
        }

        [Test]
        [Order(2)]
        public void RemoveData()
        {
            var actualProject = ProjectService.RemoveProject(_project.Id);

            Assert.Multiple(() =>
            {
                Assert.That(actualProject.Result.IsDeleted == true);
                Assert.That(actualProject.Result.DeletedBy != null);
            });

            _project = actualProject.Result;
            _logger.Info(_project.ToString());
        }

        [Test]
        [Order(3)]
        public void GetCaseTestFromRemovedProject()
        {
            var actualCase = CaseService.GetCase(_case.Id);

            Assert.Multiple(() =>
            {
                Assert.That(actualCase.Result.Title, Is.EqualTo(_case.Title));
                Assert.That(actualCase.Result.ProjectId, Is.EqualTo(_case.ProjectId));
                Assert.That(actualCase.Result.IdDeleted == false);
            });

            _logger.Info(actualCase.Result.ToString());
        }
    }
}