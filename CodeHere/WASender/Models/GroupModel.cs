using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASender.Models
{
    public class GroupModel : Base
    {
        public string Name { get; set; }

        public bool logged { get; set; }
        public SendStatusModel sendStatusModel { get; set; }
    }
}
