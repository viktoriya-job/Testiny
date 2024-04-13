using Testiny.Helpers.Configuration;
using Testiny.Models;
using Testiny.Pages;

namespace Testiny.Tests.GUI
{
    public class ProjectsTests : BaseTest
    {
        private Project projectAdd = new()

        {
            ProjectName = "Test Project 1 for Add Test",
            ProjectKey = "PrjA1",
            Description = "Test Description for Add Test"
        };

        private Project projectDel = new()

        {
            ProjectName = "Test Project 1 for Delete Test",
            ProjectKey = "PrjD1",
            Description = "Test Description for Delete Test"
        };

        //[TestCase]
        //public void EnterProjectKeyValueSuccessTest()
        //{

        //}

        [Test]
        public void AddProjectTest()
        {
            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            ProjectPage projectPage = ProjectSteps
                .AddProjectSuccessfull(projectAdd, addProjectPage);
            Thread.Sleep(1000);

            Assert.Multiple(() =>
            {
                Assert.That(projectPage.ProjectKeyText.Text.Trim(), Is.EqualTo(projectAdd.ProjectKey.ToUpper()));

                AllProjectsPage allProjectsPage = NavigationSteps.NavigateToAllProjectsPage();

                Assert.That(allProjectsPage.ProjectKeysText.Contains(projectAdd.ProjectKey.ToUpper()));
            });
        }

        [Test]
        public void RemoveProjectTest()
        {
            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            ProjectPage projectPage = ProjectSteps
                .AddProjectSuccessfull(projectDel, addProjectPage);
            Thread.Sleep(1000);

            AllProjectsPage allProjectsPage = NavigationSteps
                .NavigateToAllProjectsPage();

            var index = allProjectsPage.ProjectKeys.FindIndex(projectKey => projectKey.Text.Trim().ToUpper() == projectDel.ProjectKey.ToUpper());

            AllProjectsPage allProjectsPageNew = allProjectsPage
                .SelectRecordByProjectKeyElement(allProjectsPage.ProjectKeys[index])
                .ClickDeleteButton()
                .ClickConfirmButton<AllProjectsPage>();

            NavigationSteps
                .NavigateToDashboardPage();

            TopMenuPage topMenuPage = new TopMenuPage(Driver);
            Assert.That(!topMenuPage.ProjectsMenu.GetOptions().Contains(projectDel.ProjectName));
        }
    }
}