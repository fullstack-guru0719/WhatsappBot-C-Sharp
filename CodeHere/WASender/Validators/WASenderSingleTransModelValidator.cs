using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASender.Models;
using FluentValidation;

namespace WASender.Validators
{
    public class WASenderSingleTransModelValidator : AbstractValidator<WASenderSingleTransModel>
    {
        public WASenderSingleTransModelValidator()
        {
            RuleFor(x => x.contactList.Count).GreaterThan(0).WithMessage(Strings.PleaseaddatleastoneContactinlist);
            RuleFor(x => x.messages.Where(a=>a !=null).Count() ).GreaterThan(0).WithMessage(Strings.PleaseEnterAmessage);
        }
    }
}
