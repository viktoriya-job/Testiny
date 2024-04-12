using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class AccountSettingsPage : BasePage
    {
        private static string _endPoint = "/settings/profile";
        private static readonly By _titleLabelBy = By.ClassName("sc-fAUVGl");
        private static readonly By _avatarButtonBy = By.CssSelector("[data-testid='section-drop-area']>div>.button-main>.button-label");

        public AccountSettingsPage(IWebDriver driver) : base(driver) { }
        public AccountSettingsPage(IWebDriver driver, bool openByUrl) : base(driver, openByUrl) { }

        public UIElement TitleLable => new(Driver, _titleLabelBy);
        public Button AvatarButton => new(Driver, _avatarButtonBy);
        public TopMenuPage topMenu => new TopMenuPage(Driver);

        [AllureStep("Checking is the avatar file added")]
        public bool AvatarAdded()
        {
            if (AvatarButton.Text.Trim() == "Remove avatar")
                return true;

            return false;
        }

        protected override string GetEndpoint() => _endPoint;

        [AllureStep("Checking is the Account settings page opened")]
        public override bool IsPageOpened()
        {
            try
            {
                return TitleLable.Text.Trim() == "My account";
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}