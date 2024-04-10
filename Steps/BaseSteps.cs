using OpenQA.Selenium;

namespace Testiny.Steps
{
    public class BaseSteps
    {
        protected IWebDriver Driver;

        public BaseSteps(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}