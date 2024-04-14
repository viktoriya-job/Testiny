using Testiny.Helpers.Configuration;
using Testiny.Models;
using Testiny.Pages;
using Testiny.Steps;

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

        private Project projectError = new()
        {
            ProjectName = "Error project",
            ProjectKey = "ErrorProjectKey",
            Description = "Test Description for Error project"
        };

        private Project projectCorrect = new()
        {
            ProjectName = "Correct project",
            ProjectKey = "CPK",
            Description = "Test Description for Correct project"
        };

        [TestCase("Pr")]
        [TestCase("Pr123")]
        [Category("Positive")]
        public void EnterProjectKeyValueSuccessTest(string projectKey)
        {
            Project project = new()
            {
                ProjectName = $"Project {projectKey}",
                ProjectKey = projectKey,
                Description = $"Test Description for {projectKey}"
            };

            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            ProjectSteps
                .InputProjectFields(project, addProjectPage);

            Assert.That(addProjectPage.AddButton.Enabled);
        }

        [TestCase("P")]
        [TestCase("Pr1234")]
        [Category("Negative")]
        public void EnterProjectKeyValueUnsuccessTest(string projectKey)
        {
             Project project = new()
             {
                ProjectName = $"Project {projectKey}",
                ProjectKey = projectKey,
                Description = $"Test Description for {projectKey}"
             };

            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            ProjectSteps
                .InputProjectFields(project, addProjectPage);

            Assert.Multiple(() =>
            {
                Assert.That(!addProjectPage.AddButton.Enabled);
                Assert.That(addProjectPage.ErrorLabel.Displayed);
            });
        }

        [Test]
        [Category("Positive")]
        public void AddProjectTest()
        {
            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            ProjectPage projectPage = ProjectSteps
                .AddProjectSuccessfull(projectAdd, addProjectPage);
            Thread.Sleep(1000);

            AllProjectsPage allProjectsPage = NavigationSteps.NavigateToAllProjectsPage();

            Assert.That(allProjectsPage.ProjectKeysText.Contains(projectAdd.ProjectKey.ToUpper()));
        }

        [Test]
        [Category("Positive")]
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
                .ConfirmButtonClick<AllProjectsPage>();

            NavigationSteps
                .NavigateToDashboardPage();

            TopMenuPage topMenuPage = new TopMenuPage(Driver);
            Assert.That(!topMenuPage.ProjectsMenu.GetOptions().Contains(projectDel.ProjectName));
        }

        [Test]
        [Category("Positive")]
        public void DialogWindowTest()
        {
            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            DialogPage dialogPage = ProjectSteps
                .AddProjectUnsuccessfull(projectAdd, addProjectPage);

            Assert.That(dialogPage.IsPageOpened);
        }

        [Test]
        [Category("Expected error")]
        public void AddIncorrectProjectTest()
        {
            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            Assert.Multiple(() =>
            {
                ProjectSteps
                    .InputProjectFields(projectCorrect, addProjectPage);

                Assert.That(addProjectPage.AddButton.Enabled);

                ProjectSteps
                    .InputProjectFields(projectError, addProjectPage);

                Assert.That(addProjectPage.AddButton.Enabled);
            });
        }
    }
}