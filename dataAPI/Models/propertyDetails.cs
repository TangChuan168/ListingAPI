using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dataAPI.Models
{
    public class propertyDetails
    {
        [Key]
        public Guid PPDguid { get; set; }
        public string heading { get; set; }
        public string AskPrice { get; set; }
        public string Address { get; set; }
        public string descriptions { get; set; }
        public List<keyText> KeyFeatures { get; set; }
        public List<PicUrl> pictureUrl { get; set; }
        public List<Contacts> contacts { get; set; }
        public DateTime updateTime { get; set; }

        //room detials
        public int bedRoom { get; set; }
        public int office { get; set; }
        public int LivingRoom { get; set; }
        public int shower { get; set; }
        public int Carpark { get; set; }
        public int toilet { get; set; }

        // Further Details
        public string PropertyType { get; set; }
        public string PropertyUse { get; set; }
        public string SaleMethod { get; set; }
        public string OpenHomeSessions { get; set; }
        public string FloorArea { get; set; }
        public string Reference { get; set; }


    };

    public class keyText
    {
        [Key]
        public Guid Textguid { get; set; }
        public string kText {get;set;}
        public Guid PPDguid { get; set; }
        public propertyDetails propertyDetails { get; set; }
    }

    public class Contacts
    {
        [Key]
        public Guid Contaxtsguid { get; set; }

        public string Name { get; set; }
        public string phone { get; set; }
        public Guid PPDguid { get; set; }
        public propertyDetails propertyDetails { get; set; }
    }
    public class PicUrl
    {
        [Key]
        public Guid Picguid { get; set; }
        public string PictureUrl { get; set; }
        public Guid PPDguid { get; set; }
        public propertyDetails propertyDetails { get; set; }
    }
}
