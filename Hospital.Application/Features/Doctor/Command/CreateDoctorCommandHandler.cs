using MediatR;
using Hospital.Domain.Entities;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Hospital.Application.Features.Doctor.Command
{
    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, int>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDoctorCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
           
            var newUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email
            };

            var identityResult = await _userManager.CreateAsync(newUser, request.Password);

            if (!identityResult.Succeeded)
            {
               
                throw new InvalidOperationException("Failed to create user. " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));
            }

           // assign Doctor Role ;)
            await _userManager.AddToRoleAsync(newUser, "Doctor");

           
            var newDoctorProfile = new Domain.Entities.Doctor
            {
                Specialty = request.Specialty,
                Phone = request.Phone,
                Email = request.Email,
                DepartmentId = request.DepartmentId,
                ApplicationUserId = newUser.Id
               
            };

           
            await _unitOfWork.Doctors.AddAsync(newDoctorProfile);

          
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newDoctorProfile.Id;
        }
    }
}