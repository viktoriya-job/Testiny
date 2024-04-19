using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class TopMenuPage : BasePage
    {
        private static readonly By _projectsMenuBy = By.CssSelector("[data-testid='button-projects']>div");
        private static readonly By _feedbackButtonBy = By.CssSelector("[data-testid='button-feedback']");

        public TopMenuPage(IWebDriver driver) : base(driver) { }
        public TopMenuPage(IWebDriver driver, bool openPageByUrl = false) : base(driver, openPageByUrl) { }

        public DropDownMenu ProjectsMenu => new(Driver, _projectsMenuBy);
        public Button FeedbackButton => new(Driver, _feedbackButtonBy);

        [AllureStep("Select the 'Create a new project' menu")]
        public AddProjectPage CreateProjectMenuSelect()
        {
            ProjectsMenu.SelectByText("Create a new project");
            return new AddProjectPage(Driver);
        }

        protected override string GetEndpoint()
        {
            throw new NotImplementedException();
        }

        [AllureStep("Checking is the TopMenu Page opened")]
        public override bool IsPageOpened()
        {
            try
            {
                return ProjectsMenu.Enabled;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}