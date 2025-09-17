using Hospital.Application.Exceptions;
using Hospital.Application.Features.User.Command;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public AssignRoleToUserCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

       
        var result = await _userManager.AddToRoleAsync(user, request.RoleName);
        if (!result.Succeeded)
            return false; 

      
        switch (request.RoleName)
        {
            case "Doctor":
                var doctorExists = await _unitOfWork.Doctors.GetByUserIdAsync(user.Id);
                if (doctorExists == null)
                {
                    await _unitOfWork.Doctors.AddAsync(new Hospital.Domain.Entities.Doctor
                    {

                        ApplicationUserId = user.Id,
                        Specialty = "General",
                        Phone = user.PhoneNumber,
                        Email = user.Email,
                        DepartmentId = 1
                    });
                }
                break;

            case "Pharmacist":
                var pharmacistExists = await _unitOfWork.Pharmacists.GetByUserIdAsync(user.Id);
                if (pharmacistExists == null)
                {
                    await _unitOfWork.Pharmacists.AddAsync(new Hospital.Domain.Entities.Pharmacist
                    {
                        
                        ApplicationUserId = user.Id,
                        Shift = "Morning", 
                        Phone = user.PhoneNumber, 
                        Email = user.Email 
                    });
                }
                break;
           


            case "Nurse":
                var nurseExists = await _unitOfWork.Nurses.GetByUserIdAsync(user.Id);
                if (nurseExists == null)
                {
                    await _unitOfWork.Nurses.AddAsync(new Hospital.Domain.Entities.Nurse
                    {
                        ApplicationUserId = user.Id,
                        Shift = "Morning",
                        Phone = user.PhoneNumber,
                        Email = user.Email,
                        DepartmentId = 1 //default department

                    });
                }
                break;


        }

        // Finally, save all changes to the database
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}