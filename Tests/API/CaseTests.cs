using Allure.NUnit.Attributes;
using NLog;
using System.Reflection;
using Testiny.Helpers;
using Testiny.Models;
using Allure.Net.Commons;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Testiny.Tests.API
{
    [TestFixture]
    [AllureSuite("TestCases API Tests")]
    public class CaseTests : BaseApiTest
    {
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
            Logger.Info(_project.ToString());


            //Adding TestCase from Json
            path = Path.Combine(location, "Resources", caseFileName);

            Case tCase = JsonHelper<Case>.FromJson(path, FileMode.Open);
            tCase.ProjectId = _project.Id;

            _case = CaseService.AddCase(tCase).Result;
            Logger.Info(_case.ToString());
        }

        [Test]
        [Category("GET Method NFE Tests")]
        public void GetCaseByIdTest()
        {
            var result = CaseService.GetCaseById(_case.Id);

            JObject resultData = JObject.Parse(result.Result.Content);
            Case actualCase = JsonHelper<Case>.FromJson(result.Result.Content);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result.StatusCode == HttpStatusCode.OK);
                Assert.That(actualCase.Title, Is.EqualTo(_case.Title));
                Assert.That(actualCase.ProjectId, Is.EqualTo(_case.ProjectId));
            });

            Logger.Info(actualCase.ToString());
        }

        [Test]
        [Category("GET Method NFE Tests")]
        public void GetCaseByQueryTest()
        {
            var result = CaseService.GetCaseByQuery("{\"filter\": {\"project_id\": 43}}");

            JObject resultData = JObject.Parse(result.Result.Content);
            //Case actualCase = JsonHelper<Case>.FromJson(result.Result.Content);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result.StatusCode == HttpStatusCode.OK);
                //Assert.That(result.Result.StatusCode == HttpStatusCode.Forbidden);
                //Assert.That(actualCase.Title, Is.EqualTo(_case.Title));
                //Assert.That(actualCase.ProjectId, Is.EqualTo(_case.ProjectId));
            });

            //Logger.Info(actualCase.ToString());
        }

        [Test]
        [Category("GET Method AFE Tests")]
        public void GetCaseNotAuth()
        {
            var result = CaseServiceNotAuth.GetCaseById(_case.Id);

            JObject resultData = JObject.Parse(result.Result.Content);
            FailedResponse response = JsonHelper<FailedResponse>.FromJson(result.Result.Content);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result.StatusCode == HttpStatusCode.Forbidden);
                Assert.That(response.Code, Is.EqualTo("API_ACCESS_DENIED"));
                Assert.That(response.Message, Is.EqualTo("Invalid API key"));
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