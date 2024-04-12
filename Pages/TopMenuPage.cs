using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class TopMenuPage : BasePage
    {
        private static string _endPoint = "";
        private static readonly By _titleIconBy = By.Id("icon-logo-mainbar");
        private static readonly By _projectsMenuBy = By.Id("portal-root");
        private static readonly By _buttonAccountMenuBy = By.CssSelector("[data-testid='button-account']>div");

        public TopMenuPage(IWebDriver driver) : base(driver) { }
        public TopMenuPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl) { }

        public UIElement TitleIcon => new(Driver, _titleIconBy);
        public DropDownMenu ProjectsMenu => new(Driver, _projectsMenuBy);
        public DropDownMenu ButtonAccountMenu => new(Driver, _buttonAccountMenuBy);

        protected override string GetEndpoint()
        {
            throw new NotImplementedException();
        }

        public override bool IsPageOpened()
        {
            try
            {
                return TitleIcon.Enabled;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}