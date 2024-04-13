using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Testiny.Helpers.Configuration;

namespace Testiny.Elements
{
    public class DropDownMenu
    {
        private UIElement _uiElement;
        private List<UIElement> _options;
        private By _locatorOptions = By.CssSelector("[data-testid='dropdown-menu']>li");
        private By _locatorAOptions = By.CssSelector("[data-testid='dropdown-menu']>a>li");

        public DropDownMenu(IWebDriver webDriver, By locator, bool separatedOptions)
        {
            _uiElement = new UIElement(webDriver, locator);
            _uiElement.Click();
            if (separatedOptions)
            {
                _options = _uiElement.FindUIElementsFull(_locatorAOptions);
            }
            else
            {
                _options = _uiElement.FindUIElementsFull(_locatorOptions);
            }  
        }

        public bool Enabled => _uiElement.Enabled;

        public bool Displayed => _uiElement.Displayed;

        private void Click() => _uiElement.Click();

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
    }
}