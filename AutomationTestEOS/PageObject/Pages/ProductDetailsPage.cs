using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.PageObjects;

namespace AutomationTestEOS.PageObject.Pages
{
    class ProductDetailsPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public ProductDetailsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "productTitle")]
        [CacheLookup]
        private IWebElement productTitle;

        [FindsBy(How = How.ClassName, Using = "a-button-selected")]
        [CacheLookup]
        private IWebElement productElement;

        [FindsBy(How = How.Id, Using = "add-to-cart-button")]
        [CacheLookup]
        private IWebElement addToCartButton;

        public IWebElement getProductTitle()
        {
            return productTitle;
        }

        public IWebElement getProductElement()
        {
            return productElement;
        }

        public IWebElement getAddToCartButton()
        {
            return addToCartButton;
        }

        public bool testProductTitle(string title)
        {
            return getProductTitle().Text.Contains(title);
        }

        public bool testProductPrice(string price)
        {
            return getProductElement().Text.Contains(price);
        }

        public bool testProductType(string type)
        {
            return getProductElement().Text.Contains(type);
        }

        public AddedToCartPage testClickOnAddToCartButton()
        {
            getAddToCartButton().Click();

            return new AddedToCartPage(driver);
        }

        public void loadComplete()
        {
            // Wait for the page to load
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
