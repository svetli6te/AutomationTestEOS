using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using SeleniumExtras.PageObjects;


namespace AutomationTestEOS.PageObject.Pages
{
    class AddedToCartPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public AddedToCartPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'a-checkbox')]/label/input")]
        [CacheLookup]
        private IWebElement checkBoxElement;

        [FindsBy(How = How.XPath, Using = "//a[contains(@class,'sc-product-link')]/img")]
        [CacheLookup]
        private IWebElement productImageElement;

        [FindsBy(How = How.XPath, Using = "//span[@class = 'ewc-subtotal-amount']/span")]
        [CacheLookup]
        private IWebElement productPriceElement;

        //[FindsBy(How = How.Id, Using = "hlb-view-cart-announce")]
        [FindsBy(How = How.Id, Using = "nav-cart")]
        [CacheLookup]
        private IWebElement basketButtonElement;

        public IWebElement getCheckBoxElement()
        {
            return checkBoxElement;
        }

        public IWebElement getProuctImageElement()
        {
            return productImageElement;
        }

        public IWebElement getPriceElement()
        {
            return productPriceElement;
        }

        public IWebElement getBasketButtonElement()
        {
            return basketButtonElement;
        }
        

        public bool testProductTitle(string title)
        {
            return getProuctImageElement().GetAttribute("alt").Contains(title);
        }

        public bool testProductPrice(string price)
        {
            return getPriceElement().Text.Contains(price);
        }

        public CartPage testGoToBasketButton()
        {
            getBasketButtonElement().Click();

            return new CartPage(driver);
        }

        public void loadComplete()
        {
            // Wait for the page to load
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            // Will wait the image to load
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(@class,'sc-product-link')]/img")));
        }
    }
}
