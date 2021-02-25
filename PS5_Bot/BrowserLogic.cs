using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json;
using PS5_Bot.Models;

namespace PS5_Bot
{
    public class BrowserLogic
    {
        private IWebDriver _driver;
        private Amazon_de _xpath;

        public BrowserLogic(Amazon_de xpath)
        {
            _xpath = xpath;
        }

        [SetUp]
        public async Task StartBrowser()
        {
            ChromeOptions options = new ChromeOptions();
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            options.AddArguments("user-data-dir=" + appdataPath + "\\Google\\Chrome\\User Data\\Default"); // path to local Chrome Data
            options.AddArguments("--start-maximized");
            try
            {
                await CloseChrome_driver();
                _driver = new ChromeDriver(options);
            }
            catch { }
        }

        public async Task OpenWebsite(string site)
        {
            try
            {
                if (_driver != null && !string.IsNullOrEmpty(site))
                    _driver.Url = site;
                else if (_driver == null)
                {
                    await StartBrowser(); // calls StartBrowser function

                    Thread.Sleep(1000);
                    _driver.Url = site; // tells the _driver to go the items url
                }
            }
            catch { }
        }

        public async Task CloseChrome_driver()
        {
            try
            {
                Process[] processlist = Process.GetProcesses();

                foreach (Process process in processlist)
                {
                    if (process.ProcessName.Contains("chrome")) // checks if the process name contains word chrome
                        process.Kill(); // kills all processes with chrome in their name
                }
            }
            catch { }
        }

        public async Task<bool> IsElementPresent(By by)
        {
            try
            {
                if (_driver != null)
                {
                    _driver.FindElement(by); // checks if element is present on website
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
            if (_driver != null)
            {
                _driver.Close(); // closes chrome
                _driver.Quit();  // closes chrome_driver
                await CloseChrome_driver(); // makes sure all chrome_drivers are closed
            }
        }

        public async Task<IWebElement> GetElementUsingXPath(string xpath)
        {
            bool elementIsPresent = await IsElementPresent(By.XPath(xpath));

            if (elementIsPresent.Equals(true) && _driver != null) // checks if the element is present on the wesbite
            {
                return _driver.FindElement(By.XPath(xpath));
            }
            else
            {
                return null;
            }
        }

        public async Task OpenPS5Screen()
        {
            await OpenWebsite(_xpath.product_url); // link to the product you want to check and buy
        }

        public async Task<bool> BuyProductIfAvailable(string product, int delay)
        {
            try
            {
                bool run = true;

                if (run.Equals(true))
                {
                    run = await ClickProductTab(product); // Remove these 2 line if you are checking for another Product

                    if (run.Equals(true))
                    {
                        Thread.Sleep(delay);
                        run = await AddToCart(delay);

                        if (run.Equals(true))
                        {
                            Thread.Sleep(delay);
                            return await Checkout(delay);
                        }
                        MainWindow.AppWindow.UpdateInfoBox("Currently out of stock!", System.Windows.Media.Brushes.Red);
                    }
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        private async Task<bool> ClickProductTab(string product)
        {
            IWebElement productTab = null;

            if (product == "PS5_Digital")
            {
                productTab = await GetElementUsingXPath(_xpath.ps5_digital_edition_ger); // Gets XPath of PS5 Digital Version tab
            }
            else if (product == "PS5_Disc")
            {
                productTab = await GetElementUsingXPath(_xpath.ps5_disc_edition_ger); // Gets XPath of PS5 Disc Version tab
            }

            if (productTab != null)
            {
                productTab.Click(); // Clicks on the Products Tab
                return true;
            }
            else
            {
                if (product == "PS5_Digital")
                {
                    productTab = await GetElementUsingXPath(_xpath.ps5_digital_edition_eng); // Gets XPath of PS5 Digital Version tab
                }
                else if (product == "PS5_Disc")
                {
                    productTab = await GetElementUsingXPath(_xpath.ps5_disc_edition_eng); // Gets XPath of PS5 Disc Version tab
                }

                if (productTab != null)
                {
                    productTab.Click(); // Clicks on the Products Tab
                    return true;
                }
            }

            return false;
        }

        private async Task<bool> AddToCart(int delay)
        {
            IWebElement addToCart = await GetElementUsingXPath(_xpath.add_to_cart);

            if (addToCart != null)
            {
                addToCart.Click();
                MainWindow.AppWindow.UpdateInfoBox("Update: In Stock!", System.Windows.Media.Brushes.GreenYellow);
                Thread.Sleep(delay);

                IWebElement noCoverage = await GetElementUsingXPath( // starting point if you to check for a different product
                    "//button[@id='siNoCoverage-announce']");

                if (noCoverage != null) // this is for some products that offer coverage
                {
                    try
                    {
                        noCoverage.Click();
                        Thread.Sleep(delay);
                    }
                    catch
                    {
                        MainWindow.AppWindow.UpdateInfoBox("Error: Check Amazon!", System.Windows.Media.Brushes.Red);
                        Thread.Sleep(delay);
                    }
                }

                await OpenWebsite(_xpath.cart_url); // Link to your Cart
                return true;
            }

            return false;
        }

        private async Task<bool> Checkout(int delay)
        {
            IWebElement proceedToCheckout = await GetElementUsingXPath(_xpath.checkout_order);

            if (proceedToCheckout != null)
            {
                proceedToCheckout.Click();
                MainWindow.AppWindow.UpdateInfoBox("Update: Proceeding to Checkout", System.Windows.Media.Brushes.GreenYellow);
                IWebElement confirmOrder = await GetElementUsingXPath(_xpath.place_order); // confirm order button

                Thread.Sleep(delay);

                if (confirmOrder != null)
                {
                    confirmOrder.Click();
                    MainWindow.AppWindow.UpdateInfoBox("Update: Bought Product!", System.Windows.Media.Brushes.GreenYellow);
                    MessageBox.Show(
                        "PS5 has been bought or Amazon is asking for a Password! Anyway check out your Amazon tab in chrome!");
                    return false;
                }
            }
            else
            {
                await OpenPS5Screen();
            }
            return true;
        }

        public async Task ReloadTab()
        {
            _driver.Navigate().Refresh();
        }
    }
}
