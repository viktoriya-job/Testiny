using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class ProjectPage : BasePage
    {
        private static readonly By _titleLabelBy = By.CssSelector("[data-testid='header-dashboard']>p");
        private static readonly By _projectKeyTextBy = By.CssSelector("[data-testid='text-project-key']");
        private static readonly By _addTestcasesButtonBy = By.CssSelector("[data-testid='button-create-tc']");

        public ProjectPage(IWebDriver driver) : base(driver) { }

        public UIElement TitleLable => new(Driver, _titleLabelBy);
        public UIElement ProjectKeyText => new(Driver, _projectKeyTextBy);
        public Button AddTestcasesButton => new(Driver, _addTestcasesButtonBy);
        public TopMenuPage TopMenu => new TopMenuPage(Driver);

        [AllureStep("Click Create Testcases Button")]
        public AllTestCasesPage TestcasesButtonClick()
        {
            AddTestcasesButton.Click();
            return new AllTestCasesPage(Driver);
        }

        protected override string GetEndpoint()
        {
            throw new NotImplementedException();
        }

        [AllureStep("Checking is the Project page opened")]
        public override bool IsPageOpened()
        {
            try
            {
                return AddTestcasesButton.Displayed && TitleLable.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}