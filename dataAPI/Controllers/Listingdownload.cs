using dataAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Listingdownload : ControllerBase
    {
        
        private readonly ListingServices _ListingService;
    public Listingdownload(
            ListingServices sv
        )
        {
            _ListingService = sv;
        }
       
        [HttpGet("geturl")]
        public void GetUrl()
        {
            _ListingService.downLoadListing();
        }
       
        [HttpGet("test")]
        public ReadOnlyCollection<IWebElement> test()
        {
            var driver = new ChromeDriver("C:\\coding2022\\BACK-endAPI\\ListingAPI\\dataAPI");
            var residentialSale = "https://www.jameslaw.co.nz/residential";
            driver.Navigate().GoToUrl(residentialSale);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 4));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='listing_grid']/div/div/div")));

            List<string> Urls = new List<string>();
            ReadOnlyCollection<IWebElement> elements = driver.
            FindElements(By.CssSelector("div.button-property-icon a.btn"));
            foreach (var j in elements)
            {
                Urls.Add(j.GetAttribute("href"));
            }

            //find if next page is existing?
            ReadOnlyCollection<IWebElement> element2 = driver.FindElements(By.XPath("//*[@id='listing_grid']/div/pagination-template/nav/ul/li[4]/a"));
            var IsNextPage3 = driver.FindElement(By.CssSelector("a[aria-label='Next']"));
            
            driver.ExecuteScript("arguments[0].click();", IsNextPage3);
            Thread.Sleep(3000);
            return elements;
        }
       
    }
}
