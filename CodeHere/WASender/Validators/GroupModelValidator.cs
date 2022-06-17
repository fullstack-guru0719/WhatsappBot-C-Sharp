using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASender.Models;
using FluentValidation;


namespace WASender.Validators
{
    public class GroupModelValidator : AbstractValidator<GroupModel>
    {
        public GroupModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(Strings.ContactNumberShouldNotbeEmpty); 
        }
    }
}
