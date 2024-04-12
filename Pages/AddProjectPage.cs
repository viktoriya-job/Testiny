using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class AddProjectPage : BasePage
    {
        private static readonly By _titleIconBy = By.Id("icon-project-add-20");
        private static readonly By _titleLabelBy = By.XPath("//h4[contains(text(),'Create a new project')]");
        private static readonly By _nameInputBy = By.Id(":rl:");
        private static readonly By _projectKeyInputBy = By.Id(":rn:");
        private static readonly By _descriptionInputBy = By.Id(":rp:");
        private static readonly By _addButtonBy = By.CssSelector("[data-testid='button-save-entity'] div");
        private static readonly By _closeButtonBy = By.CssSelector("[data-testid='button-close-entity'] div");

        public AddProjectPage(IWebDriver driver) : base(driver) { }
        //public AddProjectPage(IWebDriver driver, bool openByUrl) : base(driver, openByUrl) { }

        public UIElement TitleIcon => new(Driver, _titleIconBy);
        public UIElement TitleLabel => new(Driver, _titleLabelBy);
        public UIElement NameInput => new(Driver, _nameInputBy);
        public UIElement ProjectKeyInput => new(Driver, _projectKeyInputBy);
        public UIElement DescriptionInput => new(Driver, _descriptionInputBy);
        public Button AddButton => new(Driver, _addButtonBy);
        public Button CloseButton => new(Driver, _closeButtonBy);

        public AddProjectPage InputNameValue(string value)
        {
            NameInput.SendKeys(value);
            return this;
        }

        public AddProjectPage InputProjectKeyValue(string value)
        {
            ProjectKeyInput.SendKeys(value);
            return this;
        }

        public AddProjectPage InputDescriptionValue(string value)
        {
            DescriptionInput.SendKeys(value);
            return this;
        }

        public ProjectPage ClickAddButton()
        {
            AddButton.Click();
            return new ProjectPage(Driver);
        }

        public AddProjectPage ClickCloseButton()
        {
            CloseButton.Click();
            return this;
        }

        protected override string GetEndpoint()
        {
            throw new NotImplementedException();
        }

        public override bool IsPageOpened()
        {
            try
            {
                return TitleLabel.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}