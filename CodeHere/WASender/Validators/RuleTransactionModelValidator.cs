using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaAutoReplyBot.Models;
using WASender;

namespace WaAutoReplyBot.Validators
{
    public class RuleTransactionModelValidator : AbstractValidator<RuleTransactionModel>
    {
        public RuleTransactionModelValidator()
        {
            RuleFor(x => x.userInput).NotEmpty().When(x => x.IsFallBack == false).WithMessage(Strings.UsermessageMustEmptyincaseoffallback);
            RuleFor(x => x.messages.Count).GreaterThan(0).WithMessage(Strings.PleaseAddatleastoneMessage);
        }
    }
}
