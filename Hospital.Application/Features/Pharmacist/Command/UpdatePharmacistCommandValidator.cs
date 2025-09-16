using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Hospital.Application.Features.Pharmacist.Command
{
    public class UpdatePharmacistCommandValidator : AbstractValidator<UpdatePharmacistCommand>
    {
        public UpdatePharmacistCommandValidator()
        {
            RuleFor(x => x.FirstName)
              .NotEmpty().WithMessage("First name is required.")
              .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

        }
    }
}
