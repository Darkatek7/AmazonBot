using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PS5_Bot
{
    public class BrowserLogic
    {
        private int DelayInSec(int delay)
        {
            int delayInSec = delay * 1000;
            return delayInSec;
        }

        IWebDriver driver;

        [SetUp]
        public async Task StartBrowser()
        {
            ChromeOptions options = new ChromeOptions();
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            options.AddArguments("user-data-dir=" + appdataPath + "\\Google\\Chrome\\User Data\\Default"); // path to local Chrome Data
            options.AddArguments("--start-maximized");
            try
            {
                driver = new ChromeDriver("C:\\WebDriver\\", options);  // path to chromedriver
            }
            catch { }
        }

        public async Task OpenWebsite(string site)
        {
            try
            {
                if (driver != null && !string.IsNullOrEmpty(site))
                    driver.Url = site;
                else if (driver == null)
                {
                    await StartBrowser(); // calls StartBrowser function

                    Thread.Sleep(1000);
                    driver.Url = site; // tells the driver to go the items url
                }
            }
            catch
            {
            }
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                if (driver != null)
                {
                    driver.FindElement(by); // checks if element is present on website
                    return true;
                }

                return false;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        [TearDown]
        public async Task CloseBrowser()
        {
            if (driver != null)
            {
                driver.Close(); // closes chrome
                driver.Quit();  // closes chromedriver
            }
            Environment.Exit(Environment.ExitCode); // exits program
        }

        public IWebElement GetElementUsingXPath(string xpath)
        {
            bool elementIsPresent = IsElementPresent(By.XPath(xpath));

            if (elementIsPresent.Equals(true) && driver != null) // checks if the element is present on the wesbite
            {
                return driver.FindElement(By.XPath(xpath));
            }
            else
            {
                return null;
            }
        }

        public async Task OpenPS5Screen()
        {
            await OpenWebsite(
                "https://www.amazon.de/gp/product/B08H98GVK8?pf_rd_r=8WGMBRVYKV6137VVWK67&pf_rd_p=4ba7735c-ede3-4212-a657-844b25584948&pd_rd_r=5a951b88-da02-4e0f-bd04-95c3f37726cd&pd_rd_w=Yk4dL&pd_rd_wg=WdijX&ref_=pd_gw_unk&th=1"); // link to the product you want to check and buy
        }

        public bool CheckIfProductIsAvailable()
        {
            // Remove following if you want to check for a different Product
            //IWebElement productTab = GetElementUsingXPath(
            //"/html/body/div[2]/div[2]/div[5]/div[5]/div[4]/div[30]/div[1]/div/form/div/ul/li[7]"); // change to '/html/body/div[2]/div[2]/div[5]/div[5]/div[4]/div[30]/div[1]/div/form/div/ul/li[6]' to check for disc edition

            //if (productTab != null)
            //{
            //    productTab.Click();
            //    Thread.Sleep(DelayInSec(3));

            // also don't forget to remove one '}' closing Bracket at the end of the function.

            IWebElement productTab = GetElementUsingXPath(
                "/html/body/div[2]/div[2]/div[5]/div[5]/div[4]/div[30]/div[1]/div/form/div/ul/li[3]"); // change to '/html/body/div[2]/div[2]/div[5]/div[5]/div[4]/div[30]/div[1]/div/form/div/ul/li[6]' to check for disc edition

            if (productTab != null)
            {
                productTab.Click();
                Thread.Sleep(DelayInSec(3));

                IWebElement addToCart = GetElementUsingXPath( // starting point if you to check for a different product
                    "//input[@id='add-to-cart-button']");

                if (addToCart != null)
                {
                    addToCart.Click();

                    Thread.Sleep(DelayInSec(3));

                    IWebElement noCoverage = GetElementUsingXPath( // starting point if you to check for a different product
                        "//button[@id='siNoCoverage-announce']");

                    if (noCoverage != null) // this is for some products that offer coverage
                    {
                        try
                        {
                            noCoverage.Click();
                            Thread.Sleep(DelayInSec(3));
                        }
                        catch
                        {
                            MessageBox.Show("Amazon asks for Coverage");
                        }
                    }

                    OpenWebsite("https://www.amazon.de/gp/cart/view.html?ref_=nav_cart");

                    IWebElement proceedToCheckout = GetElementUsingXPath(
                        "//input[@name='proceedToRetailCheckout']");

                    if (proceedToCheckout != null)
                    {
                        proceedToCheckout.Click();

                        IWebElement buy = GetElementUsingXPath(
                            "//input[@aria-labelledby='attach-sidesheet-checkout-button-announce']"); // label of checkout button

                        IWebElement confirmOrder = GetElementUsingXPath(
                            "//input[@name='placeYourOrder1']"); // confirm order button

                        Thread.Sleep(DelayInSec(3));

                        if (confirmOrder != null)
                        {
                            confirmOrder.Click();
                            MessageBox.Show(
                                "PS5 has been bought or Amazon is asking for a Password! Anyway check out your Amazon tab in chrome!");
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public async Task ReloadTab()
        {
            driver.Navigate().Refresh();
        }
    }
}
