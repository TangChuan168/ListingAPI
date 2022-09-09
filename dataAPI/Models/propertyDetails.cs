using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dataAPI.Models
{
    public class propertyDetails
    {
       
        public Guid PPDguid { get; set; }
        public string heading { get; set; }
        public string AskPrice { get; set; }
        public string Address { get; set; }
        public string Descriptions { get; set; }
        public DateTime UpdateTime { get; set; }

        //room detials
        public string BedRoom { get; set; }
        public string Study { get; set; }
        public string Couch { get; set; }
        public string Bath { get; set; }
        public string Garage { get; set; }
        public string Toilet { get; set; }

        // Further Details
        public string PropertyType { get; set; }
        public string PropertyUse { get; set; }
        public string SaleMethod { get; set; }
        public string OpenHomeSessions { get; set; }
        public string FloorArea { get; set; }
        public string Reference { get; set; }

        //reference to Listing
        public Guid ListingGuid { get; set; }
        public Listing Listing { get; set; }

        public virtual ICollection<keyText> Texts { get; set; }
        public virtual ICollection<Contacts> Contactz { get; set; }
        public virtual ICollection<PicUrl> PicUrls { get; set; }
    };

    public class keyText
    {
        //[Key]
        public Guid Textguid { get; set; }
        public string kText {get;set;}
        
        public propertyDetails currentDetails { get; set; }
       

    }

    public class Contacts
    {
        //[Key]
        public Guid Contaxtsguid { get; set; }

        public string Name { get; set; }
        public string phone { get; set; }

        public propertyDetails currentDetails { get; set; }
    }
    public class PicUrl
    {
        //[Key]
        public Guid Picguid { get; set; }
        public string PictureUrl { get; set; }
        public  propertyDetails propertyDetails { get; set; }
    }
}
