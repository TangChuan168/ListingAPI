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
using System.Threading.Tasks;

namespace dataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Listingdownload : ControllerBase
    {
       /* private readonly ListingServices _ListingService;
        Listingdownload(
            ListingServices sv
        )
        {
            _ListingService = sv;
        }
       
        [HttpGet]
        public void GetUrl()
        {
            var res = _ListingService.downLoadListing();
        }
        */
        [HttpGet]
        public ReadOnlyCollection<IWebElement> test()
        {
            var driver = new ChromeDriver("C:\\coding2022\\BACK-endAPI\\ListingAPI\\dataAPI");
            var residentialSale = "https://www.jameslaw.co.nz/residential";
            driver.Navigate().GoToUrl(residentialSale);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 4));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='listing_grid']/div/div/div")));
            
            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath("//*[@id='listing_grid']/div/div/div"));

            //find if next page is existing?
            var IsNextPage = driver.FindElement(By.XPath("//*[@id='listing_grid']/div/pagination-template/nav/ul/li[3]/a"));
            IsNextPage.Click();
            return elements;
        }
    }
}
