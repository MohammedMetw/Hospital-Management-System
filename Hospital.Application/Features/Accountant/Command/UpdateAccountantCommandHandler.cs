using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Accountant.Command
{
  public  class UpdateAccountantCommandHandler : IRequestHandler<UpdateAccountantCommand, AccountantDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateAccountantCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AccountantDto>Handle(UpdateAccountantCommand request,CancellationToken cancellationToken)
        {
            var accountant = await _unitOfWork.Accountants.GetByIdAsync(request.Id);
            if (accountant == null)
            {
                throw new Exception("Accountant not found");
            }
            accountant.ApplicationUser.FirstName = request.FirstName;
            accountant.ApplicationUser.LastName = request.LastName;
            accountant.ApplicationUser.PhoneNumber = request.Phone;
            accountant.ApplicationUser.Email = request.email;
            await _unitOfWork.Accountants.UpdateAsync(accountant);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new AccountantDto
            {
                Id = accountant.Id,
                FullName = $"{accountant.ApplicationUser.FirstName} {accountant.ApplicationUser.LastName}",
                Email = accountant.ApplicationUser.Email,
                PhoneNumber = accountant.ApplicationUser.PhoneNumber
            };
        }
    }
}
