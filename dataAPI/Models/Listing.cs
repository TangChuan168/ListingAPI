using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dataAPI.Models
{
    public class Listing
    {       
        //[Key]
        public Guid Guid { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public string ReType { get; set; }
        public string options { get; set; }
        public  propertyDetails propertyDetails { get; set; }

        public virtual ICollection<keyText> Texts { get; set; }
        public virtual ICollection<Contacts> Contactz { get; set; }
        public virtual ICollection<PicUrl> PicUrls { get; set; }

    }
}
