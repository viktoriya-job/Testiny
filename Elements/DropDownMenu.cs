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
        private WebDriverWait _wait;

        public DropDownMenu(IWebDriver webDriver, By locator)
        {
            _uiElement = new UIElement(webDriver, locator);
            _wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(Configurator.WaitsTimeout));
            _uiElement.Click();
            _options = _uiElement.FindUIElementsFull(_locatorOptions);
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