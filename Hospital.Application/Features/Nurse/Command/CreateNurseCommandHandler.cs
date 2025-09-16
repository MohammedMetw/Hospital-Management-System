using System.Threading;
using System.Threading.Tasks;
using Hospital.Domain.Entities;
using Hospital.Application.Interfaces;
using Hospital.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Application.Features.Nurse.Command
{
    public class CreateNurseCommandHandler : IRequestHandler<CreateNurseCommand, NurseDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNurseCommandHandler(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            _unitOfWork = unitOfWork;
        }

        public async Task<NurseDto> Handle(CreateNurseCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber=request.PhoneNumber,
                UserName = request.Email // ضروري للـ Identity
            };
            var nurse = new Domain.Entities.Nurse
            {
                Email = request.Email,
                Phone = request.PhoneNumber,
                Shift = request.Shift,
                ApplicationUserId = user.Id,

                DepartmentId = request.DepartmentId
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            await _userManager.AddToRoleAsync(user, "Nurse");
            await _unitOfWork.Nurses.AddAsync(nurse);
            await _unitOfWork.SaveChangesAsync();

           
            var nurseDto = new NurseDto
            {
                Id = nurse.Id, // assuming Nurse has Id
                FullName = $"{user.FirstName} {user.LastName}",
                Email = nurse.Email,
                PhoneNumber = nurse.Phone,
                Shift = nurse.Shift,
                DepartmentId = nurse.DepartmentId
            };

            return nurseDto;
        }
    }
}
