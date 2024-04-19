using OpenQA.Selenium;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Testiny.Elements;

namespace Testiny.Helpers
{
    public class WaitsHelper(IWebDriver driver, TimeSpan timeout)
    {
        private readonly WebDriverWait _wait = new WebDriverWait(driver, timeout);

        public IWebElement WaitForExists(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementExists(locator));
        }

        public ReadOnlyCollection<IWebElement> WaitForAllVisibleElementsLocatedBy(By locator)
        {
            return _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
        }

        public ReadOnlyCollection<IWebElement> WaitForPresenceOfAllElementsLocatedBy(By locator)
        {
            return _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }

        public UIElement WaitChildElement(IWebElement webElement, By by)
        {
            return new UIElement(driver, _wait.Until(_ => webElement.FindElement(by)));
        }

        public List<UIElement> WaitForAllVisibleUiElementsLocatedBy(By locator)
        {
            List<UIElement> result = new List<UIElement>();

            foreach (IWebElement element in WaitForPresenceOfAllElementsLocatedBy(locator))
            {
                result.Add(new UIElement(driver, element));
            }

            return result;
        }
    }
}