using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASender.Models;
using FluentValidation;

namespace WASender.Validators
{
    public class ContactModelValidator  :AbstractValidator<ContactModel>
    {
        public ContactModelValidator()
        {
            RuleFor(x => x.number).NotEmpty().WithMessage(Strings.ContactNumberShouldNotbeEmpty);
            //RuleFor(x => x.number).MinimumLength(9).WithMessage(Strings.PleaseCheckMobileNumberyouhaveadded);
        }
    }
}
