using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Application.Features.User.Command
{
    public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveRoleFromUserCommandHandler(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains(request.RoleName))
            {
                throw new Exception($"User does not have the role {request.RoleName}");
            }

            var result= await _userManager.RemoveFromRoleAsync(user, request.RoleName);

            switch (request.RoleName)
            {
                case "Doctor":
                    var doctor = await _unitOfWork.Doctors.GetByUserIdAsync(user.Id);
                    if (doctor != null)
                    {
                        await _unitOfWork.Doctors.DeleteAsync(doctor);
                    }
                    break;

                case "Pharmacist":
                    var pharmacist = await _unitOfWork.Pharmacists.GetByUserIdAsync(user.Id);
                    if (pharmacist != null)
                    {
                        await _unitOfWork.Pharmacists.DeleteAsync(pharmacist);
                    }
                    break;

                case "Nurse":
                    var nurse = await _unitOfWork.Nurses.GetByUserIdAsync(user.Id);
                    if (nurse != null)
                    {
                        await _unitOfWork.Nurses.DeleteAsync(nurse);
                    }
                    break;

                // Patient
                // بصراحه انا مش شايفه حل حلو ان اعمل سويتش بس دا الي جي في بالي دلوقت
               
            }
            await _unitOfWork.SaveChangesAsync();

            return result.Succeeded;

        }
    }
}
