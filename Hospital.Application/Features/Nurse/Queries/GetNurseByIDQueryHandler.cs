using System.Threading;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Nurse.Queries
{
    public class GetNurseByIDQueryHandler : IRequestHandler<GetNurseByIDQuery, NurseDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNurseByIDQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NurseDto?> Handle(GetNurseByIDQuery request, CancellationToken cancellationToken)
        {
            var nurse = await _unitOfWork.Nurses.GetByIdAsync(request.Id);

            if (nurse == null)
            {
                return null;
            }

            var nurseDto = new NurseDto
            {
                Id = nurse.Id,
                FullName = $"{nurse.ApplicationUser.FirstName} {nurse.ApplicationUser.LastName}",
                Email = nurse.ApplicationUser.Email,
                PhoneNumber = nurse.ApplicationUser.PhoneNumber,
                Shift = nurse.Shift,
                DepartmentId = nurse.DepartmentId,
            };

            return nurseDto;
        }
    }
}
