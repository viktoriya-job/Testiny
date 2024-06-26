﻿using OpenQA.Selenium;
using Testiny.Pages;

namespace Testiny.Steps
{
    public class BaseSteps(IWebDriver driver)
    {
        protected readonly IWebDriver Driver = driver;
        protected LoginPage? LoginPage { get; set; }
    }
}