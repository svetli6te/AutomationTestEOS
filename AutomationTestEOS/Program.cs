using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using System.IO;

namespace AutomationTestEOS
{
    class Program
    {
        public static IWebDriver driver;
        public static String stringToSearch = "Harry Potter and the Cursed Child - Parts One and Two";

        [SetUp]
        public void startBrowser()
        {
           
    
            string driverPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(
    TestContext.CurrentContext.TestDirectory))) + "/Drivers";
            driver = new ChromeDriver(driverPath);
        }

        [Test]
        public void Test()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.amazon.co.uk/");

            // Waiting for loading navFooter as guarantie that the page is loaded
            Assert.True(WaitUntilElementVisible(By.XPath(".//*[@id='navFooter']")).Displayed);          
            Assert.True(driver.Title.Contains("Amazon.co.uk"));

            // Accepting cookies
            driver.FindElement(By.Id("sp-cc-accept")).Click();

            // Open the drop-down menu and choose Books option
            driver.FindElement(By.Id("searchDropdownBox")).FindElement(By.XPath("//option[text() = 'Books']")).Click();

            // Finding the search field and enter the book title
            IWebElement textBox = driver.FindElement(By.Id("twotabsearchtextbox"));
            textBox.SendKeys(stringToSearch);
            driver.FindElement(By.Id("nav-search-submit-text")).Click();

            // Waiting for the search result and getting the first title from the result list
            IWebElement firstElementOfTheSearch = WaitUntilElementVisible(By.XPath(".//*[@cel_widget_id='MAIN-SEARCH_RESULTS-1']"));
            
            // Taking the element title
            IWebElement textOfTheElement = firstElementOfTheSearch.FindElement(By.XPath(".//h2/a/span"));

            // Taking the elements with the paperback text and link
            IWebElement paperBackElement = firstElementOfTheSearch.FindElement(By.XPath("//div/div/div[2]/div[2]/div/div[2]/div[1]/div/div[1]"));
            IWebElement paperBackElementLink = paperBackElement.FindElement(By.XPath(".//a"));// Paperback

            // Taking paperback price element
            IWebElement paperBackPriceSymbolElement = paperBackElement.FindElement(By.XPath("//span[contains(@class, 'a-price-symbol')]"));
            IWebElement paperBackPriceWholeElement = paperBackElement.FindElement(By.XPath("//span[contains(@class, 'a-price-whole')]"));
            IWebElement paperBackPriceFractionElement = paperBackElement.FindElement(By.XPath("//span[contains(@class, 'a-price-fraction')]"));

            // Constructing price string from the price elements
            string priceString = $"{paperBackPriceSymbolElement.Text}{paperBackPriceWholeElement.Text}.{paperBackPriceFractionElement.Text}";

            // Checking for the correct title
            Assert.True(textOfTheElement.Text.Contains(stringToSearch));

            // Checking is this a paperback option
            Assert.True(paperBackElementLink.Text.Contains("Paperback"));

            paperBackElementLink.Click();

            // Waiting for the product page to be loaded
            IWebElement secondPageTitleElement = WaitUntilElementVisible(By.XPath(".//*[@id='productTitle']"));

            // Checking is this the correct title
            Assert.True(secondPageTitleElement.Text.Contains(stringToSearch));

            // Getting the selected element paperback
            IWebElement secondPagePaperBackElement = driver.FindElement(By.ClassName("a-button-selected"));

            // Checking is this the paperback option and is the price correct
            Assert.True(secondPagePaperBackElement.Text.Contains("Paperback"));
            Assert.True(secondPagePaperBackElement.Text.Contains(priceString));

            // Adding to the basket
            IWebElement secondPageAddToCartButton = driver.FindElement(By.Id("add-to-cart-button"));
            secondPageAddToCartButton.Click();

            // Waiting for the next page loading and checing the gift option
            IWebElement thirdPageGiftCheckboxElement = WaitUntilElementVisible(By.XPath("//div[contains(@class,'a-checkbox')]/label/input"));
            thirdPageGiftCheckboxElement.Click();

            // Taking the image and price elements from the right side of the screan
            IWebElement thirdPageProductImageElement = WaitUntilElementVisible(By.XPath("//a[contains(@class,'sc-product-link')]/img"));
            IWebElement thirdPageProductPriceElement = WaitUntilElementVisible(By.XPath("//span[@class = 'ewc-subtotal-amount']/span"));

            // Taking the basket button
            IWebElement thirdPageEditBasketButtonElement = driver.FindElement(By.Id("hlb-view-cart-announce"));

            // Checking for the book title in the "alt" attribute of the image
            Assert.True(thirdPageProductImageElement.GetAttribute("alt").Contains(stringToSearch));

            // Checking for the price is it the same as in the first page
            Assert.True(thirdPageProductPriceElement.Text.Contains(priceString));

            thirdPageEditBasketButtonElement.Click();

            // Getting nav-cart-count element by its ID which represents actually how many we have in the basket
            IWebElement fourthPageBasketCountElement = driver.FindElement(By.Id("nav-cart-count"));

            // Checking if there is only one item in the basket
            Int32 basketCount = Int32.Parse(fourthPageBasketCountElement.Text);
            Assert.True(basketCount == 1);

            // Getting product title, paperback and price as elements 
            IWebElement fourthPageProductElement = driver.FindElement(By.ClassName("sc-product-title"));
            IWebElement fourthPagePaperbackElement = driver.FindElement(By.ClassName("sc-product-binding"));
            IWebElement fourthPagePriceElement = driver.FindElement(By.ClassName("sc-product-price"));

            // Checking those elements against strings
            Assert.True(fourthPageProductElement.Text.Contains(stringToSearch));
            Assert.True(fourthPagePaperbackElement.Text.Contains("Paperback"));
            Assert.True(fourthPagePriceElement.Text.Contains(priceString));

            // Checking for the quantity is it = 1
            IWebElement fourthPageQTYElement = driver.FindElement(By.ClassName("sc-action-quantity"));
            Assert.True(Int32.Parse(fourthPageQTYElement.GetAttribute("data-old-value")) == 1);
        }
        public static IWebElement WaitUntilElementVisible(By elementLocator, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
                throw;
            }
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Quit();
        }

    }
}