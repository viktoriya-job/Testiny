using Allure.NUnit.Attributes;
using Testiny.Helpers.Configuration;
using Testiny.Pages;

namespace Testiny.Tests.GUI
{
    [AllureSuite("Upload UI Tests")]
    public class UploadTests : BaseTest
    {
        [Test]
        [AllureFeature("Positive UI Tests")]
        public void UploadCSVFileTest()
        {
            string filePath = Path.Combine(Configurator.LocationResources, "testiny_testcase_import_sample.csv");

            NavigationSteps
                .SuccessfulLogin(Configurator.Admin);

            AllTestCasesPage allTestCasesPage = NavigationSteps
                .NavigateToImportTestCasesPage()
                .SelectCSVOption()
                .FileUpload(filePath)
                .ConfirmButtonClick<ImportTestCasesPage>()
                .ImportButtonClick();

            Assert.That(allTestCasesPage.IsPageOpened);
        }
    }
}