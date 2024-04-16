using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using Testiny.Helpers.Configuration;
using Testiny.Models;
using Testiny.Pages;
using Testiny.Steps;

namespace Testiny.Tests.GUI
{
    [AllureSuite("Project UI Tests")]
    public class ProjectsTests : BaseTest
    {
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

        [TestCase("")]
        [TestCase("Pr")]
        [TestCase("Pr123")]
        [AllureSubSuite("Checking input fields Tests")]
        [AllureFeature("Positive UI Tests")]
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

            AllureApi.Step("Checking is the Create button enabled");
            Assert.That(addProjectPage.AddButton.Enabled);
        }

        [TestCase("P")]
        [TestCase("Pr1234")]
        [AllureSubSuite("Checking input fields Tests")]
        [AllureFeature("Negative UI Tests")]
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

            AllureApi.Step("Checking is the Create button disabled and error text displayed");
            Assert.Multiple(() =>
            {
                Assert.That(!addProjectPage.AddButton.Enabled);
                Assert.That(addProjectPage.ErrorLabel.Displayed);
            });
        }

        [Test]
        [AllureSubSuite("Add / Remove project Tests")]
        [AllureFeature("Positive UI Tests")]
        public void AddProjectTest()
        {
            Project projectAdd = new()
            {
                ProjectName = $"Test Project {Random.Next(10000)} for Add Test",
                ProjectKey = $"p{Random.Next(100)}",
                Description = "Test Description for Add Test"
            };

            AddProjectPage addProjectPage = NavigationSteps
                    .SuccessfulLogin(Configurator.Admin)
                    .CreateProjectMenuSelect();

            ProjectPage projectPage = ProjectSteps
                .AddProjectSuccessfull(projectAdd, addProjectPage);
            Thread.Sleep(1000); //pause to avoid UnhandledAlertException

            AllProjectsPage allProjectsPage = NavigationSteps.NavigateToAllProjectsPage();

            AllureApi.Step("Checking is the allProjectsPage contains new project");
            Assert.Multiple(() =>
            {
                Assert.That(allProjectsPage.ProjectKeysText.Contains(projectAdd.ProjectKey.ToUpper()));
                Assert.That(allProjectsPage.ProjectNamesText.Contains(projectAdd.ProjectName));
            });
        }

        [Test]
        [AllureSubSuite("Add / Remove project Tests")]
        [AllureFeature("Positive UI Tests")]
        public void RemoveProjectTest()
        {
            Project projectDel = new()
            {
                ProjectName = $"Test Project {Random.Next(10000)} for Add Test",
                ProjectKey = $"p{Random.Next(100)}",
                Description = "Test Description for Delete Test"
            };

            AddProjectPage addProjectPage = NavigationSteps
                    .SuccessfulLogin(Configurator.Admin)
                    .CreateProjectMenuSelect();

            ProjectPage projectPage = ProjectSteps
                .AddProjectSuccessfull(projectDel, addProjectPage);
            Thread.Sleep(1000); //pause to avoid UnhandledAlertException 

            AllProjectsPage allProjectsPage = NavigationSteps
                .NavigateToAllProjectsPage();

            var index = allProjectsPage.ProjectKeys.FindIndex(projectKey => projectKey.Text.Trim().ToUpper() == projectDel.ProjectKey.ToUpper());

            AllProjectsPage allProjectsPageNew = allProjectsPage
                .SelectRecordByProjectKeyElement(allProjectsPage.ProjectKeys[index])
                .DeleteButtonClick()
                .ConfirmButtonClick<AllProjectsPage>();

            NavigationSteps
                .NavigateToDashboardPage();

            TopMenuPage topMenuPage = new TopMenuPage(Driver);

            AllureApi.Step("Checking is the project list does not contain removed project");
            Assert.That(!topMenuPage.ProjectsMenu.GetOptions().Contains(projectDel.ProjectName));
        }

        [Test]
        [AllureSubSuite("Dialog Window Tests")]
        [AllureFeature("Positive UI Tests")]
        public void DialogWindowTest()
        {
            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            DialogPage dialogPage = ProjectSteps
                .AddProjectUnsuccessfull(projectCorrect, addProjectPage);

            Assert.That(dialogPage.IsPageOpened);
        }

        [Test]
        [AllureSubSuite("Add / Remove project Tests")]
        [AllureFeature("Expected error UI Tests")]
        public void AddIncorrectProjectTest()
        {
            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            Assert.Multiple(() =>
            {
                AllureApi.Step("Input correct Project fields");
                ProjectSteps
                    .InputProjectFields(projectCorrect, addProjectPage);

                AllureApi.Step("Checking is the Login button enabled");
                Assert.That(addProjectPage.AddButton.Enabled);

                AllureApi.Step("Input incorrect Project fields");
                ProjectSteps
                    .InputProjectFields(projectError, addProjectPage);

                AllureApi.Step("Checking is the Login button enabled");
                Assert.That(addProjectPage.AddButton.Enabled);
            });
        }
    }
}