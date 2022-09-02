using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dataAPI.Models
{
    public class Listing
    {
        [Key]
        public Guid Guid { get; set; }
        public string tagNo { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public realEstateTypes ReType { get; set; }
        public businessOptions options { get; set; }
        public propertyDetails ppDetails { get; set; }

    }
    public enum realEstateTypes
    {
        Residential,
        Commercial
    }
    public enum businessOptions
    {
        Rent,
        Sale
    }
}
