using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class ImportTestCasesPage : BasePage
    {
        private static string _endPoint = "/testcases/import";
        private static readonly By _titleLabelBy = By.XPath("//h4[contains(text(),'Import test cases')]");
        private static readonly By _csvButtonBy = By.CssSelector("[data-testid='button-import_csv']");
        private static readonly By _uploadInputBy = By.CssSelector("input[type='file']");
        private static readonly By _importButtonBy = By.CssSelector("[data-testid='button-execute']");

        //public ImportTestCasesPage(IWebDriver driver, bool openByUrl, string projectKey) : base(driver, openByUrl) 
        //{
        //    _endPoint = String.Concat(projectKey,_endPoint);
        //}

        public ImportTestCasesPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl) { }
        public ImportTestCasesPage(IWebDriver driver) : base(driver) { }


        public UIElement TitleLable => new(Driver, _titleLabelBy);
        public Button CSVButton => new(Driver, _csvButtonBy);
        public UIElement UploadInput => new(Driver, _uploadInputBy);
        public Button ImportButton => new(Driver, _importButtonBy);

        [AllureStep("Select CSV option")]
        public ImportTestCasesPage SelectCSVOption()
        {
            CSVButton.Click();
            return this;
        }

        [AllureStep("Upload File")]
        public DialogPage FileUpload(string path)
        {
            UploadInput.SendKeys(path);
            return new DialogPage(Driver);
        }

        [AllureStep("Click Import Button")]
        public AllTestCasesPage ImportButtonClick()
        {
            ImportButton.Click();
            return new AllTestCasesPage(Driver);
        }

        protected override string GetEndpoint() => _endPoint;

        [AllureStep("Checking is the Import TestCases page opened")]
        public override bool IsPageOpened()
        {
            try
            {
                return TitleLable.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}