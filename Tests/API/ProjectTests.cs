using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using NLog;
using System.Net;
using Testiny.Helpers;
using Testiny.Models;

namespace Testiny.Tests.API
{
    [TestFixture]
    [AllureSuite("Project API Tests")]
    public class ProjectTests : BaseApiTest
    {
        private Project _project = null;

        [Test]
        [Order(1)]
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
            var actualProject = ProjectService.AddProject(_project);

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
        [AllureFeature("API GET Method")]
        [AllureFeature("API NFE Tests")]
        public void GetProjectTest()
        {
            AllureApi.Step("Sending a request");
            var result = ProjectService.GetProject(_project.Id);

            AllureApi.Step("Response processing");
            Project actualProject = JsonHelper<Project>.FromJson(result.Result.Content);

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
        [AllureFeature("API DELETE Method")]
        [AllureFeature("API NFE Tests")]
        public void RemoveProjectTest()
        {
            AllureApi.Step("Sending a request and processing response");
            int projectId = _project.Id;
            var actualProject = ProjectService.RemoveProject(projectId);

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
        [AllureFeature("API GET Method")]
        [AllureFeature("API AFE Tests")]
        public void GetDeletedProjectTest()
        {
            AllureApi.Step("Sending a request");
            var result = ProjectService.GetProject(_project.Id);

            AllureApi.Step("Response processing");
            FailedResponse response = JsonHelper<FailedResponse>.FromJson(result.Result.Content);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result.StatusCode == HttpStatusCode.NotFound);
                Assert.That(response.Code, Is.EqualTo("API_DATA_NOT_FOUND"));
                Assert.That(response.Message, Is.EqualTo($"The entity with id {_project.Id} was not found."));
            });
        }
    }
}