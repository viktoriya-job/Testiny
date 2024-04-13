using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class DialogPage : BasePage
    {
        private static readonly By _titleLabelBy = By.CssSelector("[data-testid='text-title']>p");
        private static readonly By _confirmButtonBy = By.CssSelector("[data-testid='button-affirm']");
        private static readonly By _discardButtonBy = By.CssSelector("[data-testid='button-negate']");
        private static readonly By _cancelButtonBy = By.CssSelector("[data-testid='button-cancel']");

        public DialogPage(IWebDriver driver) : base(driver) { }

        public UIElement TitleLable => new(Driver, _titleLabelBy);
        public Button ConfirmButton => new(Driver, _confirmButtonBy);
        public Button DiscardButton => new(Driver, _discardButtonBy);
        public Button CancelButton => new(Driver, _cancelButtonBy);


        [AllureStep("Click Confirm Button")]
        public T ConfirmButtonClick<T>()
        {
            ConfirmButton.Click();
            return (T)Activator.CreateInstance(typeof(T), Driver, false);
        }

        [AllureStep("Click Discard Button")]
        public T DiscardButtonClick<T>()
        {
            DiscardButton.Click();
            return (T)Activator.CreateInstance(typeof(T), Driver, false);
        }

        [AllureStep("Click Cancel Button")]
        public T CancelButtonClick<T>()
        {
            CancelButton.Click();
            return (T)Activator.CreateInstance(typeof(T), Driver, false);
        }

        protected override string GetEndpoint()
        {
            throw new NotImplementedException();
        }

        public override bool IsPageOpened()
        {
            try
            {
                return TitleLable.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
