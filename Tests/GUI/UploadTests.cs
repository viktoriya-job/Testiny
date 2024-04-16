using Allure.Net.Commons;
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

        //[SetUp]
        //public void AddProjectForUpload()
        //{

        //}

        [Test]
        [AllureFeature("Positive UI Tests")]
        public void UploadCSVFileTest()
        {
            string filePath = Path.Combine(Configurator.LocationResources, "testiny_testcase_import_sample.csv");
            
            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            

            ProjectPage projectPage = ProjectSteps
                .AddProjectSuccessfull(project, addProjectPage);

            //Thread.Sleep(1000); //pause to avoid StaleElementReferenceException


            //AllTestCasesPage allTestCasesPage = ProjectSteps
            //    .InportTestCasesFromCsvFile(projectPage, filePath);

            AllTestCasesPage allTestCasesPage = projectPage
                .TestcasesButtonClick()
                .ImportButtonClick()
                .SelectCSVOption()
                .FileUpload(filePath)
                .ConfirmButtonClick<ImportTestCasesPage>()
                .ImportButtonClick();

            Assert.That(allTestCasesPage.IsPageOpened);
            //v1
            //Thread.Sleep(2000); // pause to create constructor with ProjectKey

            //AllTestCasesPage allTestCasesPage = NavigationSteps
            //    .NavigateToImportTestCasesPage(project.ProjectKey.ToUpper())
            //    .SelectCSVOption()
            //    .FileUpload(filePath)
            //    .ConfirmButtonClick<ImportTestCasesPage>()
            //    .ImportButtonClick();


            //v2
            //AllProjectsPage allProjectsPage = NavigationSteps
            //    .NavigateToAllProjectsPage();

            //var index = allProjectsPage.ProjectKeys.FindIndex(projectKey => projectKey.Text.Trim().ToUpper() == project.ProjectKey.ToUpper());

            //AllTestCasesPage allTestCasesPage = allProjectsPage
            //    .SelectRecordByProjectNameElementLink(allProjectsPage.ProjectNames[index])
            //    .TestcasesButtonClick()
            //    .ImportButtonClick()
            //    .SelectCSVOption()
            //    .FileUpload(filePath)
            //    .ConfirmButtonClick<ImportTestCasesPage>()
            //    .ImportButtonClick();

            //v3
            //ProjectPage projectPage = new ProjectPage(Driver);

            //AllTestCasesPage allTestCasesPage = projectPage
            //    .TestcasesButtonClick()
            //    .ImportButtonClick()
            //    .SelectCSVOption()
            //    .FileUpload(filePath)
            //    .ConfirmButtonClick<ImportTestCasesPage>()
            //    .ImportButtonClick();


        }

        //[TearDown]
        [Test]
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