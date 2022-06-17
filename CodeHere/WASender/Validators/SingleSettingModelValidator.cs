using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using WASender.Models;

namespace WASender.Validators
{
    public class SingleSettingModelValidator : AbstractValidator<SingleSettingModel>
    {
        public SingleSettingModelValidator()
        {
            RuleFor(x => x.delayAfterMessages).NotEmpty().WithMessage(Strings.delayAfterMessagesShouldNotbeEmpty);
            RuleFor(x => x.delayAfterMessagesFrom).NotEmpty().WithMessage(Strings.delayAfterMessagesFromShouldNotbeEmpty);
            RuleFor(x => x.delayAfterMessagesTo).NotEmpty().WithMessage(Strings.delayAfterMessagesTOShouldNotbeEmpty);
            RuleFor(x => x.delayAfterEveryMessageFrom).NotEmpty().WithMessage(Strings.delayAfterEveryMessageFromShouldNotbeEmpty);
            RuleFor(x => x.delayAfterEveryMessageTo).NotEmpty().WithMessage(Strings.delayAfterEveryMessageToShouldNotbeEmpty);
            RuleFor(x => x.delayAfterMessages).GreaterThanOrEqualTo(0).WithMessage(Strings.delayAfterMessages_ShouldGraterthenoero);
            RuleFor(x => x.delayAfterMessagesFrom).GreaterThanOrEqualTo(0).WithMessage(Strings.delayAfterMessagesFrom_ShouldGraterthenoero);
            RuleFor(x => x.delayAfterMessagesTo).GreaterThanOrEqualTo(0).WithMessage(Strings.delayAfterMessagesTo_ShouldGraterthenoero);
            RuleFor(x => x.delayAfterEveryMessageFrom).GreaterThanOrEqualTo(0).WithMessage(Strings.delayAfterEveryMessageFrom_ShouldGraterthenoero);
            RuleFor(x => x.delayAfterEveryMessageTo).GreaterThanOrEqualTo(0).WithMessage(Strings.delayAfterEveryMessageTo_ShouldGraterthenoero);
            RuleFor(x => x.delayAfterEveryMessageFrom).LessThanOrEqualTo(x => x.delayAfterEveryMessageTo).WithMessage(Strings.thetoamountismustbegraterthenstartingamount);
            RuleFor(x => x.delayAfterMessagesFrom).LessThanOrEqualTo(x => x.delayAfterMessagesTo).WithMessage(Strings.thetoamountismustbegraterthenstartingamount);


        }
    }
}
