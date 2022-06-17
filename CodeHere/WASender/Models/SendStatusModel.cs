using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASender.enums;

namespace WASender.Models
{
    public class SendStatusModel
    {
        public string dateTime { get; set; }
        public SendStatusEnum sendStatusEnum { get; set; }
        public bool isDone { get; set; }
        public bool IsSuccess { get; set; }
    }
}
