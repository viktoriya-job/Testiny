using Allure.NUnit.Attributes;
using NLog;
using Testiny.Helpers;
using Testiny.Models;
using System.Net;
using Testiny.Helpers.Configuration;
using Allure.Net.Commons;

namespace Testiny.Tests.API
{
    [TestFixture]
    [AllureSuite("TestCases API Tests")]
    public class CaseTests : BaseApiTest
    {
        private List<Case> _cases = new List<Case>();
        private Project _project = null;

        [OneTimeSetUp]
        public void AddData()
        {
            string projectFilePath = Path.Combine(Configurator.LocationResources, "projectTestdata.json");
            string caseFilePath = Path.Combine(Configurator.LocationResources, "caseTestdata.json");

            Project project = JsonHelper<Project>.FromJson(projectFilePath, FileMode.Open);

            _project = ProjectService.AddProject(project).Result;
            Logger.Info(_project.ToString());

            _cases = JsonHelper<List<Case>>.FromJson(caseFilePath, FileMode.Open);

            for (int i = 0; i < _cases.Count; i++)
            {
                _cases[i].ProjectId = _project.Id;
                _cases[i] = CaseService.AddCase(_cases[i]).Result;
            }

            Assert.That(_cases.Count == 5);
            Logger.Info($"{_cases.Count} test-cases added to the project {_project.Id}, {_project.ProjectName}");
        }

        [Test]
        [AllureFeature("API GET Method")]
        [AllureFeature("API NFE Tests")]
        public void GetCaseByIdTest()
        {
            AllureApi.Step("Sending a request");
            var response = CaseService.GetCaseById(_cases[0].Id);

            AllureApi.Step("Response processing");
            Case actualCase = JsonHelper<Case>.FromJson(response.Result.Content);

            Assert.Multiple(() =>
            {
                Assert.That(response.Result.StatusCode == HttpStatusCode.OK);
                Assert.That(actualCase.Title, Is.EqualTo(_cases[0].Title));
                Assert.That(actualCase.ProjectId, Is.EqualTo(_cases[0].ProjectId));
            });

            Logger.Info(actualCase.ToString());
        }

        [Test]
        [AllureFeature("API GET Method")]
        [AllureFeature("API NFE Tests")]
        public void GetCasesByQueryTest()
        {
            AllureApi.Step("Sending a request");
            string query = "{\"filter\": {\"project_id\": " + $"{_project.Id}" + "}}";
            var response = CaseService.GetCasesByQuery(query);

            AllureApi.Step("Response processing");
            Cases actualCases = JsonHelper<Cases>.FromJson(response.Result.Content);

            Logger.Info(actualCases.CaseList.Count());
            Logger.Info(_cases.Count());

            Assert.Multiple(() =>
            {
                Assert.That(response.Result.StatusCode == HttpStatusCode.OK);
                Assert.That(actualCases.CaseList.Count() == _cases.Count);
            });

            Logger.Info(actualCases.Meta.ToString());
        }

        [Test]
        [AllureFeature("API GET Method")]
        [AllureFeature("API AFE Tests")]
        public void GetCaseNotAuth()
        {
            AllureApi.Step("Sending a request");
            var response = CaseServiceNotAuth.GetCaseById(_cases[0].Id);

            AllureApi.Step("Response processing");
            FailedResponse responseBody = JsonHelper<FailedResponse>.FromJson(response.Result.Content);

            Assert.Multiple(() =>
            {
                Assert.That(response.Result.StatusCode == HttpStatusCode.Forbidden);
                Assert.That(responseBody.Code, Is.EqualTo("API_ACCESS_DENIED"));
                Assert.That(responseBody.Message, Is.EqualTo("Invalid API key"));
            });
        }

        [OneTimeTearDown]
        public void RemoveData()
        {
            var actualProject = ProjectService.RemoveProject(_project.Id);

            Assert.Multiple(() =>
            {
                Assert.That(actualProject.Result.IsDeleted == true);
                Assert.That(actualProject.Result.DeletedBy != null);
            });

            _project = actualProject.Result;
            Logger.Info(_project.ToString());
        }
    }
}