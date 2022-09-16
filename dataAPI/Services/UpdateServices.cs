using dataAPI.Contracts;
using dataAPI.Models;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dataAPI.Services
{
    public class UpdateServices : ListingServices
    {
        private readonly IEnumerable<Listing> zero;
        private readonly IEnumerable<Listing> one;
        private readonly IEnumerable<Listing> two;
        private readonly IEnumerable<Listing> three;
        private readonly IEnumerable<Listing> four;
        private readonly IEnumerable<Listing> five;
        private readonly IEnumerable<Listing> six;
        private readonly IEnumerable<Listing> seven;
        private readonly IEnumerable<Listing> eight;
        private readonly IEnumerable<Listing> nine;

        public UpdateServices(
         IRepository<Listing> ListDB,
         IRepository<keyText> KeyTextDB,
         IRepository<Contacts> ContactsDB,
         IRepository<PicUrl> PicUrlDB,
         IRepository<propertyDetails> propertyDetails) : base(ListDB, KeyTextDB, ContactsDB, PicUrlDB, propertyDetails) 
        {
            one = _ListingRepo.getAll(x => x.LastDigit == 1);
            two = _ListingRepo.getAll(x => x.LastDigit == 2);
            three = _ListingRepo.getAll(x => x.LastDigit == 3);
            four = _ListingRepo.getAll(x => x.LastDigit == 4);
            five = _ListingRepo.getAll(x => x.LastDigit == 5);
            six = _ListingRepo.getAll(x => x.LastDigit == 6);
            seven = _ListingRepo.getAll(x => x.LastDigit == 7);
            eight = _ListingRepo.getAll(x => x.LastDigit == 8);
            nine = _ListingRepo.getAll(x => x.LastDigit == 9);
            zero = _ListingRepo.getAll(x => x.LastDigit == 0);

        }

        public void ResidentialSaleUpdate()
        {
            var residentialSale = "https://www.jameslaw.co.nz/residential";
            var Urls = base.getUrls(residentialSale, "residencialSale");
            var updateUrls = this.urlsNeedtoUpdate(Urls);
        }

        public List<urlData> urlsNeedtoUpdate(List<urlData> newData)
        {
            var updates = new List<urlData>();
            foreach (var k in newData)
            {
                var IsData = Isrequired(k);
                if (IsData)
                {
                    updates.Add(k);
                }
            }
            return updates;
        }

        //# find out if the data needs to update
        public bool Isrequired(urlData sigleNewdata)
        {
            //#1 find last digit of data
           switch (sigleNewdata.lastDigit)
            {
                case 0:
                    return IsExist(sigleNewdata, zero);
                case 1:
                    return IsExist(sigleNewdata, one);
                case 2:
                    return IsExist(sigleNewdata, two);
                case 3:
                    return IsExist(sigleNewdata, three);
                case 4:
                    return IsExist(sigleNewdata, four);
                case 5:
                    return IsExist(sigleNewdata, five);
                case 6:
                    return IsExist(sigleNewdata, six);
                case 7:
                    return IsExist(sigleNewdata, seven);
                case 8:
                    return IsExist(sigleNewdata, eight);
                case 9:
                    return IsExist(sigleNewdata, nine);
                default:
                    return false;
            }           
        }

        //# compare data with dadabase see if its existing
        public bool IsExist(urlData sigleNewdata, IEnumerable<Listing> group)
        {
            bool flag = false;
            foreach(var j in group)
            {
                if (j.TagNum == sigleNewdata.tagId)
                {
                    break;
                }
                else
                {
                    flag = true;
                }
            };
            if (flag) { return true; } else
            {
                return false;
            }         
        }
    }
}
