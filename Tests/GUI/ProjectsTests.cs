using Testiny.Helpers.Configuration;
using Testiny.Models;
using Testiny.Pages;

namespace Testiny.Tests.GUI
{
    public class ProjectsTests : BaseTest
    {
        [Test]
        public void AddProjectTest()
        {
            Project project = new()

            {
                ProjectName = "Test Project1",
                ProjectKey = "Proj1",
                Description = "Test Description"
            };

            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            ProjectPage projectPage = ProjectSteps
                .AddProjectSuccessfull(project, addProjectPage);

            Thread.Sleep(1000);

            Assert.Multiple(() =>
            {
                Assert.That(projectPage.ProjectKeyText.Text.Trim(), Is.EqualTo(project.ProjectKey.ToUpper()));

                AllProjectsPage allProjectsPage = NavigationSteps.NavigateToAllProjectsPage();

                Assert.That(allProjectsPage.ProjectKeysText.Contains(project.ProjectKey.ToUpper()));
            });
        }
    }
}