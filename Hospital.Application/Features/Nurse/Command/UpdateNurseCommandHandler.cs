using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Nurse.Command
{
 public   class UpdateNurseCommandHandler : IRequestHandler<UpdateNurseCommand, NurseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateNurseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<NurseDto> Handle(UpdateNurseCommand request, CancellationToken cancellationToken)
        {
            var nurse = await _unitOfWork.Nurses.GetByIdAsync(request.Id);
            if (nurse == null)
            {
                throw new NotFoundException("Nurse not found");
            }
            nurse.ApplicationUser.FirstName = request.FirstName;
            nurse.ApplicationUser.LastName = request.LastName;
            nurse.ApplicationUser.PhoneNumber = request.Phone;
            nurse.Shift = request.Shift;
            nurse.DepartmentId = request.DepartmentId;
            await _unitOfWork.Nurses.UpdateAsync(nurse);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new NurseDto
            {
                Id = nurse.Id,
                FullName = $"{nurse.ApplicationUser.FirstName} {nurse.ApplicationUser.LastName}",
                Email = nurse.ApplicationUser.Email,
                PhoneNumber = nurse.ApplicationUser.PhoneNumber,
                Shift = nurse.Shift,
                DepartmentId = nurse.DepartmentId
            };
        }


    }
}
