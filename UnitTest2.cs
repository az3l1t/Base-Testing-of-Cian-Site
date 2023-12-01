using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace TestProject1
{
    public class Test2
    {

        private static string GenerateInvalidNumber()
        {
            // Генерируем случайный код региона
            int regionCode = 1 + new Random().Next(999);

            // Генерируем случайный номер абонента
            int subscriberNumber = 10000000 + new Random().Next(99999999);

            // Возвращаем номер в формате +7[regionCode][subscriberNumber]
            return "+7" + regionCode.ToString("03") + subscriberNumber.ToString("08");
        }

        //Negative
        private IWebDriver driver;
        private readonly By _loginInputButton = By.XPath("//input[@class='_25d45facb5--input--qT5Pp']");
        private readonly By _loginInputAfter = By.XPath("//input[@class='_25d45facb5--input--qT5Pp']");
        private readonly By _signInButton = By.XPath("//span[text()='Войти']");
        private readonly By _ErrorMessage = By.XPath("//span[@class='_25d45facb5--error-message--NCPjP']");
        private readonly By _Back = By.XPath("//span[text()='Назад']");

        [SetUp]
        public void SetUp()
        {

            
            //Init. of ChromeDriver from C:\\chromedriver\\chromedriver.exe
            driver = new ChromeDriver("C:\\chromedriver");
            //NavigateToURL
            driver.Navigate().GoToUrl("https://cian.ru");
            //Make the window fullscreen
            driver.Manage().Window.Maximize();

        }
        [Test] public void NegativeWayPhoneNumbers() 
        {


            //Sign In (Find the button of SignIn)
            var signIn = driver.FindElement(_signInButton);
            signIn.Click();

            List<string> invalidNumbers = new List<string>();
            var logIn = driver.FindElement(_loginInputButton);

            for (int i = 0; i < 100; i++)
            {
                string number = GenerateInvalidNumber();

                invalidNumbers.Add(number);
            }
            
            foreach (string number in invalidNumbers)
            {
                int check_numbers_negative = 0;
                bool check = true;
                logIn.SendKeys(number);
                Task.Delay(TimeSpan.FromSeconds(3)).Wait();
                try
                {
                    var Error = driver.FindElement(_ErrorMessage);
                    logIn.Clear();
                    check = true;
                    check_numbers_negative += 1;
                    Console.WriteLine(number);
                }
                catch (NoSuchElementException) {

                    var Back = driver.FindElement(_Back);
                    Back.Click();
                    Task.Delay(TimeSpan.FromSeconds(3)).Wait();
                    logIn.Clear(); 
                    check = false;
                }
            }

            //Input for login (the phone number)

        }
        [TearDown] public void TearDown() 
        {
            Thread.Sleep(4000);
            driver.Close();
        }
    }
}
