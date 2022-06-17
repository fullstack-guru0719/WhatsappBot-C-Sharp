using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASender.enums;

namespace WASender.Models
{
    
    public class WASenderSingleTransModel :Base
    {
        public int Id { get; set; }

        public string CampaignName { get; set; }

        public int MessageSendingType { get; set; }

        public List<ContactModel> contactList { get; set; }

        public List<MesageModel> messages { get; set; }

        public SingleSettingModel settings { get; set; }

        public CampaignStatusEnum campaignStatusEnum { get; set; }

    }
}
