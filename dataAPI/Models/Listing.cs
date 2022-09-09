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
        public string tagNo { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public string ReType { get; set; }
        public string options { get; set; }
        public  propertyDetails propertyDetails { get; set; }

    }
}
