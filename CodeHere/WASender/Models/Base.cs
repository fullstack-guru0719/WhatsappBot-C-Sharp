using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASender.Models
{
    public class Base
    {
        public IList<ValidationFailure> validationFailures { get; set; }
    }
}
