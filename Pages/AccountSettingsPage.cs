using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class AccountSettingsPage// : BasePage
    {
        //private static string _endPoint = "/settings/profile";
        //private static readonly By _titleLabelBy = By.ClassName("sc-fAUVGl");
        //private static readonly By _uploadInputBy = By.CssSelector("input[type='file']");
        //private static readonly By _avatarButtonBy = By.CssSelector("[data-testid='section-drop-area']>div>.button-main>.button-label");

        //public AccountSettingsPage(IWebDriver driver) : base(driver) { }
        //public AccountSettingsPage(IWebDriver driver, bool openByUrl) : base(driver, openByUrl) { }

        //public UIElement TitleLable => new(Driver, _titleLabelBy);
        //public UIElement UploadInput => new(Driver, _uploadInputBy);
        //public Button AvatarButton => new(Driver, _avatarButtonBy);
        //public TopMenuPage TopMenu => new TopMenuPage(Driver);

        //[AllureStep("Checking is the avatar file added")]
        //public bool IsAvatarAdded()
        //{
        //    if (AvatarButton.Text.Trim() == "Remove avatar")
        //        return true;

        //    return false;
        //}

        //[AllureStep("Remove avatar")]
        //public AccountSettingsPage RemoveAvatar()
        //{
        //    if (AvatarButton.Text.Trim() == "Remove avatar")
        //    {
        //        AvatarButton.Click();
        //    }
        //    else
        //    {
        //        throw new Exception("It is impossible to delete an avatar - the avatar was not added");
        //    }
        //    return this;
        //}

        //protected override string GetEndpoint() => _endPoint;

        //[AllureStep("Checking is the Account settings page opened")]
        //public override bool IsPageOpened()
        //{
        //    try
        //    {
        //        return TitleLable.Text.Trim() == "My account";
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
    }
}