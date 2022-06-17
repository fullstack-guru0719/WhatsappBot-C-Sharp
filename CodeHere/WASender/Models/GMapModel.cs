using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASender.Models
{
    public class GMapModel
    {
        public string Name { get; set; }
        public string reviewCount { get; set; }

        public string rating { get; set; }

        public string category { get; set; }
        public string address { get; set; }

        public string website { get; set; }

        public string PlusCode { get; set; }

        public string mobilenumber { get; set; }

        public bool Logged { get; set; }
    }
}

