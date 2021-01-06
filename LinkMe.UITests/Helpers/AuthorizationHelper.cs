using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMe.UITests.Helpers
{
    public static class AuthorizationHelper
    {
        public static void PerformAuthentication(IWebDriver driver, string username, string password)
        {
            driver.FindElement(By.Id("login")).Click();

            var waitDriver = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var emailInput = waitDriver.Until(e => e.FindElement(By.Id("Input_Email")));
            emailInput.SendKeys(username);
            var passwordInput = waitDriver.Until(e => e.FindElement(By.Id("Input_Password")));
            passwordInput.SendKeys(password);
            driver.FindElement(By.ClassName("btn-primary")).Click();
        }
    }
}
