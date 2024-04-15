using OpenQA.Selenium;

namespace Testiny.Elements
{
    public class DropDownMenu
    {
        private UIElement _uiElement;
        private List<UIElement> _options;
        private List<UIElement> _optionsValues;
        private By _locatorOptions = By.CssSelector("[data-testid='dropdown-menu']>li");
        private By _locatorAOptions = By.CssSelector("[data-testid='dropdown-menu']>a>li");
        private By _locatorOptionsText = By.CssSelector("[data-testid='dropdown-menu']>li>div.menu-label");
        private By _locatorAOptionsText = By.CssSelector("[data-testid='dropdown-menu']>a>li>div.menu-label");

        public DropDownMenu(IWebDriver webDriver, By locator, bool separatedOptions)
        {
            _uiElement = new UIElement(webDriver, locator);
            _uiElement.Click();

            if (separatedOptions)
            {
                _options = _uiElement.FindUIElementsFull(_locatorAOptions);
                _optionsValues = _uiElement.FindUIElementsFull(_locatorAOptionsText);
            }
            else
            {
                _options = _uiElement.FindUIElementsFull(_locatorOptions);
                _optionsValues = _uiElement.FindUIElementsFull(_locatorOptionsText);
            }
        }

        public bool Enabled => _uiElement.Enabled;

        public bool Displayed => _uiElement.Displayed;

        private void Click() => _uiElement.Click();

        public List<string> GetOptions()
        {
            var result = new List<string>();

            foreach (UIElement element in _optionsValues)
            {
                result.Add(element.Text);
            }

            return result;
        }

        public void SelectByIndex(int index)
        {
            if (index < _options.Count)
            {
                _options[index].Click();
            }
            else
            {
                throw new AssertionException("Cannot locate option with index: " + index);
            }
        }

        public void SelectByText(string text)
        {
            bool flag = false;

            if (String.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text", "text must not be null");
            }

            foreach (UIElement option in _optionsValues)
            {
                if (option.Text.Trim() == text)
                {
                    option.Click();
                    flag = true;
                    return;
                }
            }

            if (!flag)
            {
                throw new NoSuchElementException("Cannot locate element with text: " + text);
            }
        }
    }
}