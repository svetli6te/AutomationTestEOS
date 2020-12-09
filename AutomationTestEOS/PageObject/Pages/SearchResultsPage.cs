using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.PageObjects;

namespace AutomationTestEOS.PageObject.Pages
{
    class SearchResultsPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public SearchResultsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = ".//*[@cel_widget_id='MAIN-SEARCH_RESULTS-0']")]
        [CacheLookup]
        private IWebElement firstElementOfSearch;

        [FindsBy(How = How.XPath, Using = ".//*[@cel_widget_id='MAIN-SEARCH_RESULTS-0']//h2/a/span")]
        [CacheLookup]
        private IWebElement textElementOfTheFirstSearchResult;

        [FindsBy(How = How.XPath, Using = "//span[contains(@class, 'a-price-symbol')]")]
        [CacheLookup]
        private IWebElement priceSymbolElement;

        [FindsBy(How = How.XPath, Using = "//span[contains(@class, 'a-price-whole')]")]
        [CacheLookup]
        private IWebElement priceWholeElement;

        [FindsBy(How = How.XPath, Using = "//span[contains(@class, 'a-price-fraction')]")]
        [CacheLookup]
        private IWebElement priceFractionElement;

        public IWebElement getTypeOfFirstSearchResult(string type = "Paperback")
        {

            return getFirstElementOfSearch().FindElement(By.XPath("//a[normalize-space(text()) = '" + type + "']"));
            return driver.FindElement(By.XPath("//a[normalize-space(text()) = '" + type + "']"));
        }

        public IWebElement getFirstElementOfSearch()
        {
            return firstElementOfSearch;
        }

        public IWebElement getTextOfTheFirstSearchResult()
        {
            return textElementOfTheFirstSearchResult;
        }

        public IWebElement getPriceSymbolElement()
        {
            return priceSymbolElement;
        }

        public IWebElement getPriceWholeElement()
        {
            return priceWholeElement;
        }

        public IWebElement getPriceFractionElement()
        {
            return priceFractionElement;
        }

        public string getPriceString()
        {
            return getPriceSymbolElement().Text + getPriceWholeElement().Text + "." + getPriceFractionElement().Text;
        }

        public ProductDetailsPage testClickOnFirstResultWithType(string type = "Paperback")
        {
            getTypeOfFirstSearchResult(type).Click();

            return new ProductDetailsPage(driver);
        }

        public void loadComplete()
        {
            // Wait for the page to load
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
