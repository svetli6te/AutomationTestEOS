using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.PageObjects;

namespace AutomationTestEOS.PageObject.Pages
{
    class CartPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));

            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "nav-cart-count")]
        [CacheLookup]
        private IWebElement cartCountElement;

        [FindsBy(How = How.ClassName, Using = "sc-product-title")]
        [CacheLookup]
        private IWebElement productTitleElement;

        [FindsBy(How = How.ClassName, Using = "sc-product-binding")]
        [CacheLookup]
        private IWebElement productTypeElement;

        [FindsBy(How = How.ClassName, Using = "sc-product-price")]
        [CacheLookup]
        private IWebElement productPriceElement;

        [FindsBy(How = How.ClassName, Using = "sc-action-quantity")]
        [CacheLookup]
        private IWebElement productQuantityElement;

        public IWebElement getCartCountElement()
        {
            return cartCountElement;
        }

        public IWebElement getProductTitleElement()
        {
            return productTitleElement;
        }

        public IWebElement getProductTypeElement()
        {
            return productTypeElement;
        }

        public IWebElement getProductPriceElement()
        {
            return productPriceElement;
        }

        public IWebElement getProductQuantityElement()
        {
            return productQuantityElement;
        }

        public string getPageTitle()
        {
            return driver.Title;
        }

        public bool testCartCount(Int32 count = 1)
        {
            return Int32.Parse(getCartCountElement().Text) == count;
        }

        public bool testProductTitle(string title)
        {
            Console.WriteLine(getProductTitleElement().Text);
            return getProductTitleElement().Text.Contains(title);
        }

        public bool testProductType(string type)
        {
            return getProductTypeElement().Text.Contains(type);
        }

        public bool testProductQuantity(Int32 quantity) 
        {
            return Int32.Parse(getProductQuantityElement().GetAttribute("data-old-value")) == quantity;
        }

        public bool testProductPrice(string price)
        {
            return getProductPriceElement().Text.Contains(price);
        }

        public void loadComplete()
        {
            // Wait for the page to load
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
