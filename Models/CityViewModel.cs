using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaliSahaProject.Models
{
    public class CityViewModel
    {
        public IEnumerable<Cities> City { get; set; }
        public IEnumerable<Counties> County { get; set; }
    }
}