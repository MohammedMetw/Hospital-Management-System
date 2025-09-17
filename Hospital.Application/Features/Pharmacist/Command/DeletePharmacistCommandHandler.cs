using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
namespace Hospital.Application.Features.Pharmacist.Command
{
    public class DeletePharmacistCommandHandler : IRequestHandler<DeletePharmacistCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeletePharmacistCommandHandler(IUnitOfWork  unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        public async Task<Unit> Handle(DeletePharmacistCommand request, CancellationToken cancellationToken)
        {
            var pharmacist = await _unitOfWork.Pharmacists.GetByIdAsync(request.Id);
            if (pharmacist == null)
            {
                throw new Exception("Pharmacist not found");
            }

            await _unitOfWork.Pharmacists.DeleteAsync(pharmacist);
            // remove user from role
            await _userManager.RemoveFromRoleAsync(pharmacist.ApplicationUser, "Pharmacist");
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
