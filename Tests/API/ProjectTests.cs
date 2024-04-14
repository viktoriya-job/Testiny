using Allure.Net.Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using RestSharp;
using System.Net;
using Testiny.Helpers;
using Testiny.Models;

namespace Testiny.Tests.API
{
    [TestFixture]
    public class ProjectTests : BaseApiTest
    {
        private Project _project = null;

        [Test]
        [Order (1)]
        public void AddProjectTest()
        {
            _project = new Project()
            {
                ProjectName = $"ProjectApiTest {Random.Next(10000)}",
                Description = "ProjectApiTest Description"
            };

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
        public void GetProjectTest()
        {
            var result = ProjectService.GetProject(_project.Id);

            JObject resultData = JObject.Parse(result.Result.Content);
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
        public void RemoveProjectTest()
        {
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
        public void GetDeletedProjectTest()
        {
            var result = ProjectService.GetProject(_project.Id);
                
            Assert.That(result.Result.StatusCode == HttpStatusCode.NotFound);

            Logger.Info(result.Result.StatusCode);
        }
    }
}