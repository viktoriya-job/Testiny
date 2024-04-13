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
        public TopMenuPage topMenu => new TopMenuPage(Driver);

        protected override string GetEndpoint()
        {
            throw new NotImplementedException();
        }

        public override bool IsPageOpened()
        {
            try
            {
                return AddTestcasesButton.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}