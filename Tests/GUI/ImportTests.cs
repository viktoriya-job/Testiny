using OpenQA.Selenium;
using System;
using System.Reflection;
using Testiny.Helpers.Configuration;
using Testiny.Pages;

namespace Testiny.Tests.GUI
{
    public class ImportTests : BaseTest
    {
        [Test]
        [Category("Positive")]
        public void ImportCSVFileTest()
        {
            string fileName = "testiny_testcase_import_sample.csv";

            string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(location, "Resources", fileName);

            NavigationSteps
                .SuccessfulLogin(Configurator.Admin);

            AllTestCasesPage allTestCasesPage = NavigationSteps
                .NavigateToImportTestCasesPage()
                .SelectCSV()
                .UploadFile(path)
                .ConfirmButtonClick<ImportTestCasesPage>()
                .ImportButtonClick();

            Assert.That(allTestCasesPage.IsPageOpened);
        }
    }
}