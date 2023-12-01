using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace TestProject1
{
    public class Tests
    {
        public static string GenerateRandomString(int size, bool lowercase = true)
        {
            // Create a StringBuilder to store the generated string
            var stringBuilder = new StringBuilder();
            // Create a Random object to generate random characters
            var random = new Random();
            // Generate random characters and append them to the StringBuilder
            for (int i = 0; i < size; i++)
            {
                var character = Convert.ToChar(random.Next(97, 123));
                stringBuilder.Append(character);
            }
            // Convert the StringBuilder to a string and return it
            return lowercase ? stringBuilder.ToString().ToLower() : stringBuilder.ToString();
        }

        private bool ValidatePhoneNumber(string phoneNumber)
        {
            // Regular expression pattern for valid phone numbers
            string pattern = @"^\d{11}$";

            // Check if the phone number matches the pattern
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

        private IWebDriver driver;
        private readonly By _loginInputButton = By.XPath("//input[@placeholder='Телефон']");
        private readonly By _signInButton = By.XPath("//span[text()='Войти']");
        //private readonly By _FirstAgreement = By.XPath("//span[@class='_25d45facb5--box--TSmoe _25d45facb5--box--aD_nX']");

        private readonly By _AfterAgreementButton = By.XPath("//div[@class='_25d45facb5--controls-row--pnq8w']");
        private readonly By _New_Building = By.XPath("//a[@href='https://www.cian.ru/novostrojki/']");

        private readonly By _AfterScrollBack = By.XPath("//span[@class='_025a50318d--link-area--bIF7s']"); 
        private readonly By _AfterScrollBackSecond = By.XPath("//span[@class='_025a50318d--link-area--bIF7s']"); 
        private readonly By _AfterScrollBackThird = By.XPath("//span[@class='_025a50318d--link-area--bIF7s']"); 

        private readonly By _FavoriteButton = By.XPath("//button[@class='_7a3fb80146--favorite_button--h25dU']");
        private readonly By _AllFavoriteButton = By.XPath("//div[@data-name='UtilityFavoritesContainer']");

        private readonly By _Arend = By.XPath("//a[@class='_25d45facb5--link--rqF9a']");

        private readonly By _RoomsButton = By.XPath("//button[@title='1, 2 комн.']");
        private readonly By _CookiesButton = By.XPath("//button[@class='_25d45facb5--button--KVooB _25d45facb5--button--gs5R_ _25d45facb5--M--I5Xj6 _25d45facb5--button--DsA7r']");
        private readonly By _OpenPlan = By.XPath("//span[text()='Свободная планировка']");
        private readonly By _PriceButton = By.XPath("//div[@class='_025a50318d--container--H8VA5 _025a50318d--container--_El9Q']");

        private readonly By _FirstPrice = By.XPath("//input[@placeholder='от']");
        private readonly By _SecondPrice = By.XPath("//input[@placeholder='до']");
        private readonly By _FindButton = By.XPath("//a[text()='Найти']");

        //Positive
        //Before
        [SetUp]
        public void Setup()
        {
            //Init. of ChromeDriver from C:\\chromedriver\\chromedriver.exe
            driver = new ChromeDriver("C:\\chromedriver");
            //NavigateToURL
            driver.Navigate().GoToUrl("https://cian.ru");
            //Make the window fullscreen
            driver.Manage().Window.Maximize();
        }
        //Tests
        [Test]
        public void TestOfCreatngTheAccountAndAddToFavourite()
        {
            //Sign In (Find the button of SignIn)
            var signIn = driver.FindElement(_signInButton);
            signIn.Click();

            //Input for login (the phone number)
            var logIn = driver.FindElement(_loginInputButton);
            logIn.SendKeys("79999709618");

            //Sleep 10 sec
            Thread.Sleep(10000);

            //Agreement with some rules (only need if it is ur first acc)
            var element_present = true;
            try {
                var AfterAgreementButton = driver.FindElement(_AfterAgreementButton);
                if (element_present)
                {
                    AfterAgreementButton.Click();
                }
            }
            catch(NoSuchElementException NoButton) { element_present = false; }
            

            //NewBuildings finding and click
            var ButtonNewBuilding = driver.FindElement(_New_Building);
            ButtonNewBuilding.Click();

            Thread.Sleep(3000);

            //JS executor for scrolling the page down until the first offers
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,1350)");

            Thread.Sleep(3000);

            //Choice of some offer in []. May be do random element
            Random randomOf1to3 = new Random();
            int SizeOfRand = randomOf1to3.Next(1, 4);
            switch (SizeOfRand) { 
                case 1:
                    var AfterScrollChoice1 = driver.FindElement(_AfterScrollBackSecond);
                    AfterScrollChoice1.Click();
                    break;
                case 2:
                    var AfterScrollChoice = driver.FindElement(_AfterScrollBack);
                    AfterScrollChoice.Click();
                    break;
                case 3:
                    var AfterScrollChoice2 = driver.FindElement(_AfterScrollBackThird);
                    AfterScrollChoice2.Click();
                    break;
            } 

            //Add to favorite []
            var FavoriteButton = driver.FindElement(_FavoriteButton);
            FavoriteButton.Click();

            Thread.Sleep(3000);

            //Click on all favourites
            var AllFavButton = driver.FindElement(_AllFavoriteButton);
            AllFavButton.Click();

            Thread.Sleep(3000);

            //Click on arend button
            var ArendButton = driver.FindElement(_Arend);
            ArendButton.Click();

            Thread.Sleep(3000);

            //Agree with cookies
            var Cookies = driver.FindElement(_CookiesButton);
            Cookies.Click();

            Thread.Sleep(3000);

            //rooms button
            var RoomsButton = driver.FindElement(_RoomsButton);
            RoomsButton.Click();

            Thread.Sleep(3000);

            //Price button click
            var OpenPlan = driver.FindElement(_OpenPlan);
            OpenPlan.Click();

            Thread.Sleep(3000);

            //Choice of OpenPlanElement
            var PriceButton = driver.FindElement(_PriceButton);
            PriceButton.Click();

            var presention = true;
            Random random_first = new Random();
            Random random_second = new Random();
            int int_random1 = random_first.Next(1_000_000, 5_000_000);
            int int_random2 = random_second.Next(6_000_000, 15_000_000);
            string string_random1 = int_random1.ToString();
            string string_random2 = int_random2.ToString();
            try
            {
                var FirstPrice = driver.FindElement(_FirstPrice);
                var SecondPrice = driver.FindElement(_SecondPrice);
                if (presention)
                {
                    FirstPrice.SendKeys(string_random1);
                    Thread.Sleep(3000);
                    SecondPrice.SendKeys(string_random2);
                }
            }
            catch (NoSuchElementException NoButton) { presention = false; }

            Thread.Sleep(3000);

            //FindButton click
            var FindButton = driver.FindElement(_FindButton);
            FindButton.Click();

        }
        //After
        [TearDown]
        public void TearDown()
        {
            Thread.Sleep(4000);
            driver.Close();
        }
    }
}