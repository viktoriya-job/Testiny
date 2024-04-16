using Allure.NUnit.Attributes;
using Testiny.Helpers.Configuration;
using Testiny.Models;
using Testiny.Pages;
using Testiny.Steps;

namespace Testiny.Tests.GUI
{
    [AllureSuite("Upload UI Tests")]
    public class UploadTests : BaseTest
    {
        Project project = new()
        {
            ProjectName = "Project For Upload Test Case",
            ProjectKey = "UPLTC",
            Description = "Test Project To Upload Cases from file"
        };

        [Test]
        [AllureFeature("Positive UI Tests")]
        public void UploadCSVFileTest()
        {
            string filePath = Path.Combine(Configurator.LocationResources, "testiny_testcase_import_sample.csv");
            
            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            AllTestCasesPage allTestCasesPage = ProjectSteps
                .AddProjectSuccessfull(project, addProjectPage)
                .TestcasesButtonClick()
                .ImportButtonClick()
                .SelectCSVOption()
                .FileUpload(filePath)
                .ConfirmButtonClick<ImportTestCasesPage>()
                .ImportButtonClick();

            Assert.That(allTestCasesPage.IsPageOpened);
        }

        [TearDown]
        public void RemoveProjectForUpload()
        {
            AllProjectsPage allProjectsPage = NavigationSteps
                .NavigateToAllProjectsPage();

            var index = allProjectsPage.ProjectKeys.FindIndex(projectKey => projectKey.Text.Trim().ToUpper() == project.ProjectKey.ToUpper());

            AllProjectsPage allProjectsPageNew = allProjectsPage
                .SelectRecordByProjectKeyElement(allProjectsPage.ProjectKeys[index])
                .DeleteButtonClick()
                .ConfirmButtonClick<AllProjectsPage>();
        }
    }
}