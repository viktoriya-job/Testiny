using OpenQA.Selenium;

namespace Testiny.Elements
{
    public class DropDownMenu
    {
        private UIElement _uiElement;
        private List<UIElement> _options;

        private By _optionsBy = By.CssSelector("[data-testid='dropdown-menu'] li>div.menu-label");

        public DropDownMenu(IWebDriver webDriver, By locator)
        {
            _uiElement = new UIElement(webDriver, locator);
            _uiElement.Click();

            _options = _uiElement.FindUIElementsFull(_optionsBy);
        }

        public bool Enabled => _uiElement.Enabled;

        public bool Displayed => _uiElement.Displayed;

        public List<string> GetOptions()
        {
            var result = new List<string>();

            foreach (UIElement element in _options)
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
                throw new ArgumentNullException(text, "text must not be null");
            }

            foreach (UIElement option in _options)
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