using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.PageObjects;

namespace AutomationTestEOS.PageObject.Pages
{
    class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "sp-cc-accept")]
        [CacheLookup]
        private IWebElement acceptCookiesButton;

        [FindsBy(How = How.Id, Using = "searchDropdownBox")]
        [CacheLookup]
        private IWebElement searchDropDownBox;

        [FindsBy(How = How.Id, Using = "twotabsearchtextbox")]
        [CacheLookup]
        private IWebElement searchTextBox;

        [FindsBy(How = How.Id, Using = "nav-search-submit-text")]
        [CacheLookup]
        private IWebElement submitButton;


        public void closeAcceptCookies()
        {
            acceptCookiesButton.Click();
        }

        public IWebElement getSearchDropdownBox()
        {
            return searchDropDownBox;
        }

        public IWebElement getSearchDropdownBoxBooksElement(string searchType)
        {
            return getSearchDropdownBox().FindElement(By.XPath("//option[text() = '" + searchType + "']"));
        }

        public IWebElement getSearchBoxInput()
        {
            return searchTextBox;
        }

        public IWebElement getSubmitButton()
        {
            return submitButton;
        }

        public string getTitle()
        {
            return driver.Title;
        }

        public void goToPage(string url)
        {
            driver.Navigate().GoToUrl(url);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public SearchResultsPage testSearchFor(string searchString, string into = "Books")
        {
            IWebElement searchBoxTypeElement = getSearchDropdownBoxBooksElement(into);
            IWebElement searchBoxInput = getSearchBoxInput();
            IWebElement submitButton = getSubmitButton();

            searchBoxTypeElement.Click();
            searchBoxInput.SendKeys(searchString);

            submitButton.Click();

            return new SearchResultsPage(driver);
        }

        public void loadComplete()
        {
            // Wait for the page to load
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
