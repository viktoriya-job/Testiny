using Allure.NUnit.Attributes;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using Testiny.Models;
using Testiny.Pages;

namespace Testiny.Steps
{
    public class ProjectsSteps(IWebDriver driver) : BaseSteps(driver)
    {
        [AllureStep("Adding project with correct project data")]
        public ProjectPage AddProjectSuccessfull(Project project, AddProjectPage addProjectPage)
        {
            var result = addProjectPage
                    .NameValueInput(project.ProjectName)
                    .ProjectKeyValueInput(project.ProjectKey)
                    .DescriptionValueInput(project.Description)
                    .AddButtonClick();

            Thread.Sleep(1000); //pause to avoid StaleElementReferenceException

            return result;
        }

        [AllureStep("Adding project with correct project data, but checking 'Close' button")]
        public DialogPage AddProjectUnsuccessfull(Project project, AddProjectPage addProjectPage)
        {
            addProjectPage
                    .NameValueInput(project.ProjectName)
                    .ProjectKeyValueInput(project.ProjectKey)
                    .DescriptionValueInput(project.Description)
                    .CloseButtonClick();

            return new DialogPage(Driver);
        }

        [AllureStep("Input Project Fields")]
        public AddProjectPage InputProjectFields(Project project, AddProjectPage addProjectPage)
        {
            return addProjectPage
                    .NameValueInput(project.ProjectName)
                    .ProjectKeyValueInput(project.ProjectKey)
                    .DescriptionValueInput(project.Description);
        }
    }
}