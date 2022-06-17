using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASender.Models;
using FluentValidation;
namespace WASender.Validators
{
    public class WASenderGroupTransModelValidator : AbstractValidator<WASenderGroupTransModel>
    {
        public WASenderGroupTransModelValidator(bool IsGrouJoiner=false)
        {
            RuleFor(x => x.groupList).NotNull().WithMessage(Strings.PleaseEnterAmessage); ;
            RuleFor(x => x.groupList.Count).GreaterThan(0).WithMessage(Strings.PleaseaddatleastoneGroupinlist);
            if (IsGrouJoiner != true)
            {
                RuleFor(x => x.messages.Where(a => a != null).Count()).GreaterThan(0).WithMessage(Strings.PleaseEnterAmessage);
            }
        }
    }
}
