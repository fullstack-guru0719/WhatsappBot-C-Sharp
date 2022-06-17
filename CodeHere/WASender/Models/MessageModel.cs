using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaAutoReplyBot.Models
{
    public class MessageModel
    {
        public string LongMessage { get; set; }

        public List<string> Files { get; set; }

        public bool IsEditMode { get; set; }
    }
}
