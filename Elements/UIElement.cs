using System.Collections.ObjectModel;
using System.Drawing;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Testiny.Helpers;
using Testiny.Helpers.Configuration;

namespace Testiny.Elements;

public class UIElement : IWebElement
{
    private IWebDriver _webDriver;
    private WaitsHelper _waitsHelper;
    private IWebElement _webElement;
    private Actions _actions;

    private UIElement(IWebDriver webDriver)
    {
        _webDriver = webDriver;
        _waitsHelper = new WaitsHelper(webDriver, TimeSpan.FromSeconds(Configurator.WaitsTimeout));
        _actions = new Actions(webDriver);
    }

    public UIElement(IWebDriver webDriver, By by) : this(webDriver)
    {
        _webElement = _waitsHelper.WaitForExists(by);
    }

    public UIElement(IWebDriver webDriver, IWebElement webElement) : this(webDriver)
    {
        _webElement = webElement;
    }

    public IWebElement FindElement(By by)
    {
        return _waitsHelper.WaitChildElement(_webElement, by);
    }

    public UIElement FindUIElement(By by)
    {
        return new UIElement(_webDriver, FindElement(by));
    }

    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
        return _webElement.FindElements(by);
    }

    public List<UIElement> FindUIElements(By by)
    {
        var result = new List<UIElement>();
        foreach (var webElement in FindElements(by))
        {
            result.Add(new UIElement(_webDriver, webElement));
        }

        return result;
    }

    public ReadOnlyCollection<IWebElement> FindElementsFull(By by)
    {
        return _waitsHelper.WaitForAllVisibleElementsLocatedBy(by);
    }

    public List<UIElement> FindUIElementsFull(By by)
    {
        var result = new List<UIElement>();
        foreach (var webElement in FindElementsFull(by))
        {
            result.Add(new UIElement(_webDriver, webElement));
        }

        return result;
    }

    public void Clear()
    {
        _webElement.Clear();
    }

    public void SendKeys(string text)
    {
        _webElement.SendKeys(text);
    }

    public void Submit()
    {
        _webElement.Submit();
    }

    public void Click()
    {
        try
        {
            _webElement.Click();
        }
        catch (ElementNotInteractableException)
        {
            try
            {
                _actions
                    .MoveToElement(_webElement)
                    .Click()
                    .Build()
                    .Perform();
            }
            catch (Exception)
            {
                MoveToElement();
                ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].click();", _webElement);
            }
        }
    }

    public void Click(Func<IWebElement> element)
    {
        for (int i = 0; i <= 20; i++)
        {
            try
            {
                Console.WriteLine($"Try #{i} to click on element.");
                element().Click();
                Console.WriteLine("Clicking on the element was successful.");
                return;
            }
            catch (StaleElementReferenceException)
            {
                Console.WriteLine($"StaleElementReferenceException was thrown: try #{i}.");
            }
        }

        Console.WriteLine("Failed to click on element.");
        throw new NoSuchElementException("Stale element not found");
    }

    public string GetAttribute(string attributeName)
    {
        return _webElement.GetAttribute(attributeName);
    }

    public string GetDomAttribute(string attributeName)
    {
        return _webElement.GetDomAttribute(attributeName);
    }

    public string GetDomProperty(string propertyName)
    {
        return _webElement.GetDomProperty(propertyName);
    }

    public string GetCssValue(string propertyName)
    {
        return _webElement.GetCssValue(propertyName);
    }

    public ISearchContext GetShadowRoot()
    {
        return _webElement.GetShadowRoot();
    }

    public void MoveToElement()
    {
        ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", _webElement);
    }

    public void Hover()
    {
        _actions.MoveToElement(_webElement).Build().Perform();
    }

    public string TagName => _webElement.TagName;

    public string Text
    {
        get
        {

            if (_webElement.Text != null || _webElement.Text.Equals(""))
            {
                return GetAttribute("innerText");
            }

            return _webElement.Text;
        }
    }

    public bool Enabled => _webElement.Enabled;
    public bool Selected => _webElement.Selected;
    public Point Location => _webElement.Location;
    public Size Size => _webElement.Size;
    public bool Displayed => _webElement.Displayed;
}