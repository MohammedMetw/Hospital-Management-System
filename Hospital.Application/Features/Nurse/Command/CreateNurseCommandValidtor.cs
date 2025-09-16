using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Hospital.Application.Features.Nurse.Command
{
  public  class CreateNurseCommandValidtor : AbstractValidator<CreateNurseCommand>
    {
        public CreateNurseCommandValidtor() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("A valid department is required.");
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("A valid email is required.");

        }

    }
}
