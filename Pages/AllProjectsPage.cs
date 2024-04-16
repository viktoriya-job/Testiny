using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class AllProjectsPage : BasePage
    {
        private static string _endPoint = "settings/projects";

        private static readonly By _titleLabelBy = By.CssSelector("div h3.sc-fAUVGl");
        private static readonly By _projectKeysBy = By.CssSelector("[data-testid='cell-project_key'] span");
        private static readonly By _projectNamesBy = By.CssSelector("[data-testid='cell-name'] span");

        public AllProjectsPage(IWebDriver driver) : base(driver, false) { }
        public AllProjectsPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl) { }

        public UIElement TitleLabel => new(Driver, _titleLabelBy);
        public List<UIElement> ProjectKeys => WaitsHelper.WaitForAllVisibleUiElementsLocatedBy(_projectKeysBy);
        public List<UIElement> ProjectNames => WaitsHelper.WaitForAllVisibleUiElementsLocatedBy(_projectNamesBy);

        public List<string> ProjectKeysText
        {
            get
            {
                List<string> result = new List<string>();

                foreach (UIElement element in ProjectKeys)
                {
                    result.Add(element.Text);
                }

                return result;
            }
        }

        public List<string> ProjectNamesText
        {
            get
            {
                List<string> result = new List<string>();

                foreach (UIElement element in ProjectNames)
                {
                    result.Add(element.Text);
                }

                return result;
            }
        }

        [AllureStep("Select record in All Projects Page")]
        public EditProjectPage SelectRecordByProjectKeyElement(UIElement element)
        {
            element.Click();
            return new EditProjectPage(Driver);
        }

        protected override string GetEndpoint() => _endPoint;

        [AllureStep("Checking is the All Projects page opened")]
        public override bool IsPageOpened()
        {
            try
            {
                return TitleLabel.Text.Trim() == "Projects";
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}