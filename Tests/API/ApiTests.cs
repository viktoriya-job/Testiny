using Allure.NUnit.Attributes;
using NLog;
using Testiny.Helpers;
using Testiny.Models;
using System.Net;
using Allure.Net.Commons;
using Testiny.Helpers.Configuration;

namespace Testiny.Tests.API
{
    [AllureSuite("API Tests")]
    public class ApiTests : BaseApiTest
    {
        private Project _project = null;
        private Project _projectCase = null;
        private List<Case> _cases = new List<Case>();

        [OneTimeSetUp]
        public void AddDataFromJson()
        {
            string projectFilePath = Path.Combine(Configurator.LocationResources, "projectTestdata.json");
            string caseFilePath = Path.Combine(Configurator.LocationResources, "caseTestdata.json");

            Project project = JsonHelper<Project>.FromJson(projectFilePath, FileMode.Open);

            _projectCase = ProjectService.AddProject(project).Result;
            Logger.Info(_projectCase.ToString());

            _cases = JsonHelper<List<Case>>.FromJson(caseFilePath, FileMode.Open);

            for (int i = 0; i < _cases.Count; i++)
            {
                _cases[i].ProjectId = _projectCase.Id;
                _cases[i] = CaseService.AddCase(_cases[i]).Result;
            }

            Assert.That(_cases.Count == 5);
            Logger.Info($"{_cases.Count} test-cases added to the project {_projectCase.Id}, {_projectCase.ProjectName}");
        }

        [Test]
        [AllureSubSuite("TestCase API Tests")]
        [AllureFeature("API GET Method")]
        [AllureFeature("API NFE Tests")]
        public void GetCaseByIdTest()
        {
            AllureApi.Step("Sending a request");
            var response = CaseService.GetCaseById(_cases[0].Id);

            AllureApi.Step("Response processing");
            Case actualCase = JsonHelper<Case>.FromJson(response.Result.Content);

            AllureApi.Step("Checking is the Status code is OK and data is correct");
            Assert.Multiple(() =>
            {
                Assert.That(response.Result.StatusCode == HttpStatusCode.OK);
                Assert.That(actualCase.Title, Is.EqualTo(_cases[0].Title));
                Assert.That(actualCase.ProjectId, Is.EqualTo(_cases[0].ProjectId));
            });

            Logger.Info(actualCase.ToString());
        }

        [Test]
        [AllureSubSuite("TestCase API Tests")]
        [AllureFeature("API GET Method")]
        [AllureFeature("API NFE Tests")]
        public void GetCasesByQueryTest()
        {
            AllureApi.Step("Sending a request");
            string query = "{\"filter\": {\"project_id\": " + $"{_projectCase.Id}" + "}}";
            var response = CaseService.GetCasesByQuery(query);

            AllureApi.Step("Response processing");
            Cases actualCases = JsonHelper<Cases>.FromJson(response.Result.Content);

            Logger.Info(actualCases.CaseList.Count());
            Logger.Info(_cases.Count());

            AllureApi.Step("Checking is the Status code is OK and data is correct");
            Assert.Multiple(() =>
            {
                Assert.That(response.Result.StatusCode == HttpStatusCode.OK);
                Assert.That(actualCases.CaseList.Count() == _cases.Count);
            });

            Logger.Info(actualCases.Meta.ToString());
        }

        [Test]
        [AllureSubSuite("TestCase API Tests")]
        [AllureFeature("API GET Method")]
        [AllureFeature("API AFE Tests")]
        public void GetCaseNotAuthTest()
        {
            AllureApi.Step("Sending a request with an invalid token");
            var response = CaseServiceNotAuth.GetCaseById(_cases[0].Id);

            AllureApi.Step("Response processing");
            FailedResponse responseBody = JsonHelper<FailedResponse>.FromJson(response.Result.Content);

            AllureApi.Step("Checking is the Status code is Forbidden and message is correct");
            Assert.Multiple(() =>
            {
                Assert.That(response.Result.StatusCode == HttpStatusCode.Forbidden);
                Assert.That(responseBody.Code, Is.EqualTo("API_ACCESS_DENIED"));
                Assert.That(responseBody.Message, Is.EqualTo("Invalid API key"));
            });
        }

        [Test]
        [Order(1)]
        [AllureSubSuite("Project API Tests")]
        [AllureFeature("API POST Method")]
        [AllureFeature("API NFE Tests")]
        public void AddProjectTest()
        {
            _project = new Project()
            {
                ProjectName = $"ProjectApiTest {Random.Next(10000)}",
                Description = "ProjectApiTest Description"
            };

            AllureApi.Step("Sending a request and processing response");
            AllureApi.AddTestParameter("Project Name", _project.ProjectName);
            AllureApi.AddTestParameter("Project Description", _project.Description);
            var actualProject = ProjectService.AddProject(_project);

            AllureApi.Step("Checking is the data is correct");
            Assert.Multiple(() =>
            {
                Assert.That(actualProject.Result.ProjectName, Is.EqualTo(_project.ProjectName));
                Assert.That(actualProject.Result.Description, Is.EqualTo(_project.Description));
            });

            _project = actualProject.Result;
            Logger.Info(_project.ToString());
        }

        [Test]
        [Order(2)]
        [AllureSubSuite("Project API Tests")]
        [AllureFeature("API GET Method")]
        [AllureFeature("API NFE Tests")]
        public void GetProjectTest()
        {
            AllureApi.Step("Sending a request");
            var result = ProjectService.GetProject(_project.Id);

            AllureApi.Step("Response processing");
            Project actualProject = JsonHelper<Project>.FromJson(result.Result.Content);

            AllureApi.Step("Checking is the Status code is OK and data is correct");
            Assert.Multiple(() =>
            {
                Assert.That(result.Result.StatusCode == HttpStatusCode.OK);
                Assert.That(actualProject.Id, Is.EqualTo(_project.Id));
                Assert.That(actualProject.ProjectName, Is.EqualTo(_project.ProjectName));
                Assert.That(actualProject.Description, Is.EqualTo(_project.Description));
            });

            Logger.Info(actualProject.ToString());
        }

        [Test]
        [Order(3)]
        [AllureSubSuite("Project API Tests")]
        [AllureFeature("API DELETE Method")]
        [AllureFeature("API NFE Tests")]
        public void RemoveProjectTest()
        {
            AllureApi.Step("Sending a request and processing response");
            AllureApi.AddTestParameter("Project Id", _project.Id);
            int projectId = _project.Id;
            var actualProject = ProjectService.RemoveProject(projectId);

            AllureApi.Step("Checking is the data is correct");
            Assert.Multiple(() =>
            {
                Assert.That(actualProject.Result.Id, Is.EqualTo(projectId));
                Assert.That(actualProject.Result.IsDeleted == true);
                Assert.That(actualProject.Result.DeletedBy != null);
            });

            _project = actualProject.Result;
            Logger.Info(_project.ToString());
        }

        [Test]
        [Order(4)]
        [AllureSubSuite("Project API Tests")]
        [AllureFeature("API GET Method")]
        [AllureFeature("API AFE Tests")]
        public void GetDeletedProjectTest()
        {
            AllureApi.Step("Sending a request");
            var result = ProjectService.GetProject(_project.Id);

            AllureApi.Step("Response processing");
            FailedResponse response = JsonHelper<FailedResponse>.FromJson(result.Result.Content);

            AllureApi.Step("Checking is the Status code is NotFound and message is correct");
            Assert.Multiple(() =>
            {
                Assert.That(result.Result.StatusCode == HttpStatusCode.NotFound);
                Assert.That(response.Code, Is.EqualTo("API_DATA_NOT_FOUND"));
                Assert.That(response.Message, Is.EqualTo($"The entity with id {_project.Id} was not found."));
            });
        }

        [OneTimeTearDown]
        public void RemoveData()
        {
            var actualProject = ProjectService.RemoveProject(_projectCase.Id);

            Assert.Multiple(() =>
            {
                Assert.That(actualProject.Result.IsDeleted == true);
                Assert.That(actualProject.Result.DeletedBy != null);
            });

            _projectCase = actualProject.Result;
            Logger.Info(_projectCase.ToString());
        }
    }
}