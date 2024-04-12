using OpenQA.Selenium;

namespace Testiny.Elements
{
    public class DropDownMenu
    {
        private UIElement _uiElement;
        //private List<UIElement> _options;
        private By _locatorOptions = By.CssSelector("[data-testid='dropdown-menu']>li");

        public DropDownMenu(IWebDriver webDriver, By locator)
        {
            _uiElement = new UIElement(webDriver, locator);
        }

        public List<UIElement> Options
        {
            get
            {
                Click();
                return _uiElement.FindUIElements(_locatorOptions);
            }
        }

        public bool Enabled => _uiElement.Enabled;

        public bool Displayed => _uiElement.Displayed;

        private void Click() => _uiElement.Click();

        public List<string> GetOptions()
        {
            var result = new List<string>();
            foreach (UIElement element in Options)
            {
                result.Add(element.Text);
            }
            return result;
        }

        public void SelectByText(string text)
        {
            bool flag = false;

            if (String.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text", "text must not be null");
            }

            foreach (UIElement option in Options)
            {
                if (option.Text.Trim() == text)
                {
                    SelectOption(option);
                    flag = true;
                    return;
                }
            }
            if (!flag)
            {
                throw new NoSuchElementException("Cannot locate element with text: " + text);
            }
        }

        public void SelectByIndex(int index)
        {
            if (index < Options.Count)
            {
                SelectOption(Options[index]);
            }
            else
            {
                throw new AssertionException("Cannot locate option with index: " + index);
            }
        }

        private void SelectOption(UIElement option)
        {
            _uiElement.Click();
            option.Click();
        }
    }
}