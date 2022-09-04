using dataAPI.Contracts;
using dataAPI.Models;
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

namespace dataAPI.Services
{
    public class ListingServices
    {
        ChromeDriver driver;
        /*
        private readonly IRepository<Listing> _ListingRepo;
        private readonly IRepository<keyText> _KeyTextRepo;
        private readonly IRepository<Contacts> _ContactsRepo;
        private readonly IRepository<PicUrl> _PicUrlRepo;
        */
        public ListingServices(
         //IRepository<Listing> ListDB,
         //IRepository<keyText> KeyTextDB,
         //IRepository<Contacts> ContactsDB,
         //IRepository<PicUrl> PicUrlDB

        )
        {
           // _ListingRepo = ListDB;
           // _KeyTextRepo = KeyTextDB;
           // _ContactsRepo = ContactsDB;
           // _PicUrlRepo = PicUrlDB;
            driver = new ChromeDriver("C:\\coding2022\\BACK-endAPI\\ListingAPI\\dataAPI");

        }
     

        public void downLoadListing()
        {
            var residentialSale = "https://www.jameslaw.co.nz/residential";
            var residentialRent = "https://www.jameslaw.co.nz/residential-rent";
            var commercialSale = "https://www.jameslaw.co.nz/commercial-sale";
            var commercialLease = "https://www.jameslaw.co.nz/commercial-lease";

            var ResidentialSale = this.getUrls(residentialSale,"residencialSale");
            var ResidentialRent = this.getUrls(residentialRent,"residencialRent");
            var CommercialSale = this.getUrls(commercialSale,"commercialSale");
            var CommercialRent = this.getUrls(commercialLease,"commercialRent");
            Console.WriteLine("finished@@@");
        }

        public List<urlData> getUrls(string url, string types)
        {
            driver.Navigate().GoToUrl(url);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='listing_grid']/div/div/div")));
            int Pages = GetPageNumber(driver,types);
            var res = getListing(Pages,driver);
            return res;
        }

        //this Function is using for calculate the total pages existing 
        public int GetPageNumber(ChromeDriver driver,string option)
        {
            var ReSale = "/html/body/app-root/main/app-residential/section[2]/div/div[1]/div/div/div/span";
            var ReRent = "/html/body/app-root/main/app-residential-rent/section[2]/div/div[1]/div/div/div/span";
            var ComSale = "/html/body/app-root/main/app-commercial-sale/section[2]/div/div[1]/div/div/div/span";
            var ComRent = "/html/body/app-root/main/app-commercial-lease/section[2]/div/div[1]/div/div/div/span";

            if (option == "residencialSale") return pages(driver, ReSale);
            if(option == "residencialRent") return pages(driver, ReRent);
            if (option == "commercialSale") return pages(driver, ComSale);
            if (option == "commercialRent") return pages(driver, ComRent);
            else return 0;
        }

        public int pages(ChromeDriver driver, string url)
        {
            var total = driver.FindElement(By.XPath(url));
            var totalText = total.Text.Split(' ');
            var Numb = Int32.Parse(totalText[0]);
            if (Numb % 9 == 0)
            {
                return Numb / 9;
            }
            else
            {
                return (Numb / 9) + 1;
            }
        }

        public List<urlData> getListing(int pages, ChromeDriver driver)
        {
            List<urlData> Urls = new List<urlData>(); // use to store listings for each pages

            for (int k = 1; k <= pages; k++)
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
                if (k < pages)
                {
                    
                    var NextPage = driver.FindElement(By.CssSelector("li.page-item a[aria-label='Next']"));
                    driver.ExecuteScript("arguments[0].click();", NextPage);
                };
            }
            return Urls;
        }
    }
}
