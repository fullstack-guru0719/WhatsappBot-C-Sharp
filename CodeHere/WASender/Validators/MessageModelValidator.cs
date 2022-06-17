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
    public class MessageModelValidator : AbstractValidator<MessageModel>
    {
        public MessageModelValidator()
        {
            RuleFor(x => x.LongMessage).NotEmpty().WithMessage(Strings.MessageShouldNotbeEmpty);
        }
    }
}
