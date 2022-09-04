using dataAPI.Models;
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
        public List<urlData> test()
        {
            var driver = new ChromeDriver("C:\\coding2022\\BACK-endAPI\\ListingAPI\\dataAPI");
            //var residentialSale = "https://www.jameslaw.co.nz/residential";
            var commercialLease = "https://www.jameslaw.co.nz/commercial-lease";
            driver.Navigate().GoToUrl(commercialLease);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 4));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='listing_grid']/div/div/div")));
            List<urlData> Urls = new List<urlData>();

            for (int k = 1; k <= 56; k++)
            {
                try
                {
                    Thread.Sleep(3000);
                    ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector("div.button-property-icon a.btn"));                  
                    foreach (var j in elements)
                    {
                        var data = new urlData { id = k, url = j.GetAttribute("href") };
                        Urls.Add(data);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception");
                    Console.WriteLine(e);
                }

                //find next page and if this the page is the last page stop find next page and click it
                if (k < 56)
                {
                    var NextPage = driver.FindElement(By.CssSelector("li.page-item a[aria-label='Next']"));
                    driver.ExecuteScript("arguments[0].click();", NextPage);
                };
            }
            return Urls;
        }
       
    }
}
