using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Hospital.Application.Features.Accountant.Command
{
    public class DeleteAccountantCommandHandler : IRequestHandler<DeleteAccountantCommand, AccountantDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteAccountantCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<AccountantDto> Handle(DeleteAccountantCommand request, CancellationToken cancellationToken)
        {
            var accountant = await _unitOfWork.Accountants.GetByIdAsync(request.Id);

            if (accountant == null )
            {
                throw new Exception("Accountant not Found");
            }

            await _userManager.RemoveFromRoleAsync(accountant.ApplicationUser, "Accountant");
            await _unitOfWork.Accountants.DeleteAsync(accountant);
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
