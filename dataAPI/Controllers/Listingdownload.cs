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

        [HttpGet("getResidentialSale")]
        public void getResidentialSale()
        {
            _ListingService.ResidentialListingSale();
        }

        [HttpGet("getResidentialRent")]
        public void getResidentialRent()
        {
            _ListingService.ResidentialListringRent();
        }

        [HttpGet("getCommercialSale")]
        public void getCommercialSale()
        {
            _ListingService.CommercialListringSale();
        }

        [HttpGet("getCommercialLease")]
        public void getCommercialLease()
        {
            _ListingService.CommercialListringLease();
        }


        //this API is for testing purpose only
        [HttpGet("test")]
        public List<urlData> test()
        {
            var driver = new ChromeDriver("C:\\coding2022\\BACK-endAPI\\ListingAPI\\dataAPI");
            var residentialSale = "https://www.jameslaw.co.nz/residential";
            //var commercialLease = "https://www.jameslaw.co.nz/commercial-lease";
            driver.Navigate().GoToUrl(residentialSale);
            //WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 4));
            //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='listing_grid']/div/div/div")));
            List<urlData> Urls = new List<urlData>();

            for (int k = 1; k <= 2; k++)
            {
                try
                {
                    Thread.Sleep(2000);
                    ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector("div.button-property-icon a.btn"));
                    foreach (var j in elements)
                    {
                        var tagNoSet = j.GetAttribute("href").Split('/');
                        var tagNo = Int32.Parse(tagNoSet[4]);
                        var data = new urlData { id = tagNo, url = j.GetAttribute("href") };
                        Urls.Add(data);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception");
                    Console.WriteLine(e);
                }

                //find next page and if this the page is the last page stop find next page and click it
                if (k < 2)
                {
                    var NextPage = driver.FindElement(By.CssSelector("li.page-item a[aria-label='Next']"));
                    driver.ExecuteScript("arguments[0].click();", NextPage);
                };
            }
            return Urls;
        }
        [HttpGet("detialsTesting")] 
        public void DetailTesting()
        {
            var driver = new ChromeDriver("C:\\coding2022\\BACK-endAPI\\ListingAPI\\dataAPI");
            var residentialSale = "https://www.jameslaw.co.nz/property/6400";
            driver.Navigate().GoToUrl(residentialSale);
            Thread.Sleep(3000);
            var Names = driver.FindElements(By.XPath("//*[@id='property']/div[16]/div[2]/div/div[2]/div[1]"));
            var phones = driver.FindElements(By.XPath("//*[@id='property']/div[16]/div[2]/div/div[2]/a"));
            var Contactz = new List<Contacts>();
            foreach (var name in Names)
            {
                Contactz.Add(new Contacts { Contaxtsguid = Guid.NewGuid(), Name = name.Text }); ;
            }
            List<string> phonez = new List<string>();
            foreach (var ph in phones)
            {
                phonez.Add(ph.Text);
            }
            var count = Contactz.Count();

            for (var i = 0; i < count; i++)
            {
                Contactz[i].phone = phonez[i];
            }


            //pictures
            var PicsData = driver.FindElements(By.XPath("//*[@id='property']/div[9]/a"));
            var Pictures = new List<PicUrl>();

            foreach (var pic in PicsData)
            {
                Pictures.Add(new PicUrl {Picguid = Guid.NewGuid(), PictureUrl = pic.GetAttribute("href") });
            }

            Thread.Sleep(3000);

        }
    }
}
