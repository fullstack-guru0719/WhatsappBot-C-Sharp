using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASender.enums
{
    public enum SendStatusEnum
    {
        Failed,
        Success,
        ContactNotFound,
        GroupAdminOnly,
        GroupNotFound,
        NotValidLink,
        GroupFull,
        Available,
        NotAvailable
    }
}
