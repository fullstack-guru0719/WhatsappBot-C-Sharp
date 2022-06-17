using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASender.Models
{
    public class SingleSettingModel :Base
    {
        public int delayAfterMessages { get; set; }
        public int delayAfterMessagesFrom { get; set; }
        public int delayAfterMessagesTo { get; set; }

        public int delayAfterEveryMessageFrom { get; set; }
        public int delayAfterEveryMessageTo { get; set; }

        public int minGroupMembers { get; set; }

        public int typingSpeedFrom { get; set; }
        public int typingSpeedTo { get; set; }
    }
}
