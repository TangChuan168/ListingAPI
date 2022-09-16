using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dataAPI.Models
{
    public class urlData
    {
        public int id { get; set; }
        public string url { get; set; }
        public int tagId { get; set; }
        public int lastDigit { get; set; }
    }
}
