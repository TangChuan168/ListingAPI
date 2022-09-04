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

            var ResidentialSale = this.residentialSale(residentialSale);
            var ResidentialRent = this.residentialRent(residentialRent);
            var CommercialSale = this.commercialSale(commercialSale);
            var CommercialRent = this.commercialRent(commercialLease);

        }

        public List<string> residentialSale(string url)
        {
            driver.Navigate().GoToUrl(url);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='listing_grid']/div/div/div")));
            int Pages = GetPageNumber(driver, "residencialSale");
            var res = getResidentialSale(Pages,driver);
            return res;
        }

        public List<string> residentialRent(string url)
        {
            driver.Navigate().GoToUrl(url);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='listing_grid']/div/div/div")));
            int Pages = GetPageNumber(driver, "residencialRent");
            var res = getResidentialRent(Pages, driver);
            return res;
        }

        public List<string> commercialSale(string url)
        {
            driver.Navigate().GoToUrl(url);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='listing_grid']/div/div/div")));
            int Pages = GetPageNumber(driver, "commercialSale");
            var res = getcommercialSale(Pages, driver);
            return res;
        }

        public List<string> commercialRent(string url)
        {
            driver.Navigate().GoToUrl(url);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='listing_grid']/div/div/div")));
            int Pages = GetPageNumber(driver, "commercialRent");
            var res = getcommercialRent(Pages, driver);
            return res;
        }


        //this Function is use for calculate how many total pages exist 
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

        //loop all pages and grab all url
        public List<string> getResidentialSale(int pages, ChromeDriver driver)
        {
            List<string> Urls = new List<string>(); // use to store listings for each pages

            for (int k = 1; k <= pages; k++)
                {
                 ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector("div.button-property-icon a.btn"));          
                 foreach(var j in elements)
                    {
                        Urls.Add(j.GetAttribute("href"));
                    }

                //find next page and if this the page is the last page stop find next page and click it
                if (k < pages) {
                    var NextPage = driver.FindElement(By.CssSelector("a[aria-label='Next']"));
                    driver.ExecuteScript("arguments[0].click();", NextPage);
                }
            }
            return Urls;
        }

        public List<string> getResidentialRent(int pages, ChromeDriver driver)
        {
            List<string> Urls = new List<string>(); // use to store listings for each pages

            for (int k = 1; k <= pages; k++)
            {
                ReadOnlyCollection<IWebElement> elements = driver
                .FindElements(By.CssSelector("div.button-property-icon a.btn"));
                foreach (var j in elements)
                {
                    Urls.Add(j.GetAttribute("href"));
                }

                //
                if (k < pages)
                {
                    var NextPage = driver.FindElement(By.CssSelector("a[aria-label='Next']"));
                    driver.ExecuteScript("arguments[0].click();", NextPage);
                }
            }
            return Urls;
        }

        public List<string> getcommercialSale(int pages, ChromeDriver driver)
        {
            List<string> Urls = new List<string>(); // use to store listings for each pages

            for (int k = 1; k <= pages; k++)
            {
                ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector("div.button-property-icon a.btn"));
                foreach (var j in elements)
                {
                    Urls.Add(j.GetAttribute("href"));
                }

                //find next page and if this the page is the last page stop find next page and click it
                if (k < pages)
                {
                    var NextPage = driver.FindElement(By.CssSelector("a[aria-label='Next']"));
                    driver.ExecuteScript("arguments[0].click();", NextPage);
                }
            }
            return Urls;
        }

        public List<string> getcommercialRent(int pages, ChromeDriver driver)
        {
            List<string> Urls = new List<string>(); // use to store listings for each pages

            for (int k = 1; k <= pages; k++)
            {
                ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector("div.button-property-icon a.btn"));
                foreach (var j in elements)
                {
                    try
                    {
                        var data = j.GetAttribute("href");
                        //var url = j.GetAttribute("href") != null ? j.GetAttribute("href") : "no DATA";
                        Urls.Add(data);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("no more node!!!!!!!!!!!!!!!!!!!!");
                        Console.WriteLine(e);
                    }
                }

                //find next page and if this the page is the last page stop find next page and click it
                if (k < pages)
                {
                    var NextPage = driver.FindElement(By.CssSelector("a[aria-label='Next']"));
                    driver.ExecuteScript("arguments[0].click();", NextPage);
                }
            }
            return Urls;
        }



    }
}
