using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Application.Features.Pharmacist.Command
{
    internal class CreatePharmacistCommandHandler : IRequestHandler<CreatePharmacistCommand, int>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePharmacistCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }


        public async Task<int> Handle(CreatePharmacistCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.Phone
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new NotFoundException("User creation failed");
            }
            var newPharmacistProfile = new Domain.Entities.Pharmacist
            {
               
                Email = request.Email,
                Phone = request.Phone,
                Shift = request.Shift,
                ApplicationUserId = user.Id

            };

            await _unitOfWork.Pharmacists.AddAsync(newPharmacistProfile);
            await _userManager.AddToRoleAsync(user, "Pharmacist");

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newPharmacistProfile.Id;

        }
    }
}
