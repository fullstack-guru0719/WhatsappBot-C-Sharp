using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASender.Models
{
    public class ContactModel : Base
    {
        public string number { get; set; }
        public string name { get; set; }
        public SendStatusModel sendStatusModel { get; set; }

        public List<ParameterModel> parameterModelList { get; set; }
        public bool logged { get; set; }
    }
}
