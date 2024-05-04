using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Security.Cryptography;

namespace PinTests
{
    [TestFixture]
    public class PinTests
    {
        private IWebDriver driver;
        private readonly By _pricingLink  = By.XPath("//div[text()='Просмотреть']");
        private readonly By _loginButton = By.XPath("//div[text()='Войти']");
        private readonly By _emailInput = By.XPath("//input[@id='email']");
        private readonly By _passwordInput = By.XPath("//input[@id='password']");
        private readonly By _loginButton2 = By.XPath("//button[@type='submit']");
        private readonly By _profile = By.XPath("//*[@id=\"HeaderContent\"]/div/div/div[2]/div/div/div/div[5]/div[4]/div/div/div/a/div/div");


        [SetUp]
        public void Setup()
        {
            // Инициализация драйвера браузера
            driver = new ChromeDriver(@"E:\testing\chromedriver-win32");
            driver.Navigate().GoToUrl("https://ru.pinterest.com/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [Test]
        public void TestPageTitle()
        {
            // Проверка заголовка страницы
            Assert.That(driver.Title, Is.EqualTo("Pinterest – Пинтерест"));
        }

        [Test]
        public void TestObjectVisibility()
        {
            // Пример проверки видимости объектов
            var element = driver.FindElement(_loginButton);
            Assert.IsTrue(element.Displayed);
        }

        [Test]
        public void TestLinkNavigation()
        {
            // Пример проверки перехода по ссылке
            var link = driver.FindElement(_pricingLink);
            Thread.Sleep(1000);
            link.Click();
            Thread.Sleep(1000);
            Assert.AreEqual("https://ru.pinterest.com/ideas/", driver.Url);
        }

        [Test]
        public void TestLogin()
        {
            // Пример заполнения текстового поля
            Thread.Sleep(2000);
            var s1 = driver.FindElement(_loginButton);
            s1.Click();
            Thread.Sleep(1000);
            var s2 = driver.FindElement(_emailInput);
            s2.SendKeys("lisyaokrutoi@outlook.com");
            Thread.Sleep(500);
            var s3 = driver.FindElement(_passwordInput);
            s3.SendKeys("uiu+*fwUhF:W8t?");
            // Проверка введенного текста
            Assert.AreEqual("lisyaokrutoi@outlook.com", s2.GetAttribute("value"));
            Assert.AreEqual("uiu+*fwUhF:W8t?", s3.GetAttribute("value"));
            Thread.Sleep(500);
            var s4 = driver.FindElement(_loginButton2);
            s4.Click();
            Thread.Sleep(5000);
            var s5 = driver.FindElement(_profile);
            s5.Click();
            Thread.Sleep(1000);
            Assert.AreEqual("https://ru.pinterest.com/lisyaokrutoi/", driver.Url);
        }

        [TearDown]
        public void TearDown()
        {
            // Закрытие браузера после каждого теста
            driver.Close();
        }
    }
}
