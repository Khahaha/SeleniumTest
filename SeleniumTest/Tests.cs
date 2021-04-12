using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    public class Tests
    {
        public ChromeDriver driver;
        public WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized"); 
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); 
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); 
        }

        private By emailInputLocator = By.Name("email");// поле ввода email
        private By buttonLocator = By.Id("sendMe"); // кнопка Подобрать имя
        private By formErrorLocator = By.ClassName("form-error"); // форма ошибки
        private By girlinputLocator = By.Id("girl"); // инпут выбора девочки
        private By emailResultLocator = By.ClassName("your-email"); // Результат на который пришлют имена
        private By resultTextBlockLocator = By.ClassName("result-text"); // текст если заявка отправлена
        private By buttonAnotherEmailLocator = By.Id("anotherEmail");

        string expectedEmail = "test@mail.ru";

        [Test]
        public void ParrotNameSite_ClickPickUpName_EmailInputIsEmpty()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys("");
            driver.FindElement(buttonLocator).Click();

            Assert.AreEqual("Введите email", driver.FindElement(formErrorLocator).Text, "Не показали ошибку на пустое поле email");
        }

        [Test]
        public void ParrotNameSite_ClickPickUpName_EmailInputIsIncorrect()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys("test");
            driver.FindElement(buttonLocator).Click();

            Assert.AreEqual("Некорректный email", driver.FindElement(formErrorLocator).Text, "Не показали ошибку на некорректно заполненный email");
        }

        [Test]
        public void ParrotNameSite_FillFormWithEmailSuccess()
        {

            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(buttonLocator).Click();

            Assert.AreEqual("test@mail.ru", driver.FindElement(emailResultLocator).Text, "Отправили список имен не на тот адрес");
        }

        [Test]
        public void ParrotNameSite_ParrotNameForBoy()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(buttonLocator).Click();

            Assert.AreEqual("Хорошо, мы пришлём имя для вашего мальчика на e-mail:", driver.FindElement(resultTextBlockLocator).Text, "Отправили список имен не для мальчика");
        }

        [Test]
        public void ParrotNameSite_ParrotNameForGirl()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(girlinputLocator).Click();
            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(buttonLocator).Click();

            Assert.AreEqual("Хорошо, мы пришлём имя для вашей девочки на e-mail:", driver.FindElement(resultTextBlockLocator).Text, "Отправили список имен не для девочек");
        }

        [Test]
        public void ParrotNameSite_ClickAnotherEmail_EmailInputIsEmpty()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(buttonLocator).Click();
            driver.FindElement(buttonAnotherEmailLocator).Click();

            Assert.AreEqual(string.Empty, driver.FindElement(emailInputLocator).Text, "После клика по ссылке поле не очистилось");
        }

        [TearDown]
        public void TearDown()
        { 
           driver.Quit();
        }

    }
}
