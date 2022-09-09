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
        
        private readonly IRepository<Listing> _ListingRepo;
        private readonly IRepository<keyText> _KeyTextRepo;
        private readonly IRepository<Contacts> _ContactsRepo;
        private readonly IRepository<PicUrl> _PicUrlRepo;
        
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
            driver = new ChromeDriver("C:\\coding2022\\BACK-endAPI\\ListingAPI\\dataAPI");

        }
     

        public List<Listing> downLoadListing()
        {
            var residentialSale = "https://www.jameslaw.co.nz/residential";
            var residentialRent = "https://www.jameslaw.co.nz/residential-rent";
            var commercialSale = "https://www.jameslaw.co.nz/commercial-sale";
            var commercialLease = "https://www.jameslaw.co.nz/commercial-lease";

            var ResidentialSale = this.getUrls(residentialSale,"residencialSale");
            //var ResidentialRent = this.getUrls(residentialRent,"residencialRent");
            //var CommercialSale = this.getUrls(commercialSale,"commercialSale");
            //var CommercialRent = this.getUrls(commercialLease,"commercialRent");

            var data= getListingDetials(driver, ResidentialSale);
            return data;
            
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

        //this function to identify un existing element and grab value
        public string valueFind(ChromeDriver driver, string xpath)
        {
            try
            {
                return driver.FindElement(By.XPath(xpath)).Text;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return "";
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

        public  List<Listing> getListingDetials(ChromeDriver driver,List<urlData> data)
        {
            var ListingDatas = new List<Listing>();
            
            foreach (var Listing in data)
                {

                    driver.Navigate().GoToUrl(Listing.url);
                    Thread.Sleep(3500);
                    //WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 4));
                    //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='property']/div[9]")));

                    var Heading = driver.FindElement(By.XPath("//*[@id='home']/div[1]/div[1]")).Text;
                    var AskPrice = driver.FindElement(By.XPath("//*[@id=\"home\"]/div[1]/div[2]/span[1]")).Text;
                    var Address = driver.FindElement(By.XPath("//*[@id=\"property\"]/div[1]/div/div")).Text;
                    var Descriptions = driver.FindElement(By.XPath("//*[@id=\"property\"]/div[2]/div[1]/div[2]")).Text;

                    string bedRoom ="";
                    string couch = "";
                    string bath = "";
                    string garage = "";
                    string study = "";
                    string toilet = "";

                    string PropertyType = "";
                    string PropertyUse = "";
                    string SaleMethod = "";
                    string OpenHomeSessions = "";
                    string FloorArea = "";
                    string Reference = "";

                    string bedroomXpath = "//*[@id=\"property\"]/div[2]/div[1]/div[1]/span[1]";
                    string couchXpath = "//*[@id=\"property\"]/div[2]/div[1]/div[1]/span[2]";
                    string bathXpath = "//*[@id=\"property\"]/div[2]/div[1]/div[1]/span[3]";
                    string garageXpath = "//*[@id='property]/div[2]/div[1]/div[1]/span[4]";
                    string studyXpath = "//*[@id='property']/div[2]/div[1]/div[1]/span[5]";
                    string toiletXpath = "//*[@id='property']/div[2]/div[1]/div[1]/span[6]";

                    string PropertyTypeXpath = "//*[@id='property']/div[4]/div[2]/table/tbody/tr[1]/td[2]";
                    string PropertyUseXpath = "//*[@id='property']/div[4]/div[2]/table/tbody/tr[2]/td[2]";
                    string SaleMethodXpath = "//*[@id='property']/div[4]/div[2]/table/tbody/tr[3]/td[2]";
                    string OpenHomeSessionsXpath = "//*[@id='property']/div[4]/div[2]/table/tbody/tr[4]/td[2]";
                    string FloorAreaXpath = "//*[@id='property']/div[4]/div[2]/table/tbody/tr[5]/td[2]";
                    string ReferenceXpath = "//*[@id='property']/div[4]/div[2]/table/tbody/tr[6]/td[2]";

                    bedRoom = this.valueFind(driver, bedroomXpath);
                    couch = this.valueFind(driver, couchXpath);
                    bath = this.valueFind(driver, bathXpath);
                    garage = this.valueFind(driver, garageXpath);
                    study = this.valueFind(driver, studyXpath);
                    toilet = this.valueFind(driver, toiletXpath);

                    PropertyType = this.valueFind(driver, PropertyTypeXpath);
                    PropertyUse = this.valueFind(driver, PropertyUseXpath);
                    SaleMethod = this.valueFind(driver, SaleMethodXpath);
                    OpenHomeSessions = this.valueFind(driver, OpenHomeSessionsXpath);
                    FloorArea = this.valueFind(driver, FloorAreaXpath);
                    Reference = this.valueFind(driver, ReferenceXpath);

                    //Key features
                    ReadOnlyCollection<IWebElement> Features = driver.FindElements(By.XPath("//*[@id='property']/div[3]/div[2]/ul/li"));
                    var KeyFeatures = new List<keyText>();
                    foreach (var element in Features)
                    {

                        KeyFeatures.Add(new keyText { kText = element.Text });

                    };
                    //further detials


                    //Contacts
                    var Names = driver.FindElements(By.XPath("//*[@id='property']/div[16]/div[2]/div/div[2]/div[1]"));
                    var phones = driver.FindElements(By.XPath("//*[@id='property']/div[16]/div[2]/div/div[2]/a"));
                    var Contactz = new List<Contacts>();
                    foreach (var name in Names)
                    {
                        Contactz.Add(new Contacts { Contaxtsguid = Guid.NewGuid(), Name = name.Text });
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
                        Pictures.Add(new PicUrl { Picguid = Guid.NewGuid(), PictureUrl = pic.GetAttribute("href") });
                    }

                    //add all values to Model

                    var PropertyDetials = new propertyDetails
                    {
                        PPDguid = Guid.NewGuid(),
                        heading = Heading,
                        AskPrice = AskPrice,
                        Address = Address,
                        Descriptions = Descriptions,

                        //PicUrls = Pictures,
                        //Contactz = Contactz,

                        UpdateTime = DateTime.Now,

                        BedRoom = bedRoom,
                        Couch = couch,
                        Bath = bath,
                        Garage = garage,
                        Study = study,
                        Toilet = toilet,

                        PropertyType = PropertyType,
                        PropertyUse = PropertyUse,
                        SaleMethod = SaleMethod,
                        OpenHomeSessions = OpenHomeSessions,
                        FloorArea = FloorArea,
                        Reference = Reference
                    };

                    //add key features to database
                    foreach(var ele in KeyFeatures)
                        {
                            var text1 = new keyText
                            {
                                Textguid = ele.Textguid,
                                kText = ele.kText,
                                currentDetails = PropertyDetials
                            };

                            _KeyTextRepo.Add(text1);
                        }

                    //add contact to database
                    foreach (var ele in Contactz)
                    {
                        var contact1 = new Contacts
                        {
                            Contaxtsguid = ele.Contaxtsguid,
                            Name = ele.Name,
                            phone = ele.phone,
                            currentDetails = PropertyDetials
                        };

                        _ContactsRepo.Add(contact1);
                    }

                    foreach (var ele in Pictures)
                    {
                        var pics = new PicUrl
                        {
                            Picguid = ele.Picguid,
                            PictureUrl = ele.PictureUrl,
                            propertyDetails = PropertyDetials
                        };

                        _PicUrlRepo.Add(pics);
                    }

                //add picUrl to database

                var Listing1 = new Listing
                    {
                        Guid = Guid.NewGuid(),
                        Url = Listing.url,
                        IsActive = true,
                        ReType = "Residential",
                        options = "Rent",
                        propertyDetails = PropertyDetials
                    };

                

                    _ListingRepo.Add(Listing1);

                    

                }

            
            return ListingDatas;
        }


        
    }
}
