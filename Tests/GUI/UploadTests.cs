using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using Testiny.Helpers.Configuration;
using Testiny.Models;
using Testiny.Pages;

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

        [SetUp]
        public void AddProjectForUpload()
        {
            AddProjectPage addProjectPage = NavigationSteps
                    .SuccessfulLogin(Configurator.Admin)
                    .CreateProjectMenuSelect();

            ProjectSteps
                .AddProjectSuccessfull(project, addProjectPage);
        }

        [Test]
        [AllureFeature("Positive UI Tests")]
        public void UploadCSVFileTest()
        {
            string filePath = Path.Combine(Configurator.LocationResources, "testiny_testcase_import_sample.csv");
            
            Thread.Sleep(2000); // pause to create constructor with ProjectKey

            AllTestCasesPage allTestCasesPage = NavigationSteps
                .NavigateToImportTestCasesPage(project.ProjectKey.ToUpper())
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