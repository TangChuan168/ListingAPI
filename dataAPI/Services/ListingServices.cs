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
        private readonly IRepository<Listing> _ListingRepo;
        private readonly IRepository<keyText> _KeyTextRepo;
        private readonly IRepository<Contacts> _ContactsRepo;
        private readonly IRepository<PicUrl> _PicUrlRepo;

        ChromeDriver driver;

        public ListingServices(
         IRepository<Listing> ListDB,
         IRepository<keyText> KeyTextDB,
         IRepository<Contacts> ContactsDB,
         IRepository<PicUrl> PicUrlDB

        )
        {
            _ListingRepo = ListDB;
            _KeyTextRepo = KeyTextDB;
            _ContactsRepo = ContactsDB;
            _PicUrlRepo = PicUrlDB;
            driver = new ChromeDriver();

        }



        public ReadOnlyCollection<IWebElement> downLoadListing()
        {
            var residentialSale = "https://www.jameslaw.co.nz/residential";
            var residentialRent = "https://www.jameslaw.co.nz/residential-rent";

            var commercialSale = "https://www.jameslaw.co.nz/commercial-sale";
            var commercialLease = "https://www.jameslaw.co.nz/commercial-lease";

           var Rsale = this.residentialSale(residentialSale);
            return Rsale;

        }

        public ReadOnlyCollection<IWebElement> residentialSale(string url)
        {
            driver.Navigate().GoToUrl(url);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 3));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=listing_grid]/div/div/div")));
            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath("//*[@id='listing_grid']/div/div/div"));
            return elements;
        }

    }
}
