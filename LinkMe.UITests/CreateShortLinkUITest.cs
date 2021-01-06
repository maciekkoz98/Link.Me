using LinkMe.UITests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace LinkMe.UITests
{
    public class CreateShortLinkUITest
    {
        private readonly string baseUrl;
        private readonly IWebDriver driver;

        public CreateShortLinkUITest()
        {
            this.baseUrl = "https://localhost:44388/";

            this.driver = new ChromeDriver();
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            this.driver.Manage().Window.Maximize();
            this.driver.Navigate().GoToUrl(this.baseUrl);
        }

        [Fact]
        public void CreateShortLinkUnlogged()
        {
            this.driver.FindElement(By.Id("Link_OriginalLink")).SendKeys("https://www.isod.ee.pw.edu.pl");
            this.driver.FindElement(By.Name("btnsubmit")).Click();

            var waitDriver = new WebDriverWait(this.driver, TimeSpan.FromSeconds(5));
            var shortUrlInput = waitDriver.Until(e => e.FindElement(By.Id("shortLinkInput")));
            var shortUrl = shortUrlInput.GetProperty("value");
            Assert.True(shortUrl.Length > 0);
            this.driver.Quit();
        }

        [Fact]
        public void CreateShortLinkLogged()
        {
            AuthorizationHelper.PerformAuthentication(this.driver, "test@test.com", "Test123!");
            this.driver.FindElement(By.Id("Link_OriginalLink")).SendKeys("https://www.isod.ee.pw.edu.pl");
            this.driver.FindElement(By.Name("btnsubmit")).Click();

            var waitDriver = new WebDriverWait(this.driver, TimeSpan.FromSeconds(5));
            var shortUrlInput = waitDriver.Until(e => e.FindElement(By.Id("shortLinkInput")));
            var shortUrl = shortUrlInput.GetProperty("value");
            Assert.True(shortUrl.Length > 0);
            this.driver.Quit();
        }

        [Fact]
        public void ShowStatsPage()
        {
            AuthorizationHelper.PerformAuthentication(this.driver, "test@test.com", "Test123!");
            this.driver.FindElement(By.Id("Link_OriginalLink")).SendKeys("https://www.isod.ee.pw.edu.pl");
            this.driver.FindElement(By.Name("btnsubmit")).Click();

            var waitDriver = new WebDriverWait(this.driver, TimeSpan.FromSeconds(5));
            var shortUrlInput = waitDriver.Until(e => e.FindElement(By.Id("shortLinkInput")));
            var shortUrl = shortUrlInput.GetProperty("value");
            Assert.True(shortUrl.Length > 0);

            this.driver.Navigate().GoToUrl($"{this.baseUrl}");
            var statsButton = waitDriver.Until(e => e.FindElement(By.ClassName("no-accent")));
            statsButton.Click();

            this.driver.Quit();
        }
    }
}
