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
            options.AddArguments("user-data-dir="+appdataPath+ "\\Google\\Chrome\\User Data\\Default");
            options.AddArguments("--start-maximized");
            try
            {
                driver = new ChromeDriver("C:\\WebDriver\\", options);
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
                    await StartBrowser();

                    Thread.Sleep(1000);
                    driver.Url = site;
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
                    driver.FindElement(by);
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
                driver.Close();
                driver.Quit();
            }
            Environment.Exit(Environment.ExitCode);
        }

        public async Task CloseTab()
        {
            if (driver != null)
            {
                var tabs = driver.WindowHandles;

                if (tabs.Count > 1)
                {
                    driver.SwitchTo().Window(tabs[0]);
                    driver.Close();
                }
            }
        }
        public IWebElement GetElementUsingXPath(string xpath)
        {
            bool elementIsPresent = IsElementPresent(By.XPath(xpath));

            if (elementIsPresent.Equals(true) && driver != null)
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
                "https://www.amazon.de/gp/product/B08H98GVK8?pf_rd_r=8WGMBRVYKV6137VVWK67&pf_rd_p=4ba7735c-ede3-4212-a657-844b25584948&pd_rd_r=5a951b88-da02-4e0f-bd04-95c3f37726cd&pd_rd_w=Yk4dL&pd_rd_wg=WdijX&ref_=pd_gw_unk&th=1");
        }

        public bool CheckIfProductIsAvailable()
        {
            IWebElement productTab = GetElementUsingXPath(
                "/html/body/div[2]/div[2]/div[5]/div[5]/div[4]/div[30]/div[1]/div/form/div/ul/li[3]");

            if (productTab != null)
            {
                productTab.Click();
                Thread.Sleep(DelayInSec(3));

                IWebElement addToCart = GetElementUsingXPath(
                    "//input[@id='add-to-cart-button']");

                if (addToCart != null)
                {
                    addToCart.Click();
                    
                    Thread.Sleep(DelayInSec(3));

                    IWebElement buy = GetElementUsingXPath(
                        "//input[@aria-labelledby='attach-sidesheet-checkout-button-announce']");

                    if (buy != null)
                    {
                        buy.Click();

                        IWebElement confirmOrder = GetElementUsingXPath(
                            "//input[@name='placeYourOrder1']");

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
