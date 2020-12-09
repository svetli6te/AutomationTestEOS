using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using AutomationTestEOS.PageObject.Pages;

namespace AutomationTestEOS.Test.Scripts
{
    [TestFixture]
    class HarryPotterTest
    {
        public static IWebDriver driver;
        public static String stringToSearch = "Harry Potter and the Cursed Child - Parts One and Two";

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Test()
        {
            driver.Manage().Window.Maximize();
 
            HomePage homePage = new HomePage(driver);
            homePage.goToPage("https://www.amazon.co.uk/");

            homePage.loadComplete();

            Assert.True(driver.Title.Contains("Amazon.co.uk"));

            homePage.closeAcceptCookies();

            Assert.True(homePage.getTitle().Contains("Amazon.co.uk"));

            SearchResultsPage searchResultsPage = homePage.testSearchFor(stringToSearch);

            searchResultsPage.loadComplete();

            string priceString = searchResultsPage.getPriceString();

            // Checking for the correct title
            Assert.True(searchResultsPage.getFirstElementOfSearch().Text.Contains(stringToSearch));
            Assert.True(searchResultsPage.getTypeOfFirstSearchResult().Text.Contains("Paperback"));

            ProductDetailsPage productDetailsPage = searchResultsPage.testClickOnFirstResultWithType();

            productDetailsPage.loadComplete();

            Assert.True(productDetailsPage.getProductTitle().Text.Contains(stringToSearch));
            Assert.True(productDetailsPage.getProductElement().Text.Contains("Paperback"));
            Assert.True(productDetailsPage.getProductElement().Text.Contains(priceString));

            AddedToCartPage addedToCartPage = productDetailsPage.testClickOnAddToCartButton();

            addedToCartPage.loadComplete();

            addedToCartPage.getCheckBoxElement().Click();

            Assert.True(addedToCartPage.testProductTitle(stringToSearch));
            Assert.True(addedToCartPage.testProductPrice(priceString));

            CartPage cartPage = addedToCartPage.testGoToBasketButton();

           
            cartPage.loadComplete();

            Console.WriteLine(cartPage.getPageTitle());

            Assert.True(cartPage.testProductTitle(stringToSearch));
            Assert.True(cartPage.testCartCount(1));
            Assert.True(cartPage.testProductType("Paperback"));
            Assert.True(cartPage.testProductPrice(priceString));
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Quit();
        }
    }
}
