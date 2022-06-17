using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaAutoReplyBot.enums;

namespace WaAutoReplyBot.Models
{
    public class RuleTransactionModel
    {

        public string userInput { get; set; }
        public OperatorsEnum operatorsEnum { get; set; }
        public List<MessageModel> messages { get; set; }
        public bool IsActive { get; set; }
        public bool IsSaved { get; set; }
        public bool IsEditMode { get; set; }
        public int MyProperty { get; set; }
        public bool IsFallBack { get; set; }
    }
}
