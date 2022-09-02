using dataAPI.Contracts;
using dataAPI.Models;
using System;
using System.Collections.Generic;
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

        }

        public List<Listing> getNewListings()
        {


            return null;
        }

    }
}
